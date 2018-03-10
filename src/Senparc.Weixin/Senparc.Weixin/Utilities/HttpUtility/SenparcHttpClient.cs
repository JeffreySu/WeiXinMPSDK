using System;
using System.Collections.Generic;
using System.Linq;
#if NETCOREAPP2_1
using System.Net.Http;
#endif
using System.Text;

namespace Senparc.Weixin.HttpUtility
{
#if NETCOREAPP2_1
    public class SenparcHttpClient
    {
        public SenparcHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
        public HttpClient HttpClient { get; }
    }
#endif
}
