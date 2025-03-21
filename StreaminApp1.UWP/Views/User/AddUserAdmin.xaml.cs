using StreaminApp1.UWP.Views.User;
using StreamingApp.InfraStructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace StreamingApp.UWP.Views.Users
{
    public sealed partial class AddUserAdmin : Page
    {
        private int[] ChallengeNumbers { get; set; }
        private string FormattedChallengeText { get; set; }

        public AddUserAdmin()
        {
            this.InitializeComponent();
        }

        #region Event Handlers

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTextBox.Text) ||
                string.IsNullOrEmpty(UsernameTextBox.Text) ||
                string.IsNullOrEmpty(PhoneNumberTextBox.Text) ||
                string.IsNullOrEmpty(PasswordBox.Password) ||
                string.IsNullOrEmpty(ConfirmPasswordBox.Password))
            {
                ErrorMessageTextBlock.Text = "All fields are required.";
                return;
            }

            if (!IsValidEmail(EmailTextBox.Text))
            {
                ErrorMessageTextBlock.Text = "Please enter a valid email address.";
                return;
            }

            if (!IsValidPhoneNumber(PhoneNumberTextBox.Text))
            {
                ErrorMessageTextBlock.Text = "Please enter a valid phone number.";
                return;
            }

            if (PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                ErrorMessageTextBlock.Text = "Passwords do not match.";
                return;
            }

            // Perform user registration
            try
            {
                using (var uow = new UnitOfWork())
                {
                    if (await uow.UserRepository.FindByUsernameAsync(
                            UsernameTextBox.Text) != null)
                    {
                        ErrorMessageTextBlock.Text = "Username is already registered.";
                        return;
                    }

                    if (await uow.UserRepository.FindByEmailAsync(EmailTextBox.Text) !=
                        null)
                    {
                        ErrorMessageTextBlock.Text = "Email address is already registered.";
                        return;
                    }

                    if (await uow.UserRepository.FindByPhoneNumberAsync(
                            PhoneNumberTextBox.Text) != null)
                    {
                        ErrorMessageTextBlock.Text = "Phone number is already registered.";
                        return;
                    }

                    // Create new user
                    var isAdmin = AdminCheckBox.IsChecked.HasValue && AdminCheckBox.IsChecked.Value;
                    await uow.UserRepository.CreateNormalUserAsync(
                        UsernameTextBox.Text, PasswordBox.Password, EmailTextBox.Text,
                        PhoneNumberTextBox.Text, isAdmin);

                    await ShowRegistrationSuccessMessage();

                    Frame.Navigate(typeof(UserManagePage));
                }
            }
            catch (Exception ex)
            {
                ErrorMessageTextBlock.Text = $"An error occurred during registration: {ex.Message}";
            }
        }

        private async Task<bool> ShowRegistrationSuccessMessage()
        {
            ContentDialog successDialog = new ContentDialog
            {
                Title = "Success",
                Content = "Account created successfully!",
                CloseButtonText = "OK"
            };

            ContentDialogResult result = await successDialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }

        #endregion

        #region Input Validation

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
            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\d{9}$");
        }

        #endregion

        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                RegisterButton_Click(sender, e);
            }
        }
    }
}
