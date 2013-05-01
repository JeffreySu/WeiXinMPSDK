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

        /// <summary>
        /// 请求是否发起自微信客户端的浏览器
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static bool IsWeixinClientRequest(HttpContext httpContext)
        {
            return !string.IsNullOrEmpty(httpContext.Request.UserAgent) &&
                   httpContext.Request.UserAgent.Contains("MicroMessenger");
        }
    }
}
