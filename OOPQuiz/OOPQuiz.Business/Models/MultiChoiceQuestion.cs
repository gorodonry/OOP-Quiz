using System.Collections.Generic;
using System;
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
            // Ensure that the answer to the question is in fact one of the choices.
            List<string> potentialAnswers = new();

            foreach (Choice choice in choices)
            {
                potentialAnswers.Add(choice.PotentialAnswer);
            }

            if (!potentialAnswers.Contains(answer))
                throw new ArgumentException("Answer not among choices provided");

            // Ensure the right number of choices has been provided.
            if (choices.Count < 2 || choices.Count > 6)
                throw new ArgumentException("Number of choices not in the correct range (2-6)");

            _question = question;
            _answer = answer;
            _imageURI = imageURI;
            _feedback = feedback;
            _choices = choices;

            _choices.Shuffle();
        }

        public string Question => _question;

        public string Answer => _answer;

        public string ImageURI => _imageURI;

        public string Feedback => _feedback;

        public List<Choice> Choices => _choices;
    }
}
