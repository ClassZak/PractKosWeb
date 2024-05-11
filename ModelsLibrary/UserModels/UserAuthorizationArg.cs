using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.UserModels
{
    public class UserAuthorizationArg : AUser
    {
        #region Constructors
        public UserAuthorizationArg() : base()
        {
            Token=GenerateToken();
        }
        public UserAuthorizationArg(string name, string password, string token) : base(name, password, token)
        {
        }
        public UserAuthorizationArg(UserAuthorizationArg userAuthorizationArg) :
            base(userAuthorizationArg.Name, userAuthorizationArg.Password, userAuthorizationArg.Token)
        {
        }
        public UserAuthorizationArg(UserAuthorizationArg userAuthorizationArg, string token) : base(userAuthorizationArg)
        {
        }
        #endregion



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
