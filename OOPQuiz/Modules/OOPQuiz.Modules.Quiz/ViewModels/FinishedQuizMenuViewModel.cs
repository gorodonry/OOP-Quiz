using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Modules.Quiz.Models;
using Prism.Commands;
using OOPQuiz.Core;
using OOPQuiz.Modules.Quiz.Views;
using System.Collections.Generic;
using OOPQuiz.Business.Models;
using OOPQuiz.Services.Interfaces;

namespace OOPQuiz.Modules.Quiz.ViewModels
{
    /// <summary>
    /// View model for the finished quiz menu.
    /// </summary>
    public class FinishedQuizMenuViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly FinishedQuizMenuModel _model = new();

        /// <summary>
        /// The number of the question currently selected by the user.
        /// </summary>
        public int SelectedQuestionNumber => _model.SelectedQuestionNumber;

        /// <summary>
        /// The category of questions asked in the recent quiz.
        /// </summary>
        public string QuestionCategory => _model.QuestionCategory;

        /// <summary>
        /// The number of questions the user got correct in the recent quiz.
        /// </summary>
        public int NumberOfQuestionsCorrect => _model.NumberOfQuestionsCorrect;

        /// <summary>
        /// The number of questions in the recent quiz.
        /// </summary>
        public int NumberOfQuestionsInQuiz => _model.NumberOfQuestionsInQuiz;

        /// <summary>
        /// The final score the user got in the recent quiz.
        /// </summary>
        public int Score => _model.Score;

        /// <summary>
        /// The time taken to complete the recent quiz.
        /// </summary>
        public string TimeTaken => _model.TimeTakenAsString;

        /// <summary>
        /// The user's provisional placing against all other highscores.
        /// </summary>
        public int? ProvisionalPlacing
        {
            get
            {
                if (_model.QuestionCategory is not null)
                    return _highscoreService.DetermineProvisionalPlacing(_model.QuestionCategory, _model.Score, _model.TimeTaken);
                return null;
            }
        } 

        /// <summary>
        /// General feedback based off the user's performance.
        /// </summary>
        public string GeneralFeedback => _model.GeneralFeedback;

        /// <summary>
        /// A list of <see cref="QuestionNumberAnswerPair"/>'s indicating which questions the user got right.
        /// </summary>
        public List<QuestionNumberAnswerPair> FinalAnswerStatuses => _model.FinalAnswerStatuses;

        /// <summary>
        /// The question currently being reviewed by the user.
        /// </summary>
        public IQuestion SelectedQuestion => _model.SelectedQuestion;

        /// <summary>
        /// The question-specific feedback for the question currently being reviewed by the user.
        /// </summary>
        public string FeedbackForSelectedQuestion => _model.FeedbackForSelectedQuestion;

        /// <summary>
        /// The answer given by the user for the question currently being reviewed by the user.
        /// </summary>
        /// <remarks>
        /// Useful for when the user is reviewing open-ended questions.
        /// </remarks>
        public string AnswerGivenForSelectedQuestion => _model.AnswerGivenForSelectedQuestion;

        /// <summary>
        /// The answer status for the question currently being reviewed by the user.
        /// </summary>
        public string AnswerStatusForSelectedQuestion => _model.AnswerStatusForSelectedQuestion;

        /// <summary>
        /// Indicates whether or not the question the user is reviewing is open-ended.
        /// </summary>
        public bool SelectedQuestionIsOpenEnded => _model.SelectedQuestionIsOpenEnded;

        private string _userName;

        /// <summary>
        /// The name of the user who took the quiz.
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set
            {
                SetProperty(ref _userName, value.Trim());

                RaisePropertyChanged(nameof(UserName));
                SubmitScore.RaiseCanExecuteChanged();
            }
        }

        private bool _scoreSubmitted = false;

        /// <summary>
        /// A boolean indicating whether or not the user's score has been submitted.
        /// </summary>
        public bool ScoreSubmitted => _scoreSubmitted;

        private DelegateCommand<QuestionNumberAnswerPair> _selectQuestion;
        public DelegateCommand<QuestionNumberAnswerPair> SelectQuestion =>
            _selectQuestion ?? (_selectQuestion = new DelegateCommand<QuestionNumberAnswerPair>(ExecuteSelectQuestion));

        /// <summary>
        /// Changes the selected question to the question specified.
        /// </summary>
        /// <param name="questionInfo">The number of the question to display.</param>
        void ExecuteSelectQuestion(QuestionNumberAnswerPair questionInfo)
        {
            _model.SelectedQuestionNumber = questionInfo.QuestionNumber;

            RaisePropertyChanged(nameof(SelectedQuestionNumber));
            RaisePropertyChanged(nameof(SelectedQuestion));
            RaisePropertyChanged(nameof(SelectedQuestionIsOpenEnded));
            RaisePropertyChanged(nameof(FeedbackForSelectedQuestion));
            RaisePropertyChanged(nameof(AnswerGivenForSelectedQuestion));
            RaisePropertyChanged(nameof(AnswerStatusForSelectedQuestion));
        }

        private DelegateCommand _submitScore;
        public DelegateCommand SubmitScore =>
            _submitScore ?? (_submitScore = new DelegateCommand(ExecuteSubmitScore, CanExecuteSubmitScore));

        /// <summary>
        /// Adds the user's score to the leaderboard.
        /// </summary>
        void ExecuteSubmitScore()
        {
            _highscoreService.SubmitHighscore(QuestionCategory, new(UserName, _model.Score, _model.TimeTaken));

            _scoreSubmitted = true;

            RaisePropertyChanged(nameof(ScoreSubmitted));
            SubmitScore.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Determines if the user can submit their score or not.
        /// </summary>
        /// <returns>True if the user has entered a username, otherwise False.</returns>
        bool CanExecuteSubmitScore()
        {
            return !string.IsNullOrEmpty(UserName) && !ScoreSubmitted;
        }

        private DelegateCommand _exitToMainMenu;
        public DelegateCommand ExitToMainMenu =>
            _exitToMainMenu ?? (_exitToMainMenu = new DelegateCommand(ExecuteExitToMainMenu));

        /// <summary>
        /// Navigates to the main menu.
        /// </summary>
        void ExecuteExitToMainMenu()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(MainMenu));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Obtain all necessary information for this menu from the model.
            _model.ProcessQuizInformation(navigationContext.Parameters.GetValue<QuizRunnerModel>("QuizModel"));

            // Alert the view to the new information.
            RaisePropertyChanged(nameof(QuestionCategory));
            RaisePropertyChanged(nameof(FinalAnswerStatuses));
            RaisePropertyChanged(nameof(NumberOfQuestionsCorrect));
            RaisePropertyChanged(nameof(NumberOfQuestionsInQuiz));
            RaisePropertyChanged(nameof(Score));
            RaisePropertyChanged(nameof(TimeTaken));
            RaisePropertyChanged(nameof(ProvisionalPlacing));
            RaisePropertyChanged(nameof(GeneralFeedback));
            RaisePropertyChanged(nameof(SelectedQuestion));
        }

        public bool KeepAlive => false;

        private readonly IRegionManager _regionManager;

        private readonly IHighscoreService _highscoreService;

        public FinishedQuizMenuViewModel(IRegionManager regionManager, IHighscoreService highscoreService)
        {
            _regionManager = regionManager;

            _highscoreService = highscoreService;
        }
    }
}
