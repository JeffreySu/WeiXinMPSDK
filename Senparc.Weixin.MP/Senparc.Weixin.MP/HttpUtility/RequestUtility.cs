using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Senparc.Weixin.MP.HttpUtility
{
    public static class RequestUtility
    {
        public static string DownloadString(string url)
        {
            WebClient wc = new WebClient();
            return wc.DownloadString(url);
        }
    }
}
