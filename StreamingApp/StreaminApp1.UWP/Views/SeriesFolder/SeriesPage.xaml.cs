
using StreamingApp.UWP.ViewModels;
using StreamingApp.Domain.Models;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

using StreamingApp.UWP.Converters;
using StreaminApp1.UWP.Views.Movies;
using Windows.UI.Xaml;
using StreaminApp1.UWP.Views.SeriesFolder;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP.Views.Serie
{
    public sealed partial class SeriesPage : Page
    {
        public SeriesViewModel SeriesViewModel { get; set; }
        static ByteToImageConverter Converter { get; set; }

        public UserViewModel UserViewModel { get; set; }

        public SeriesPage()
        {
            this.InitializeComponent();
            SeriesViewModel = new SeriesViewModel();
            Converter = new ByteToImageConverter();
            UserViewModel = App.UserViewModel;
            LoadCategories();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await SeriesViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }
        private async void LoadCategories()
        {
            await SeriesViewModel.LoadAllCategoriesAsync();
        }

        private void gridCategories_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is SeriesClass s)
            {
                SeriesViewModel.SeriesClass = s;
                Frame.Navigate(typeof(SeriesInfoPage), SeriesViewModel);

                if (UserViewModel.IsAdmin)
                {
                    Frame.Navigate(typeof(SeriesInfoPageAdmin), SeriesViewModel);

                }
                else
                {
                    Frame.Navigate(typeof(SeriesInfoPage), SeriesViewModel);
                }
            }
        }

        private async void categoryComboBox_SelectionChanged(
            object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                SeriesViewModel.SelectedCategory = comboBox.SelectedItem as Category;

                // Check if the SelectedCategory is not null
                if (SeriesViewModel.SelectedCategory != null)
                {
                    await SeriesViewModel.LoadAllAsyncByCategory(SeriesViewModel.SelectedCategory);
                }
            }
        }
    }
}
