using StreamingApp.UWP.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP.Views.SeriesFolder
{
    public sealed partial class EpisodeFormPage : Page
    {
        public EpisodeViewModel EpisodeViewModel { get; set; }

        public EpisodeFormPage()
        {
            EpisodeViewModel = new EpisodeViewModel();
            this.InitializeComponent();
        }
        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Call the appropriate method in your ViewModel to save the episode
            bool saved = await EpisodeViewModel.CreateOrUpdateEpisodeAsync();

            if (saved)
            {
                Frame.GoBack();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null &&
                e.Parameter is SeasonViewModel seasonViewModel)
            {
                // Access the necessary information from seriesViewModel
                int seasonId = seasonViewModel.Id;

                // Set the SeriesId in SeasonViewModel
                EpisodeViewModel.SelectedSeasonId = seasonId;
            }
            base.OnNavigatedTo(e);
        }
    }
}
