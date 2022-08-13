using System.Collections.Generic;
using System;

namespace OOPQuiz.Business.Models
{
    /// <summary>
    /// A question with a multichoice format. Conforms to <see cref="IQuestion"/>.
    /// </summary>
    public class MultiChoiceQuestion : IQuestion
    {
        protected readonly string question;
        protected readonly string answer;
        protected readonly List<string> choices;

        /// <summary>
        /// Instantiates a new multichoice question.
        /// </summary>
        /// <param name="question">The question being asked.</param>
        /// <param name="answer">The answer to the question.</param>
        /// <param name="choices">The answers the user has to choose from.</param>
        /// <exception cref="ArgumentException"></exception>
        public MultiChoiceQuestion(string question, string answer, List<string> choices)
        {
            if (!choices.Contains(answer))
                throw new ArgumentException("Correct answer must be in the choices provided");
            if (choices.Count < 2 || choices.Count > 6)
                throw new ArgumentException("There must be between 2 and 6 choices to choose from");
            this.question = question;
            this.answer = answer;
            this.choices = choices;
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
        /// The choices the user has for the question.
        /// </summary>
        /// <remarks>
        /// There can be anywhere between 2 and 6 choices to choose from, but typically there are 4.
        /// </remarks>
        public List<string> Choices => choices;
    }
}
