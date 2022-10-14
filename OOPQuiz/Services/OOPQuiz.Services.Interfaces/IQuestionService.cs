using System.Collections.Generic;
using OOPQuiz.Business.Models;
using System.Collections.Immutable;

namespace OOPQuiz.Services.Interfaces
{
    /// <summary>
    /// Information about questions.
    /// </summary>
    public interface IQuestionService
    {
        /// <summary>
        /// Get the question categories in this program.
        /// </summary>
        /// <returns>A list of question categories.</returns>
        public ImmutableList<string> GetQuestionCategories();

        /// <summary>
        /// Get the questions in this program.
        /// </summary>
        /// <returns>A dictionary with the key as the question category and the value as a list of questions for that category.</returns>
        public Dictionary<string, List<IQuestion>> GetQuestions();
    }
}
