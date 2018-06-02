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
    
    文件名：JSSDKHelper.cs
    文件功能描述：JSSDK生成签名的方法等
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：HADB - 20150512
    修改描述：将方法调用改为静态方式

    修改标识：Senparc - 20170203
    修改描述：优化代码，更新到最新的Helpers方法调用

    修改标识：Senparc - 20170203
    修改描述：v14.3.137 修改 JSSDKHelper.GetAddrSign 传入参数，应该传入OAuth的AccessToken

    修改标识：Senparc - 20170327
    修改描述：v14.3.138 修改 JSSDKHelper.GetAddrSign() 方法
    
    修改标识：Senparc - 20170623
    修改描述：v14.4.14 修改 JSSDKHelper.GetcardExtSign()和CreateNonekeySha1() 方法，使用 ASCII 字典排序
                          排序规则统一为字典排序（ASCII）
                          
    修改标识：Senparc - 20170817
    修改描述：v14.6.3 添加 JSSDKHelper.GetJsSdkUiPackageAsync() 异步方法

    修改标识：Senparc - 20171010
    修改描述：v14.8.1 修复几处GetNoncestr还在使用GBK编码

----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Helpers;

using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;
using Senparc.CO2NET.Helpers;

namespace Senparc.Weixin.MP.Helpers
{
    /// <summary>
    /// JS-SDK 帮助类
    /// </summary>
    public class JSSDKHelper
    {
        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <returns></returns>
        public static string GetNoncestr()
        {
            return EncryptHelper.GetMD5(Guid.NewGuid().ToString(), "UTF-8");
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimestamp()
        {
            //var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //return Convert.ToInt64(ts.TotalSeconds).ToString();
            var ts = DateTimeHelper.GetWeixinDateTime(DateTime.Now);
            return ts.ToString();
        }

        /// <summary>
        /// sha1加密
        /// </summary>
        /// <returns></returns>
        private static string CreateSha1(Hashtable parameters)
        {
            var sb = new StringBuilder();
            var akeys = new ArrayList(parameters.Keys);
            akeys.Sort(ASCIISort.Create());

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
            return EncryptHelper.GetSha1(sb.ToString());
        }

        /// <summary>
        /// 生成cardSign的加密方法
        /// </summary>
        /// <returns></returns>
        private static string CreateCardSha1(Hashtable parameters)
        {
            var sb = new StringBuilder();
            var akeys = new ArrayList(parameters.Keys);
            akeys.Sort(ASCIISort.Create());

            foreach (var k in akeys)
            {
                if (parameters[k] != null)
                {
                    var v = (string)parameters[k];
                    sb.Append(v);
                }
            }
            return EncryptHelper.GetSha1(sb.ToString()).ToString().ToLower();
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
            aValues.Sort(ASCIISort.Create());
            foreach (var v in aValues)
            {
                sb.Append(v);
            }
            return EncryptHelper.GetSha1(sb.ToString()).ToString().ToLower();
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
        /// <param name="oauthAccessToken">必须是OAuth的AccessToken</param>
        /// <param name="noncestr"></param>
        /// <param name="timestamp"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetAddrSign(string appId, string oauthAccessToken, string noncestr, string timestamp,
            string url)
        {
            //TODO:此处的accessToken应该为OAuth的AccessToken
            //var accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret);
            var parameters = new Hashtable();
            parameters.Add("appid", appId);
            parameters.Add("noncestr", noncestr);
            parameters.Add("timestamp", timestamp);
            parameters.Add("url", url);
            parameters.Add("accesstoken", oauthAccessToken);
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
        public static string GetCardSign(string appId, string appSecret, string locationId, string noncestr,
            string timestamp, string cardId, string cardType)
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
        public static string GetcardExtSign(string api_ticket, string timestamp, string card_id, string nonce_str = "",
            string code = "", string openid = "")
        {
            var parameters = new Hashtable();
            parameters.Add("api_ticket", api_ticket);
            parameters.Add("timestamp", timestamp);
            parameters.Add("card_id", card_id);
            if (!string.IsNullOrEmpty(code))
            {
                parameters.Add("code", code);
            }
            if (!string.IsNullOrEmpty(openid))
            {
                parameters.Add("openid", openid);
            }
            if (!string.IsNullOrEmpty(nonce_str))
            {
                parameters.Add("nonce_str", nonce_str);
            }
            return CreateNonekeySha1(parameters);
        }

        /// <summary>
        /// 获取给UI使用的JSSDK信息包
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static JsSdkUiPackage GetJsSdkUiPackage(string appId, string appSecret, string url)
        {
            //获取时间戳
            var timestamp = GetTimestamp();
            //获取随机码
            string nonceStr = GetNoncestr();
            string ticket = JsApiTicketContainer.TryGetJsApiTicket(appId, appSecret);
            //获取签名
            string signature = JSSDKHelper.GetSignature(ticket, nonceStr, timestamp, url);
            //返回信息包
            return new JsSdkUiPackage(appId, timestamp, nonceStr, signature);
        }


#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】获取给UI使用的JSSDK信息包
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<JsSdkUiPackage> GetJsSdkUiPackageAsync(string appId, string appSecret, string url)
        {
            //获取时间戳
            var timestamp = GetTimestamp();
            //获取随机码
            string nonceStr = GetNoncestr();
            string ticket = await JsApiTicketContainer.TryGetJsApiTicketAsync(appId, appSecret);
            //获取签名
            string signature = JSSDKHelper.GetSignature(ticket, nonceStr, timestamp, url);
            //返回信息包
            return new JsSdkUiPackage(appId, timestamp, nonceStr, signature);
        }

        #endregion
#endif
    }
}

