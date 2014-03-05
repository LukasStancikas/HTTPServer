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

    public class Service
    {
        private static string DavidsCatalog = @"C:\Users\Dejv\Documents\Visual Studio 2013\Projects\HTTPServerRepository";
        private static string LukasCatalog = @"C:\Users\Lukas\Documents\Visual Studio 2013\Projects\HTTPServerRepository";
        public static readonly String RootCatalog = LukasCatalog;

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

        public Service(TcpClient Client)
        {
            _tcpClient = Client;
            requestedFile = null;
            requestedFileType = null;
            
            fullRequest = null;
          
        }
        public void doIt(int Port)
        {

            try
            {
                HTTPRequest request = new HTTPRequest();
                HTTPResponse response = new HTTPResponse();

                new ReadingRequest(_tcpClient,ref request, Port);
                //new ReadingRequest(_tcpClient, ref fullRequest, ref requestedFile, ref requestLine);
                //new HandlingRequest(requestLine, ref requestedFile, ref answer);
                new HandlingRequest(ref request, ref response);
                //new SendingResponse(_tcpClient, ref answer, RootCatalog, ref requestedFile);
                new SendingResponse(_tcpClient, ref response, RootCatalog);
            }
            catch (SocketException)
            {
                Server.Logger.Warn("Service.cs: Caught SocketException");
            }
            catch (IOException)
            {
                Server.Logger.Warn("Service.cs: Caught IOException");
            }
            finally
            {
                Server.Logger.Info("Socket connection closed with the Client");
                _tcpClient.Close();
            }
        }
    }
}
