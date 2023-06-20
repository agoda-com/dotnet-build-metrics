namespace Agoda.Tests.Metrics.xUnit.Tests
{
    public class TestSuite2
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

        [Theory]
        [InlineData(100)]
        [InlineData(75)]
        [InlineData(50)]
        [InlineData(25)]
        public void Test5(int n)
        {
            Assert.True(n > 25);
        }
    }
}
