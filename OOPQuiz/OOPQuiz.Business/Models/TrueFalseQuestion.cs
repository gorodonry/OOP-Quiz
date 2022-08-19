using System.Collections.Generic;
using OOPQuiz.Core.Models;

namespace OOPQuiz.Business.Models
{
    /// <summary>
    /// A question with a true/false format. Conforms to <see cref="IQuestion"/>.
    /// </summary>
    public class TrueFalseQuestion : IQuestion
    {
        protected readonly string question;
        protected readonly bool answer;
        protected readonly string imageURI;
        protected readonly string feedback;
        protected readonly Dictionary<string, string> choicesWithFeedback = new() { { "True", "" }, { "False", "" } };

        /// <summary>
        /// Instantiates a new true/false question.
        /// </summary>
        /// <param name="question">The question being asked.</param>
        /// <param name="answer">The answer to the question. Must be either true or false.</param>
        /// <param name="imageURI">URI of the supporting image for the question.</param>
        /// <param name="feedback">Feedback for the user after they answer. Set to an empty string if none.</param>
        public TrueFalseQuestion(string question, bool answer, string imageURI, string feedback = "")
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
        public string Answer => Methods.Capitalise(answer.ToString());

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
        /// The choices the user has for the question (true/false).
        /// </summary>
        /// <remarks>
        /// Any feedback for true/false questions should be provided as general feedback.
        /// </remarks>
        public Dictionary<string, string> ChoicesWithFeedback => choicesWithFeedback;
    }
}
