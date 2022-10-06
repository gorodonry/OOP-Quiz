using OOPQuiz.Core.Models;
using System;

namespace OOPQuiz.Modules.Quiz.Models
{
    /// <summary>
    /// Stores information about a question.
    /// </summary>
    /// <remarks>
    /// Speciically, this class stores a boolean indicating whether or not the user got the question right, and the number of the question.
    /// </remarks>
    public class QuestionNumberAnswerPair
    {
        protected bool _answer;
        protected int _questionNumber;

        /// <summary>
        /// Constructor for a question number-answer pair.
        /// </summary>
        /// <param name="answer">A boolean indicating whether or not the user got the question right.</param>
        /// <param name="questionNumber">The number of the question.</param>
        public QuestionNumberAnswerPair(bool answer, int questionNumber)
        {
            _answer = answer;
            _questionNumber = questionNumber;
        }

        /// <summary>
        /// Constructor for a question number-answer pair.
        /// </summary>
        /// <param name="answer">Either "True" or "False" - indicating whether or not the user got the question right.</param>
        /// <param name="questionNumber">The number of the question.</param>
        /// <exception cref="ArgumentException"></exception>
        public QuestionNumberAnswerPair(string answer, int questionNumber)
        {
            if (answer != "True" && answer != "False")
                throw new ArgumentException($"Answer cannot be converted to a boolean: '{answer}'");

            _answer = answer == "True";
            _questionNumber = questionNumber;
        }

        /// <summary>
        /// The answer status as a string.
        /// </summary>
        /// <remarks>
        /// Will either return "True" indicating the user got the question right, or "False", indicating the opposite.
        /// </remarks>
        public string AnswerAsString => Methods.Capitalise(_answer.ToString());

        /// <summary>
        /// The number of the question.
        /// </summary>
        public int QuestionNumber => _questionNumber;
    }
}
