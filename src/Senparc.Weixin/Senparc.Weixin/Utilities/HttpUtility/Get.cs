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

    文件名：Get.cs
    文件功能描述：Get


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：zeje - 20160422
    修改描述：v4.5.19 为GetJson方法添加maxJsonLength参数

    修改标识：zeje - 20170305
    修改描述：v14.3.132 添加Get.DownloadAsync(string url, string dir)方法
----------------------------------------------------------------*/

using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Newtonsoft.Json;

namespace Senparc.Weixin.HttpUtility
{
    /// <summary>
    /// Get 请求处理
    /// </summary>
    public static class Get
	{
		#region 同步方法

		/// <summary>
		/// GET方式请求URL，并返回T类型
		/// </summary>
		/// <typeparam name="T">接收JSON的数据类型</typeparam>
		/// <param name="url"></param>
		/// <param name="encoding"></param>
		/// <param name="maxJsonLength">允许最大JSON长度</param>
		/// <returns></returns>
		public static T GetJson<T>(string url, Encoding encoding = null, int? maxJsonLength = null)
		{
			string returnText = RequestUtility.HttpGet(url, encoding);

			WeixinTrace.SendApiLog(url, returnText);

			if (returnText.Contains("errcode"))
			{
				//可能发生错误
				WxJsonResult errorResult = JsonConvert.DeserializeObject<WxJsonResult>(returnText);
				if (errorResult.errcode != ReturnCode.请求成功)
				{
					//发生错误
					throw new ErrorJsonResultException(
						string.Format("微信请求发生错误！错误代码：{0}，说明：{1}",
										(int)errorResult.errcode, errorResult.errmsg), null, errorResult, url);
				}
			}

			T result = JsonConvert.DeserializeObject<T>(returnText);

			return result;
		}

		/// <summary>
		/// 从Url下载
		/// </summary>
		/// <param name="url"></param>
		/// <param name="stream"></param>
		public static void Download(string url, Stream stream)
		{
			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
			//ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

			HttpClient httpClient = new HttpClient();
			var t = httpClient.GetByteArrayAsync(url);
			t.Wait();
			var data = t.Result;
			foreach (var b in data)
			{
				stream.WriteByte(b);
			}
		}

        static System.Net.Http.HttpClient httpClient = new HttpClient();
        public static string Download(string url, string dir)
        {
            Directory.CreateDirectory(dir);
            System.Net.Http.HttpClient httpClient = new HttpClient();
            using (var responseMessage = httpClient.GetAsync(url).Result)
            {
                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var fullName = Path.Combine(dir, responseMessage.Content.Headers.ContentDisposition.FileName.Trim('"'));
                    using (var fs = File.Open(fullName, FileMode.Create))
                    {
                        using (var responseStream = responseMessage.Content.ReadAsStreamAsync().Result)
                        {
                            responseStream.CopyTo(fs);
                            return fullName;
                        }
                    }
                }
            }
            return null;
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】异步GetJson
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <param name="maxJsonLength">允许最大JSON长度</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ErrorJsonResultException"></exception>
        public static async Task<T> GetJsonAsync<T>(string url, Encoding encoding = null, int? maxJsonLength = null)
		{
			string returnText = await RequestUtility.HttpGetAsync(url, encoding);

			if (returnText.Contains("errcode"))
			{
				//可能发生错误
				WxJsonResult errorResult = JsonConvert.DeserializeObject<WxJsonResult>(returnText);
				if (errorResult.errcode != ReturnCode.请求成功)
				{
					//发生错误
					throw new ErrorJsonResultException(
						string.Format("微信请求发生错误！错误代码：{0}，说明：{1}",
										(int)errorResult.errcode, errorResult.errmsg), null, errorResult, url);
				}
			}

			T result = JsonConvert.DeserializeObject<T>(returnText);

			return result;
		}

		/// <summary>
		/// 【异步方法】异步从Url下载
		/// </summary>
		/// <param name="url"></param>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static async Task DownloadAsync(string url, Stream stream)
		{
			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
			//ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

			HttpClient httpClient = new HttpClient();
			var data = await httpClient.GetByteArrayAsync(url);
			await stream.WriteAsync(data, 0, data.Length);
			//foreach (var b in data)
			//{
			//    stream.WriteAsync(b);
			//}
		}

        ///// <summary>
        ///// 【异步方法】从Url下载，并保存到指定目录
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="dir"></param>
        ///// <returns></returns>
        //public static async Task<string> DownloadAsync(string url, string dir)
        //{
        //    Directory.CreateDirectory(dir);
        //    System.Net.Http.HttpClient httpClient = new HttpClient();
        //    using (var responseMessage = await httpClient.GetAsync(url))
        //    {
        //        if (responseMessage.StatusCode == HttpStatusCode.OK)
        //        {
        //            var fullName = Path.Combine(dir, responseMessage.Content.Headers.ContentDisposition.FileName.Trim('"'));
        //            using (var fs = File.Open(fullName, FileMode.Create))
        //            {
        //                using (var responseStream = await responseMessage.Content.ReadAsStreamAsync())
        //                {
        //                    await responseStream.CopyToAsync(fs);
        //                    return fullName;
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}
        #endregion


    }
}
