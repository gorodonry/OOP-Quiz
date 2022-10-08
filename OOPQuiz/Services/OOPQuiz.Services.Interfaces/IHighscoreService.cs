using OOPQuiz.Business.Models;
using System;
using System.Collections.Generic;

namespace OOPQuiz.Services.Interfaces
{
    /// <summary>
    /// Information on the highscores people have achieved.
    /// </summary>
    public interface IHighscoreService
    {
        /// <summary>
        /// A dictionary of all the scores achieved by users, with the performances ordered by score and time.
        /// </summary>
        /// <returns>A dictionary with the key as the question category, and the value as a list of user performances.</returns>
        public Dictionary<string, List<Performance>> GetOrderedHighscores();

        /// <summary>
        /// Returns a provisional placing for the user, should they wish to submit their score.
        /// </summary>
        /// <param name="category">The question category they were answering.</param>
        /// <param name="score">The score they got.</param>
        /// <param name="timeTaken">The time they took.</param>
        /// <returns>An integer indicating their overall placing.</returns>
        public int DetermineProvisionalPlacing(string category, int score, TimeSpan timeTaken);

        /// <summary>
        /// Submits a newly achieved highscore (or lowscore as the case may be).
        /// </summary>
        /// <param name="category">The category of quiz taken.</param>
        /// <param name="performance">Information about the user and how well they did.</param>
        public void SubmitHighscore(string category, Performance performance);
    }
}
