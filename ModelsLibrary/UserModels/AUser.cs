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


        #region Constructors
        public AUser()
        {
            Name = string.Empty;
            Password = string.Empty;
        }
        public AUser(string name, string password) : this()
        {
            Name = name;
            Password = password;
        }
        public AUser(AUser userAuthorizationArg) :
            this(userAuthorizationArg.Name, userAuthorizationArg.Password)
        {
        }
        #endregion
    }
}
