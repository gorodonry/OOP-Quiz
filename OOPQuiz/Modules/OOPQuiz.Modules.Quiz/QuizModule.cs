using OOPQuiz.Modules.Quiz.ViewModels;
using OOPQuiz.Modules.Quiz.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using OOPQuiz.Core;

namespace OOPQuiz.Modules.Quiz
{
    public class QuizModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public QuizModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(MainMenu));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register the menus for navigation.
            containerRegistry.RegisterForNavigation<MainMenu, MainMenuViewModel>();
            containerRegistry.RegisterForNavigation<FinishedQuizMenu, FinishedQuizMenuViewModel>();

            // Register the quiz and quiz-affiliated views for navigation.
            containerRegistry.RegisterForNavigation<QuizRunner, QuizRunnerViewModel>();
            containerRegistry.RegisterForNavigation<BettingPage, BettingPageViewModel>();
        }
    }
}
