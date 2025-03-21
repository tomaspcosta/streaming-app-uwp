using StreamingApp.Domain.Models;
using StreamingApp.UWP.Converters;
using StreamingApp.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace StreaminApp1.UWP.Views.Movies
{
    public sealed partial class MovieInfoPage : Page
    {
        public MovieViewModel MovieViewModel { get; set; }

        static ByteToImageConverter Converter { get; set; }

        public MovieInfoPage()
        {
            this.InitializeComponent();
            MovieViewModel = new MovieViewModel();

            Converter = new ByteToImageConverter();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                MovieViewModel = e.Parameter as MovieViewModel;

                if (MovieViewModel.Movie.IsFavourite == true)
                {
                    ToggleButtonText.Text = "Remove";
                }
                else
                {
                    ToggleButtonText.Text = "Add";
                }
            }

            base.OnNavigatedTo(e);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            if (toggleButton != null)
            {
                bool isFavorite = toggleButton.IsChecked == true;

                if (isFavorite)
                {
                    MovieViewModel.AddToFavorites();
                    SetToggleButtonText("Remove");
                }
                else
                {
                    MovieViewModel.RemoveFromFavorites();
                    SetToggleButtonText("Add");
                }
            }
        }

        private void SetToggleButtonText(string text)
        {
            ToggleButtonText.Text = text;
        }






    }
}
