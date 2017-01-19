using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Analyzer.Common
{
    
    public class GZipWebClient : WebClient
    {
        // To automatically use GZip if available to minimize traffic: http://stackoverflow.com/questions/678547/does-nets-httpwebresponse-uncompress-automatically-gziped-and-deflated-respons
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            return request;
        }
    }
}
