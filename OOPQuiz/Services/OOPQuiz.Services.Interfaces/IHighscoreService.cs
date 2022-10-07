using OOPQuiz.Business.Models;
using System.Collections.Generic;

namespace OOPQuiz.Services.Interfaces
{
    /// <summary>
    /// Information on the highscores people have achieved.
    /// </summary>
    public interface IHighscoreService
    {
        /// <summary>
        /// A dictionary of all the scores achieved by users.
        /// </summary>
        /// <returns>A dictionary with the key as the question category, and the value as a list of user performances.</returns>
        public Dictionary<string, List<Performance>> GetHighscores();

        /// <summary>
        /// Submits a newly achieved highscore (or lowscore as the case may be).
        /// </summary>
        /// <param name="category">The category of quiz taken.</param>
        /// <param name="performance">Information about the user and how well they did.</param>
        public void SubmitHighscore(string category, Performance performance);
    }
}
