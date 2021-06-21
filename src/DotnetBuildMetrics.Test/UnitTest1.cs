using Agoda.Builds.Metrics;
using NUnit.Framework;

namespace DotnetBuildMetrics.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            MeasureBuildTime mb = new MeasureBuildTime();
            var test=mb.DebugOutput;

            Assert.Pass();
        }
    }
}