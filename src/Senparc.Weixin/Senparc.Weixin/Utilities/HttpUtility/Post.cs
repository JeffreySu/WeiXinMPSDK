#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc

    文件名：Post.cs
    文件功能描述：Post


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间

    修改标识：zhanghao-kooboo - 20150316
    修改描述：增加

    修改标识：Senparc - 20150407
    修改描述：发起Post请求方法修改，为了上传永久视频素材
 
    修改标识：Senparc - 20160720
    修改描述：增加了PostFileGetJsonAsync的异步方法（与之前的方法多一个参数）

    修改标识：Senparc - 20170409
    修改描述：v4.11.9 修改Download方法
----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;

#if NET35 || NET40 || NET45
using System.Web.Script.Serialization;
using Senparc.Weixin.HttpUtility;
#endif
#if !NET35 && !NET40
using System.Net.Http;
#endif

namespace Senparc.Weixin.HttpUtility
{
    /// <summary>
    /// Post 请求处理
    /// </summary>
    public static class Post
    {
        /// <summary>
        /// 获取Post结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnText"></param>
        /// <returns></returns>
        public static T GetResult<T>(string returnText)
        {
#if NET35 || NET40 || NET45
            JavaScriptSerializer js = new JavaScriptSerializer();
#endif

            if (returnText.Contains("errcode"))
            {
                //可能发生错误
#if NET35 || NET40 || NET45
                WxJsonResult errorResult = js.Deserialize<WxJsonResult>(returnText);
#else
                WxJsonResult errorResult = Newtonsoft.Json.JsonConvert.DeserializeObject<WxJsonResult>(returnText);
#endif
                if (errorResult.errcode != ReturnCode.请求成功)
                {
                    //发生错误
                    throw new ErrorJsonResultException(
                        string.Format("微信Post请求发生错误！错误代码：{0}，说明：{1}",
                                      (int)errorResult.errcode,
                                      errorResult.errmsg),
                        null, errorResult);
                }
            }

#if NET35 || NET40 || NET45
            T result = js.Deserialize<T>(returnText);
#else
            T result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(returnText);
#endif

            //TODO:加入特殊情况下的回调处理

            return result;
        }


        #region 同步方法

        /// <summary>
        /// 发起Post请求，可上传文件
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="useAjax"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="fileDictionary">需要Post的文件（Dictionary 的 Key=name，Value=绝对路径）</param>
        /// <param name="postDataDictionary">需要Post的键值对（name,value）</param>
        /// <returns></returns>
        public static T PostFileGetJson<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null,
            Dictionary<string, string> postDataDictionary = null, Encoding encoding = null, X509Certificate2 cer = null, bool useAjax = false,
            int timeOut = CO2NET.Config.TIME_OUT)
        {
            var result = CO2NET.HttpUtility.Post.PostFileGetJson<T>(url, cookieContainer, fileDictionary,
                            postDataDictionary, encoding, cer, useAjax, Get.AfterReturnText, timeOut);

            return result;
        }

        /// <summary>
        /// 发起Post请求，可包含文件流
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="fileStream">文件流</param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="useAjax">是否使用Ajax请求</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="checkValidationResult">验证服务器证书回调自动验证</param>
        /// <returns></returns>
        public static T PostGetJson<T>(string url, CookieContainer cookieContainer = null, Stream fileStream = null, Encoding encoding = null,
            X509Certificate2 cer = null, bool useAjax = false, bool checkValidationResult = false, int timeOut = CO2NET.Config.TIME_OUT)
        {
            var result = CO2NET.HttpUtility.Post.PostGetJson<T>(url, cookieContainer, fileStream, encoding,
                            cer, useAjax, checkValidationResult, Get.AfterReturnText, timeOut);
            return result;
        }

