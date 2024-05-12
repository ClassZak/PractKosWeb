using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.UserModels;

namespace PractKosWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<ModelsLibrary.UserModels.User> _users=new();


        [HttpGet("GetList")]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_users);
        }


        [HttpPost("AddUser")]
        public ActionResult<string> Post_AddUser([FromBody]  UserAuthorizationArg user)
        {
            User newUser= new User(user);
            
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
            return Ok(!(_users.Find(x => x.Token==user.Token) is null));
        }
        [HttpPost("CheckPassword")]
        public ActionResult<bool> Post_CheckPassword([FromBody] User user)
        {
            return Ok
            (
                _users.IndexOf(user)==-1 ?
                false :
                _users.ElementAt(_users.IndexOf(user)).Password==user.Password
            );
        }
    }
}
