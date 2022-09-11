using Prism.Mvvm;
using System.Collections.Generic;

namespace OOPQuiz.Business.Models
{
    /// <summary>
    /// A question with an open-ended format. Conforms to <see cref="IQuestion"/>.
    /// </summary>
    public class OpenEndedQuestion : BindableBase, IQuestion
    {
        protected readonly string _question;
        protected readonly string _answer;
        protected readonly string _imageURI;
        protected readonly string _feedback;
        protected readonly List<Choice> _choices = new();

        /// <summary>
        /// Instantiates a new open-ended question.
        /// </summary>
        /// <param name="question">The question being asked.</param>
        /// <param name="answer">The answer to the question.</param>
        /// <param name="imageURI">URI of the supporting image for the question.</param>
        /// <param name="feedback">Feedback for the user after they answer. Set to an empty string if none.</param>
        public OpenEndedQuestion(string question, string answer, string imageURI, string feedback = "")
        {
            _question = question;
            _answer = answer;
            _imageURI = imageURI;
            _feedback = feedback;
        }

        /// <summary>
        /// The question for the user to answer.
        /// </summary>
        public string Question => _question;

        /// <summary>
        /// The answer to the question.
        /// </summary>
        public string Answer => _answer;

        /// <summary>
        /// The supporting image for the question.
        /// </summary>
        public string ImageURI => _imageURI;

        /// <summary>
        /// Feedback for the user after the question.
        /// </summary>
        /// <remarks>
        /// Empty if the question doesn't provide any feedback.
        /// </remarks>
        public string Feedback => _feedback;

        /// <summary>
        /// Returns an empty dictionary since this is an open-ended question.
        /// </summary>
        public List<Choice> Choices => _choices;
    }
}
