using Prism.Mvvm;
using Prism.Regions;

namespace OOPQuiz.Modules.Quiz.ViewModels
{
    /// <summary>
    /// View model for the finished quiz menu.
    /// </summary>
    public class FinishedQuizMenuViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private string _questionCategory;

        public string QuestionCategory => _questionCategory;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _questionCategory = navigationContext.Parameters.GetValue<string>("QuestionCategory");

            RaisePropertyChanged(nameof(QuestionCategory));
        }

        public bool KeepAlive => false;

        private readonly IRegionManager _regionManager;

        public FinishedQuizMenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
    }
}
