
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{
    class ReadingRequest
    {
        public ReadingRequest(TcpClient Client, ref HTTPRequest request, int Port)
        {
            string line = "";
            String fullRequest = "";
            String requestedFile = null;
            String requestLine = null;
            if (Port == Server.ShutdownPort)
            {
                requestLine = "GET /Shutdown.html HTTP/1.1";
            }
            StreamReader reader = new StreamReader(Client.GetStream());
            line = reader.ReadLine();
            if (requestLine == null)
            {
                requestLine = line;
            }
            Console.WriteLine(requestLine);
            try
            {
                List<String> TempDirectory = requestLine.Split(' ').ToList();
                TempDirectory.RemoveAt(0);
                TempDirectory.RemoveAt(TempDirectory.Count - 1);
                requestedFile = String.Join(" ", TempDirectory.ToArray());
                requestedFile = Uri.UnescapeDataString(requestedFile);
                Console.WriteLine(requestedFile);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Caught: Null Reference exception - invald format of request");
            }
            fullRequest += line + "\r\n";
            do
            {
                line = reader.ReadLine();
                fullRequest += line + "\r\n";
            } while (line.Length != 0);

            if (request == null) request = new HTTPRequest();

            request.Method = requestLine.Split(' ')[0];
            request.URL = requestedFile;
            request.Protocol = requestLine.Split(' ')[2];
        }
    }
}
