using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{
    class HandlingRequest
    {
        public HandlingRequest(String requestLine, ref String requestedFile, ref String answer)
        {
            String[] Analizable = requestLine.Split(' ');
            

            String Type = null;
            answer =
                "HTTP/1.0 200 OK\r\n";
            
            if ((Analizable[0].Equals("GET")) || (Analizable[0].Equals("HEAD")) || (Analizable[0].Equals("POST")))
            {
            }
            else
            {
                answer = "HTTP/1.0 400 Illegal request\r\n";
                requestedFile = "/Illegal_Request.html";
            }
            if ((Analizable[2].Equals("HTTP/1.0")) || (Analizable[2].Equals("HTTP/1.1")))
            {
            }
            else
            {
                answer = "HTTP/1.0 400 Illegal protocol\r\n";
                requestedFile = "/Illegal_Protocol.html";
            }
            String temp = Analizable[2].Substring(0, Analizable[2].Length - 1);
            if ((temp.Equals("HTTP/1.")) || (temp.Equals("HTTP/1.")))
            {
            }
            else
            {
                answer = "HTTP/1.0 400 Illegal request\r\n";
                requestedFile = "/Illegal_Request.html";
            }


            if (requestedFile == "/")
            {
                requestedFile = "/index.html";
            }
          
                Type = NameToType(requestedFile);
            
           
                answer += "Content-Type: "
                         + Type + "\r\n";
            
            
        }
        private string NameToType(string fileName)
        {
            Dictionary<string,string> allowedTypes = new Dictionary<string, string>();
            allowedTypes.Add("html", "text/html");
            allowedTypes.Add("htm", "text/html");
            allowedTypes.Add("doc", "application/msword");
            allowedTypes.Add("gif", "image/gif");
            allowedTypes.Add("jpg", "image/jpeg");
            allowedTypes.Add("pdf", "application/pdf");
            allowedTypes.Add("css", "text/css");
            allowedTypes.Add("xml", "text/xml");
            allowedTypes.Add("jar", "application/x-java-archive");
            if (fileName.Contains("."))
            {
            string fileType = Path.GetExtension(fileName).Substring(1);
                if (allowedTypes.ContainsKey(fileType))
                {
                    Console.WriteLine(fileType);
                    return allowedTypes[fileType];
                }
                    
            }
            return "application/octet-stream";
        }
    }
}
