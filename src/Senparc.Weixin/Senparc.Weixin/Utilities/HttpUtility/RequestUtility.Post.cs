﻿#region Apache License Version 2.0
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

    文件名：RequestUtility.Post.cs
    文件功能描述：获取请求结果（Post）


    创建标识：Senparc - 20171006

    修改描述：移植Post方法过来

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Helpers;
#if NET35 || NET40 || NET45
using System.Web;
#else
using System.Net.Http;
using System.Net.Http.Headers;
#endif
#if NETSTANDARD1_6 || NETSTANDARD2_0 || NETCOREAPP2_0
using Microsoft.AspNetCore.Http;
using Senparc.Weixin.WebProxy;
#endif

namespace Senparc.Weixin.HttpUtility
{
    /// <summary>
    /// HTTP 请求工具类
    /// </summary>
    public static partial class RequestUtility
    {
        #region 静态公共方法

#if NET35 || NET40 || NET45


        public static HttpWebRequest HttpPost_Common_Net45(string url, CookieContainer cookieContainer = null,
            Stream postStream = null, Dictionary<string, string> fileDictionary = null, string refererUrl = null,
            Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT,
            bool checkValidationResult = false)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = timeOut;
            request.Proxy = _webproxy;
            if (cer != null)
            {
                request.ClientCertificates.Add(cer);
            }

