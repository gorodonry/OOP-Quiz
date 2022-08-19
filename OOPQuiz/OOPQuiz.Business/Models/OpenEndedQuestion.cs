using System.Collections.Generic;

namespace OOPQuiz.Business.Models
{
    /// <summary>
    /// A question with an open-ended format. Conforms to <see cref="IQuestion"/>.
    /// </summary>
    public class OpenEndedQuestion : IQuestion
    {
        protected readonly string question;
        protected readonly string answer;
        protected readonly string imageURI;
        protected readonly string feedback;
        protected readonly Dictionary<string, string> choicesWithFeedback = new();

        /// <summary>
        /// Instantiates a new open-ended question.
        /// </summary>
        /// <param name="question">The question being asked.</param>
        /// <param name="answer">The answer to the question.</param>
        /// <param name="imageURI">URI of the supporting image for the question.</param>
        /// <param name="feedback">Feedback for the user after they answer. Set to an empty string if none.</param>
        public OpenEndedQuestion(string question, string answer, string imageURI, string feedback = "")
        {
            this.question = question;
            this.answer = answer;
            this.imageURI = imageURI;
            this.feedback = feedback;
        }

        /// <summary>
        /// The question for the user to answer.
        /// </summary>
        public string Question => question;

        /// <summary>
        /// The answer to the question.
        /// </summary>
        public string Answer => answer;

        /// <summary>
        /// The supporting image for the question.
        /// </summary>
        public string ImageURI => imageURI;

        /// <summary>
        /// Feedback for the user after the question.
        /// </summary>
        /// <remarks>
        /// Empty if the question doesn't provide any feedback.
        /// </remarks>
        public string Feedback => feedback;

        /// <summary>
        /// Returns an empty dictionary since this is an open-ended question.
        /// </summary>
        public Dictionary<string, string> ChoicesWithFeedback => choicesWithFeedback;
    }
}
