/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：JSSDKHelper.cs
    文件功能描述：JSSDK生成签名的方法等
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：HADB - 20150512
    修改描述：将方法调用改为静态方式
----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.Helpers
{
    public class JSSDKHelper
    {
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            var random = new Random();
            return MD5UtilHelper.GetMD5(random.Next(1000).ToString(), "GBK");
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// sha1加密
        /// </summary>
        /// <returns></returns>
        private static string CreateSha1(Hashtable parameters)
        {
            var sb = new StringBuilder();
            var akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (var k in akeys)
            {
                if (parameters[k] != null)
                {
                    var v = (string)parameters[k];

                    if (sb.Length == 0)
                    {
                        sb.Append(k + "=" + v);
                    }
                    else
                    {
                        sb.Append("&" + k + "=" + v);
                    }
                }
            }
            return SHA1UtilHelper.GetSha1(sb.ToString()).ToLower();
        }

        /// <summary>
        /// 生成cardSign的加密方法
        /// </summary>
        /// <returns></returns>
        private static string CreateCardSha1(Hashtable parameters)
        {
            var sb = new StringBuilder();
            var akeys = new ArrayList(parameters.Keys);
            akeys.Sort();

            foreach (var k in akeys)
            {
                if (parameters[k] != null)
                {
                    var v = (string)parameters[k];
                    sb.Append(v);
                }
            }
            return SHA1UtilHelper.GetSha1(sb.ToString()).ToString().ToLower();
        }
		/// <summary>
		/// 添加卡券Ext参数的签名加密方法
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private static string CreateNonekeySha1(Hashtable parameters)
		{
			var sb = new StringBuilder();
			var aValues = new ArrayList(parameters.Values);
			aValues.Sort();

			foreach (var v in aValues)
			{
				sb.Append(v);
			}
			return SHA1UtilHelper.GetSha1(sb.ToString()).ToString().ToLower();
		}
        /// <summary>
        /// 获取JS-SDK权限验证的签名Signature
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="noncestr"></param>
        /// <param name="timestamp"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetSignature(string ticket, string noncestr, string timestamp, string url)
        {
            var parameters = new Hashtable();
            parameters.Add("jsapi_ticket", ticket);
            parameters.Add("noncestr", noncestr);
            parameters.Add("timestamp", timestamp);
            parameters.Add("url", url);
            return CreateSha1(parameters);
        }

        /// <summary>
        /// 获取位置签名AddrSign
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="noncestr"></param>
        /// <param name="timestamp"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetAddrSign(string appId, string appSecret, string noncestr, string timestamp, string url)
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            var parameters = new Hashtable();
            parameters.Add("appId", appId);
            parameters.Add("noncestr", noncestr);
            parameters.Add("timestamp", timestamp);
            parameters.Add("url", url);
            parameters.Add("accesstoken", accessToken);
            return CreateSha1(parameters);
        }

        /// <summary>
        /// 获取卡券签名CardSign
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="locationId"></param>
        /// <param name="noncestr"></param>
        /// <param name="timestamp"></param>
        /// <param name="cardId"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public static string GetCardSign(string appId, string appSecret, string locationId, string noncestr, string timestamp, string cardId, string cardType)
        {
            var parameters = new Hashtable();
            parameters.Add("appId", appId);
            parameters.Add("appsecret", appSecret);
            parameters.Add("location_id", locationId);
            parameters.Add("nonce_str", noncestr);
            parameters.Add("times_tamp", timestamp);
            parameters.Add("card_id", cardId);
            parameters.Add("card_type", cardType);
            return CreateCardSha1(parameters);
        }

		/// <summary>
		///获取添加卡券时Ext参数内的签名 
		/// </summary>
		/// <param name="api_ticket"></param>
		/// <param name="timestamp"></param>
		/// <param name="card_id"></param>
		/// <param name="nonce_str"></param>
		/// <param name="code"></param>
		/// <param name="openid"></param>
		/// <returns></returns>
		public static string GetcardExtSign(string api_ticket, string timestamp, string card_id, string nonce_str, string code = "", string openid = "")
		{
			var parameters = new Hashtable();
			parameters.Add("api_ticket", api_ticket);
			parameters.Add("timestamp", timestamp);
			parameters.Add("card_id", card_id);
			parameters.Add("code", code);
			parameters.Add("openid", openid);
			parameters.Add("nonce_str", nonce_str);
			return CreateNonekeySha1(parameters);
		}

        /// <summary>
        /// 获取给UI使用的JSSDK信息包
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static JsSdkUiPackage GetJsSdkUiPackage(string appId ,string appSecret,string url)
        {
            //获取时间戳
            var timestamp = GetTimestamp();
            //获取随机码
            string nonceStr = GetNoncestr();
            string ticket = JsApiTicketContainer.TryGetJsApiTicket(appId,appSecret);
            //获取签名
            string signature = JSSDKHelper.GetSignature(ticket, nonceStr, timestamp, url);
            //返回信息包
            return new JsSdkUiPackage(appId,timestamp,nonceStr,signature);
        }
    }
}
