using StreamingApp.Domain.Models;
using StreamingApp.UWP.ViewModels;
using System;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StreaminApp1.UWP.Views.Quiz
{
    public sealed partial class DurationMovieQuizPage : Page
    {
        private Category selectedMovieCategory; 

        public MovieViewModel MovieViewModel { get; set; }

        public DurationMovieQuizPage()
        {
            MovieViewModel = new MovieViewModel();
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Retrieve the selected category from the navigation parameter
            if (e.Parameter is Category category)
            {
                selectedMovieCategory = category;
            }
        }

        private async void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve duration from TextBox
            string durationText = textBoxDuration.Text.Trim();

            // Specify the expected format (hh:mm:ss)
            string format = "hh\\:mm\\:ss";

            if (TimeSpan.TryParseExact(durationText, format, CultureInfo.InvariantCulture, out TimeSpan selectedDuration))
            {
                // Call the new method to filter movies by category and duration
                await MovieViewModel.LoadAllByCategoryAndDurationAsync(selectedMovieCategory, selectedDuration);

                // Navigate to the next page and pass the filtered movies
                Frame.Navigate(typeof(RecommendedQuizPage), MovieViewModel.Movies);
            }
            else
            {
             
            }
        }
    }
}
