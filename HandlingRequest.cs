using System;
using System.Collections.Generic;
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
            Console.WriteLine(Type);
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
            string[,] array2Db = new string[,]
            {
                {"html", "text/html"},
                {"htm", "text/html"},
                {"doc", "application/msword"},
                {"gif", "image/gif"},//gif 	image/gif
                {"jpg", "image/jpeg"},//jpg 	image/jpeg
                {"pdf", "application/pdf"},//pdf 	application/pdf
                {"css", "text/css"},//css 	text/css
                {"xml", "text/xml"},//xml 	text/xml  	
                {"jar", "application/x-java-archive"},//jar application/x-java-archive
            };
          
            if (fileName.Contains("."))
            {
            string fileType = fileName.Split('.')[1];
            for (int i = 0; i < array2Db.Length; i++)
            {
                if (array2Db[i, 0] == fileType)
                {
                    return array2Db[i, 1];
                }
            }
            }
            return "application/octet-stream";
        }
    }
}
