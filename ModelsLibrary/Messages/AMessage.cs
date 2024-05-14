namespace ModelsLibrary.Messages
{
    public abstract class AMessage
    {
        public string Content { get; set; }
        public string Token { get; set; }


        public AMessage()
        {
            Content = "";
            Token = "";
        }

        public AMessage(string Content, string Username)
        {
            this.Content = Content;
            this.Token = Username;
        }
    }
}
