using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Core;
using OOPQuiz.Modules.Quiz.Views;
using Prism.Commands;
using System;
using System.Diagnostics;

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

        private int _score;

        /// <summary>
        /// The user's score.
        /// </summary>
        public int Score => _score;

        private int _betSizeAsPercentage;

        /// <summary>
        /// The size of the user's bet as a percentage of their current points.
        /// </summary>
        /// <remarks>
        /// Maximum value is 100 and minimum value is 0.
        /// </remarks>
        public int BetSizeAsPercentage
        {
            get { return _betSizeAsPercentage; }
            set { SetProperty(ref _betSizeAsPercentage, Math.Min(Math.Max(value, 0), 100)); }
        }

        private DelegateCommand _submitBet;
        public DelegateCommand SubmitBet =>
            _submitBet ?? (_submitBet = new DelegateCommand(ExecuteSubmitBet, CanExecuteSubmitBet));

        void ExecuteSubmitBet()
        {
            var parameters = new NavigationParameters
            {
                { "OnNavigatedTo", "Process Bet" },
                { "BetSizeAsPercentage", BetSizeAsPercentage }
            };

            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(QuizRunner), parameters);
        }

        bool CanExecuteSubmitBet()
        {
            return true;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _questionCategory = navigationContext.Parameters.GetValue<string>("QuestionCategory");
            _score = navigationContext.Parameters.GetValue<int>("CurrentScore");

            // The following is a placeholder until the betting page has been properly set up.
            BetSizeAsPercentage = 0;

            ExecuteSubmitBet();
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
