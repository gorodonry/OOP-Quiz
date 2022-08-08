using Prism.Mvvm;

namespace OOPQuiz.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "OOP Quiz";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
