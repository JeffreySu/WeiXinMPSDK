using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.HttpUtility
{
    public static class RequestUtility
    {
        /// <summary>
        /// 使用Get方法获取字符串结果（暂时没有加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            WebClient wc = new WebClient();
            return wc.DownloadString(url);
        }

        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null)
        {
            string dataString = GetQueryString(formData);
            var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return HttpPost(url, cookieContainer, ms);
        }

        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, string fileName = null)
        {
            //读取文件
            var fileStream = FileHelper.GetFileStream(fileName);
            return HttpPost(url, cookieContainer, fileStream, true);
        }


        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="postStream"></param>
        /// <param name="isFile">postStreams是否是文件流</param>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, Stream postStream = null, bool isFile = false)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postStream != null ? postStream.Length : 0;

            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }

            if (postStream != null)
            {
                //上传文件流
                Stream requestStream = request.GetRequestStream();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                postStream.Close();//关闭文件访问
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                {
                    string retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }


        /// <summary>
        /// 请求是否发起自微信客户端的浏览器
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static bool IsWeixinClientRequest(this HttpContext httpContext)
        {
            return !string.IsNullOrEmpty(httpContext.Request.UserAgent) &&
                   httpContext.Request.UserAgent.Contains("MicroMessenger");
        }

        /// <summary>
        /// 组装QueryString的方法
        /// 参数之间用&连接，首位没有符号，如：a=1&b=2&c=3
        /// </summary>
        /// <param name="formData"></param>
        /// <returns></returns>
        public static string GetQueryString(this Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            var i = 0;
            foreach (var kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                if (i < formData.Count)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 封装System.Web.HttpUtility.HtmlEncode
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string html)
        {
            return System.Web.HttpUtility.HtmlEncode(html);
        }
        /// <summary>
        /// 封装System.Web.HttpUtility.HtmlDecode
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string html)
        {
            return System.Web.HttpUtility.HtmlDecode(html);
        }
        /// <summary>
        /// 封装System.Web.HttpUtility.UrlEncode
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(this string url)
        {
            return System.Web.HttpUtility.UrlEncode(url);
        }
        /// <summary>
        /// 封装System.Web.HttpUtility.UrlDecode
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(this string url)
        {
            return System.Web.HttpUtility.UrlDecode(url);
        }
    }
}
