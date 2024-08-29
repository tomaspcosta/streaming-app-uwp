using StreaminApp1.UWP.Views.Movies;
using StreaminApp1.UWP.Views.SeriesFolder;
using StreamingApp.Domain.Models;
using StreamingApp.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace StreaminApp1.UWP.Views.Favorites
{
    public sealed partial class FavouritesPage : Page
    {
        MovieViewModel MovieViewModel { get; set; }
        SeriesViewModel SeriesViewModel { get; set; }
        public FavouritesPage()
        {
            SeriesViewModel = new SeriesViewModel();
            MovieViewModel = new MovieViewModel();
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await MovieViewModel.LoadAllAsync();
            await SeriesViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private void gridCategories_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is Movie m)
            {
                MovieViewModel.Movie = m;
                Frame.Navigate(typeof(MovieInfoPage), MovieViewModel);
            }
        }

        private void gridCategories_Tapped2(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is SeriesClass s)
            {
                SeriesViewModel.SeriesClass = s;
                Frame.Navigate(typeof(SeriesInfoPage), SeriesViewModel);
            }
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
