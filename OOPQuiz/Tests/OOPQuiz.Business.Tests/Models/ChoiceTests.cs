namespace OOPQuiz.Business.Tests.Models
{
    public class ChoiceTests
    {
        [Theory]
        [InlineData("Answer", "Answer")]
        public void TestPotentialAnswer(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var choice = new Choice(input);

            var actual = choice.PotentialAnswer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestFeedbackForAnswerDefaultBehaviour()
        {
            // Should return an empty string.
            var choice = new Choice("");

            var actual = choice.FeedbackForAnswer;

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData("Feedback", "Feedback")]
        public void TestFeedbackForAnswerSetUponInstantiation(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var choice = new Choice("", input);

            var actual = choice.FeedbackForAnswer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestBackgroundHexCodeDefaultBehaviour()
        {
            // Should return the specified string.
            string expected = "#F2F2F2";

            var choice = new Choice("");

            var actual = choice.BackgroundHexCode;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("#F183B2", "#F183B2")]
        public void TestBackgroundHexCodeSetUponInstantiation(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var choice = new Choice("", backgroundHexCode: input);

            var actual = choice.BackgroundHexCode;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("#F183B2", "#F183B2")]
        public void TestBackgroundHexCodeSetAfterInstantiation(string expected, string input)
        {
            // Should be equal to what it was set to.
            var choice = new Choice("");

            choice.BackgroundHexCode = input;

            var actual = choice.BackgroundHexCode;

            Assert.Equal(expected, input);
        }
    }
}
