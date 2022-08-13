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

        /// <summary>
        /// Instantiates a new true/false question.
        /// </summary>
        /// <param name="question">The question being asked.</param>
        /// <param name="answer">The answer to the question. Must be either true or false.</param>
        public TrueFalseQuestion(string question, bool answer)
        {
            this.question = question;
            this.answer = answer;
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
        /// The choices the user has for the question (true/false).
        /// </summary>
        public List<string> Choices => new() { "True", "False" };
    }
}
