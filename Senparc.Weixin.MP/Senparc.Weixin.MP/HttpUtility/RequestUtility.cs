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
        /// 使用Get方法获取字符串结果（没有加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string url, Encoding encoding = null)
        {
            WebClient wc = new WebClient();
            wc.Encoding = encoding ?? Encoding.UTF8;
            //if (encoding != null)
            //{
            //    wc.Encoding = encoding;
            //}
            return wc.DownloadString(url);
        }

        /// <summary>
        /// 使用Get方法获取字符串结果（加入Cookie）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpGet(string url, CookieContainer cookieContainer = null, Encoding encoding = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.GetEncoding("utf-8")))
                {
                    string retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }

        /// <summary>
        /// 使用Post方法获取字符串结果，常规提交
        /// </summary>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null)
        {
            string dataString = GetQueryString(formData);
            var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);
            MemoryStream ms = new MemoryStream();
            ms.Write(formDataBytes, 0, formDataBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);//设置指针读取位置
            return HttpPost(url, cookieContainer, ms, false, null, encoding);
        }

        /// <summary>
        /// 使用Post方法获取字符串结果，上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, string fileName = null, Encoding encoding = null)
        {
            //读取文件
            var fileStream = FileHelper.GetFileStream(fileName);
            return HttpPost(url, cookieContainer, fileStream, fileName, null, encoding);
        }

        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="postStream"></param>
        /// <param name="fileName">文件名（如果需要上传文件）</param>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, Stream postStream = null, string fileName=null, string refererUrl = null, Encoding encoding = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postStream != null ? postStream.Length : 0;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";

            if (!string.IsNullOrEmpty(refererUrl))
            {
                request.Referer = refererUrl;
            }
            request.UserAgent =
                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }

            if (postStream != null)
            {
                //上传文件流
                Stream requestStream = request.GetRequestStream();

                if (isFile)
                {
                    //postStream.Position = 0;
                    //long length = 0;
                    string boundary = "------------------" + DateTime.Now.Ticks.ToString("x");
                    byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n–" + boundary + "\r\n");

                    string formdataTemplate = "\r\n–" + boundary + "\r\nContent-Disposition: form-data;  name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
                    var formdata = Encoding.ASCII.GetBytes(string.Format(formdataTemplate, "media", fileName));
                    requestStream.Write(formdata, 0, formdata.Length);

                    //写入文件
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }

                    //结尾
                    requestStream.Write(boundarybytes, 0, boundarybytes.Length);
                }
                else
                {
                    //写入文件
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }
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
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.GetEncoding("utf-8")))
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
