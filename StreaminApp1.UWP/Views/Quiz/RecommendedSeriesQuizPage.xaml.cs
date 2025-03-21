using StreaminApp1.UWP.Views.Movies;
using StreaminApp1.UWP.Views.SeriesFolder;
using StreamingApp.Domain.Models;
using StreamingApp.UWP.ViewModels;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StreaminApp1.UWP.Views.Quiz
{
    public sealed partial class RecommendedSeriesQuizPage : Page
    {

        public SeriesViewModel SeriesViewModel { get; set; }
        public RecommendedSeriesQuizPage()
        {
            SeriesViewModel = new SeriesViewModel();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Check if the parameter is a list of filtered movies
            if (e.Parameter is ObservableCollection<SeriesClass> filteredSeries)
            {

                RecommendedGridView.ItemsSource = filteredSeries;
            }
        }
        private void gridCategories_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is SeriesClass s)
            {
                SeriesViewModel.SeriesClass = s;
                Frame.Navigate(typeof(SeriesInfoPage), SeriesViewModel);
            }
        }

    }
}
