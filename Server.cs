using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{
    public class ClientList
    {
        public List<TcpClient> TCPList = new List<TcpClient>();
        public List<Stream> StreamList = new List<Stream>();
    }
    class Server
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(80);
            ClientList ServerList = new ClientList();
            serverSocket.Start();
            while (true)
            {

                //Console.WriteLine("Initiated");
                Service Service = new Service(serverSocket.AcceptTcpClient(), ServerList.TCPList, ServerList.StreamList);
                Task.Factory.StartNew(() => Service.doIt(ServerList));
            }

            serverSocket.Stop();
        }
    }
}
