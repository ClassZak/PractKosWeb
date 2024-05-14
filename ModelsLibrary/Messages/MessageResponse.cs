namespace ModelsLibrary.Messages
{
    public class MessageResponse : AMessage
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Usename { get; set; }


        public MessageResponse() : base()
        {
            DateTime= DateTime.Now;
            Usename = "";
        }


        public MessageResponse(MessageRequest messageRequest)
            : base(messageRequest.Content)
        {
            DateTime = DateTime.Now;
            Usename = "";
        }
        public MessageResponse(MessageRequest messageRequest,string username):this(messageRequest)
        {
            Usename = username;
        }
        public MessageResponse(MessageRequest messageRequest, string username,int id) : this(messageRequest,username)
        {
            this.Id = id;
        }


        public MessageResponse(MessageRequest messageRequest,int id): this(messageRequest)
        {
            this.Id = id;
        }
    }
}
