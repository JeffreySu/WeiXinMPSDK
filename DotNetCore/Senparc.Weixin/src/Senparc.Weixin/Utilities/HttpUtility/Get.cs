using System;
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
	public static class Get
	{
		#region 同步方法

		/// <summary>
		/// GET方式请求URL，并返回T类型
		/// </summary>
		/// <typeparam name="T">接收JSON的数据类型</typeparam>
		/// <param name="url"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public static T GetJson<T>(string url, Encoding encoding = null)
		{
			string returnText = RequestUtility.HttpGet(url, encoding);

			WeixinTrace.SendLog(url, returnText);

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

		#endregion

		#region 异步方法

		/// <summary>
		/// 异步GetJsonA
		/// </summary>
		/// <param name="url"></param>
		/// <param name="encoding"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		/// <exception cref="ErrorJsonResultException"></exception>
		public static async Task<T> GetJsonAsync<T>(string url, Encoding encoding = null)
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
		/// 异步从Url下载
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

		#endregion
	}
}