            if (checkValidationResult)
            {
                ServicePointManager.ServerCertificateValidationCallback =
                  new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            #region 处理Form表单文件上传
            var formUploadFile = fileDictionary != null && fileDictionary.Count > 0;//是否用Form上传文件
            if (formUploadFile)
            {

                //通过表单上传文件
                string boundary = "----" + DateTime.Now.Ticks.ToString("x");

                postStream = postStream ?? new MemoryStream();
                //byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                string fileFormdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                string dataFormdataTemplate = "\r\n--" + boundary +
                                                "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";


                foreach (var file in fileDictionary)
                {
                    try
                    {
                        var fileName = file.Value;
                        //准备文件流
                        using (var fileStream = FileHelper.GetFileStream(fileName))
                        {

                            string formdata = null;
                            if (fileStream != null)
                            {
                                //存在文件
                                formdata = string.Format(fileFormdataTemplate, file.Key, /*fileName*/ Path.GetFileName(fileName));
                            }
                            else
                            {
                                //不存在文件或只是注释
                                formdata = string.Format(dataFormdataTemplate, file.Key, file.Value);
                            }

                            //统一处理
                            var formdataBytes = Encoding.UTF8.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);//第一行不需要换行
                            postStream.Write(formdataBytes, 0, formdataBytes.Length);

                            //写入文件
                            if (fileStream != null)
                            {
                                byte[] buffer = new byte[1024];
                                int bytesRead = 0;
                                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    postStream.Write(buffer, 0, bytesRead);
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                //结尾
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);

                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion

            request.ContentLength = postStream != null ? postStream.Length : 0;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;

            if (!string.IsNullOrEmpty(refererUrl))
            {
                request.Referer = refererUrl;
            }
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";

            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }

            return request;
        }

#endif

#if NETSTANDARD1_6 || NETSTANDARD2_0 || NETCOREAPP2_0
        public static HttpClient HttpPost_Common_NetCore(string url, out HttpContent hc, CookieContainer cookieContainer = null,
            Stream postStream = null, Dictionary<string, string> fileDictionary = null, string refererUrl = null,
            Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT,
            bool checkValidationResult = false)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookieContainer;

            if (checkValidationResult)
            {
                handler.ServerCertificateCustomValidationCallback = new Func<HttpRequestMessage, X509Certificate2, X509Chain, SslPolicyErrors, bool>(CheckValidationResult);
            }

            if (cer != null)
            {
                handler.ClientCertificates.Add(cer);
            }

            HttpClient client = new HttpClient(handler);
            HttpClientHeader(client, timeOut);


        #region 处理Form表单文件上传

            var formUploadFile = fileDictionary != null && fileDictionary.Count > 0;//是否用Form上传文件
            if (formUploadFile)
            {

                //通过表单上传文件
                string boundary = "----" + DateTime.Now.Ticks.ToString("x");
                hc = new MultipartFormDataContent(boundary);

                foreach (var file in fileDictionary)
                {
                    try
                    {
                        var fileName = file.Value;
                        //准备文件流
                        using (var fileStream = FileHelper.GetFileStream(fileName))
                        {
                            if (fileStream != null)
                            {
                                //存在文件
                                //hc.Add(new StreamContent(fileStream), file.Key, Path.GetFileName(fileName)); //报流已关闭的异常
                                fileStream.Dispose();
                                (hc as MultipartFormDataContent).Add(CreateFileContent(File.Open(fileName, FileMode.Open), Path.GetFileName(fileName)), file.Key, Path.GetFileName(fileName));
                            }
                            else
                            {
                                //不存在文件或只是注释
                                (hc as MultipartFormDataContent).Add(new StringContent(string.Empty), file.Key, file.Value);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                hc.Headers.ContentType = MediaTypeHeaderValue.Parse(string.Format("multipart/form-data; boundary={0}", boundary));
            }
            else
            {
                hc = new StreamContent(postStream);

                //使用Url格式Form表单Post提交的时候才使用application/x-www-form-urlencoded
                //hc.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                hc.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            }

            //HttpContentHeader(hc, timeOut);
        #endregion

            if (!string.IsNullOrEmpty(refererUrl))
            {
                client.DefaultRequestHeaders.Referrer = new Uri(refererUrl);
            }


            return client;
        }
#endif

        #endregion

        #region 同步方法

        /// <summary>
        /// 使用Post方法获取字符串结果，常规提交
        /// </summary>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT)
        {
            MemoryStream ms = new MemoryStream();
            formData.FillFormDataStream(ms);//填充formData
            return HttpPost(url, cookieContainer, ms, null, null, encoding, cer, timeOut);
        }

        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="postStream"></param>
        /// <param name="fileDictionary">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="timeOut"></param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <param name="refererUrl"></param>
        /// <returns></returns>
        public static string HttpPost(string url, CookieContainer cookieContainer = null, Stream postStream = null, Dictionary<string, string> fileDictionary = null, string refererUrl = null, Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT, bool checkValidationResult = false)
        {
            if (cookieContainer == null)
            {
                cookieContainer = new CookieContainer();
            }

#if NET35 || NET40 || NET45
            var request = HttpPost_Common_Net45(url, cookieContainer, postStream, fileDictionary, refererUrl, encoding, cer, timeOut, checkValidationResult);

            #region 输入二进制流
            if (postStream != null)
            {
                postStream.Position = 0;

                //直接写入流
                Stream requestStream = request.GetRequestStream();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                //debug
                //postStream.Seek(0, SeekOrigin.Begin);
                //StreamReader sr = new StreamReader(postStream);
                //var postStr = sr.ReadToEnd();

                postStream.Close();//关闭文件访问
            }
            #endregion

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
#else
            HttpContent hc;
            var client = HttpPost_Common_NetCore(url, out hc, cookieContainer, postStream, fileDictionary, refererUrl, encoding, cer, timeOut, checkValidationResult);

            var t = client.PostAsync(url, hc).GetAwaiter().GetResult();
            //t.Wait();

            if (t.Content.Headers.ContentType.CharSet != null &&
                t.Content.Headers.ContentType.CharSet.ToLower().Contains("utf8"))
            {
                t.Content.Headers.ContentType.CharSet = "utf-8";
            }

            var t1 = t.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return t1;

            //t1.Wait();
            //return t1.Result;
#endif
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 使用Post方法获取字符串结果，常规提交
        /// </summary>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT)
        {
#if NET35 || NET40 || NET45
            MemoryStream ms = new MemoryStream();
            await formData.FillFormDataStreamAsync(ms);//填充formData
            return await HttpPostAsync(url, cookieContainer, ms, null, null, encoding, cer, timeOut);
#else
            MemoryStream ms = new MemoryStream();
            await formData.FillFormDataStreamAsync(ms);//填充formData
            return await HttpPostAsync(url, cookieContainer, ms, null, null, encoding, cer, timeOut);
#endif

        }


        /// <summary>
        /// 使用Post方法获取字符串结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="postStream"></param>
        /// <param name="fileDictionary">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>
        /// <param name="cer"></param>
        /// <param name="timeOut"></param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <param name="refererUrl"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static async Task<string> HttpPostAsync(string url, CookieContainer cookieContainer = null, Stream postStream = null, Dictionary<string, string> fileDictionary = null, string refererUrl = null, Encoding encoding = null, X509Certificate2 cer = null, int timeOut = Config.TIME_OUT, bool checkValidationResult = false)
        {
            if (cookieContainer == null)
            {
                cookieContainer = new CookieContainer();
            }

#if NET35 || NET40 || NET45
            var request = HttpPost_Common_Net45(url, cookieContainer, postStream, fileDictionary, refererUrl, encoding, cer, timeOut, checkValidationResult);

        #region 输入二进制流
            if (postStream != null)
            {
                postStream.Position = 0;

                //直接写入流
                Stream requestStream = await request.GetRequestStreamAsync();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = await postStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    await requestStream.WriteAsync(buffer, 0, bytesRead);
                }


                //debug
                //postStream.Seek(0, SeekOrigin.Begin);
                //StreamReader sr = new StreamReader(postStream);
                //var postStr = await sr.ReadToEndAsync();

                postStream.Close();//关闭文件访问
            }
        #endregion

            HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());

            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.GetEncoding("utf-8")))
                {
                    string retString = await myStreamReader.ReadToEndAsync();
                    return retString;
                }
            }
#else
            HttpContent hc;
            var client = HttpPost_Common_NetCore(url, out hc, cookieContainer, postStream, fileDictionary, refererUrl, encoding, cer, timeOut, checkValidationResult);

            var r = await client.PostAsync(url, hc);

            if (r.Content.Headers.ContentType.CharSet != null &&
                r.Content.Headers.ContentType.CharSet.ToLower().Contains("utf8"))
            {
                r.Content.Headers.ContentType.CharSet = "utf-8";
            }

            return await r.Content.ReadAsStringAsync();
#endif
        }


        #endregion
#endif

    }
}
