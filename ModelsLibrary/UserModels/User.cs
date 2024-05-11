using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.UserModels
{
    public class User : AUser
    {
        #region Constructors
        public User() : base()
        {
        }
        public User(string name, string password, string token) : base(name,password,token)
        {
        }
        public User(User userAuthorizationArg) :
            base(userAuthorizationArg.Name, userAuthorizationArg.Password,userAuthorizationArg.Token)
        {
        }
        public User(User userAuthorizationArg,string token) : base(userAuthorizationArg)
        {
        }
        #endregion
    }
}
