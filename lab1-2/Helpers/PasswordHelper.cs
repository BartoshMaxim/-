using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab1_2.Helpers
{
    public static class PasswordHelper
    {
        public static bool CheckPasswd(string passwd)
        {
            Regex rgx = new Regex(@"(?=.*[0-9])(?=.*[a-zA-Z]).*");
            return rgx.IsMatch(passwd);
        }
    }
}
