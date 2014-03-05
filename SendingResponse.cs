using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{
    class SendingResponse
    {
        public SendingResponse(TcpClient Client, ref HTTPResponse Response, String RootCatalog)
        {

            NetworkStream Stream = Client.GetStream();
            StreamWriter writer = new StreamWriter(Stream);
            FileStream TempStream = null;

            try
            {
                TempStream = new FileStream(RootCatalog + Response.File, FileMode.Open);
            }
            catch (DirectoryNotFoundException e)
            {
                Response = new HTTPResponse("HTTP/1.0", 404, "Not Found","/directory_not_found.html","html");
                TempStream = new FileStream(RootCatalog + Response.File, FileMode.Open);
                Console.WriteLine(RootCatalog + Response.File);
            }
            catch (FileNotFoundException ex)
            {
                Response = new HTTPResponse("HTTP/1.0", 404, "Not Found", "/page_not_found.html", "html");
                TempStream = new FileStream(RootCatalog + Response.File, FileMode.Open);

            }
            writer.WriteLine(Response.ToAnswer());
            writer.Flush();
            TempStream.CopyTo(Stream);
            TempStream.Flush();
            writer.Close();
            TempStream.Close();
            Stream.Close();
        }
    }
}
