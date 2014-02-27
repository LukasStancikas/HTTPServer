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
            TcpListener serverSocket = new TcpListener(1234); 
            serverSocket.Start();
            while (true)
            {
                Service Service = new Service(serverSocket.AcceptTcpClient());
                Task.Factory.StartNew(() => Service.doIt());

            }

            serverSocket.Stop();
        }
    }
}
