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

    class Server
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(8080); 
            serverSocket.Start();
            Console.WriteLine("'start' - start the server");
            Console.WriteLine("'exit' - exit the program/close the Server");
            String Command = "";
            bool Started = false;
            TcpListener closeSocket = new TcpListener(8081);

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
            closeSocket.Start();
            closeSocket.AcceptTcpClient();
            Console.WriteLine("Server is shutting down ...");
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
