using OOPQuiz.Modules.Quiz;
using OOPQuiz.Services;
using OOPQuiz.Services.Interfaces;
using OOPQuiz.Views;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace OOPQuiz
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IQuestionService, QuestionService>();
            containerRegistry.RegisterSingleton<IHighscoreService, HighscoreService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<QuizModule>();
        }
    }
}
