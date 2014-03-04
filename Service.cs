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
     *      Response() - in case of FileNotFoundException should be aslo sended Error code, not OK code
     */

    internal class Service
    {
        private readonly String RootCatalog =
            @"C:\Users\Lukas\Documents\Visual Studio 2013\Projects\HTTPServer";

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
            requestLine = null;
            fullRequest = null;
        }




        private void AnalyzeFileType()
        {
            if (requestedFile.Split('.')[1].Length > 3) ;
        }
        private void AnalyzeRequest()
        {


            AnalyzeFileType();
        }
       

        public void doIt()
        {

            try
            {
               
                new ReadingRequest(_tcpClient, ref fullRequest, ref requestedFile, ref requestLine);
                new HandlingRequest(requestLine, ref requestedFile, ref answer);
                new SendingResponse(_tcpClient, ref answer, RootCatalog, ref requestedFile);
                
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
