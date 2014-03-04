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
            StreamWriter writer = new StreamWriter(Client.GetStream());
            FileStream TempStream=null;
           
            try
            {

               
                    Console.WriteLine("test");
                    
                    TempStream = new FileStream(RootCatalog + requestedFile, FileMode.Open);
                    
               
                    Console.WriteLine("test");
                  
               

            }
            catch (DirectoryNotFoundException e)
            {
                requestedFile = "/directory_not_found.html";
                answer = "HTTP/1.0 404 Not Found\r\n";
                using (FileStream FileReader = new FileStream(RootCatalog + requestedFile, FileMode.Open))
                {

                    FileReader.CopyTo(TempStream);
                }
            }
            catch (FileNotFoundException ex)
            {
                requestedFile = "/page_not_found.html";
                answer = "HTTP/1.0 404 Not Found\r\n";
                using (FileStream FileReader = new FileStream(RootCatalog + requestedFile, FileMode.Open))
                {

                    FileReader.CopyTo(TempStream);
                }
            }
           
           
            writer.AutoFlush = false;
            
            writer.WriteLine(answer);
            Console.WriteLine(answer);
         
            writer.WriteLine();
            writer.Flush();
            TempStream.CopyTo(Client.GetStream());
          
            
           
           
          
        }
    }
}
