#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
  
    文件名：TenPayHttpHandler.cs
    文件功能描述：微信支付V3 HttpHandler
    
    
    创建标识：Senparc - 20210815

    修改标识：Senparc - 20210822
    修改描述：重构使用ISenparcWeixinSettingForTenpayV3初始化实例

    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 微信支付 HttpHandler
    /// </summary>
    public class TenPayHttpHandler : DelegatingHandler
    {
        //private readonly string merchantId;
        //private readonly string serialNo;
        //private readonly string privateKey;

        //public TenPayHttpHandler(string merchantId, string merchantSerialNo, string privateKey)
        //{
        //    InnerHandler = new HttpClientHandler();

        //    this.merchantId = merchantId;
        //    this.serialNo = merchantSerialNo;
        //    this.privateKey = privateKey;
        //}

        //TODO: 此处重构使用ISenparcWeixinSettingForTenpayV3初始化实例
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        public TenPayHttpHandler(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            InnerHandler = new HttpClientHandler();

            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
        }

        /// <summary>
        /// 重写 SendAsync 方法
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var auth = await BuildAuthAsync(request);
            string value = $"WECHATPAY2-SHA256-RSA2048 {auth}";
            request.Headers.Add("Authorization", value);

            return await base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// 生成 Authorization 头
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected async Task<string> BuildAuthAsync(HttpRequestMessage request)
        {
            string method = request.Method.ToString();
            string body = "";
            if (method == "POST" || method == "PUT" || method == "PATCH")
            {
                var content = request.Content;
                body = await content.ReadAsStringAsync();
            }

            string uri = request.RequestUri.PathAndQuery;
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            string nonce = Path.GetRandomFileName();

            string message = $"{method}\n{uri}\n{timestamp}\n{nonce}\n{body}\n";
            ////此处重构待测试
            //string signature = TenPaySignHelper.CreateSign(message, privateKey);

            //return $"mchid=\"{merchantId}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",serial_no=\"{serialNo}\",signature=\"{signature}\"";

            //TODO:此处重构使用ISenparcWeixinSettingForTenpayV3
            string signature = TenPaySignHelper.CreateSign(message, _tenpayV3Setting.TenPayV3_PrivateKey);

            return $"mchid=\"{_tenpayV3Setting.TenPayV3_MchId}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",serial_no=\"{_tenpayV3Setting.TenPayV3_SerialNumber}\",signature=\"{signature}\"";
        }
    }
}
