using StreamingApp.Domain.Models;
using StreamingApp.UWP.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP.Views.Movies
{
    public sealed partial class MovieManage : Page
    {

        public MovieViewModel MovieViewModel { get; set; }
        public MovieManage()
        {
            this.InitializeComponent();
            MovieViewModel = new MovieViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await MovieViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private void gridCategories_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // var fe=(FrameworkElement)sender;
            //var c=(Category)fe.DataContext;

            if (sender is FrameworkElement fe && fe.DataContext is Movie m)
            {
                MovieViewModel.Movie = m;
                Frame.Navigate(typeof(MovieForm), MovieViewModel);
            }

        }

        private async void DeleteButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var deleteDialog = new ContentDialog
            {
                Title = "Delete",
                Content = "Are you sure you want to delete?",
                PrimaryButtonText = "Delete",
                SecondaryButtonText = "Cancel"
            };
            var result = await deleteDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (sender is FrameworkElement fe && fe.DataContext is Movie m)
                {
                    MovieViewModel.Movie = m;
                    MovieViewModel.DeleteAsync();
                }
            }
        }
        private void btnAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MovieForm));
        }

    }
}
