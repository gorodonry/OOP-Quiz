namespace OOPQuiz.Business.Tests.Models
{
    public class OpenEndedQuestionTests
    {
        [Theory]
        [InlineData("Lorem ipsum dolor sit amet?", "Lorem ipsum dolor sit amet?")]
        public void TestQuestion(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var openEndedQuestion = new OpenEndedQuestion(input, "");

            var actual = openEndedQuestion.Question;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Answer", "Answer")]
        public void TestAnswer(string expected, string input)
        {
            // Should be the same as what was specified upon instantiation.
            var openEndedQuestion = new OpenEndedQuestion("", input);

            var actual = openEndedQuestion.Answer;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestChoices()
        {
            // Should always return an empty list.
            var openEndedQuestion = new OpenEndedQuestion("", "");

            var actual = openEndedQuestion.Choices;

            Assert.Empty(actual);
        }
    }
}
