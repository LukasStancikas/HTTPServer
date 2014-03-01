using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{   
    /**
     *  TODO: 
     *      Response() - in case of FileNotFoundException should be aslo sended Error code, not OK code
     */
   
    internal class Service
    {
        private readonly String RootCatalog =
            @"C:\Users\Dejv\Documents\Visual Studio 2013\Projects\HTTPServer\HTTPServer\";

        private StreamReader reader;
        private StreamWriter writer;
        private TcpClient _tcpClient;
        private string requestedFile;

        public TcpClient TCPClient
        {
            set { _tcpClient = value; }
            get { return _tcpClient; }
        }

        public Service(TcpClient Client)
        {
            _tcpClient = Client;
            reader = new StreamReader(Client.GetStream());
            writer = new StreamWriter(Client.GetStream());
            requestedFile = null;
        }

        private void ReadRequest()
        {
            string line = "";
            do
            {
                line = reader.ReadLine();
                if (requestedFile == null)
                    requestedFile = line.Split(' ')[1];
            } while (line.Length != 0);
        }

        private void Response()
        {
            string answer =
                "HTTP/1.0 200 OK \r\n" +
                "\r\n";
            if (requestedFile == "/")
            {
                requestedFile = "/index.html";
            }
            try
            {
                using (StreamReader FileReader = new StreamReader(RootCatalog + requestedFile))
                {
                    string Body = FileReader.ReadLine();
                    answer += Body;
                }
            }
            catch (FileNotFoundException ex)
            {
                requestedFile = "/page_not_found.html";
                using (StreamReader FileReader = new StreamReader(RootCatalog + requestedFile))
                {
                    string Body = FileReader.ReadLine();
                    answer += Body;
                }
            }
            writer.AutoFlush = true;
            writer.WriteLine(answer);
        }

    public void doIt()
        {

        try
        {
            ReadRequest();
            Response();
            Console.WriteLine("Request:" + requestedFile);
        }
        catch (SocketException)
        {
            Console.WriteLine("Caught SocketException");
        }
        catch (IOException)
        {
            Console.WriteLine("Caught IOException");
        }
        finally
        {
            _tcpClient.Close();
        }
        }
    }
}
