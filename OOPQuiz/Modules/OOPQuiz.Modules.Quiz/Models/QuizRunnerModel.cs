﻿using OOPQuiz.Business.Models;
using System.Collections.Generic;
using OOPQuiz.Services.Interfaces;
using System;
using OOPQuiz.Core.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;

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
        protected ObservableCollection<string> _answerStatuses = new();
        protected int _currentQuestionIndex = 0;

        protected bool _currentQuestionAnswered = false;
        protected bool? _userCorrect;
        protected string _feedbackForCurrentQuestion = string.Empty;

        protected List<string> _feedbackGiven = new();
        protected List<string> _answersGiven = new();

        protected int _score = 0;
        protected Stopwatch _stopwatch = new Stopwatch();
        protected int _pointsBet = 0;

        protected string _buttonAction = "Submit Answer";

        /// <summary>
        /// Creates a new instance of the quiz runner.
        /// </summary>
        public QuizRunnerModel()
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// Information on the question currently being asked.
        /// </summary>
        public IQuestion CurrentQuestion
        {
            get
            {
                if (_questions.Count != 0)
                    return _questions[_currentQuestionIndex];
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
                if (_questionCategory is null)
                {
                    _questionCategory = value;

                    // Having set the question category, the questions to ask can be determined.
                    SelectQuestions();
                }
            }
        }

        /// <summary>
        /// The number of the current question.
        /// </summary>
        public int CurrentQuestionNumber => _currentQuestionIndex + 1;

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
        /// Indicates whether or not the user got the current question right.
        /// </summary>
        public bool? UserCorrect => _userCorrect;

        /// <summary>
        /// A string explaining the correct answer to the current question.
        /// </summary>
        /// <remarks>
        /// Not provided for every question. Set to an empty string if the question has not yet been answered.
        /// </remarks>
        public string FeedbackForCurrentQuestion => _feedbackForCurrentQuestion;

        /// <summary>
        /// Returns a boolean indicating whether or not the current question is open-ended.
        /// </summary>
        public bool CurrentQuestionIsOpenEnded
        {
            get
            {
                if (CurrentQuestion is not null)
                    return CurrentQuestion.Choices.Count == 0;
                return false;
            }
        }

        /// <summary>
        /// The user's score for the current quiz.
        /// </summary>
        public int UserScore => _score;

        /// <summary>
        /// The number of points the user has bet for the current question.
        /// </summary>
        public int PointsBetByUser => _pointsBet;

        /// <summary>
        /// A list of strings indicating the answer status for each question.
        /// </summary>
        /// <remarks>
        /// Questions that have not yet been answered or selected have a null value.
        /// </remarks>
        public ObservableCollection<string> AnswerStatuses => _answerStatuses;

        /// <summary>
        /// Information on what action is currently available to the user.
        /// </summary>
        /// <remarks>
        /// One of "Submit Answer", "Next Question", and "Finish Quiz".
        /// </remarks>
        public string ButtonAction => _buttonAction;

        /// <summary>
        /// A boolean indicating whether or not all questions have been answered.
        /// </summary>
        public bool QuizCompleted => AnswerStatuses.Count(x => x == "True") + AnswerStatuses.Count(x => x == "False") == numberOfQuestions;

        /// <summary>
        /// The feedback provided for each question answered so far.
        /// </summary>
        /// <remarks>
        /// Only returned if the quiz has been finished.
        /// </remarks>
        public List<string> FeedbackGiven
        {
            get
            {
                if (QuizCompleted)
                    return _feedbackGiven;
                return null;
            }
        }

        /// <summary>
        /// The answers given by the user.
        /// </summary>
        /// <remarks>
        /// Only returned if the quiz has been finished.
        /// </remarks>
        public List<string> AnswersGiven
        {
            get
            {
                if (QuizCompleted)
                    return _answersGiven;
                return null;
            }
        }

        /// <summary>
        /// The questions asked in the quiz.
        /// </summary>
        /// <remarks>
        /// Only returned if the quiz has been finished.
        /// </remarks>
        public List<IQuestion> Questions
        {
            get
            {
                if (QuizCompleted)
                    return _questions;
                return null;
            }
        }

        /// <summary>
        /// The time taken by the user to complete the quiz.
        /// </summary>
        /// <remarks>
        /// Only returned if the quiz has been finished.
        /// </remarks>
        public TimeSpan TimeTaken
        {
            get
            {
                if (QuizCompleted)
                    return _stopwatch.Elapsed;
                throw new AccessViolationException("The quiz has not yet finished");
            }
        }

        /// <summary>
        /// Loads the information for the next question.
        /// </summary>
        public void LoadNextQuestion()
        {
            if (_currentQuestionIndex < numberOfQuestions - 1)
            {
                _currentQuestionIndex += 1;

                _currentQuestionAnswered = false;

                _userCorrect = null;

                _answerStatuses[_currentQuestionIndex] = "Selected";

                _buttonAction = "Submit Answer";
            }
        }

        /// <summary>
        /// Processes a bet made by the user.
        /// </summary>
        /// <param name="pointsBet"></param>
        public void MakeBet(int pointsBet)
        {
            if (pointsBet <= _score)
            {
                _pointsBet = pointsBet;

                _score -= _pointsBet;
            }
            else
            {
                throw new ArgumentException("Bet made is larger than score");
            }
        }

        /// <summary>
        /// Processes the user's answer and updates the model accordingly.
        /// </summary>
        /// <param name="userAnswer">The user's answer to the question.</param>
        /// <remarks>
        /// If the user is correct, their score will be increased.
        /// </remarks>
        public void AnswerQuestion(string userAnswer)
        {
            if (userAnswer == CurrentQuestion.Answer)
            {
                _userCorrect = true;

                _feedbackForCurrentQuestion = CurrentQuestion.Feedback;

                // If the user bet all their points, award them quadruple points, otherwise award them double points.
                if (_score == 0)
                    _score += _pointsBet * 2;

                _score += pointsPerQuestion;

                _score += _pointsBet * 2;
            }
            else
            {
                _userCorrect = false;

                // Get the answer specific feedback and set the background colour of the answer chosen to red (signifying incorrect).
                string answerSpecificFeedback = string.Empty;

                for (int i = 0; i < CurrentQuestion.Choices.Count; i++)
                {
                    if (CurrentQuestion.Choices[i].PotentialAnswer == userAnswer)
                    {
                        answerSpecificFeedback = CurrentQuestion.Choices[i].FeedbackForAnswer;

                        _questions[_currentQuestionIndex].Choices[i].BackgroundHexCode = "#FF0000";
                    }
                }

                string generalFeedback = CurrentQuestion.Feedback;

                bool bothTypesOfFeedback = !string.IsNullOrEmpty(answerSpecificFeedback) && !string.IsNullOrEmpty(generalFeedback);

                _feedbackForCurrentQuestion = $"{answerSpecificFeedback}{(bothTypesOfFeedback ? " " : "")}{generalFeedback}";
            }

            // Set the background colour of the correct answer to green.
            for (int i = 0; i < CurrentQuestion.Choices.Count; i++)
            {
                if (CurrentQuestion.Choices[i].PotentialAnswer == CurrentQuestion.Answer)
                {
                    _questions[_currentQuestionIndex].Choices[i].BackgroundHexCode = "#00FF00";
                }
            }

            if (_currentQuestionIndex == numberOfQuestions - 1)
            {
                _buttonAction = "Finish Quiz";
            }
            else
            {
                _buttonAction = "Next Question";
            }

            _pointsBet = 0;

            _currentQuestionAnswered = true;

            _answerStatuses[_currentQuestionIndex] = Methods.Capitalise(_userCorrect.ToString());

            _feedbackGiven.Add(_feedbackForCurrentQuestion);

            _answersGiven.Add(userAnswer);

            if (QuizCompleted)
                _stopwatch.Stop();
        }

        /// <summary>
        /// Provides the model with a list of questions.
        /// </summary>
        /// <param name="questions">Dictionary of all questions, obtained from <see cref="IQuestionService"/>.</param>
        public void ImportQuestions(Dictionary<string, List<IQuestion>> questions)
        {
            allQuestions ??= questions;
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

                    _answerStatuses.Add(null);
                }

                _answerStatuses[0] = "Selected";
            }
            else
            {
                throw new ArgumentException($"Invalid question category: {QuestionCategory}");
            }
        }
    }
}
