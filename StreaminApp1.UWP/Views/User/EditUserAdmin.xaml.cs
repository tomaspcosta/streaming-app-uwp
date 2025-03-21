
using StreamingApp.InfraStructure;
using StreamingApp.UWP.Converters;
using StreamingApp.UWP.ViewModels;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StreamingApp.UWP.Views.Users
{
    public sealed partial class EditUserAdmin : Page
    {
        public UserViewModel UserViewModel { get; set; }
        static ByteToImageConverter Converter { get; set; }

        public EditUserAdmin()
        {
            this.InitializeComponent();
            UserViewModel = App.UserViewModel;
            DataContext = UserViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                UserViewModel = e.Parameter as UserViewModel;
            }
            base.OnNavigatedFrom(e);
        }

        private void UpdateUI()
        {
            UserViewModel.Email = UserViewModel.User.Email;
            UserViewModel.Username = UserViewModel.User.Username;
            UserViewModel.PhoneNumber = UserViewModel.User.PhoneNumber;
        }



        private void LogoutButton_Click(object sender,
                                        Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Handle the logout button click
            UserViewModel.User = null;

            // Clear the navigation history and navigate to the login page
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame?.BackStack.Clear();
            rootFrame?.Navigate(typeof(LoginPage));
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);

                List<string> allowedDomains =
                    new List<string> { "gmail.com", "yahoo.com", "hotmail.com", "live", "ipb" };

                if (addr.Address == email && addr.Host.Contains(".") &&
                    allowedDomains.Contains(addr.Host.ToLower()) &&
                    addr.User.Length <= 64 && addr.Host.Length <= 255)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber,
                                                                @"^\d{9}$");
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            await UserViewModel.EditUserInfoAsync();
            Frame.GoBack();
        }

    }
}

