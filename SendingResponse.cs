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
                Server.Logger.Info("Succesfully opened FileStream. Directory:" + RootCatalog + Response.File);
               
            }
            catch (DirectoryNotFoundException e)
            {
                Response = new HTTPResponse("HTTP/1.0", 404, "Not Found","/directory_not_found.html","html");
                TempStream = new FileStream(RootCatalog + Response.File, FileMode.Open);
                Console.WriteLine(RootCatalog + Response.File);
                Server.Logger.Warn("Caught DirectoryNotFound Exception");
                Server.Logger.Warn("New Response line: " + Response.ToAnswer());
                Server.Logger.Info("New Response content-type:" + Response.FileType);
                Server.Logger.Info("New Response File:" + Response.File);
            }
            catch (FileNotFoundException ex)
            {
                Response = new HTTPResponse("HTTP/1.0", 404, "Not Found", "/page_not_found.html", "html");
                TempStream = new FileStream(RootCatalog + Response.File, FileMode.Open);
                Server.Logger.Warn("Caught FileNotFound Exception");
                Server.Logger.Warn("New Response line: " + Response.ToAnswer());
                Server.Logger.Info("New Response content-type:" + Response.FileType);
                Server.Logger.Info("New Response File:" + Response.File);
            }
            writer.WriteLine(Response.ToAnswer());
            writer.Flush();
            TempStream.CopyTo(Stream);
            TempStream.Flush();
            writer.Close();
            TempStream.Close();
            Stream.Close();
            Server.Logger.Info("StreamWriter and Stream Flushed and closed");
        }
    }
}
