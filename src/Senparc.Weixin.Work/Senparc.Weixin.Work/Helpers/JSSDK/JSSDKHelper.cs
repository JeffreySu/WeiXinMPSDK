/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：JSSDKHelper.cs
    文件功能描述：JSSDK生成签名的方法等
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20170516
    修改描述：v4.2.4 修改JSSDKHelper.GetNoncestr()方法
    
    修改标识：Senparc - 20170522
    修改描述：v14.4.9 修改TenPayUtil.GetNoncestr()方法，将编码由GBK改为UTF8


----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers;
using Senparc.Weixin.Helpers;
using System;
using System.Collections;
using System.Text;

namespace Senparc.Weixin.Work.Helpers
{
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
        /// <remarks>
        /// 2016-05-03：修改返回类型，方便GetSignature调用，避免再次类型转换
        /// </remarks>
        /// </summary>
        /// <returns></returns>
        public static long GetTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        /// <summary>
        /// 获取JS-SDK权限验证的签名Signature
        /// </summary>
        /// <param name="jsapi_ticket">jsapi_ticket</param>
        /// <param name="noncestr">随机字符串(必须与wx.config中的nonceStr相同)</param>
        /// <param name="timestamp">时间戳(必须与wx.config中的timestamp相同)</param>
        /// <param name="url">当前网页的URL，不包含#及其后面部分(必须是调用JS接口页面的完整URL)</param>
        /// <returns></returns>
        public static string GetSignature(string jsapi_ticket, string noncestr, long timestamp, string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("jsapi_ticket=").Append(jsapi_ticket).Append("&")
             .Append("noncestr=").Append(noncestr).Append("&")
             .Append("timestamp=").Append(timestamp).Append("&")
             .Append("url=").Append(url.IndexOf("#") >= 0 ? url.Substring(0, url.IndexOf("#")) : url);
            return EncryptHelper.GetSha1(sb.ToString()).ToLower();
        }


    }
}
