using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPProject
{
    class HTTPRequest
    {
        public String Method;
        public String URL;
        public String Protocol;
        public HTTPRequest()
        {
            
        }

        public HTTPRequest(String _method, String _url, String _protocol)
        {
            Method = _method;
            URL = _url;
            Protocol = _protocol;
        }
        
    }
}
