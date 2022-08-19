using System.Collections.Generic;
using System;
using System.Linq;

namespace OOPQuiz.Business.Models
{
    /// <summary>
    /// A question with a multichoice format. Conforms to <see cref="IQuestion"/>.
    /// </summary>
    public class MultiChoiceQuestion : IQuestion
    {
        protected readonly string question;
        protected readonly string answer;
        protected readonly string imageURI;
        protected readonly string feedback;
        protected readonly Dictionary<string, string> choicesWithFeedback;

        /// <summary>
        /// Instantiates a new multichoice question.
        /// </summary>
        /// <param name="question">The question being asked.</param>
        /// <param name="answer">The answer to the question.</param>
        /// <param name="imageURI">URI of the supporting image for the question.</param>
        /// <param name="choicesWithFeedback">The answers the user has to choose from paired with the specific feedback for each answer.</param>
        /// <param name="feedback">Feedback for the user after they answer. Set to an empty string if none.</param>
        /// <exception cref="ArgumentException"></exception>
        public MultiChoiceQuestion(string question, string answer, string imageURI, Dictionary<string, string> choicesWithFeedback, string feedback = "")
        {
            if (!choicesWithFeedback.Keys.ToList().Contains(answer))
                throw new ArgumentException("Correct answer must be in the choices provided");
            if (choicesWithFeedback.Count < 2 || choicesWithFeedback.Count > 6)
                throw new ArgumentException("There must be between 2 and 6 choices to choose from");
            this.question = question;
            this.answer = answer;
            this.imageURI = imageURI;
            this.feedback = feedback;
            this.choicesWithFeedback = choicesWithFeedback;
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
        /// The choices the user has for the question.
        /// </summary>
        /// <remarks>
        /// There can be anywhere between 2 and 6 choices to choose from, but typically there are 4.
        /// </remarks>
        public Dictionary<string, string> ChoicesWithFeedback => choicesWithFeedback;
    }
}
