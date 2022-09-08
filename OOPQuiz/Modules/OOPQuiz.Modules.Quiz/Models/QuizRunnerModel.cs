using OOPQuiz.Business.Models;
using System.Collections.Generic;
using OOPQuiz.Services.Interfaces;
using System;
using System.Diagnostics;

namespace OOPQuiz.Modules.Quiz.Models
{
    /// <summary>
    /// Stores all necessary information to run the quiz, along with the interaction logic.
    /// </summary>
    public class QuizRunnerModel
    {
        protected const int numberOfQuestions = 10;

        protected Dictionary<string, List<IQuestion>> allQuestions;

        protected string _questionCategory;
        protected List<IQuestion> _questions = new();
        protected int _currentQuestionNumber = 1;

        /// <summary>
        /// Creates a new instance of the quiz runner.
        /// </summary>
        public QuizRunnerModel()
        {

        }

        /// <summary>
        /// The category of questions asked by this instance of the quiz runner.
        /// </summary>
        public string QuestionCategory
        {
            get { return _questionCategory; }
            set { _questionCategory = value; }
        }

        /// <summary>
        /// The number of the current question.
        /// </summary>
        public int CurrentQuestionNumber
        {
            get { return _currentQuestionNumber; }
        }

        /// <summary>
        /// Provides the model with a list of questions.
        /// </summary>
        /// <param name="questions">Dictionary of all questions, obtained from <see cref="IQuestionService"/>.</param>
        public void ImportQuestions(Dictionary<string, List<IQuestion>> questions)
        {
            if (allQuestions is null)
                allQuestions = questions;
        }

        /// <summary>
        /// Sets up the list of questions to use in the quiz.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public void SelectQuestions()
        {
            if (allQuestions.ContainsKey(QuestionCategory))
            {
                List<IQuestion> questionPool = allQuestions[QuestionCategory];

                // Randomly select the desired number of questions.
                for (int i = 0; i < numberOfQuestions; i++)
                {
                    IQuestion question = questionPool[new Random().Next(0, questionPool.Count)];

                    _questions.Add(question);

                    questionPool.Remove(question);
                }
            }
            else
            {
                throw new ArgumentException($"Invalid question category: {QuestionCategory}");
            }
        }
    }
}
