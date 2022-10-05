namespace OOPQuiz.Modules.Quiz.Tests.Models
{
    public class FinishedQuizFeedbackTests
    {
        [Theory]
        [InlineData("Maybe you'll do better next time... after all, you could hardly do worse :)", new int[2] { 0, 0 })]
        [InlineData("Maybe you'll do better next time... after all, you could hardly do worse :)", new int[2] { 0, 3495250 })]
        [InlineData("L. Some definite room for improvement. Check out the question review section on the right to see where you went wrong.", new int[2] { 1, 0 })]
        [InlineData("L. Some definite room for improvement. Check out the question review section on the right to see where you went wrong.", new int[2] { 1, 3495250 })]
        [InlineData("L. Some definite room for improvement. Check out the question review section on the right to see where you went wrong.", new int[2] { 2, 0 })]
        [InlineData("L. Some definite room for improvement. Check out the question review section on the right to see where you went wrong.", new int[2] { 2, 3495250 })]
        [InlineData("Better luck next time!", new int[2] { 3, 0 })]
        [InlineData("Better luck next time!", new int[2] { 3, 3495250 })]
        [InlineData("Better luck next time!", new int[2] { 4, 0 })]
        [InlineData("Better luck next time!", new int[2] { 4, 3495250 })]
        [InlineData("Halfway there, if we bend a few rules of statistics. Not terrible.", new int[2] { 5, 0 })]
        [InlineData("Halfway there, if we bend a few rules of statistics. Not terrible.", new int[2] { 5, 3495250 })]
        [InlineData("Getting there - a few more questions right and you'll be there!", new int[2] { 6, 0 })]
        [InlineData("Getting there - a few more questions right and you'll be there!", new int[2] { 6, 3495250 })]
        [InlineData("Getting there - a few more questions right and you'll be there!", new int[2] { 7, 0 })]
        [InlineData("Getting there - a few more questions right and you'll be there!", new int[2] { 7, 3495250 })]
        [InlineData("You can do better than that! That perfect score is just round the corner...", new int[2] { 8, 0 })]
        [InlineData("You can do better than that! That perfect score is just round the corner...", new int[2] { 8, 3495250 })]
        [InlineData("You can do better than that! That perfect score is just round the corner...", new int[2] { 9, 0 })]
        [InlineData("You can do better than that! That perfect score is just round the corner...", new int[2] { 9, 3495250 })]
        [InlineData("A perfect score - well done! But did you get maximum points..?", new int[2] { 10, 0 })]
        [InlineData("100%!!!!!", new int[2] { 10, 3495250 })]
        public void TestGetFeedbackBasedOnNumberOfQuestionsCorrectlyAnswered(string expected, int[] input)
        {
            // Each different score should correspond to the above messages.
            var actual = FinishedQuizFeedback.GetFeedbackBasedOnNumberOfQuestionsCorrectlyAnswered(input[0], input[1]);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("An error occurred while retrieving your feedback. It appears you somehow answered less than no questions in the quiz.", -1)]
        [InlineData("An error occurred while retrieving your feedback. It appears you somehow answered more questions than were in the quiz.", 11)]
        public void TestGetFeedbackBasedOnNumberOfQuestionsCorrectlyAnsweredBehaviourWithUnexpectedInputs(string expected, int input)
        {
            // These should never arise, but in case they do, above is how they should be handled.
            var actual = FinishedQuizFeedback.GetFeedbackBasedOnNumberOfQuestionsCorrectlyAnswered(input, 0);

            Assert.Equal(expected, actual);
        }
    }
}
