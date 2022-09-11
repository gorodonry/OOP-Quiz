﻿using OOPQuiz.Business.Models;
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
        protected const int pointsPerQuestion = 10;

        protected Dictionary<string, List<IQuestion>> allQuestions;

        protected string _questionCategory;
        protected List<IQuestion> _questions = new();
        protected int _currentQuestionNumber = 1;

        protected bool _currentQuestionAnswered = false;
        protected string _userAnswerToCurrentQuestion;
        protected string _feedbackForCurrentQuestion = string.Empty;

        protected int _score = 0;

        protected string _buttonAction = "Submit Answer";

        /// <summary>
        /// Creates a new instance of the quiz runner.
        /// </summary>
        public QuizRunnerModel()
        {

        }

        /// <summary>
        /// Information on the question currently being asked.
        /// </summary>
        public IQuestion CurrentQuestion
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
        /// Indicates whether or not the current question has been answered.
        /// </summary>
        public bool CurrentQuestionAnswered
        {
            get
            {
                if (CurrentQuestion is not null)
                    return _currentQuestionAnswered;
                return false;
            }
        }

        /// <summary>
        /// A string explaining the correct answer to the current question.
        /// </summary>
        /// <remarks>
        /// Not provided for every question. Set to an empty string if the question has not yet been answered.
        /// </remarks>
        public string FeedbackForCurrentQuestion
        {
            get { return _feedbackForCurrentQuestion; }
        }

        /// <summary>
        /// Returns a boolean indicating whether or not the current question is open-ended.
        /// </summary>
        public bool CurrentQuestionIsOpenEnded
        {
            get
            {
                if (CurrentQuestion is not null)
                    return CurrentQuestion.ChoicesWithFeedback.Count == 0;
                return false;
            }
        }

        /// <summary>
        /// The user's score for the current quiz.
        /// </summary>
        public int UserScore
        {
            get { return _score; }
        }

        /// <summary>
        /// Information on what action is currently available to the user.
        /// </summary>
        /// <remarks>
        /// One of "Submit Answer", "Next Question", and "Finish Quiz".
        /// </remarks>
        public string ButtonAction
        {
            get { return _buttonAction; }
        }

        /// <summary>
        /// Loads the information for the next question.
        /// </summary>
        public void LoadNextQuestion()
        {
            if (_currentQuestionNumber < numberOfQuestions)
            {
                _currentQuestionNumber += 1;

                _currentQuestionAnswered = false;

                _buttonAction = "Submit Answer";
            }
        }

        public void AnswerQuestion(string userAnswer)
        {

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
