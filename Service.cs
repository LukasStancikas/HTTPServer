using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{
    /**
     *  TODO: 
     *      
     */

    internal class Service
    {
        private static string DavidsCatalog = @"C:\Users\Dejv\Documents\Visual Studio 2013\Projects\HTTPServerRepository";
        private static string LukasCatalog = @"C:\Users\Lukas\Documents\Visual Studio 2013\Projects\HTTPServerRepository";
        private readonly String RootCatalog = DavidsCatalog;

        private StreamReader reader;
        private StreamWriter writer;
        private TcpClient _tcpClient;
        private String requestedFile;
        private String requestedFileType;
        private String requestLine;
        private String fullRequest;
        private String answer;
        public TcpClient TCPClient
        {
            set { _tcpClient = value; }
            get { return _tcpClient; }
        }

        public Service(TcpClient Client, int Port)
        {
            _tcpClient = Client;
            requestedFile = null;
            requestedFileType = null;
            
            fullRequest = null;
            if (Port == Server.DefaultPort)
            {
                requestLine = null;
            }
            if (Port == Server.ShutdownPort)
            {
                requestLine = "GET /Shutdown.html HTTP/1.1";
            }
        }
        public void doIt()
        {

            try
            {
                HTTPRequest request = new HTTPRequest();
                HTTPResponse response = new HTTPResponse();

                new ReadingRequest(_tcpClient,ref request);
                //new ReadingRequest(_tcpClient, ref fullRequest, ref requestedFile, ref requestLine);
                //new HandlingRequest(requestLine, ref requestedFile, ref answer);
                new HandlingRequest(ref request, ref response);
                //new SendingResponse(_tcpClient, ref answer, RootCatalog, ref requestedFile);
                new SendingResponse(_tcpClient, ref response, RootCatalog);

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
