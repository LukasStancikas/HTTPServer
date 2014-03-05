using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{
    class HTTPResponse
    {
        public String Protocol;
        public int  Code;
        public String Message;
        public String File;
        public String FileType;

        public HTTPResponse()
        {
        }

        public HTTPResponse(String _protocol, int _code, String _message, String _file, String _fileType)
        {
            Protocol = _protocol;
            Code = _code;
            Message = _message;
            File = _file;
            FileType = _fileType;
        }

        public String ToAnswer()
        {
            //answer = "HTTP/1.0 404 Not Found\r\n";
            return Protocol + " " + Code + " " + Message + "\r\n";
        }
    }
}
