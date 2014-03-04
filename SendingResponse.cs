using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{
    class SendingResponse
    {


        public SendingResponse(TcpClient Client, ref String _answer, String RootCatalog, ref String requestedFile)
        {
            StreamWriter writer = new StreamWriter(Client.GetStream());
            FileStream TempStream = null;
            string answer =
                "HTTP/1.0 200 OK\r\n" +
                "\r\n";
            if (answer != null)
            {
                _answer = answer;
            }

            try
            {
                using (FileStream FileReader = new FileStream(RootCatalog + requestedFile, FileMode.Open))
                {


                    FileReader.CopyTo(TempStream);
                }
            }
            catch (DirectoryNotFoundException e)
            {
                requestedFile = "/directory_not_found.html";
                answer = "HTTP/1.0 404 Not Found\r\n" +
                         "\r\n";
                using (FileStream FileReader = new FileStream(RootCatalog + requestedFile, FileMode.Open))
                {

                    FileReader.CopyTo(TempStream);
                }
            }
            catch (FileNotFoundException ex)
            {
                requestedFile = "/page_not_found.html";
                answer = "HTTP/1.0 404 Not Found\r\n" +
                         "\r\n";
                using (FileStream FileReader = new FileStream(RootCatalog + requestedFile, FileMode.Open))
                {

                    FileReader.CopyTo(TempStream);
                }
            }

            writer.AutoFlush = true;
            writer.Write(answer);

            writer.WriteLine(TempStream);

        }
    }
}
