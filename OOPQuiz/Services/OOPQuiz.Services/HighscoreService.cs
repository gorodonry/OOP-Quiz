using OOPQuiz.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using OOPQuiz.Business.Models;
using System;
using System.Linq;

using System.Diagnostics;

namespace OOPQuiz.Services
{
    public class HighscoreService : IHighscoreService
    {
        private const string JSONFile = @"..\..\..\..\Services\OOPQuiz.Services\highscores.json";

        /// <summary>
        /// Returns the information stored in the JSON file.
        /// </summary>
        /// <returns>The information stored in the JSON file in the desired format.</returns>
        private static Dictionary<string, List<Performance>> ReadJSON()
        {
            return JsonSerializer.Deserialize<Dictionary<string, List<Performance>>>(File.ReadAllText(JSONFile))!;
        }

        /// <summary>
        /// Writes a new perfomance to the JSON file.
        /// </summary>
        /// <param name="category">The category of quiz taken.</param>
        /// <param name="performance">Information about the user and how well they did.</param>
        /// <exception cref="ArgumentException"></exception>
        private static void WriteJSON(string category, Performance performance)
        {
            Dictionary<string, List<Performance>> JSON = ReadJSON();

            if (!JSON.ContainsKey(category))
                throw new ArgumentException($"Category specified is invalid: {category}");

            JSON[category].Add(performance);

            File.WriteAllText(JSONFile, JsonSerializer.Serialize(JSON, new JsonSerializerOptions { WriteIndented = true }));
        }

        private static Dictionary<string, List<Performance>> Highscores => ReadJSON();

        public int DetermineProvisionalPlacing(string category, int score, TimeSpan timeTaken)
        {
            if (!Highscores.ContainsKey(category))
                throw new ArgumentException($"Category specified is invalid: {category}");

            List<Performance> otherCategoryScores = Highscores[category];

            // Find the number of scores higher than the user, and combine it with the number of same scores but a faster time than the user.
            return otherCategoryScores.Count(x => x.Score > score) + 1 +
                otherCategoryScores.Where(x => x.Score == score).Count(x => x.TimeTaken.TotalSeconds <= timeTaken.TotalSeconds);
        }

        public void SubmitHighscore(string category, Performance performance)
        {
            WriteJSON(category, performance);
        }

        public Dictionary<string, List<Performance>> GetOrderedHighscores()
        {
            Dictionary<string, List<Performance>> highscores = Highscores;

            foreach (string category in highscores.Keys)
            {
                highscores[category].Sort();
            }

            return highscores;
        }
    }
}
