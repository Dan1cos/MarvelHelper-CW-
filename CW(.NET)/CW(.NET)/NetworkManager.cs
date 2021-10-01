using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CW_.NET_
{
    class NetworkManager
    {
        public string GetJsonFromUrl(string url) => new WebClient().DownloadString(url);
    }
}