        /// <summary>
        /// 【异步方法】Form表单Post数据，获取JSON
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="formData"></param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="useAjax">是否使用Ajax请求</param>
        /// <param name="timeOut"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T PostGetJson<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null,
            Encoding encoding = null, X509Certificate2 cer = null, bool useAjax = false, int timeOut = CO2NET.Config.TIME_OUT)
        {
            var result = CO2NET.HttpUtility.Post.PostGetJson<T>(url, cookieContainer, formData, encoding,
                             cer, useAjax, Get.AfterReturnText, timeOut);
            return result;
        }

        /// <summary>
        /// 使用Post方法上传数据并下载文件或结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="stream"></param>
        public static void Download(string url, string data, Stream stream)
        {
#if NET35 || NET40 || NET45
            WebClient wc = new WebClient(); 
            var file = wc.UploadData(url, "POST", Encoding.UTF8.GetBytes(string.IsNullOrEmpty(data) ? "" : data));
            stream.Write(file, 0, file.Length);     

            //foreach (var b in file)
            //{
            //    stream.WriteByte(b);
            //}
#else
            HttpClient httpClient = new HttpClient();
            HttpContent hc = new StringContent(data);
            var ht = httpClient.PostAsync(url, hc);
            ht.Wait();
            var ft = ht.Result.Content.ReadAsByteArrayAsync();
            ft.Wait();
            var file = ft.Result;
            stream.Write(file, 0, file.Length);
#endif

        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】发起Post请求，可上传文件
        /// </summary>
        /// <typeparam name="T">返回数据类型（Json对应的实体）</typeparam>
        /// <param name="url">请求Url</param>
        /// <param name="cookieContainer">CookieContainer，如果不需要则设为null</param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="useAjax"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="fileDictionary">需要Post的文件（Dictionary 的 Key=name，Value=绝对路径）</param>
        /// <param name="postDataDictionary">需要Post的键值对（name,value）</param>
        /// <returns></returns>
        public static async Task<T> PostFileGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null,
            Dictionary<string, string> postDataDictionary = null, Encoding encoding = null, X509Certificate2 cer = null, bool useAjax = false,
            int timeOut = CO2NET.Config.TIME_OUT)
        {
            var result = await CO2NET.HttpUtility.Post.PostFileGetJsonAsync<T>(url, cookieContainer, fileDictionary,
                           postDataDictionary, encoding, cer, useAjax, Get.AfterReturnText, timeOut);
            return result;
        }


        /// <summary>
        /// 【异步方法】发起Post请求，可包含文件流
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="fileStream"></param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="useAjax">是否使用Ajax请求</param>
        /// <param name="timeOut"></param>
        /// <param name="checkValidationResult"></param>
        /// <returns></returns>
        public static async Task<T> PostGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Stream fileStream = null, Encoding encoding = null,
            X509Certificate2 cer = null, bool useAjax = false, bool checkValidationResult = false, int timeOut = CO2NET.Config.TIME_OUT)
        {
            var result =  await CO2NET.HttpUtility.Post.PostGetJsonAsync<T>(url, cookieContainer, fileStream, encoding,
                      cer, useAjax, checkValidationResult, Get.AfterReturnText, timeOut);
            return result;
        }


        /// <summary>
        /// 【异步方法】Form表单Post数据，获取JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="formData"></param>
        /// <param name="encoding"></param>
        /// <param name="cer">证书，如果不需要则保留null</param>
        /// <param name="useAjax">是否使用Ajax请求</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<T> PostGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> formData = null, 
            Encoding encoding = null, X509Certificate2 cer = null, bool useAjax = false, int timeOut = CO2NET.Config.TIME_OUT)
        {
            var result = await CO2NET.HttpUtility.Post.PostGetJsonAsync<T>(url, cookieContainer, formData, encoding,
                           cer, useAjax, Get.AfterReturnText, timeOut);
            return result;
        }

        ///// <summary>
        ///// PostFileGetJson的异步版本
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="url"></param>
        ///// <param name="cookieContainer"></param>
        ///// <param name="fileDictionary"></param>
        ///// <param name="encoding"></param>
        ///// <param name="cer">证书，如果不需要则保留null</param>
        ///// <param name="timeOut"></param>
        ///// <returns></returns>
        //public static async Task<T> PostFileGetJsonAsync<T>(string url, CookieContainer cookieContainer = null, Dictionary<string, string> fileDictionary = null, Encoding encoding = null, X509Certificate cer = null, int timeOut = CO2NET.Config.TIME_OUT)
        //{
        //    string returnText = await RequestUtility.HttpPostAsync(url, cookieContainer, null, fileDictionary, null, encoding, cer, timeOut);
        //    var result = GetResult<T>(returnText);
        //    return result;
        //}



        /// <summary>
        /// 使用Post方法上传数据并下载文件或结果
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="stream"></param>
        public static async Task DownloadAsync(string url, string data, Stream stream)
        {
#if NET35 || NET40 || NET45
            WebClient wc = new WebClient();

            var fileBytes = await wc.UploadDataTaskAsync(url, "POST", Encoding.UTF8.GetBytes(string.IsNullOrEmpty(data) ? "" : data));
            await stream.WriteAsync(fileBytes, 0, fileBytes.Length);//也可以分段写入
#else
            HttpClient httpClient = new HttpClient();
            HttpContent hc = new StringContent(data);
            var ht = await httpClient.PostAsync(url, hc);
            var fileBytes = await ht.Content.ReadAsByteArrayAsync();
            await stream.WriteAsync(fileBytes, 0, fileBytes.Length);//也可以分段写入
#endif

        }

        #endregion
#endif
    }
}
