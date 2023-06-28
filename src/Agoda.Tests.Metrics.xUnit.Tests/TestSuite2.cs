namespace Agoda.Tests.Metrics.xUnit.Tests
{
    public class TestSuite2
    {
        [Fact]
        public void Test1()
        {
        }

        [Fact]
        public void Test2()
        {
        }

        [Fact]
        public void Test3()
        {
        }

        [Fact]
        public void Test4()
        {
        }

        [Theory]
        [InlineData(100)]
        [InlineData(75)]
        [InlineData(50)]
        [InlineData(25)]
        public void Test5(int n)
        {
            Assert.True(n >= 25);
        }
    }
}
