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
        }
        public UserAuthorizationArg(string name, string password, string token) : base(name, password)
        {
        }
        public UserAuthorizationArg(UserAuthorizationArg userAuthorizationArg) :
            base(userAuthorizationArg.Name, userAuthorizationArg.Password)
        {
        }
        public UserAuthorizationArg(UserAuthorizationArg userAuthorizationArg, string token) : base(userAuthorizationArg)
        {
        }
        #endregion
    }
}
