using ModelsLibrary;
using ModelsLibrary.Messages;
using Newtonsoft.Json;
using System.Text;


namespace Service
{
    public class Service
    {
        public List<ModelsLibrary.Messages.MessageResponse> Messages
        { get; private set; }

        public Uri Uri { get; set; }

        public Service()
        {
            Messages = new();
            Uri = new("http://localhost:0/swagger/index.html");
        }


        public Service(Uri uri) : this()
        {
            this.Uri = Uri;
        }
        public Service(string IP, int port) : this()
        {
            this.Uri = new Uri($"http://{IP}:{port}/api/messages");
        }




        public async void Post(ModelsLibrary.Messages.MessageRequest message,Uri uri)
        {
            HttpClient client = new HttpClient();
            StringContent content = new StringContent
            (
                JsonConvert.SerializeObject(message),
                Encoding.UTF8,
                "application/json"
            );


            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
        public async void Get(Uri uri)
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }




        public void Post(ModelsLibrary.Messages.MessageRequest message)
        {
            this.Post(message,this.Uri);
        }
        public void Get()
        {
            this.Get(this.Uri);
        }










    }
}
