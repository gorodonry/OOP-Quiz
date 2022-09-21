using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Core;
using OOPQuiz.Modules.Quiz.Views;
using Prism.Commands;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace OOPQuiz.Modules.Quiz.ViewModels
{
    /// <summary>
    /// View model for the page that provides betting options between questions in the quiz.
    /// </summary>
    public class BettingPageViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private string _questionCategory;

        /// <summary>
        /// The category of questions in the quiz.
        /// </summary>
        /// <remarks>
        /// Obtained from the NavigationContext.
        /// </remarks>
        public string QuestionCategory => _questionCategory;

        private ObservableCollection<string> _answersForProgressBar;

        /// <summary>
        /// A list of strings indicating the answer status for each question.
        /// </summary>
        /// <remarks>
        /// Obtained from the NavigationContext.
        /// </remarks>
        public ObservableCollection<string> AnswersForProgressBar => _answersForProgressBar;

        private int _score;

        /// <summary>
        /// The user's score.
        /// </summary>
        public int Score => _score;

        private string _pointsBetByUser;

        /// <summary>
        /// The number of points the user wants to bet as a string.
        /// </summary>
        public string PointsBetByUser
        {
            get { return _pointsBetByUser; }
            set
            {
                SetProperty(ref _pointsBetByUser, value);

                SubmitBet.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand _submitBet;
        public DelegateCommand SubmitBet =>
            _submitBet ?? (_submitBet = new DelegateCommand(ExecuteSubmitBet, CanExecuteSubmitBet));

        void ExecuteSubmitBet()
        {
            var parameters = new NavigationParameters
            {
                { "OnNavigatedTo", "Process Bet" },
                { "PointsBetByUser", int.Parse(PointsBetByUser) }
            };

            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(QuizRunner), parameters);
        }

        bool CanExecuteSubmitBet()
        {
            return !string.IsNullOrEmpty(PointsBetByUser);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _questionCategory = navigationContext.Parameters.GetValue<string>("QuestionCategory");
            _answersForProgressBar = navigationContext.Parameters.GetValue<ObservableCollection<string>>("AnswersForProgressBar");
            _score = navigationContext.Parameters.GetValue<int>("CurrentScore");

            // Update the view to reflect the new information.
            RaisePropertyChanged(nameof(QuestionCategory));
            RaisePropertyChanged(nameof(AnswersForProgressBar));
            RaisePropertyChanged(nameof(Score));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public bool KeepAlive => false;

        private readonly IRegionManager _regionManager;

        public BettingPageViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
    }
}
