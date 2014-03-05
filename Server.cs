using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace HTTPProject
{

    public class Server
    {
        public const int DefaultPort = 8080;
        public const int ShutdownPort = 8081;
        private TcpListener serverSocket;
        private TcpListener closeSocket;
        private Task[] Tasks;
        public static readonly ILog Logger = LogManager.GetLogger(typeof(Server));
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
            bool ShouldClose = false;
            Tasks = new Task[2];

            Tasks[0] = Task.Factory.StartNew(() =>
            {
                Logger.Info("Server Started");
                while (ShouldClose == false)
                {
                    Service WebService = new Service(serverSocket.AcceptTcpClient());
                    Logger.Info("Client connected to port:" + DefaultPort);
                    Task.Factory.StartNew(() => WebService.doIt(DefaultPort));
                    Logger.Info("Handling End. Client with port: " + DefaultPort);
                }
            });

            closeSocket.Start();

            Tasks[1] = Task.Factory.StartNew(() =>
            {
                Service ShuttingService = new Service(closeSocket.AcceptTcpClient());
                Logger.Info("Client connected to port:" + ShutdownPort);
                Task.Factory.StartNew(() => ShuttingService.doIt(ShutdownPort));

                ShouldClose = true;
                Logger.Info("Server starting shutting down. Reason: Connection to port:" + ShutdownPort);
                Thread.Sleep(1000);
            });
            StopServer();
        }

        public void StopServer()
        {
            Task.WaitAny(Tasks);
            serverSocket.Stop();
            closeSocket.Stop();
            Logger.Info("Server fully shutdown\r\n --------------------------------------------------------------------");
        }
        public static void Main(string[] args)
        {
            FileInfo configFile = new FileInfo(@"..\..\logconfig.xml");
            XmlConfigurator.Configure(configFile);
            new Server();
           
        }
    }
}
