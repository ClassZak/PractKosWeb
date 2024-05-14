namespace ModelsLibrary.Messages
{
    public class MessageRequest : AMessage
    {
        public string Token {  get; set; }

        public MessageRequest():base()
        {
            Token = "";
        }
        public MessageRequest(string Content,string Token) : base(Content)
        {
            this.Token = Token;
        }
    }
}
