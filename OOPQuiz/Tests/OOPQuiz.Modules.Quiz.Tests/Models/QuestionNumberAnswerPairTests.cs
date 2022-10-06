namespace OOPQuiz.Modules.Quiz.Tests.Models
{
    public class QuestionNumberAnswerPairTests
    {
        [Theory]
        [InlineData("True", true)]
        [InlineData("False", false)]
        public void TestAnswerAsStringWithBooleanInput(string expected, bool input)
        {
            var questionNumberAnswerPair = new QuestionNumberAnswerPair(input, 0);

            var actual = questionNumberAnswerPair.AnswerAsString;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("True", "True")]
        [InlineData("False", "False")]
        public void TestAnswerAsStringWithStringInput(string expected, string input)
        {
            var questionNumberAnswerPair = new QuestionNumberAnswerPair(input, 0);

            var actual = questionNumberAnswerPair.AnswerAsString;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("null")]
        [InlineData("stRing")]
        public void TestInvalidStringAnswerInput(string input)
        {
            // Any string that isn't either "True" or "False" should cause an exception to be raised.
            Assert.Throws<ArgumentException>(() => new QuestionNumberAnswerPair(input, 0));
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(5, 5)]
        [InlineData(10, 10)]
        public void TestQuestionNumber(int expected, int input)
        {
            // Should be the same as what was specified upon instantiation.
            var questionNumberAnswerPair = new QuestionNumberAnswerPair(true, input);

            var actual = questionNumberAnswerPair.QuestionNumber;

            Assert.Equal(expected, actual);
        }
    }
}
