using System.Collections.Generic;
using System;
using System.Linq;
using Prism.Mvvm;
using OOPQuiz.Core.Models;

namespace OOPQuiz.Business.Models
{
    /// <summary>
    /// A question with a multichoice format. Conforms to <see cref="IQuestion"/>.
    /// </summary>
    public class MultiChoiceQuestion : BindableBase, IQuestion
    {
        protected readonly string _question;
        protected readonly string _answer;
        protected readonly string _imageURI;
        protected readonly string _feedback;
        protected readonly List<Choice> _choices;

        /// <summary>
        /// Instantiates a new multichoice question.
        /// </summary>
        /// <param name="question">The question being asked.</param>
        /// <param name="answer">The answer to the question.</param>
        /// <param name="imageURI">URI of the supporting image for the question.</param>
        /// <param name="choices">The answers the user has to choose from paired with the specific feedback for each answer.</param>
        /// <param name="feedback">Feedback for the user after they answer. Set to an empty string if none.</param>
        /// <exception cref="ArgumentException"></exception>
        public MultiChoiceQuestion(string question, string answer, string imageURI, List<Choice> choices, string feedback = "")
        {
            _question = question;
            _answer = answer;
            _imageURI = imageURI;
            _feedback = feedback;
            _choices = choices;

            _choices.Shuffle();
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
        /// The choices the user has for the question.
        /// </summary>
        /// <remarks>
        /// There can be anywhere between 2 and 6 choices to choose from, but typically there are 4.
        /// </remarks>
        public List<Choice> Choices => _choices;
    }
}
