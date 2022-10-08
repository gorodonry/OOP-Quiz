using System.Windows.Controls;

namespace OOPQuiz.Modules.Quiz.Views
{
    /// <summary>
    /// Interaction logic for MainMenu
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();

            SelectHighscoreCategoryListBox.SelectedIndex = 0;
        }
    }
}
