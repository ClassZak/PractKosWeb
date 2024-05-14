using Microsoft.AspNetCore.Mvc;
using ModelsLibrary.Messages;
using PractKosWeb.Controllers;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        public static List<MessageResponse> messages = new ();
        // GET: api/messages 
        [HttpGet]
        public ActionResult<IEnumerable<MessageResponse>> Get()
        {
            return Ok(messages);
        }
        // POST: api/messages 
        [HttpPost]
        public ActionResult Post([FromBody] MessageRequest message)
        {
            ModelsLibrary.UserModels.User? user = UserController.GetUsersList().Find(x => x.Token == message.Token);

            string? UserName = user is not null ? user.Name : null;

            if(user is not null)
            {
                messages.Add(new MessageResponse(message,user.Name, messages.Count));
                return Ok();
            }
            else
                return BadRequest();
        }
        [HttpPost("ClearMessages")]
        public ActionResult Post_ClearMessages()
        {
            messages.Clear();
            return Ok();
        }
    }
}