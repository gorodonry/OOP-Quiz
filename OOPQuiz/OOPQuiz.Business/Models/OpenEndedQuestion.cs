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
        protected List<string> choices = new();

        /// <summary>
        /// Instantiates a new open-ended question.
        /// </summary>
        /// <param name="question">The question being asked.</param>
        /// <param name="answer">The answer to the question.</param>
        public OpenEndedQuestion(string question, string answer)
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
        public string Answer => answer;

        /// <summary>
        /// Returns an empty list since this is an open-ended question.
        /// </summary>
        public List<string> Choices => choices;
    }
}
