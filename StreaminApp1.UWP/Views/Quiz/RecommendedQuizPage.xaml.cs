using StreaminApp1.UWP.Views.Movies;
using StreamingApp.Domain.Models;
using StreamingApp.UWP.ViewModels;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StreaminApp1.UWP.Views.Quiz
{
    public sealed partial class RecommendedQuizPage : Page
    {

        public MovieViewModel MovieViewModel { get; set; }
        public RecommendedQuizPage()
        {
            MovieViewModel = new MovieViewModel();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Check if the parameter is a list of filtered movies
            if (e.Parameter is ObservableCollection<Movie> filteredMovies)
            {

                RecommendedGridView.ItemsSource = filteredMovies;
            }
        }
        private void gridCategories_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Movie m)
            {
                MovieViewModel.Movie = m;
                Frame.Navigate(typeof(MovieInfoPage), MovieViewModel);
            }
        }

    }
}
