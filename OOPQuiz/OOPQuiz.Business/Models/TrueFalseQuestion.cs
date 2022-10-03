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

        public string Question => _question;

        public string Answer => Methods.Capitalise(_answer.ToString());

        public string ImageURI => _imageURI;

        public string Feedback => _feedback;

        public List<Choice> Choices => _choices;
    }
}
