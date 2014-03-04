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

               // Here I read from the file in server
                    TempStream = new FileStream(RootCatalog + requestedFile, FileMode.Open);
                    
               

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
            // with first writeline i input 'HTTP/1.0 ... ...\r\nContent-Type: ' and a string Type (it's all in the 'answer')
            
            writer.WriteLine(answer);
            Console.WriteLine(answer);
            //with second writeline i input an empty line to seperate the bodies
            writer.WriteLine();
            writer.Flush();
            //now the problem is inputing data i read from the file to the writer, it only accepts Strings, and when using FileStream
            //I don't get a string from reading the file. That's why I'm figuring out how to send the data of the jpg/html etc..
            TempStream.CopyTo(Client.GetStream());
          
            
           
           
          
        }
    }
}
