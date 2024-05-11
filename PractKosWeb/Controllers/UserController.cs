using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.UserModels;

namespace PractKosWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<ModelsLibrary.UserModels.User> _users=new();


        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_users);
        }


        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            _users.Add(user);
            return Ok();
        }
    }
}
