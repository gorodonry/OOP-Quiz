using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Modules.Quiz.Models;
using Prism.Commands;
using OOPQuiz.Core;
using OOPQuiz.Modules.Quiz.Views;
using System.Collections.Generic;
using OOPQuiz.Business.Models;

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

        private DelegateCommand<QuestionNumberAnswerPair> _selectQuestion;
        public DelegateCommand<QuestionNumberAnswerPair> SelectQuestion =>
            _selectQuestion ?? (_selectQuestion = new DelegateCommand<QuestionNumberAnswerPair>(ExecuteSelectQuestion));

        /// <summary>
        /// Changes the selected question to the question specified.
        /// </summary>
        /// <param name="questionNumber">The number of the question to display.</param>
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
            RaisePropertyChanged(nameof(GeneralFeedback));
            RaisePropertyChanged(nameof(SelectedQuestion));
        }

        public bool KeepAlive => false;

        private readonly IRegionManager _regionManager;

        public FinishedQuizMenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
    }
}
