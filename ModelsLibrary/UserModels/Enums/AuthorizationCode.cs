using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.UserModels.Enums
{
    public enum AuthorizationCode
    {
        AuthorizationFailed=-2,
        WrongType =-1,
        AthorizedSuccessful,
        UserNoExists,
        WrongToken,
        WrongPassword
    }
}
