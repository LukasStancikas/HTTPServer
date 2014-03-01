using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
            while (Console.ReadLine() != "exit")
            {
                Service Service = new Service(serverSocket.AcceptTcpClient());
                Task.Factory.StartNew(() => Service.doIt());
                
            }
            Console.WriteLine("Server is shutting down ...");
            serverSocket.Stop();
            return;
        }
    }
}
