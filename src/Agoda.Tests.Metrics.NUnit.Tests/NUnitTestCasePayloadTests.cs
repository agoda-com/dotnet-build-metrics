using System.Collections;
using Agoda.DevFeedback.Common;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Agoda.Tests.Metrics.NUnit.Tests;
public class LargeAmountOftestData
{
    public static IEnumerable TestCases
    {
        get
        {
            for (var i = 1; i <= 1000; i++)
            {
                yield return new TestCaseData(i);
            }
            
        }
    }
}
[TestFixture]
public class LoadTesting
{
    [Test]
    [Ignore("Used only for local testing to verify performance")]
    [TestCaseSource(typeof(LargeAmountOftestData), nameof(LargeAmountOftestData.TestCases))]
    public void WhenHaveLargeAmountsOfTest_ShouldSendInBatches(int n)
    {
        n.ShouldBeGreaterThan(0);
    }
}

[TestFixture]
public class NUnitTestCasePayloadTests
{
    [Test]
    public void WhenInit_ShouldHaveDefaultVarsFromEnvironment()
    {
        var underTest = new NUnitTestCasePayload(null,Substitute.For<GitContext>(), new List<TestCase>());

        underTest.Id.ShouldNotBeNull();
        underTest.UserName.ShouldBe(Environment.UserName);
        underTest.CpuCount.ShouldBe(Environment.ProcessorCount);
#pragma warning disable AG0035
        underTest.Hostname.ShouldBe(Environment.MachineName);
#pragma warning restore AG0035
        underTest.Platform.ShouldBe((int)Environment.OSVersion.Platform);
        underTest.Os.ShouldBe(Environment.OSVersion.VersionString);
    }
}