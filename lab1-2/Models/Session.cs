using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1_2.Models
{
    public static class Session
    {
        private static User User;

        private static UserContext UserContext;

        public static void Initialize()
        {
            User = new User();
            UserContext = new UserContext();
        }

        public static void Initialize(User user)
        {
            User = user;
            UserContext = new UserContext();
        }

        public static User GetUser()
        {
            return User;
        }

        public static void SetUser(User user)
        {
            User = user;
        }

        public static UserContext GetUserContext()
        {
            return UserContext;
        }
    }
}
