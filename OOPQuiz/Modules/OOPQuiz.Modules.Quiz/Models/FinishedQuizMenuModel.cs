using OOPQuiz.Business.Models;
using OOPQuiz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPQuiz.Modules.Quiz.Models
{
    /// <summary>
    /// Stores all necessary information for the finished quiz menu to function.
    /// </summary>
    public class FinishedQuizMenuModel
    {
        protected int _numberOfQuestionsInQuiz;
        protected int _numberOfQuestionsCorrect;

        protected string _questionCategory;
        protected List<IQuestion> _questions;
        protected List<string> _answersGiven;
        protected List<string> _specificQuestionFeedback;
        protected List<QuestionNumberAnswerPair> _finalAnswerStatuses;

        protected int _score;
        protected TimeSpan _timeTaken;
        protected string _generalFeedback;

        protected int _selectedQuestionNumber = 1;

        /// <summary>
        /// Creates a new instance of the finished quiz menu.
        /// </summary>
        public FinishedQuizMenuModel()
        {

        }

        /// <summary>
        /// The number of questions in the recent quiz.
        /// </summary>
        public int NumberOfQuestionsInQuiz => _numberOfQuestionsInQuiz;

        /// <summary>
        /// The number of questions the user got correct in the recent quiz.
        /// </summary>
        public int NumberOfQuestionsCorrect => _numberOfQuestionsCorrect;

        /// <summary>
        /// The category of questions asked in the recent quiz.
        /// </summary>
        public string QuestionCategory => _questionCategory;

        /// <summary>
        /// A list of <see cref="QuestionNumberAnswerPair"/>'s indicating which questions the user got right.
        /// </summary>
        public List<QuestionNumberAnswerPair> FinalAnswerStatuses => _finalAnswerStatuses;

        /// <summary>
        /// The final score the user got in the recent quiz.
        /// </summary>
        public int Score => _score;

        /// <summary>
        /// The time taken to complete the recent quiz as a string.
        /// </summary>
        public string TimeTakenAsString => Methods.ConvertTimeSpanToString(_timeTaken);

        public TimeSpan TimeTaken => _timeTaken;

        /// <summary>
        /// General feedback based off the user's performance.
        /// </summary>
        public string GeneralFeedback => _generalFeedback;

        /// <summary>
        /// The number of the question currently selected by the user.
        /// </summary>
        public int SelectedQuestionNumber
        {
            get { return _selectedQuestionNumber; }
            set
            {
                if (value >= 1 && value <= _numberOfQuestionsInQuiz)
                    _selectedQuestionNumber = value;
            }
        }

        /// <summary>
        /// The question currently being reviewed by the user.
        /// </summary>
        public IQuestion SelectedQuestion
        {
            get
            {
                if (_questions is not null)
                    return _questions[_selectedQuestionNumber - 1];
                return null;
            }
        }

        /// <summary>
        /// The question-specific feedback for the question currently being reviewed by the user.
        /// </summary>
        public string FeedbackForSelectedQuestion
        {
            get
            {
                if (_specificQuestionFeedback is not null)
                    return _specificQuestionFeedback[_selectedQuestionNumber - 1];
                return null;
            }
        }

        /// <summary>
        /// The answer given by the user for the question currently being reviewed by the user.
        /// </summary>
        /// <remarks>
        /// Useful for when the user is reviewing open-ended questions.
        /// </remarks>
        public string AnswerGivenForSelectedQuestion
        {
            get
            {
                if (_answersGiven is not null)
                    return _answersGiven[_selectedQuestionNumber - 1];
                return null;
            }
        }

        /// <summary>
        /// The answer status for the question currently being reviewed by the user.
        /// </summary>
        public string AnswerStatusForSelectedQuestion
        {
            get
            {
                if (_finalAnswerStatuses is not null)
                    return _finalAnswerStatuses[_selectedQuestionNumber - 1].AnswerAsString;
                return null;
            }
        }

        /// <summary>
        /// Indicates whether or not the question the user is reviewing is open-ended.
        /// </summary>
        public bool SelectedQuestionIsOpenEnded
        {
            get
            {
                if (_questions is not null)
                    return _questions[_selectedQuestionNumber - 1].Choices.Count == 0;
                return false;
            }
        }

        /// <summary>
        /// Loads the necessary information from the recently completed quiz into the model for the finished quiz menu.
        /// </summary>
        /// <param name="modelFromQuiz">The model used for the recent quiz, containing all information from the quiz.</param>
        public void ProcessQuizInformation(QuizRunnerModel modelFromQuiz)
        {
            _questions = modelFromQuiz.Questions;
            _answersGiven = modelFromQuiz.AnswersGiven;
            _specificQuestionFeedback = modelFromQuiz.FeedbackGiven;

            _questionCategory = modelFromQuiz.QuestionCategory;

            _numberOfQuestionsCorrect = modelFromQuiz.AnswerStatuses.Count(x => x == "True");
            _numberOfQuestionsInQuiz = modelFromQuiz.AnswerStatuses.Count;
            _score = modelFromQuiz.UserScore;

            // I am only interested in accuracy to the nearest second.
            TimeSpan insanelyAccurateTime = modelFromQuiz.TimeTaken;
            _timeTaken = new(insanelyAccurateTime.Hours, insanelyAccurateTime.Minutes, insanelyAccurateTime.Seconds);

            _generalFeedback = FinishedQuizFeedback.GetFeedbackBasedOnNumberOfQuestionsCorrectlyAnswered(_numberOfQuestionsCorrect, _score);

            _finalAnswerStatuses = new();

            for (int i = 0; i < _numberOfQuestionsInQuiz; i++)
            {
                _finalAnswerStatuses.Add(new(modelFromQuiz.AnswerStatuses[i], i + 1));
            }
        }
    }
}
