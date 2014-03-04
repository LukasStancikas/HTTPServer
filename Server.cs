using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{

    public class Server
    {
        public const int DefaultPort = 8080;
        public const int ShutdownPort = 8081;
        static void Main(string[] args)
        {
            
            TcpListener serverSocket = new TcpListener(DefaultPort);
            TcpListener closeSocket = new TcpListener(ShutdownPort);
            serverSocket.Start();
         
            
            //Console.WriteLine("'start' - start the server");
            Console.WriteLine("To exit connect to 8081 port");
            String Command = "";
            bool ShouldClose = false;
           
            Task[] Tasks = new Task[2];
            Tasks[0] = Task.Factory.StartNew(() => 
            {
               
                Console.WriteLine("Server Started");
                while (ShouldClose == false)
                {
                    Service Service = new Service(serverSocket.AcceptTcpClient(),DefaultPort);
                    Task.Factory.StartNew(() => Service.doIt());
                }

            });
            closeSocket.Start();
            Tasks[1] = Task.Factory.StartNew(() =>
            {
                Service Service2 = new Service(closeSocket.AcceptTcpClient(), ShutdownPort);
                Task.Factory.StartNew(() => Service2.doIt());
                ShouldClose = true;
                Console.WriteLine("Server is shutting down ...");
            });
            Task.WaitAll(Tasks);
            closeSocket.Stop();
            serverSocket.Stop();
            /*
            while (Command.ToLower() != "exit")
            {
                Command = Console.ReadLine().ToLower();
                switch (Command.ToLower())
                {
                    case "start":
                        if (Started == false)
                        {
                            Task.Factory.StartNew(() =>
                            {
                                Started = true;
                                Console.WriteLine("Server Started");
                                while (true)
                                {
                                    Service Service = new Service(serverSocket.AcceptTcpClient());
                                    Task.Factory.StartNew(() => Service.doIt());
                                }

                            });
                        }
                        else { Console.WriteLine("Server already Started"); }
                        break;
                    case "exit":
                        break;
                    default:
                        Console.WriteLine("Wrong Command");
                        break;


                }
               
            }*/


            return;
        }
    }
}
