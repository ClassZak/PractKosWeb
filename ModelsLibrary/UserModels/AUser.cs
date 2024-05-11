using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.UserModels
{
    public class AUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }


        #region Constructors
        public AUser()
        {
            Name = string.Empty;
            Password = string.Empty;
            Random random = new Random();
            int times = 64 + random.Next(64);
            if (times == 0)
                ++times;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i != times; ++i)
                stringBuilder.Append((char)random.Next(0x10000) + 1);

            Token = stringBuilder.ToString();
        }
        public AUser(string name, string password, string token) : this()
        {
            Name = name;
            Password = password;
            Token = token;
        }
        public AUser(AUser userAuthorizationArg) :
            this(userAuthorizationArg.Name, userAuthorizationArg.Password, userAuthorizationArg.Token)
        {
        }
        public AUser(AUser userAuthorizationArg, string token) : this(userAuthorizationArg)
        {
            Token = token;
        }
        #endregion
    }
}
