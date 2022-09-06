using System.Collections.Generic;
using OOPQuiz.Business.Models;

namespace OOPQuiz.Services.Interfaces
{
    /// <summary>
    /// Information about questions.
    /// </summary>
    public interface IQuestionService
    {
        /// <summary>
        /// The question categories in this program.
        /// </summary>
        /// <returns>A list of question categories.</returns>
        public List<string> GetQuestionCategories();

        /// <summary>
        /// The questions in this program.
        /// </summary>
        /// <returns>A dictionary with the key as the question category and the value as a list of questions for that category.</returns>
        public Dictionary<string, List<IQuestion>> GetQuestions();
    }
}
