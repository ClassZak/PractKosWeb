//#define MESSAGES_DEBUG_MODE


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
            if (message.Content is null)
                return BadRequest(413);
            else if(message.Content.Length >= 512 || message.Content.Length==0)
                return BadRequest(413);

            ModelsLibrary.UserModels.User? user = UserController.GetUsersList().Find(x => x.Token == message.Token);

            if(user is not null)
            {
                if(messages.Find(x=> x.Usename==user.Name) is not null)
                if((DateTime.Now-messages.Last(x=> x.Usename== user.Name).DateTime).TotalSeconds<0.75)
                    return BadRequest(413);

                messages.Add(new MessageResponse(message,user.Name, messages.Count));
                return Ok();
            }
            else
                return BadRequest();
        }
#if MESSAGES_DEBUG_MODE
        [HttpPost("ClearMessages")]
        public ActionResult Post_ClearMessages()
        {
            messages.Clear();
            return Ok();
        }
#endif
    }
}