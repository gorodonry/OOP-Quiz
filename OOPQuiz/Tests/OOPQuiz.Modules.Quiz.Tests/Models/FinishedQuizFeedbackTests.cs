namespace OOPQuiz.Modules.Quiz.Tests.Models
{
    public class FinishedQuizFeedbackTests
    {
        [Theory]
        [InlineData("Maybe you'll do better next time... after all, you could hardly do worse :)", 0)]
        [InlineData("L. Some definite room for improvement. Check out the question review section on the right to see where you went wrong.", 1)]
        [InlineData("L. Some definite room for improvement. Check out the question review section on the right to see where you went wrong.", 2)]
        [InlineData("Better luck next time!", 3)]
        [InlineData("Better luck next time!", 4)]
        [InlineData("Halfway there, if we bend a few rules of statistics. Not terrible.", 5)]
        [InlineData("Getting there - a few more questions right and you'll be there!", 6)]
        [InlineData("Getting there - a few more questions right and you'll be there!", 7)]
        [InlineData("You can do better than that! That perfect score is just round the corner...", 8)]
        [InlineData("You can do better than that! That perfect score is just round the corner...", 9)]
        [InlineData("A perfect score - well done! But did you get maximum points?", 10)]
        public void TestGetFeedbackBasedOnNumberOfQuestionsCorrectlyAnswered(string expected, int input)
        {
            var actual = FinishedQuizFeedback.GetFeedbackBasedOnNumberOfQuestionsCorrectlyAnswered(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("An error occurred while retrieving your feedback. It appears you somehow answered less than no questions in the quiz.", -1)]
        [InlineData("An error occurred while retrieving your feedback. It appears you somehow answered more questions than were in the quiz.", 11)]
        public void TestGetFeedbackBasedOnNumberOfQuestionsCorrectlyAnsweredBehaviourWithUnexpectedInputs(string expected, int input)
        {
            var actual = FinishedQuizFeedback.GetFeedbackBasedOnNumberOfQuestionsCorrectlyAnswered(input);

            Assert.Equal(expected, actual);
        }
    }
}
