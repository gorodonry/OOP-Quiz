using System;

namespace OOPQuiz.Business.Tests.Models
{
    public class PerformanceTests
    {
        [Theory]
        [InlineData("Name", "Name")]
        [InlineData("First Last", "First Last")]
        [InlineData("First   Last", "    First   Last    ")]
        public void TestName(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var performance = new Performance(input, 0, new());

            var actual = performance.Name;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(5, 5)]
        [InlineData(10, 10)]
        [InlineData(3495250, 3495250)]
        public void TestScore(int expected, int input)
        {
            // Should be the same as what was specified upon instantiation.
            var performance = new Performance(string.Empty, input, new());

            var actual = performance.Score;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestTimeTaken()
        {
            // Should be the same as what was specified upon instantiation.
            TimeSpan input = new(0, 3, 54);

            var performance = new Performance(string.Empty, 0, input);

            var actual = performance.TimeTaken;

            Assert.Equal(input, actual);
        }
    }
}
