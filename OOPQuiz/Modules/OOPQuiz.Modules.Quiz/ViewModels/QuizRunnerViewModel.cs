﻿using Prism.Mvvm;
using Prism.Regions;
using OOPQuiz.Services.Interfaces;
using OOPQuiz.Modules.Quiz.Models;
using OOPQuiz.Business.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Prism.Commands;
using System;

namespace OOPQuiz.Modules.Quiz.ViewModels
{
    public class QuizRunnerViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly QuizRunnerModel _model = new();

        /// <summary>
        /// The category of questions asked by this instance of the quiz runner.
        /// </summary>
        public string QuestionCategory => _model.QuestionCategory;

        /// <summary>
        /// Information on the question currently being asked.
        /// </summary>
        public IQuestion Question => _model.CurrentQuestion;

        /// <summary>
        /// The number of the current question.
        /// </summary>
        public int CurrentQuestionNumber => _model.CurrentQuestionNumber;

        /// <summary>
        /// Indicates whether or not the current question is open-ended.
        /// </summary>
        public bool IsOpenEndedQuestion => _model.CurrentQuestionIsOpenEnded;

        /// <summary>
        /// Indicates whether or not the current question has been answered.
        /// </summary>
        public bool QuestionAnswered => _model.CurrentQuestionAnswered;

        private string _userAnswer;

        private Choice _userChoice;

        /// <summary>
        /// The answer the user has selected (as a <see cref="Choice"/>).
        /// </summary>
        /// <remarks>
        /// This effectively acts as a converter for xaml elements that pass the entire choice back.
        /// </remarks>
        public Choice UserChoice
        {
            private get { return _userChoice; }
            set
            {
                SetProperty(ref _userChoice, value);

                if (value is not null)
                {
                    UserAnswer = value.PotentialAnswer;
                }
            }
        }

        /// <summary>
        /// The answer the user has selected.
        /// </summary>
        public string UserAnswer
        {
            get { return _userAnswer; }
            set
            {
                SetProperty(ref _userAnswer, value);

                SubmitAnswer.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether or not the user got the right answer.
        /// </summary>
        public bool? UserCorrect => _model.UserCorrect;

        /// <summary>
        /// Feedback provided after the user answers the question.
        /// </summary>
        public string QuestionFeedback => _model.FeedbackForCurrentQuestion;

        /// <summary>
        /// Set to true only if the question has been answered and there is feedback available.
        /// </summary>
        public bool QuestionAnsweredAndFeedbackPresent => _model.CurrentQuestionAnswered && !string.IsNullOrEmpty(_model.FeedbackForCurrentQuestion);

        /// <summary>
        /// Action currently available to the user.
        /// </summary>
        public string QuizButtonAction => _model.ButtonAction;

        /// <summary>
        /// The user's score.
        /// </summary>
        public int Score => _model.UserScore;

        /// <summary>
        /// A list of strings indicating the answer status for each question.
        /// </summary>
        public ObservableCollection<string> AnswersForProgressBar => _model.AnswerStatuses;

        private DelegateCommand _submitAnswer;
        public DelegateCommand SubmitAnswer =>
            _submitAnswer ?? (_submitAnswer = new DelegateCommand(ExecuteSubmitAnswer, CanExecuteSubmitAnswer));

        void ExecuteSubmitAnswer()
        {
            if (IsOpenEndedQuestion)
            {
                _model.AnswerQuestion(UserAnswer.ToLower());
            }
            else
            {
                _model.AnswerQuestion(UserAnswer);
            }

            RaisePropertyChanged(nameof(QuestionAnswered));
            RaisePropertyChanged(nameof(QuestionAnsweredAndFeedbackPresent));
            RaisePropertyChanged(nameof(UserCorrect));
            RaisePropertyChanged(nameof(QuestionFeedback));
            RaisePropertyChanged(nameof(Score));
            RaisePropertyChanged(nameof(AnswersForProgressBar));

            RaisePropertyChanged(nameof(QuizButtonAction));
            AdvanceQuiz.RaiseCanExecuteChanged();
        }

        bool CanExecuteSubmitAnswer()
        {
            // User cannot submit a blank answer.
            if (QuizButtonAction == "Submit Answer" && !string.IsNullOrEmpty(UserAnswer))
                return true;
            return false;
        }

        private DelegateCommand _advanceQuiz;
        public DelegateCommand AdvanceQuiz =>
            _advanceQuiz ?? (_advanceQuiz = new DelegateCommand(ExecuteAdvanceQuiz, CanExecuteAdvanceQuiz));

        void ExecuteAdvanceQuiz()
        {
            if (QuizButtonAction == "Next Question")
            {
                _model.LoadNextQuestion();

                _userAnswer = string.Empty;

                // Alert the view to the change in the question.
                RaisePropertyChanged(nameof(Question));
                RaisePropertyChanged(nameof(QuestionAnswered));
                RaisePropertyChanged(nameof(QuestionAnsweredAndFeedbackPresent));
                RaisePropertyChanged(nameof(UserCorrect));
                RaisePropertyChanged(nameof(AnswersForProgressBar));
                RaisePropertyChanged(nameof(IsOpenEndedQuestion));
                RaisePropertyChanged(nameof(CurrentQuestionNumber));

                RaisePropertyChanged(nameof(QuizButtonAction));
                SubmitAnswer.RaiseCanExecuteChanged();
            }
            else
            {
                // Navigate to a finish screen for the quiz (coming in a later sprint).
            }
        }

        bool CanExecuteAdvanceQuiz()
        {
            return QuestionAnswered;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Determine the question category, and from that the questions.

            // This will be the case once the menu has been set up.
            // _model.QuestionCategory = navigationContext.Parameters.GetValue<string>("QuestionCategory");

            // In the meantime, this is a placeholder.
            _model.QuestionCategory = "Python";

            // Setting the question category will alter the following properties; alert the view to the change.
            RaisePropertyChanged(nameof(QuestionCategory));
            RaisePropertyChanged(nameof(Question));
            RaisePropertyChanged(nameof(IsOpenEndedQuestion));
            RaisePropertyChanged(nameof(AnswersForProgressBar));
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
