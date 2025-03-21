using StreamingApp.UWP.ViewModels;
using StreamingApp.Domain.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace StreamingApp.UWP.Views.Categories
{
    public sealed partial class ManageCategoriesPage : Page
    {
        public CategoryViewModel CategoryViewModel { get; set; }
        public ManageCategoriesPage()
        {
            this.InitializeComponent();
            CategoryViewModel = new CategoryViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CategoryViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        private void gridCategories_Tapped(
            object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        { }

        private void EditButton_Click(object sender,
                                      Windows.UI.Xaml.RoutedEventArgs e)
        {
            // var fe=(FrameworkElement)sender;
            // var c=(Category)fe.DataContext;

            if (sender is FrameworkElement fe && fe.DataContext is Category c)
            {
                CategoryViewModel.Category = c;
                Frame.Navigate(typeof(CategoryFormPage), CategoryViewModel);
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
                if (sender is FrameworkElement fe && fe.DataContext is Category c)
                {
                    CategoryViewModel.Category = c;
                    CategoryViewModel.DeleteAsync();
                }
            }
        }
        private void btnAdd_Click(object sender,
                                  Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CategoryFormPage));
        }
    }
}
