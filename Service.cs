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
        private static readonly String DavidsCatalog =
            @"C:\Users\Dejv\Documents\Visual Studio 2013\Projects\HTTPServer\HTTPServer\";
        private static readonly String LukasCatalog =
            @"C:\Users\Lukas\Documents\Visual Studio 2013\Projects\HTTPProject\HTTPServer";
        private String RootCatalog = DavidsCatalog;

        private StreamReader reader;
        private StreamWriter writer;
        private TcpClient _tcpClient;
        private string requestedFile;
        private string requestedFileType;
        private string requestLine;
        private string fullRequest;
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
            requestedFileType = null;
            requestLine = null;
            fullRequest = null;
        }

        private void ReadRequest()
        {
            string line = "";
            fullRequest = "";
            line = reader.ReadLine();
            requestLine = line;
            requestedFile = line.Split(' ')[1];
            fullRequest += line + "\r\n";
            do
            {
                line = reader.ReadLine();
                fullRequest += line + "\r\n";

            } while (line.Length != 0);
        
           
        }

        private string NameToType(string fileName)
        {
            string[,] array2Db = new string[,]
            {
                {"html", "text/html"},
                {"htm", "text/html"},
                {"doc", "application/msword"},
                {"gif", "image/gif"},//gif 	image/gif
                {"jpg", "image/jpeg"},//jpg 	image/jpeg
                {"pdf", "application/pdf"},//pdf 	application/pdf
                {"css", "text/css"},//css 	text/css
                {"xml", "text/xml"},//xml 	text/xml  	
                {"jar", "application/x-java-archive"},//jar application/x-java-archive
            };
            string fileType = fileName.Substring('.');

            for (int i = 0; i < array2Db.Length; i++)
            {
                if (array2Db[i,0] == fileType)
                {
                    return array2Db[i, 1];
                }
            }
            return null;
        }

        private void AnalyzeFileType()
        {
            if (requestedFile.Split('.')[1].Length>3);
        }
        private void AnalyzeRequest()
        {
           

            AnalyzeFileType();
        }
        private void Response()
        {
            string answer =
                "HTTP/1.0 200 OK\r\n" +
                "\r\n";
            if (Analize() != null)
            {
                answer = Analize();
            }
            try
            {
                using (StreamReader FileReader = new StreamReader(RootCatalog + requestedFile))
                {
                    string Body = FileReader.ReadToEnd();
                    answer += Body;
                }
            }
            catch (FileNotFoundException ex)
            {
                requestedFile = "/page_not_found.html";
                answer =    "HTTP/1.0 404 Not Found\r\n" +
                            "\r\n";
                using (StreamReader FileReader = new StreamReader(RootCatalog + requestedFile))
                {
                    string Body = FileReader.ReadLine();
                    answer += Body;
                }
            }
            writer.AutoFlush = true;
            writer.WriteLine(answer);
        }
        public string Analize()
        {
            string answer = null;
            String[] Analizable = requestLine.Split(' ');

            if ((Analizable[0].Equals("GET")) || (Analizable[0].Equals("HEAD")) || (Analizable[0].Equals("POST")))
            {
            }
            else
            {
                answer = "HTTP/1.0 400 Illegal request\r\n" +
                         "\r\n";
                requestedFile = "/Illegal_Request.html";
            }
            if ((Analizable[2].Equals("HTTP/1.0")) || (Analizable[2].Equals("HTTP/1.1")))
            {
            }
            else
            {
                answer = "HTTP/1.0 400 Illegal protocol\r\n" +
                         "\r\n";
                requestedFile = "/Illegal_Protocol.html";
            }
            String temp = Analizable[2].Substring(0, Analizable[2].Length - 1);
            if ((temp.Equals("HTTP/1.")) || (temp.Equals("HTTP/1.")))
            {
            }
            else
            {
                answer = "HTTP/1.0 400 Illegal request\r\n" +
                         "\r\n";
                requestedFile = "/Illegal_Request.html";
            }
           

            if (requestedFile == "/")
            {
                requestedFile = "/index.html";
            }
            return answer;
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
