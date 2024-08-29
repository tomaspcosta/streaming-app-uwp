using StreaminApp1.UWP.Views.Movies;
using StreaminApp1.UWP.Views.SeriesFolder;
using StreamingApp.Domain.Models;
using StreamingApp.UWP.Converters;
using StreamingApp.UWP.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP.Views.Home
{
    public sealed partial class HomePage : Page
    {
        public MovieViewModel MovieViewModel { get; set; }
        public SeriesViewModel SeriesViewModel { get; set; }
        static ByteToImageConverter Converter { get; set; }
        public UserViewModel UserViewModel { get; set; }
        public HomePage()
        {
            MovieViewModel = new MovieViewModel();
            SeriesViewModel = new SeriesViewModel();
            Converter = new ByteToImageConverter();
            UserViewModel = App.UserViewModel;
            this.InitializeComponent();

        }


        private void gridCategories_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Movie m)
            {
                MovieViewModel.Movie = m;
                if (UserViewModel.IsAdmin)
                {
                    Frame.Navigate(typeof(MovieInfoPageAdmin), MovieViewModel);

                }
                else
                {
                    Frame.Navigate(typeof(MovieInfoPage), MovieViewModel);
                }
            }
        }

        private void gridSeriesCategories_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is SeriesClass s)
            {
                SeriesViewModel.SeriesClass = s;

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

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await MovieViewModel.LoadAllAsync();
            await SeriesViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }




    }
}
