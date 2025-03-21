using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace StreaminApp1.UWP.Views.Quiz
{
    public sealed partial class FormatQuizPage : Page
    {
        public FormatQuizPage()
        {
            InitializeComponent();
        }


        private void SerieButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TypeSeriesQuizPage));
        }

        private void MovieButton_Click(object sender, RoutedEventArgs e)
        {
         
            Frame.Navigate(typeof(TypeMovieQuizPage));
        }
    }
}