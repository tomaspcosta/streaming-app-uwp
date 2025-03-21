using StreamingApp.Domain;
using StreamingApp.Domain.Models;
using StreamingApp.InfraStructure;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StreamingApp.UWP.ViewModels
{
    public class UserViewModel : BindableBase
    {
        private string _email;
        private string _username;
        private string _phoneNumber;
        private string _password;
        private string _confirmPassword;

        private bool _isNewUsernameUnique;
        private User _loggedUser;
        private bool _showError;
        private IUnitOfWork _uow;

        public UserViewModel()
        {
            User = new User();
            _uow = new UnitOfWork();
            Users = new ObservableCollection<User>();
        }
        public ObservableCollection<User> Users { get; set; }

        public User LoggedUser
        {
            get { return _loggedUser; }
            set
            {
                _loggedUser = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLogged));
                OnPropertyChanged(nameof(IsNotLogged));
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        public bool IsLogged => _loggedUser != null;

        public bool IsNotLogged => !IsLogged;

        public bool IsAdmin => LoggedUser != null && LoggedUser.IsAdmin;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { Set(ref _confirmPassword, value); }
        }

        public bool IsNewUsernameUnique
        {
            get { return _isNewUsernameUnique; }
            set
            {
                _isNewUsernameUnique = value;
                OnPropertyChanged(nameof(IsNewUsernameUnique));
            }
        }

        public bool ShowError
        {
            get { return _showError; }
            set
            {
                _showError = value;
                OnPropertyChanged(nameof(ShowError));
            }
        }

        public User _user;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                if (_user != null)
                {
                    Username = _user.Username;
                    PhoneNumber = _user.PhoneNumber;
                    Password = _user.Password;
                    Email = _user.Email;
                }
            }
        }

        internal async Task<bool> DoLoginAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var user = await uow.UserRepository.FindByUsernameAndPasswordAsync(
                    User.Username, User.Password);
                LoggedUser = user;
                ShowError = user == null;
            }
            return !ShowError;
        }
        public async Task<bool> IsNewUsernameUniqueAsync(string newUsername)
        {
            using (var uow = new UnitOfWork())
            {
                var isUnique =
                    await uow.UserRepository.IsNewUsernameUniqueAsync(newUsername);

                IsNewUsernameUnique = isUnique;

                return isUnique;
            }
        }
        public async Task LoadAllAsync()
        {
            try
            {
                var users = await _uow.UserRepository.FindAllAsync();
                Users.Clear();

                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading movies and categories: {ex.Message}");
            }
        }
        internal async void DeleteAsync()
        {
            _uow.UserRepository.Delete(User);
            Users.Remove(User);
            await _uow.SaveAsync();
        }

        private static string HashPassword(string password)
        {
            var data = Encoding.UTF8.GetBytes(password);
            var hashedData = new SHA1Managed().ComputeHash(data);
            return BitConverter.ToString(hashedData).Replace("-", "").ToUpper();
        }

        public async Task UpdateUserInfoAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var existingUser =
                    await uow.UserRepository.FindByUsernameAsync(LoggedUser.Username);

                if (existingUser != null)
                {
                    var updatedUser = new User
                    {
                        Id = LoggedUser.Id,
                        Email = Email,
                        Username = Username,
                        PhoneNumber = PhoneNumber,
                        Password = string.IsNullOrEmpty(Password) ? existingUser.Password
                                                                : HashPassword(Password),
                    };

                    await uow.UserRepository.UpdateAsync(updatedUser);
                }
            }
        }


        public async Task EditUserInfoAsync()
        {
            var existingUser = Users.FirstOrDefault(x => x.Username == Username);
            {
                if (User != null)
                {
                    User.Email = Email;
                    User.Username = Username;
                    User.PhoneNumber = PhoneNumber;
                    _uow.UserRepository.Update(User);

                }
            }
            await _uow.SaveAsync();

        }
    }
}


