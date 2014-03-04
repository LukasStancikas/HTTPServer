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


        public SendingResponse(TcpClient Client, ref String answer, String RootCatalog, ref String requestedFile)
        {
            
            NetworkStream Stream = Client.GetStream();
            StreamWriter writer = new StreamWriter(Stream);
            FileStream TempStream=null;
    
            try
            {

           
                    TempStream = new FileStream(RootCatalog + requestedFile, FileMode.Open);
                   

            }
            catch (DirectoryNotFoundException e)
            {
                requestedFile = "/directory_not_found.html";
                answer = "HTTP/1.0 404 Not Found\r\n";
                TempStream = new FileStream(RootCatalog + requestedFile, FileMode.Open);
                Console.WriteLine(RootCatalog + requestedFile);
            }
            catch (FileNotFoundException ex)
            {
                requestedFile = "/page_not_found.html";
                answer = "HTTP/1.0 404 Not Found\r\n";
                TempStream = new FileStream(RootCatalog + requestedFile, FileMode.Open);
             
            }
           
           
            writer.WriteLine(answer);
            writer.Flush();
            TempStream.CopyTo(Stream);
            TempStream.Flush();
            writer.Close();
            TempStream.Close();
            Stream.Close();
           
      
            
           
           
          
        }
    }
}
