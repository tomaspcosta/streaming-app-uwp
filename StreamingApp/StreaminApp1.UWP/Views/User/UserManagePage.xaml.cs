using StreamingApp.UWP.ViewModels;
using StreamingApp.UWP.Views.Users;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;


namespace StreaminApp1.UWP.Views.User
{
    public sealed partial class UserManagePage : Page
    {
        public UserViewModel UserViewModel { get; set; }
        public UserManagePage()
        {
            this.InitializeComponent();
            UserViewModel = new UserViewModel();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await UserViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }
        private void EditButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // var fe=(FrameworkElement)sender;
            //var c=(Category)fe.DataContext;

            if (sender is FrameworkElement fe && fe.DataContext is StreamingApp.Domain.Models.User u)
            {
                UserViewModel.User = u;
                Frame.Navigate(typeof(EditUserAdmin), UserViewModel);
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
                if (sender is FrameworkElement fe && fe.DataContext is StreamingApp.Domain.Models.User u)
                {
                    UserViewModel.User = u;
                    UserViewModel.DeleteAsync();
                }
            }
        }
        private void btnAdd_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUserAdmin));
        }



    }
}
