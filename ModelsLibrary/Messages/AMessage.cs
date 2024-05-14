namespace ModelsLibrary.Messages
{
    public abstract class AMessage
    {
        public string Content { get; set; }


        public AMessage()
        {
            Content = "";
        }

        public AMessage(string Content)
        {
            this.Content = Content;
        }
    }
}
