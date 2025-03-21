using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StreaminApp1.UWP.Views.Quiz
{
    public sealed partial class IntroductionQuizPage : Page
    {
        public IntroductionQuizPage()
        {
            InitializeComponent();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Quiz page when the button is clicked
            Frame.Navigate(typeof(FormatQuizPage));
        }
    }
}

