using OOPQuiz.Core;
using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Modules.Quiz.Views;
using OOPQuiz.Services.Interfaces;
using System.Collections.Generic;
using Prism.Commands;
using System.Linq;

using System.Diagnostics;

namespace OOPQuiz.Modules.Quiz.ViewModels
{
    /// <summary>
    /// View model for the main menu.
    /// </summary>
    public class MainMenuViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly List<string> _questionCategories;

        /// <summary>
        /// The question categories the user can pick from.
        /// </summary>
        public List<string> QuestionCategories => _questionCategories;

        private string _selectedQuestionCategory;

        /// <summary>
        /// The question category the user has selected.
        /// </summary>
        public string SelectedQuestionCategory
        {
            get { return _selectedQuestionCategory; }
            set
            {
                SetProperty(ref _selectedQuestionCategory, value);

                StartQuiz.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand _startQuiz;
        public DelegateCommand StartQuiz =>
            _startQuiz ?? (_startQuiz = new DelegateCommand(ExecuteStartQuiz, CanExecuteStartQuiz));

        /// <summary>
        /// Starts a quiz with questions in the category selected by the user.
        /// </summary>
        /// <remarks>
        /// This is done by navigating to a new instance of <see cref="QuizRunner"/>.
        /// </remarks>
        void ExecuteStartQuiz()
        {
            var parameters = new NavigationParameters
            {
                { "OnNavigatedTo", "Initialise Quiz" },
                { "QuestionCategory", SelectedQuestionCategory }
            };

            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(QuizRunner), parameters);
        }

        /// <summary>
        /// Returns a boolean indicating whether or not the user can start the quiz.
        /// </summary>
        /// <returns>True if the user has selected a question category, otherwise false.</returns>
        bool CanExecuteStartQuiz()
        {
            return !string.IsNullOrEmpty(SelectedQuestionCategory);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            RaisePropertyChanged(nameof(QuestionCategories));
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

        public MainMenuViewModel(IRegionManager regionManager, IQuestionService questionService)
        {
            _regionManager = regionManager;

            _questionCategories = questionService.GetQuestionCategories().ToList();
        }
    }
}
