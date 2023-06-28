using Agoda.DevFeedback.Common;
using NSubstitute;
using Shouldly;

namespace Agoda.Tests.Metrics.Tests;

[TestFixture]
public class TestCasePayloadTests
{
    [Test]
    public void WhenInit_ShouldHaveDefaultVarsFromEnvironment()
    {
        var underTest = new TestCasePayload(null,Substitute.For<GitContext>(), new List<TestCase>());

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