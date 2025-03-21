using StreamingApp.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace StreamingApp.Domain.Models
{
    public class User : Entity
    {
        private string _password;

        public string Username { get; set; }

        public string Password
        {
            get { return _password; }
            set
            {
                if (!IsPasswordHashed(value) || NeedsRehash(value))
                {
                    var data = Encoding.UTF8.GetBytes(value);
                    var hashedData = new SHA1Managed().ComputeHash(data);
                    _password = BitConverter.ToString(hashedData).Replace("-", "").ToUpper();
                }
                else
                {
                    _password = value;
                }
            }
        }

        private bool IsPasswordHashed(string password)
        {
            return password.Length == 40 && !password.Contains(" ");
        }

        private bool NeedsRehash(string password)
        {
            return false;
        }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        public List<Favourites> UserFavourites { get; set; }
        public byte[] Avatar { get; set; }
    }
}
