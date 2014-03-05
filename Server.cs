using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
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
        private TcpListener serverSocket;
        private TcpListener closeSocket;
        private Task[] Tasks;
        public Server()
            : this(DefaultPort, ShutdownPort)
        {
        }
        public Server(int listeningPort,int closingPort)
        {
            serverSocket = new TcpListener(DefaultPort);
            closeSocket = new TcpListener(ShutdownPort);
            serverSocket.Start();


            //Console.WriteLine("'start' - start the server");
            Console.WriteLine("To exit connect to 8081 port");
            String Command = "";
            bool ShouldClose = false;
            Tasks = new Task[2];

            Tasks[0] = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Server Started");
                while (ShouldClose == false)
                {
                    Service WebService = new Service(serverSocket.AcceptTcpClient(), DefaultPort);
                    Task.Factory.StartNew(() => WebService.doIt());
                }
            });

            closeSocket.Start();

            Tasks[1] = Task.Factory.StartNew(() =>
            {
                Service ShuttingService = new Service(closeSocket.AcceptTcpClient(), ShutdownPort);
                Task.Factory.StartNew(() => ShuttingService.doIt());
                ShouldClose = true;
                Console.WriteLine("Server is shutting down ...");
            });
            StopServer();
        }

        public void StopServer()
        {
            Task.WaitAll(Tasks);
            //Tasks[0].Dispose();
            serverSocket.Stop();
            closeSocket.Stop();
            
        }
        public static void Main(string[] args)
        {
            new Server();
        }
    }
}
