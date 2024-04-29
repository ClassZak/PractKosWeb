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




        public async void Post(ModelsLibrary.Messages.MessageRequest message, Uri uri)
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                throw;
            }
        }
        public async void Get(Uri uri)
        {
            await Task.Run(() =>
            {
                HttpClient client = new HttpClient();
                try
                {
                    HttpResponseMessage response = client.GetAsync(uri).Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    
                    try
                    {
                        Messages = JsonConvert.DeserializeObject<List<MessageResponse>>(responseBody);
                        if(Messages is null || Messages.Count==0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Не удалось создать список сообщений");
                            Console.ResetColor();
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    
                    Console.WriteLine(responseBody);
                    foreach(var i in  Messages)
                    {
                        Console.WriteLine($"{i.Content}\n");
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    throw;
                }
            });
        }




        public void Post(ModelsLibrary.Messages.MessageRequest message)
        {
            this.Post(message, this.Uri);
        }
        public void Get()
        {
            this.Get(this.Uri);
        }
    }
}