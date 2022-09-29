using OOPQuiz.Core;
using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Modules.Quiz.Views;
using OOPQuiz.Services.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;

namespace OOPQuiz.Modules.Quiz.ViewModels
{
    /// <summary>
    /// View model for the main menu.
    /// </summary>
    public class MainMenuViewModel : BindableBase, INavigationAware
    {
        private readonly List<string> _questionCategories;

        /// <summary>
        /// The question categories the user can pick from.
        /// </summary>
        public List<string> QuestionCategories => _questionCategories;

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            RaisePropertyChanged(nameof(QuestionCategories));

            // This is a placeholder until proper navigation from the main menu is set up.
            var parameters = new NavigationParameters
            {
                { "OnNavigatedTo", "Initialise Quiz" },
                { "QuestionCategory", "Python" }
            };

            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(QuizRunner), parameters);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        private readonly IRegionManager _regionManager;

        public MainMenuViewModel(IRegionManager regionManager, IQuestionService questionService)
        {
            _regionManager = regionManager;

            _questionCategories = questionService.GetQuestionCategories();
        }
    }
}
