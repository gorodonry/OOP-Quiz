using System.Collections.Generic;

namespace OOPQuiz.Business.Models
{
    /// <summary>
    /// Contains all information relevant to a single question.
    /// </summary>
    public interface IQuestion
    {
        /// <summary>
        /// The question for the user to answer.
        /// </summary>
        public string Question { get; }

        /// <summary>
        /// The answer to the question.
        /// </summary>
        public string Answer { get; }

        /// <summary>
        /// A list of answers to the question for the user to choose from.
        /// </summary>
        /// <remarks>
        /// Empty if the question requires an open-ended answer.
        /// </remarks>
        public List<string> Choices { get; }
    }
}
