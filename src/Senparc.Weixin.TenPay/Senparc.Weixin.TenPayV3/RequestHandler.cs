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
    
    创建标识：Senparc - 20210811

----------------------------------------------------------------*/
using System;
using System.Collections;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Senparc.CO2NET.Helpers;

#if NET45
using System.Web;
#else
using Microsoft.AspNetCore.Http;
#endif
using Senparc.Weixin.Helpers;


namespace Senparc.Weixin.TenPay.V3
{
    public class RequestHandler
    {

        public RequestHandler()
            : this(null)
        {
        }


        public RequestHandler(HttpContext httpContext)
        {
            this.HttpContext = httpContext;
        }

        /// <summary>
        /// 密钥
        /// </summary>
        private string Key;

        protected HttpContext HttpContext;

        /// <summary>
        /// 请求主体
        /// </summary>
        protected string Body = "";

        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public string GetKey()
        {
            return Key;
        }

        /// <summary>
        /// 设置密钥
        /// </summary>
        /// <param name="key"></param>
        public void SetKey(string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// 设置请求主体
        /// </summary>
        /// <typeparam name="T">请求主体类型</typeparam>
        /// <param name="body">请求主体</param>
        public void SetBody<T>(T body)
        {
            this.Body = JsonConvert.SerializeObject(body);
        }

        /// <summary>
        /// 创建sha256 with RSA签名 签名规则详见 https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay4_0.shtml
        /// </summary>
        /// <param name="method">请求方法</param>
        /// <param name="uri">请求的绝对URL，并去除域名部分得到参与签名的URL。如果请求中有查询参数，URL末尾应附加有'?'和对应的查询字符串。如 /v3/certificates </param>
        /// <param name="timestamp">请求时间戳。微信支付会拒绝处理很久之前发起的请求，请商户自身系统的时间准确。</param>
        /// <param name="nonce">16位请求随机串</param>
        /// <param name="privateKey">私钥</param>
        /// NOTE： 私钥不包括私钥文件起始的-----BEGIN PRIVATE KEY-----
        ///        亦不包括结尾的-----END PRIVATE KEY-----
        /// <returns></returns>
        public string CreateSign(string method, string uri, string timestamp, string nonce, string privateKey)
        {
            if (method == "GET")
            {
                this.Body = "";
            }

            string contentForSign = $"{method}\n{uri}\n{timestamp}\n{nonce}\n{Body}\n";
            byte[] keyData = Convert.FromBase64String(privateKey);
            using (CngKey cngKey = CngKey.Import(keyData, CngKeyBlobFormat.Pkcs8PrivateBlob))
            using (RSACng rsa = new RSACng(cngKey))
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(contentForSign);
                return Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }
        }

        /// <summary>
        /// 输出请求主体的json数据
        /// </summary>
        /// <returns></returns>
        public string SerializeBody()
        {
            return Body;
        }
    }
}
