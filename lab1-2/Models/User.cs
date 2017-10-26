using lab1_2.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace lab1_2.Models
{
    [Serializable]
    public class User : IEquatable<User>
    {
        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public Role Role { get; set; }

        public bool IsBlock { get; set; }

        public bool Lock { get; set; }

        private string _userPassword;

        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = CryptoHelper.CalculateMD5Hash(value);
            }
        }

        public User()
        {
            Role = Role.User;
            IsBlock = false;
            Lock = true;
        }

        public override bool Equals(object obj)
        {
            User user = obj as User;
            if (user != null)
            {
                return Equals(user);
            }
            return false;
        }

        public bool Equals(User other)
        {
            if (UserId.Equals(other.UserId) && UserLogin.Equals(other.UserLogin) 
                && UserPassword.Equals(other.UserPassword) && Role.Equals(other.Role) 
                && IsBlock.Equals(other.IsBlock) && Lock.Equals(other.Lock))
                return true;

            return false;
        }
    }
}
