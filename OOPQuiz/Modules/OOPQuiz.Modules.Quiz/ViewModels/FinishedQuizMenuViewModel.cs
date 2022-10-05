using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Modules.Quiz.Models;
using System.Linq;

namespace OOPQuiz.Modules.Quiz.ViewModels
{
    /// <summary>
    /// View model for the finished quiz menu.
    /// </summary>
    public class FinishedQuizMenuViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private string _questionCategory;

        /// <summary>
        /// The category of questions asked in the recent quiz.
        /// </summary>
        public string QuestionCategory => _questionCategory;

        private int _numberOfQuestionsCorrect;

        /// <summary>
        /// The number of questions the user got correct in the recent quiz.
        /// </summary>
        public int NumberOfQuestionsCorrect => _numberOfQuestionsCorrect;

        private int _numberOfQuestionsInQuiz;

        /// <summary>
        /// The number of questions in the recent quiz.
        /// </summary>
        public int NumberOfQuestionsInQuiz => _numberOfQuestionsInQuiz;

        private int _score;

        /// <summary>
        /// The final score the user got in the recent quiz.
        /// </summary>
        public int Score => _score;

        private string _generalFeedback;

        /// <summary>
        /// General feedback based off the user's performance.
        /// </summary>
        public string GeneralFeedback => _generalFeedback;

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
            QuizRunnerModel modelFromQuiz = navigationContext.Parameters.GetValue<QuizRunnerModel>("QuizModel");

            _questionCategory = modelFromQuiz.QuestionCategory;

            _numberOfQuestionsCorrect = modelFromQuiz.AnswerStatuses.Count(x => x == "True");
            _numberOfQuestionsInQuiz = modelFromQuiz.AnswerStatuses.Count;
            _score = modelFromQuiz.UserScore;

            _generalFeedback = FinishedQuizFeedback.GetFeedbackBasedOnNumberOfQuestionsCorrectlyAnswered(_numberOfQuestionsCorrect, _score);

            RaisePropertyChanged(nameof(QuestionCategory));

            RaisePropertyChanged(nameof(NumberOfQuestionsCorrect));
            RaisePropertyChanged(nameof(NumberOfQuestionsInQuiz));
            RaisePropertyChanged(nameof(Score));

            RaisePropertyChanged(nameof(GeneralFeedback));
        }

        public bool KeepAlive => false;

        private readonly IRegionManager _regionManager;

        public FinishedQuizMenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
    }
}
