using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Services.Interfaces;
using OOPQuiz.Modules.Quiz.Models;
using OOPQuiz.Business.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Prism.Commands;
using System;
using OOPQuiz.Core;
using OOPQuiz.Modules.Quiz.Views;

namespace OOPQuiz.Modules.Quiz.ViewModels
{
    /// <summary>
    /// View model for the page that runs the quiz.
    /// </summary>
    public class QuizRunnerViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly QuizRunnerModel _model = new();

        private bool _exitingQuiz = false;

        /// <summary>
        /// The category of questions asked by this instance of the quiz runner.
        /// </summary>
        public string QuestionCategory => _model.QuestionCategory;

        /// <summary>
        /// Information on the question currently being asked.
        /// </summary>
        public IQuestion Question => _model.CurrentQuestion;

        /// <summary>
        /// The number of the current question.
        /// </summary>
        public int CurrentQuestionNumber => _model.CurrentQuestionNumber;

        /// <summary>
        /// Indicates whether or not the current question is open-ended.
        /// </summary>
        public bool IsOpenEndedQuestion => _model.CurrentQuestionIsOpenEnded;

        /// <summary>
        /// Indicates whether or not the current question has been answered.
        /// </summary>
        public bool QuestionAnswered => _model.CurrentQuestionAnswered;

        private string _userAnswer;

        private Choice _userChoice;

        /// <summary>
        /// The answer the user has selected (as a <see cref="Choice"/>).
        /// </summary>
        /// <remarks>
        /// This effectively acts as a converter for xaml elements that pass the entire choice back.
        /// </remarks>
        public Choice UserChoice
        {
            private get { return _userChoice; }
            set
            {
                SetProperty(ref _userChoice, value);

                if (value is not null)
                {
                    UserAnswer = value.PotentialAnswer;
                }
            }
        }

