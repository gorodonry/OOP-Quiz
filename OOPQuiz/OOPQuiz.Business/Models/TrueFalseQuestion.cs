using System.Collections.Generic;
using OOPQuiz.Core.Models;
using Prism.Mvvm;

namespace OOPQuiz.Business.Models
{
    /// <summary>
    /// A question with a true/false format. Conforms to <see cref="IQuestion"/>.
    /// </summary>
    public class TrueFalseQuestion : BindableBase, IQuestion
    {
        protected readonly string _question;
        protected readonly bool _answer;
        protected readonly string _imageURI;
        protected readonly string _feedback;
        protected readonly List<Choice> _choices = new() { new Choice("True"), new Choice("False") };

        /// <summary>
        /// Instantiates a new true/false question.
        /// </summary>
        /// <param name="question">The question being asked.</param>
        /// <param name="answer">The answer to the question. Must be either true or false.</param>
        /// <param name="imageURI">URI of the supporting image for the question.</param>
        /// <param name="feedback">Feedback for the user after they answer. Set to an empty string if none.</param>
        public TrueFalseQuestion(string question, bool answer, string imageURI, string feedback = "")
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
        public string Answer => Methods.Capitalise(_answer.ToString());

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
        /// The choices the user has for the question (true/false).
        /// </summary>
        /// <remarks>
        /// Any feedback for true/false questions should be provided as general feedback.
        /// </remarks>
        public List<Choice> Choices => _choices;
    }
}
