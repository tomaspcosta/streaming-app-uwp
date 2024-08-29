using StreamingApp.UWP.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP.Views.SeriesFolder
{
    public sealed partial class SeasonFormPage : Page
    {
        public SeasonViewModel SeasonViewModel { get; private set; }
        public SeriesViewModel SeriesViewModel { get; private set; }
        public SeasonFormPage()
        {
            this.InitializeComponent();

            // Create an instance of SeasonViewModel and set it as DataContext
            SeasonViewModel = new SeasonViewModel();
            SeriesViewModel = new SeriesViewModel();
            DataContext = SeasonViewModel;
            LoadSeasons();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (await SeasonViewModel.CreateOrUpdateSeasonAsync())
            {
                // delay before navigating back
                var timer = new DispatcherTimer();
                timer.Interval =
                    TimeSpan.FromMilliseconds(100);
                timer.Tick += (s, args) =>
                {
                    Frame.GoBack();
                    timer.Stop();
                };
                timer.Start();
            }
            else
            {
                FlyoutBase.ShowAttachedFlyout(btnSave_Click);
            }
        }

        private async void AddEpisode_Click(object sender, RoutedEventArgs e)
        {
            // Save the season before navigating to EpisodeFormPage
            if (await SeasonViewModel.CreateOrUpdateSeasonAsync())
            {
                Frame.Navigate(typeof(EpisodeFormPage), SeasonViewModel);
            }
            else
            {

            }
        }

        private async void LoadSeasons()
        {
            await SeasonViewModel.LoadAllSeasonsAsync();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null &&
                e.Parameter is SeriesViewModel seriesViewModel)
            {
                // Access the necessary information from seriesViewModel
                int seriesId = seriesViewModel.Id;

                // Set the SeriesId in SeasonViewModel
                SeasonViewModel.SelectedSeriesId = seriesId;
            }
            base.OnNavigatedTo(e);
        }
    }
}
