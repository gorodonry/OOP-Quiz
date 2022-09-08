using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Services.Interfaces;
using OOPQuiz.Modules.Quiz.Models;

namespace OOPQuiz.Modules.Quiz.ViewModels
{
    public class QuizRunnerViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private QuizRunnerModel _model = new();

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
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

        public QuizRunnerViewModel(IRegionManager regionManager, IQuestionService questionService)
        {
            _regionManager = regionManager;
        }
    }
}
