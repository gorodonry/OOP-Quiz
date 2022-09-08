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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Determine the question category, and from that the questions.

            // This will be the case once the menu has been set up
            // _model.QuestionCategory = navigationContext.Parameters.GetValue<string>("QuestionCategory");

            _model.QuestionCategory = "C#";

            _model.SelectQuestions();

            RaisePropertyChanged(nameof(QuestionCategory));
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
