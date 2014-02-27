using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{
    class Service
    {
        private TcpClient _tcpClient;
        public String Name;

        public TcpClient TCPClient
        {
            set { _tcpClient = value; }
            get { return _tcpClient; }
        }
        public Service(TcpClient Client, List<TcpClient> TCPList, List<Stream> StreamList)
        {
            TCPList.Add(Client);
            _tcpClient = Client;
            StreamList.Add(Client.GetStream());

        }

        public void doIt(ClientList ServerList)
        {
            try
            {
                Console.WriteLine("Client connected");
                StreamReader sr = new StreamReader(TCPClient.GetStream());
                string message = sr.ReadLine();
                Name = message;
                string answer = "";
                while (true)
                {
                    message = sr.ReadLine();
                    Console.WriteLine(Name + " said: " + message);
                    answer = Name + " said: " + message;
                }

            }
            catch (SocketException)
            {
                Console.WriteLine("Caught SocketException");
            }
            catch (IOException)
            {
                Console.WriteLine("Caught IOException");
            }
        }
    }
}
