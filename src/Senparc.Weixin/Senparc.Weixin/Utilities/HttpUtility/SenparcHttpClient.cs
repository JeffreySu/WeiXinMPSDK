#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：SenparcHttpClient.cs
    文件功能描述：全局 HttpClient 单例（为.NET Core准备）


    创建标识：Senparc - 20171203

----------------------------------------------------------------*/

#if NET45 || NET461 || NETSTANDARD1_6 || NETSTANDARD2_0 || NETCOREAPP2_0
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.WebProxy;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net;

#if NETSTANDARD1_6 || NETSTANDARD2_0 || NETCOREAPP2_0
using Microsoft.AspNetCore.Http;
#endif

namespace Senparc.Weixin.HttpUtility
{
    /// <summary>
    /// SenparcHttpClient，全局 HttpClient 单例
    /// </summary>
    public class SenparcHttpClient
    {

        /// <summary>
        /// 默认HttpClient配置
        /// </summary>
        public static Func<HttpClient> DefaultHttpClientInstance = () => new HttpClient();

        #region 全局 HttpClient 单例

        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        SenparcHttpClient()
        {
        }

        //静态LocalCacheStrategy
        public static System.Net.Http.HttpClient Instance
        {
            get
            {
                return Nested.instance;//返回Nested类中的静态成员instance
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            //将instance设为一个初始化的LocalCacheStrategy新实例
            internal static readonly System.Net.Http.HttpClient instance = DefaultHttpClientInstance();
        }

        #endregion

#if NETSTANDARD1_6 || NETSTANDARD2_0 || NETCOREAPP2_0

        /// <summary>
        /// 设置HTTP头
        /// </summary>
        /// <param name="request"></param>
        /// <param name="refererUrl"></param>
        /// <param name="useAjax">是否使用Ajax</param>
        /// <param name="timeOut"></param>
        private void HttpClientHeader(HttpRequestMessage request, string refererUrl, bool useAjax, int timeOut)
        {
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36");
            request.Headers.Add("Timeout", timeOut.ToString());
            request.Headers.Add("Connection", "keep-alive");

            if (!string.IsNullOrEmpty(refererUrl))
            {
                request.Headers.Add("Referer", refererUrl);
            }

            if (useAjax)
            {
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            }
        }

        /// <summary>
        /// .NET Core 版本的HttpWebRequest参数设置
        /// </summary>
        /// <returns></returns>
        private HttpClient HttpGet_Common_NetCore(string url, HttpRequestMessage request, CookieContainer cookieContainer = null,
            Encoding encoding = null, X509Certificate2 cer = null,
            string refererUrl = null, bool useAjax = false, int timeOut = Config.TIME_OUT)
        {

            //using (var request = new HttpRequestMessage())
            //{

            //    request.Headers.Add(...);
            //    ...
            //    using (var response = await _httpClient.SendAsync(request))
            //    {
            //        ...
            //    }
            //}

            request.RequestUri = new Uri(url);

            if (encoding != null)
            {
                //TODO: set request encoding
            }


            var handler = new HttpClientHandler
            {
                CookieContainer = cookieContainer ?? new CookieContainer(),
                UseCookies = true,
            };

            if (cer != null)
            {
                handler.ClientCertificates.Add(cer);
            }

            HttpClient httpClient = SenparcHttpClient.Instance; //new HttpClient(handler);

            return httpClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="encoding"></param>
        /// <param name="cer"></param>
        /// <param name="refererUrl"></param>
        /// <param name="useAjax"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> SendAsync(string url, HttpRequestMessage request, CookieContainer cookieContainer = null,
            Encoding encoding = null, X509Certificate2 cer = null,
            string refererUrl = null, bool useAjax = false, int timeOut = Config.TIME_OUT)
        {
            var httpClient = HttpGet_Common_NetCore(url, request, cookieContainer, encoding, cer, refererUrl, useAjax, timeOut);

            if (cookieContainer != null)
            {
                //TODO:sync cookie
            }

            HttpClientHeader(request, refererUrl, useAjax, timeOut);

            var response = httpClient.SendAsync(request);
            return response;
        }

        /// <summary>
        /// 同步Cookie（自动处理CookieContainer）
        /// </summary>
        /// <param name="response"></param>
        /// <param name="cookieContainer"></param>
        public void SyncCookie(HttpResponseMessage response, CookieContainer cookieContainer)
        {
            //TODO:sync cookie
        }
#endif
    }
}
#endif