        /// <summary>
        /// The answer the user has selected.
        /// </summary>
        public string UserAnswer
        {
            get { return _userAnswer; }
            set
            {
                SetProperty(ref _userAnswer, value);

                SubmitAnswer.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether or not the user got the right answer.
        /// </summary>
        public bool? UserCorrect => _model.UserCorrect;

        /// <summary>
        /// Feedback provided after the user answers the question.
        /// </summary>
        public string QuestionFeedback => _model.FeedbackForCurrentQuestion;

        /// <summary>
        /// Set to true only if the question has been answered and there is feedback available.
        /// </summary>
        public bool QuestionAnsweredAndFeedbackPresent => _model.CurrentQuestionAnswered && !string.IsNullOrEmpty(_model.FeedbackForCurrentQuestion);

        /// <summary>
        /// Action currently available to the user.
        /// </summary>
        public string QuizButtonAction => _model.ButtonAction;

        /// <summary>
        /// The user's score.
        /// </summary>
        public int Score => _model.UserScore;

        /// <summary>
        /// The bet the user has made.
        /// </summary>
        public int BetMade => _model.PointsBetByUser;

        /// <summary>
        /// A boolean indicating whether or not the user has bet points.
        /// </summary>
        public bool UserHasBetPoints => _model.PointsBetByUser != 0;

        /// <summary>
        /// A list of strings indicating the answer status for each question.
        /// </summary>
        public ObservableCollection<string> AnswersForProgressBar => _model.AnswerStatuses;

        private DelegateCommand _submitAnswer;
        public DelegateCommand SubmitAnswer =>
            _submitAnswer ?? (_submitAnswer = new DelegateCommand(ExecuteSubmitAnswer, CanExecuteSubmitAnswer));

        /// <summary>
        /// Indicates the fact that the user has answered the question to the model and updates the view accordingly.
        /// </summary>
        void ExecuteSubmitAnswer()
        {
            if (IsOpenEndedQuestion)
            {
                _model.AnswerQuestion(UserAnswer.ToLower());
            }
            else
            {
                _model.AnswerQuestion(UserAnswer);
            }

            RaisePropertyChanged(nameof(QuestionAnswered));
            RaisePropertyChanged(nameof(QuestionAnsweredAndFeedbackPresent));
            RaisePropertyChanged(nameof(UserCorrect));
            RaisePropertyChanged(nameof(QuestionFeedback));
            RaisePropertyChanged(nameof(Score));
            RaisePropertyChanged(nameof(BetMade));
            RaisePropertyChanged(nameof(UserHasBetPoints));
            RaisePropertyChanged(nameof(AnswersForProgressBar));

            RaisePropertyChanged(nameof(QuizButtonAction));
            AdvanceQuiz.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Indicates whether or not the user can submit their current answer.
        /// </summary>
        /// <returns>A boolean indicating whether or not they have entered/selected an answer.</returns>
        bool CanExecuteSubmitAnswer()
        {
            // User cannot submit a blank answer.
            if (QuizButtonAction == "Submit Answer" && !string.IsNullOrEmpty(UserAnswer))
                return true;
            return false;
        }

        private DelegateCommand _advanceQuiz;
        public DelegateCommand AdvanceQuiz =>
            _advanceQuiz ?? (_advanceQuiz = new DelegateCommand(ExecuteAdvanceQuiz, CanExecuteAdvanceQuiz));

        /// <summary>
        /// Advances the quiz.
        /// </summary>
        /// <remarks>
        /// This either means loading the next question, loading the betting page, or loading the finish quiz page depending on the quiz status.
        /// </remarks>
        void ExecuteAdvanceQuiz()
        {
            if (QuizButtonAction == "Next Question" && Score != 0)
            {
                // Navigate to the betting page if the user has points to make a bet with.
                var parameters = new NavigationParameters
                {
                    { "QuestionCategory", QuestionCategory },
                    { "AnswersForProgressBar", AnswersForProgressBar },
                    { "CurrentScore", Score }
                };

                _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(BettingPage), parameters);
            }
            else if (QuizButtonAction == "Next Question")
            {
                LoadNextQuestion();
            }
            else
            {
                _exitingQuiz = true;

                // Navigate to a finish screen for the quiz (coming in a later sprint).
            }
        }

        /// <summary>
        /// Indicates whether or not the quiz is in a state that it can advance from.
        /// </summary>
        /// <returns>A boolean.</returns>
        /// <remarks>
        /// Always returns true.
        /// </remarks>
        bool CanExecuteAdvanceQuiz()
        {
            return QuestionAnswered;
        }

        /// <summary>
        /// Loads the next question from the model and alerts the view to the change.
        /// </summary>
        public void LoadNextQuestion()
        {
            _model.LoadNextQuestion();

            _userAnswer = string.Empty;

            // Alert the view to the change in the question.
            RaisePropertyChanged(nameof(Question));
            RaisePropertyChanged(nameof(QuestionAnswered));
            RaisePropertyChanged(nameof(QuestionAnsweredAndFeedbackPresent));
            RaisePropertyChanged(nameof(UserCorrect));
            RaisePropertyChanged(nameof(AnswersForProgressBar));
            RaisePropertyChanged(nameof(IsOpenEndedQuestion));
            RaisePropertyChanged(nameof(CurrentQuestionNumber));

            RaisePropertyChanged(nameof(QuizButtonAction));
            SubmitAnswer.RaiseCanExecuteChanged();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (!navigationContext.Parameters.ContainsKey("OnNavigatedTo"))
                throw new ArgumentException("No 'OnNavigatedTo' parameter provided with the NavigationContext upon navigation to the QuizRunner");

            if (navigationContext.Parameters.GetValue<string>("OnNavigatedTo") == "Initialise Quiz")
            {
                // Determine the question category, and from that the questions.
                _model.QuestionCategory = navigationContext.Parameters.GetValue<string>("QuestionCategory");

                // Setting the question category will alter the following properties; alert the view to the change.
                RaisePropertyChanged(nameof(QuestionCategory));
                RaisePropertyChanged(nameof(Question));
                RaisePropertyChanged(nameof(IsOpenEndedQuestion));
                RaisePropertyChanged(nameof(AnswersForProgressBar));
            }
            else if (navigationContext.Parameters.GetValue<string>("OnNavigatedTo") == "Process Bet")
            {
                int betMade = navigationContext.Parameters.GetValue<int>("PointsBetByUser");

                _model.MakeBet(betMade);

                // Alert the view to the change in the score due to betting.
                RaisePropertyChanged(nameof(Score));
                RaisePropertyChanged(nameof(BetMade));
                RaisePropertyChanged(nameof(UserHasBetPoints));

                LoadNextQuestion();
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public bool KeepAlive => !_exitingQuiz;

        private readonly IRegionManager _regionManager;

        public QuizRunnerViewModel(IRegionManager regionManager, IQuestionService questionService)
        {
            _regionManager = regionManager;

            _model.ImportQuestions(questionService.GetQuestions());
        }
    }
}
