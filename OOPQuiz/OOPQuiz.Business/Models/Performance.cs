using System;

namespace OOPQuiz.Business.Models
{
    /// <summary>
    /// Stores information about an attempt on the quiz by a user.
    /// </summary>
    public class Performance : IComparable
    {
        protected string _name;

        protected int _score;
        protected TimeSpan _timeTaken;

        /// <summary>
        /// Creates a new instance of the <see cref="Performance"/> class.
        /// </summary>
        /// <param name="name">The name of the user.</param>
        /// <param name="score">The score the user got.</param>
        /// <param name="timeTaken">The time the user took to complete the quiz.</param>
        public Performance(string name, int score, TimeSpan timeTaken)
        {
            _name = name.Trim();
            _score = score;
            _timeTaken = timeTaken;
        }

        /// <summary>
        /// The name of the user who achieved the score and time stored in this instance.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// The score achieved by this user.
        /// </summary>
        public int Score => _score;

        /// <summary>
        /// The time taken to complete the quiz by this user.
        /// </summary>
        public TimeSpan TimeTaken => _timeTaken;

        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;

            Performance? otherPerformance = obj as Performance;

            if (otherPerformance is null)
                throw new ArgumentException("Object is not a Performance");

            // Performance objects are sorted from highest to lowest score.
            if (otherPerformance.Score != _score)
                return otherPerformance.Score.CompareTo(_score);

            // Performance objects with the same score are sorted from shortest to longest time.
            return _timeTaken.CompareTo(otherPerformance.TimeTaken);
        }
    }
}
