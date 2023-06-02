using NUnit.Framework;
using Shouldly;

namespace Agoda.Tests.Metrics.NUnit.Tests;

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