namespace OOPQuiz.Modules.Quiz.Models
{
    /// <summary>
    /// Static methods for getting feedback relevant to the user's performance once they've finished the quiz.
    /// </summary>
    public static class FinishedQuizFeedback
    {
        /// <summary>
        /// Provides feedback based on the number of questions the user got right.
        /// </summary>
        /// <param name="numberOfQuestionsCorrectlyAnswered">The number of questions correctly answered by the user.</param>
        /// <param name="score">The final score achieved by the user.</param>
        /// <returns>A string containing feedback.</returns>
        public static string GetFeedbackBasedOnNumberOfQuestionsCorrectlyAnswered(int numberOfQuestionsCorrectlyAnswered, int score)
        {
            string feedback;

            switch (numberOfQuestionsCorrectlyAnswered)
            {
                case < 0:
                    feedback = "An error occurred while retrieving your feedback. It appears you somehow answered less than no questions in the quiz.";
                    break;

                case 0:
                    feedback = "Maybe you'll do better next time... after all, you could hardly do worse :)";
                    break;

                case < 3:
                    feedback = "L. Some definite room for improvement. Check out the question review section on the right to see where you went wrong.";
                    break;

                case < 5:
                    feedback = "Better luck next time!";
                    break;

                case 5:
                    feedback = "Halfway there, if we bend a few rules of statistics. Not terrible.";
                    break;

                case < 8:
                    feedback = "Getting there - a few more questions right and you'll be there!";
                    break;

                case < 10:
                    feedback = "You can do better than that! That perfect score is just round the corner...";
                    break;

                case 10:
                    // Check if the user also got maximum points, if so, adjust the feedback.
                    if (score == 3495250)
                    {
                        feedback = "100%!!!!!";
                    }
                    else
                    {
                        feedback = "A perfect score - well done! But did you get maximum points..?";
                    }
                    
                    break;

                default:
                    feedback = "An error occurred while retrieving your feedback. It appears you somehow answered more questions than were in the quiz.";
                    break;
            }

            return feedback;
        }
    }
}
