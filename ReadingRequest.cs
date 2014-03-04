
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


        public ReadingRequest(TcpClient Client, ref String fullRequest, ref String requestedFile, ref String requestLine)
        {
            string line = "";
            fullRequest = "";
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
                TempDirectory.RemoveAt(TempDirectory.Count-1);
                requestedFile = String.Join(" ", TempDirectory.ToArray());
                requestedFile = Uri.UnescapeDataString(requestedFile);
                Console.WriteLine(requestedFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            fullRequest += line + "\r\n";
            do
            {
                line = reader.ReadLine();
                fullRequest += line + "\r\n";
            } while (line.Length != 0);

        }
    }
}
