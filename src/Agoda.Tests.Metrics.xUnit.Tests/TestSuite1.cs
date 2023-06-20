namespace Agoda.Tests.Metrics.xUnit.Tests
{
    public class TestSuite1
    {
        private readonly Random _random = new Random();

        [Fact]
        public void Test1()
        {
            // 25% failure rate
            Assert.True(_random.Next(0, 100) > 25);
        }

        [Fact]
        public void Test2()
        {
            // 25% failure rate
            Assert.True(_random.Next(0, 100) > 25);
        }

        [Fact]
        public void Test3()
        {
            // 25% failure rate
            Assert.True(_random.Next(0, 100) > 25);
        }

        [Fact]
        public void Test4()
        {
            // 25% failure rate
            Assert.True(_random.Next(0, 100) > 25);
        }
    }
}