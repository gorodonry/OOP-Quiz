using OOPQuiz.Core;
using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Modules.Quiz.Views;
using OOPQuiz.Services.Interfaces;
using System.Collections.Generic;
using Prism.Commands;
using System.Linq;
using OOPQuiz.Business.Models;

using System.Diagnostics;

namespace OOPQuiz.Modules.Quiz.ViewModels
{
    /// <summary>
    /// View model for the main menu.
    /// </summary>
    public class MainMenuViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly Dictionary<string, List<Performance>> _highscores;

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

                HighscoreDisplayIndex = _questionCategories.IndexOf(value);

                RaisePropertyChanged(nameof(HighscoreDisplayIndex));
                RaisePropertyChanged(nameof(HighscoreDisplayCategory));
                RaisePropertyChanged(nameof(HighscoresToDisplay));
                StartQuiz.RaiseCanExecuteChanged();
            }
        }

        private int _highscoreDisplayIndex = 0;

        /// <summary>
        /// The index of the highscore category being displayed.
        /// </summary>
        /// <remarks>
        /// Initially set to 0.
        /// </remarks>
        public int HighscoreDisplayIndex
        {
            get { return _highscoreDisplayIndex; }
            set
            {
                SetProperty(ref _highscoreDisplayIndex, value);

                RaisePropertyChanged(nameof(HighscoreDisplayCategory));
                RaisePropertyChanged(nameof(HighscoresToDisplay));
            }
        }

        /// <summary>
        /// The question category of the highscores being displayed.
        /// </summary>
        public string HighscoreDisplayCategory
        {
            get
            {
                if (_questionCategories is not null)
                    return _questionCategories[_highscoreDisplayIndex];
                return null;
            }
        }

        /// <summary>
        /// The highscores for the selected category.
        /// </summary>
        public Dictionary<int, Performance> HighscoresToDisplay
        {
            get
            {
                if (_highscores is not null)
                {
                    Dictionary<int, Performance> categoryScoresWithRanks = new();

                    for (int i = 0; i < _highscores[_questionCategories[_highscoreDisplayIndex]].Count; i++)
                    {
                        categoryScoresWithRanks.Add(i + 1, _highscores[_questionCategories[_highscoreDisplayIndex]][i]);
                    }

                    return categoryScoresWithRanks;
                }

                return null;
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

        public MainMenuViewModel(IRegionManager regionManager, IQuestionService questionService, IHighscoreService highscoreService)
        {
            _regionManager = regionManager;

            _questionCategories = questionService.GetQuestionCategories().ToList();

            _highscores = highscoreService.GetOrderedHighscores();
        }
    }
}
