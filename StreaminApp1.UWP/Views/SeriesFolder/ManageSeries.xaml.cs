using StreamingApp.UWP.ViewModels;
using StreamingApp.UWP.Views.Series;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP.Views.Serie
{
    public sealed partial class ManageSeriesPage : Page
    {
        public SeriesViewModel SeriesViewModel { get; set; }

        public ManageSeriesPage()
        {
            this.InitializeComponent();
            SeriesViewModel = new SeriesViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await SeriesViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private void gridCategories_Tapped(
            object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        { }

        private void EditButton_Click(object sender,
                                      Windows.UI.Xaml.RoutedEventArgs e)
        {
            // var fe=(FrameworkElement)sender;
            // var c=(Series)fe.DataContext;

            if (sender is FrameworkElement fe &&
                fe.DataContext is StreamingApp.Domain.Models.SeriesClass c)
            {
                SeriesViewModel.SeriesClass = c;
                Frame.Navigate(typeof(SeriesFormPage), SeriesViewModel);
            }
        }

        private async void DeleteButton_Click(object sender,
                                              Windows.UI.Xaml.RoutedEventArgs e)
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
                if (sender is FrameworkElement fe &&
                    fe.DataContext is StreamingApp.Domain.Models.SeriesClass c)
                {
                    SeriesViewModel.SeriesClass = c;
                    // SeriesViewModel.DeleteAsync();
                }
            }
        }
        private void btnAdd_Click(object sender,
                                  Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SeriesFormPage));
        }
    }

}
