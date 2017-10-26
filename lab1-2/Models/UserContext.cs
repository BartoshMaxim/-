using lab1_2.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab1_2.Models
{
    public class UserContext
    {
        private List<User> _userList = new List<User>();
        private string _pathToDb = "userdb.xml";

        public UserContext()
        {
            if (!File.Exists(_pathToDb))
            {
                _userList.Add(new User { UserId = 0, UserLogin = "ADMIN", UserPassword = "", Role = Role.Admin });
                Save();
            }
            else
            {
                Load();
                if (_userList.FirstOrDefault(x => x.UserLogin == "ADMIN") == null)
                {
                    _userList.Clear();
                    _userList.Add(new User { UserId = 0, UserLogin = "ADMIN", UserPassword = "", Role = Role.Admin });
                }
            }
        }

        private void Save()
        {
            lock (_userList)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
                using (FileStream fs = new FileStream(_pathToDb, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, _userList);
                }
            }
        }

        private void Load()
        {
            lock (_userList)
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<User>));
                using (FileStream fs = new FileStream(_pathToDb, FileMode.OpenOrCreate))
                {
                    _userList = (List<User>)formatter.Deserialize(fs);
                }
            }
        }

        public bool Register(User user)
        {
            if (user.UserPassword == CryptoHelper.CalculateMD5Hash("") && !IsExist(user.UserLogin))
            {
                user.UserId = _userList.Count;
                _userList.Add(user);
                Save();
                return true;
            }
            return false;
        }

        public User Login(string login, string password)
        {
            User user = _userList.FirstOrDefault(x => x.UserLogin == login);

            if (user != null && user.UserPassword == CryptoHelper.CalculateMD5Hash(password) && !user.IsBlock)
            {
                return user;
            }

            return user;
        }

        public bool IsExist(string login)
        {
            User user = _userList.FirstOrDefault(x => x.UserLogin == login);
            return user == null ? false : true;

        }

        public IEnumerable<User> GetList()
        {
            return _userList;
        }

        public bool ChangePasswd(string passwd)
        {
            if (PasswordHelper.CheckPasswd(passwd) || !Session.GetUser().Lock)
            {
                User currentUser = _userList.FirstOrDefault(x => x.Equals(Session.GetUser()));
                if (currentUser != null)
                {
                    currentUser.UserPassword = passwd;
                    Session.GetUser().UserPassword = passwd;
                    Save();
                    return true;
                }
            }

            return false;
        }

        public void Lock(User user)
        {
            User lockUser = _userList.First(x => x.Equals(user));
            bool state = lockUser.Lock;
            lockUser.Lock = !state;
            Save();
        }

        public void IsBlock(User user)
        {
            User lockUser = _userList.First(x => x.Equals(user));
            bool state = lockUser.IsBlock;
            lockUser.IsBlock = !state;
            Save();
        }

        public void ToTop(User user)
        {
            User lockUser = _userList.First(x => x.Equals(user));
            _userList.Remove(lockUser);
            _userList.Insert(0, lockUser);
        }
    }
}
