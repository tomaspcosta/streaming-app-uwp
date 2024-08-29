
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
    public sealed partial class UserArea : Page
    {
        public UserViewModel UserViewModel { get; set; }

        public UserArea()
        {
            this.InitializeComponent();
            UserViewModel = App.UserViewModel;
            DataContext = UserViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadUserData();
            UserViewModel.Password = string.Empty;
            UserViewModel.ConfirmPassword = string.Empty;
            base.OnNavigatedTo(e);
        }

        private async void LoadUserData()
        {
            if (UserViewModel.User != null)
            {
                using (var uow = new UnitOfWork())
                {
                    var loadedUser = await uow.UserRepository.FindByUsernameAsync(
                        UserViewModel.User.Username);

                    if (loadedUser != null)
                    {
                        // Update the UserViewModel with the loaded user data
                        UserViewModel.User = loadedUser;

                        UpdateUI();
                    }
                }
            }
        }

        private void UpdateUI()
        {
            UserViewModel.Email = UserViewModel.User.Email;
            UserViewModel.Username = UserViewModel.User.Username;
            UserViewModel.PhoneNumber = UserViewModel.User.PhoneNumber;
        }



        private void LogoutButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
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
            if (!IsValidEmail(UserViewModel.Email))
            {
                DisplayErrorMessage("Please enter a valid email address.");
                return;
            }

            if (!IsValidPhoneNumber(UserViewModel.PhoneNumber))
            {
                DisplayErrorMessage("Please enter a valid phone number.");
                return;
            }

            if (UserViewModel.Password != UserViewModel.ConfirmPassword)
            {
                DisplayErrorMessage("Password and confirm password do not match.");
                return;
            }

            try
            {
                using (var uow = new UnitOfWork())
                {
                    var existingUserWithUsername =
                        await uow.UserRepository.FindByUsernameAsync(
                            UserViewModel.Username);
                    if (existingUserWithUsername != null &&
                        existingUserWithUsername.Id != UserViewModel.User.Id)
                    {
                        DisplayErrorMessage("Username already exists. Please choose a different one.");
                        return;
                    }

                    var existingUserWithEmail =
                        await uow.UserRepository.FindByEmailAsync(UserViewModel.Email);
                    if (existingUserWithEmail != null &&
                        existingUserWithEmail.Id != UserViewModel.User.Id)
                    {
                        DisplayErrorMessage("Email address is already registered. Please use a different one.");
                        return;
                    }

                    var existingUserWithPhoneNumber =
                        await uow.UserRepository.FindByPhoneNumberAsync(
                            UserViewModel.PhoneNumber);
                    if (existingUserWithPhoneNumber != null &&
                        existingUserWithPhoneNumber.Id != UserViewModel.User.Id)
                    {
                        DisplayErrorMessage("Phone number is already registered. Please use a different one.");
                        return;
                    }

                    await UserViewModel.UpdateUserInfoAsync();
                    await uow.SaveAsync();

                    DisplayErrorMessage("");

                    ShowUpdateSuccessDialog();
                }
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"An error occurred during save: {ex.Message}");
            }
        }

        private async void ShowUpdateSuccessDialog()
        {
            ContentDialog successDialog = new ContentDialog
            {
                Title = "Update Successful",
                Content = "User information has been updated successfully.",
                CloseButtonText = "OK"
            };

            await successDialog.ShowAsync();
        }

        private void DisplayErrorMessage(string message)
        {
            ErrorMessageTextBlock.Text = message;
            ErrorMessageTextBlock.Visibility = string.IsNullOrEmpty(message)
                                                   ? Visibility.Collapsed
                                                   : Visibility.Visible;
        }
    }
}
