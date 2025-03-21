
using StreamingApp.UWP.Converters;
using StreamingApp.UWP.ViewModels;
using StreamingApp.UWP.Views.SeriesFolder;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP.Views.Series
{
    public sealed partial class SeriesFormPage : Page
    {
        public SeriesViewModel SeriesViewModel { get; set; }
        static ByteToImageConverter Converter { get; set; }

        public SeriesFormPage()
        {
            this.InitializeComponent();
            SeriesViewModel = new SeriesViewModel();
            Converter = new ByteToImageConverter();
            LoadCategories();
        }
        private async void LoadCategories()
        {
            await SeriesViewModel.LoadAllCategoriesAsync();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                SeriesViewModel = e.Parameter as SeriesViewModel;
            }
            base.OnNavigatedFrom(e);
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void AddSeason_Click(object sender, RoutedEventArgs e)
        {
            // Save the series before navigating to SeasonFormPage
            if (await SeriesViewModel.CreateOrUpdateSerieAsync())
            {
                Frame.Navigate(typeof(SeasonFormPage), SeriesViewModel);
            }
            else
            {

            }
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            await SeriesViewModel.CreateOrUpdateSerieAsync();
            Frame.GoBack();
        }
    }
}
