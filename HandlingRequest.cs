using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{
    class HandlingRequest
    {
        public HandlingRequest(String requestLine,ref String requestedFile,ref String answer)
        {
            String[] Analizable = requestLine.Split(' ');

            if ((Analizable[0].Equals("GET")) || (Analizable[0].Equals("HEAD")) || (Analizable[0].Equals("POST")))
            {
            }
            else
            {
                answer = "HTTP/1.0 400 Illegal request\r\n" +
                         "\r\n";
                requestedFile = "/Illegal_Request.html";
            }
            if ((Analizable[2].Equals("HTTP/1.0")) || (Analizable[2].Equals("HTTP/1.1")))
            {
            }
            else
            {
                answer = "HTTP/1.0 400 Illegal protocol\r\n" +
                         "\r\n";
                requestedFile = "/Illegal_Protocol.html";
            }
            String temp = Analizable[2].Substring(0, Analizable[2].Length - 1);
            if ((temp.Equals("HTTP/1.")) || (temp.Equals("HTTP/1.")))
            {
            }
            else
            {
                answer = "HTTP/1.0 400 Illegal request\r\n" +
                         "\r\n";
                requestedFile = "/Illegal_Request.html";
            }


            if (requestedFile == "/")
            {
                requestedFile = "/index.html";
            }
        }
    }
}
