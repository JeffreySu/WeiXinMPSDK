#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2021 Senparc
 
    文件名：ResponseHandler.cs
    文件功能描述：新微信支付V3 响应处理
    

----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using Senparc.Weixin.Exceptions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.AspNet.HttpUtility;
using Senparc.CO2NET.Trace;
using Senparc.CO2NET;

using System.Web;
using Microsoft.AspNetCore.Http;


namespace Senparc.Weixin.TenPayV3
{
    public class ResponseHandler
    {
        /// <summary>
        /// 私钥
        /// </summary>
        private string PrivateKey;

        /// <summary>
        /// appkey
        /// </summary>
        private string Appkey;

        /// <summary>
        /// 应答签名
        /// </summary>
        public string WechatpaySignature { get; set; }

        /// <summary>
        /// 应答随机串
        /// </summary>
        public string WechatpayNonce { get; set; }

        /// <summary>
        /// 应答时间戳
        /// </summary>
        public string WechatpayTimestamp { get; set; }

        /// <summary>
        /// 请求的主体
        /// </summary>
        private string Body;

        /// <summary>
        /// debug信息
        /// </summary>
        private string DebugInfo;

        /// <summary>
        /// 原始内容
        /// </summary>
        protected string Content;

        protected HttpContext HttpContext;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// 获取页面提交的get和post参数
        /// 注意:.NetCore环境必须传入HttpContext实例，不能传Null，这个接口调试特别困难，千万别出错！
        /// </summary>
        /// <param name="httpContext"></param>
        public ResponseHandler(HttpContext httpContext)
        {

            this.HttpContext = httpContext ?? HttpContext.Current;

            WechatpayTimestamp = HttpContext.Request.Headers?["Wechatpay-Timestamp"];
            WechatpayNonce = HttpContext.Request.Headers?["Wechatpay-Nonce"];
            WechatpaySignature = HttpContext.Request.Headers?["Wechatpay-Signature"];

            //post data
            if (this.HttpContext.Request.HttpMethod == "POST")
            {
                using (var reader = new StreamReader(HttpContext.Request.InputStream))
                {
                    var body = reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 获取私钥
        /// </summary>
        /// <returns></returns>
        public string GetPrivateKey()
        { return PrivateKey; }

        /// <summary>
        /// 设置私钥
        /// </summary>
        /// <param name="pricateKey"></param>
        public void SetPrivateKey(string pricateKey)
        {
            this.PrivateKey = pricateKey;
        }

        /// <summary>
        /// 是否签名，商户必须验证回调的签名，以确保回调是由微信支付发送。
        /// 签名规则见微信官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay4_1.shtml。
        /// return boolean
        /// </summary>
        /// <returns></returns>
        public virtual Boolean IsTenpaySign()
        {
            string contentForSign = $"{WechatpayTimestamp}\n{WechatpayNonce}\n{Body}\n";

            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(PrivateKey);
            //签名返回
            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                var signData = rsa.SignData(Encoding.UTF8.GetBytes(contentForSign), sha256);
                return Convert.ToBase64String(signData).Equals(WechatpaySignature);
            }
        }

        /// <summary>
        /// 获取debug信息
        /// </summary>
        /// <returns></returns>
        public string GetDebugInfo()
        { return DebugInfo; }

        /// <summary>
        /// 设置debug信息
        /// </summary>
        /// <param name="debugInfo"></param>
        protected void SetDebugInfo(String debugInfo)
        { this.DebugInfo = debugInfo; }

        /// <summary>
        /// 获得字符集
        /// </summary>
        /// <returns></returns>
        protected virtual string GetCharset()
        {
            return this.HttpContext.Request.ContentEncoding.BodyName;
        }

        /// <summary>
        /// 输出XML
        /// </summary>
        /// <returns></returns>
        public string ParseXML()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<xml>");
            //foreach (string k in Parameters.Keys)
            //{
            //    string v = (string)Parameters[k];
            //    if (Regex.IsMatch(v, @"^[0-9.]$"))
            //    {

            //        sb.Append("<" + k + ">" + v + "</" + k + ">");
            //    }
            //    else
            //    {
            //        sb.Append("<" + k + "><![CDATA[" + v + "]]></" + k + ">");
            //    }

            //}
            //sb.Append("</xml>");
            //return sb.ToString();
        }
    }
}
