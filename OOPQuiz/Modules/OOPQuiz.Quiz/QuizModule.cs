using OOPQuiz.Quiz.ViewModels;
using OOPQuiz.Quiz.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace OOPQuiz.Quiz
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

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<QuizRunner, QuizRunnerViewModel>();
        }
    }
}
