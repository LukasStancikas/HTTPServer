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
        private readonly String RootCatalog = @"C:\Users\Lukas\Documents\Visual Studio 2013\Projects\HTTPProject\HTTPProject\";

        public TcpClient TCPClient
        {
            set { _tcpClient = value; }
            get { return _tcpClient; }
        }
        public Service(TcpClient Client)
        {
        
            _tcpClient = Client;
        

        }

        public void doIt()
        {
            try
            {
                Console.WriteLine("Client connected");
                StreamReader sr = new StreamReader(TCPClient.GetStream());
                string answer = "";
                string message="";
                message = sr.ReadLine();
                String RequestDirectory = message.Split(' ')[1];
                Console.WriteLine(message);
                answer =
                    "HTTP/1.0 200 OK \r\n" +
                    "\r\n";
               
              
                using (StreamReader FileReader = new StreamReader(RootCatalog + RequestDirectory))
                {
                    
                    string Body = FileReader.ReadLine();
                    answer += Body;
                }
              
                StreamWriter sw = new StreamWriter(TCPClient.GetStream());
                sw.AutoFlush = true;
                sw.WriteLine(answer);
                     
                
                _tcpClient.Close();

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
