using OOPQuiz.Business.Models;
using System.Collections.Generic;
using OOPQuiz.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;

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
        protected string _userAnswerToCurrentQuestion;

        /// <summary>
        /// Creates a new instance of the quiz runner.
        /// </summary>
        public QuizRunnerModel()
        {

        }

        /// <summary>
        /// Information on the question currently being asked.
        /// </summary>
        protected IQuestion CurrentQuestion
        {
            get
            {
                if (_questions.Count != 0)
                    return _questions[CurrentQuestionNumber - 1];
                return null;
            }
        }

        /// <summary>
        /// The category of questions asked by this instance of the quiz runner.
        /// </summary>
        public string QuestionCategory
        {
            get { return _questionCategory; }
            set
            {
                _questionCategory = value;

                // Having set the question category, the questions to ask can be determined.
                SelectQuestions();
            }
        }

        /// <summary>
        /// The number of the current question.
        /// </summary>
        public int CurrentQuestionNumber
        {
            get { return _currentQuestionNumber; }
        }

        /// <summary>
        /// The question for the user to answer.
        /// </summary>
        public string CurrentQuestionAsked
        {
            get
            {
                if (CurrentQuestion is not null)
                    return CurrentQuestion.Question;
                return null;
            }
        }

        /// <summary>
        /// Returns the path of the image for the question.
        /// </summary>
        public string ImageURIForCurrentQuestion
        {
            get
            {
                if (CurrentQuestion is not null)
                    return CurrentQuestion.ImageURI;
                return null;
            }
        }

        /// <summary>
        /// Returns a list of choices the user has to choose an answer from.
        /// </summary>
        /// <remarks>
        /// Returns an empty list for open-ended questions.
        /// </remarks>
        public List<string> AnswersToChooseFrom
        {
            get
            {
                if (CurrentQuestion is not null)
                    return CurrentQuestion.ChoicesWithFeedback.Keys.ToList();
                return null;
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether or not the current question is open-ended.
        /// </summary>
        public bool CurrentQuestionIsOpenEnded
        {
            get
            {
                if (CurrentQuestion is not null)
                    return AnswersToChooseFrom.Count == 0;
                return false;
            }
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
        protected void SelectQuestions()
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
