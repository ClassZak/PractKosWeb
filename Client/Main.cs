using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string IP="";
            int port;


            
            while(true)
            {
                try
                {
                    Console.WriteLine("Введите IP и порт сервера для запросов:");

                    IP = Console.ReadLine();
                    if (IP is null)
                        throw new Exception();
                    port=Convert.ToInt32(Console.ReadLine());


                    break;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Нверный формат для порта");
                    Console.ResetColor();

                    Console.WriteLine("Для повторного ввода нажмите любую клавишу . . .");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }



            Service.Service service=new Service.Service(IP,port);



            string message="";
            string username = "";
            while(true)
            {
                Console.Write("Введите имя пользователя->");
                username=Console.ReadLine();

                Console.Write("Введите текст сообщения->");
                message = Console.ReadLine();



                service.Post(new ModelsLibrary.Messages.MessageRequest(message, username));
                Thread.Sleep(5000);
                service.Get();
            }

        }
    }
}
