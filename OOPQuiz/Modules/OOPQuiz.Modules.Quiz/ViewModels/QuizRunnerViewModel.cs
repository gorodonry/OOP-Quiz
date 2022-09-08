using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Services.Interfaces;
using OOPQuiz.Modules.Quiz.Models;
using OOPQuiz.Business.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace OOPQuiz.Modules.Quiz.ViewModels
{
    public class QuizRunnerViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private QuizRunnerModel _model = new();

        /// <summary>
        /// The category of questions asked by this instance of the quiz runner.
        /// </summary>
        public string QuestionCategory
        {
            get { return _model.QuestionCategory; }
        }

        /// <summary>
        /// The number of the current question.
        /// </summary>
        public int CurrentQuestionNumber
        {
            get { return _model.CurrentQuestionNumber; }
        }

        /// <summary>
        /// The question for the user to answer.
        /// </summary>
        public string QuestionAsked
        {
            get { return _model.CurrentQuestionAsked; }
        }

        /// <summary>
        /// The path for the image that accompanies the question.
        /// </summary>
        public string ImageURIForQuestion
        {
            get { return _model.ImageURIForCurrentQuestion; }
        }

        /// <summary>
        /// Multichoice answers to choose from for the question.
        /// </summary>
        /// <remarks>
        /// Empty for open-ended questions.
        /// </remarks>
        public List<string> PossibleAnswers
        {
            get { return _model.AnswersToChooseFrom; }
        }

        /// <summary>
        /// Indicates whether or not the current question is open-ended.
        /// </summary>
        public bool OpenEndedQuestion
        {
            get { return _model.CurrentQuestionIsOpenEnded; }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Determine the question category, and from that the questions.

            // This will be the case once the menu has been set up.
            // _model.QuestionCategory = navigationContext.Parameters.GetValue<string>("QuestionCategory");

            _model.QuestionCategory = "C#";

            // Setting the question category will alter the following properties; alert the view to the change.
            RaisePropertyChanged(nameof(QuestionCategory));
            RaisePropertyChanged(nameof(QuestionAsked));
            RaisePropertyChanged(nameof(ImageURIForQuestion));
            RaisePropertyChanged(nameof(PossibleAnswers));
            RaisePropertyChanged(nameof(OpenEndedQuestion));
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

            _model.ImportQuestions(questionService.GetQuestions());
        }
    }
}
