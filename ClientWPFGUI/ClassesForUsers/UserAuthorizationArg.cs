using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPFGUI.ClassesForUsers
{
    public class UserAuthorizationArg
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token {  get; set; }


        public UserAuthorizationArg()
        {
            Name = string.Empty;
            Password = string.Empty;
            Token=GenerateToken();
        }
        public UserAuthorizationArg(string name,string password) : this()
        {
            Name = name;
            Password = password;
        }
        public UserAuthorizationArg(UserAuthorizationArg userAuthorizationArg) :
            this(userAuthorizationArg.Name,userAuthorizationArg.Password)
        {
        }



        private string GenerateToken()
        {
            Random random = new Random();
            int times = 64+random.Next(64);
            if (times == 0)
                ++times;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i != times; ++i)
                stringBuilder.Append((char)random.Next(0x10000)+1);

            return stringBuilder.ToString();
        }
    }
}
