using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.UserModels;

namespace PractKosWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<ModelsLibrary.UserModels.User> _users = new();


        [HttpGet("GetList")]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_users);
        }

        [HttpPost("ClearUsers")]
        public ActionResult Post_ClearUsers()
        {
            _users.Clear();
            return Ok();
        }
        [HttpPost("AddUser")]
        public ActionResult<string> Post_AddUser([FromBody] UserAuthorizationArg user)
        {
            User newUser = new User(user);

            if (!_users.Contains(newUser))
            {
                while (!(_users.Find(x => x.Token == newUser.Token) is null))
                    newUser.RegenerateToken();
                _users.Add(newUser);
                return Ok(newUser.Token);
            }
            else
                return Ok("");
        }
        [HttpPost("HasUser")]
        public ActionResult<bool> Post_HasUser([FromBody] User user)
        {
            return Ok(_users.Contains(user));
        }
        [HttpPost("HasUserToken")]
        public ActionResult<bool> Post_HasUserToken([FromBody] User user)
        {
            return Ok(!(_users.Find(x => x.Token == user.Token) is null));
        }
        [HttpPost("CheckPassword")]
        public ActionResult<bool> Post_CheckPassword([FromBody] User user)
        {
            return Ok
            (
                _users.IndexOf(user) == -1 ?
                false :
                _users.ElementAt(_users.IndexOf(user)).Password == user.Password
            );
        }
        [HttpPost("Authorization")]
        public ActionResult<ModelsLibrary.UserModels.Enums.AuthorizationCode>
            Post_Authorization([FromBody] User user)
        {
            try
            {
                if (user is null)
                    return Ok(ModelsLibrary.UserModels.Enums.AuthorizationCode.WrongType);
                if(!_users.Contains(user))
                    return Ok(ModelsLibrary.UserModels.Enums.AuthorizationCode.UserNoExists);
                if (!_users.Find(x=> x.Name== user.Name).IsOnline)
                {
                    if(_users.Find(x => x.Name == user.Name).Password==user.Password)
                    {
                        _users.Find(x => x.Name == user.Name).IsOnline=true;
                        _users.Find(x => x.Name == user.Name).RegenerateToken();
                        return Ok(ModelsLibrary.UserModels.Enums.AuthorizationCode.AthorizedSuccessful);
                    }
                    else
                        return Ok(ModelsLibrary.UserModels.Enums.AuthorizationCode.WrongPassword);
                }
                else if(user.Token== _users.Find(x => x.Name == user.Name).Token)
                {
                    return Ok(ModelsLibrary.UserModels.Enums.AuthorizationCode.AthorizedSuccessful);
                }
                else
                    return Ok(ModelsLibrary.UserModels.Enums.AuthorizationCode.WrongToken);

            }
            catch
            {
                return Ok(ModelsLibrary.UserModels.Enums.AuthorizationCode.AuthorizationFailed);
            }
        }
    }
}
