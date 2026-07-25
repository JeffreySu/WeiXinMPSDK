#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc

    文件名：TenPayHttpHandler.cs
    文件功能描述：微信支付V3 HttpHandler


    创建标识：Senparc - 20210815

    修改标识：Senparc - 20210822
    修改描述：重构使用ISenparcWeixinSettingForTenpayV3初始化实例

    修改标识：MartyZane - 20240530
    修改描述：TenPayV3Setting里面增加AuthrizationType属性，用于设置认证类型，选项有WECHATPAY2-SHA256-RSA2048、WECHATPAY2-SM2-WITH-SM3，默认为WECHATPAY2-SM2-WITH-SM3

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

    修改标识：Senparc - 20260725
    修改描述：v2.5.1 支持微信支付品牌 API 专用 RSA 鉴权

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Helpers;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 微信支付 HttpHandler
    /// </summary>
    public class TenPayHttpHandler : DelegatingHandler
    {
        internal const string SignatureBodyHeader = "X-Senparc-WeChatPay-Signature-Body";
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
        private readonly ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;
        private readonly TenPayBrandApiCredentials _brandApiCredentials;

        public TenPayHttpHandler(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
            : this(senparcWeixinSettingForTenpayV3, null)
        {
        }

        internal TenPayHttpHandler(TenPayBrandApiCredentials brandApiCredentials)
            : this(null, brandApiCredentials ??
                throw new ArgumentNullException(nameof(brandApiCredentials)))
        {
        }

        private TenPayHttpHandler(
            ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3,
            TenPayBrandApiCredentials brandApiCredentials)
        {
            InnerHandler = new HttpClientHandler();

            _brandApiCredentials = brandApiCredentials;
            if (_brandApiCredentials != null)
            {
                return;
            }

            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;

            if (!_tenpayV3Setting.EncryptionType.HasValue)
            {
                throw new Senparc.Weixin.Exceptions.WeixinException("没有设置证书加密类型（EncryptionType）");
            }
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

            var authorizationValue = _brandApiCredentials != null
                ? TenPayBrandApiCredentials.AuthorizationType
                : _tenpayV3Setting.EncryptionType == CertType.SM
                    ? "WECHATPAY2-SM2-WITH-SM3"
                    : "WECHATPAY2-SHA256-RSA2048";

            string value = $"{authorizationValue} {auth}";
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
                if (request.Headers.TryGetValues(SignatureBodyHeader, out var signatureBodies))
                {
                    var encodedBody = signatureBodies.FirstOrDefault();
                    request.Headers.Remove(SignatureBodyHeader);
                    body = string.IsNullOrEmpty(encodedBody)
                        ? ""
                        : Encoding.UTF8.GetString(Convert.FromBase64String(encodedBody));
                }
                else
                {
                    var content = request.Content;
                    body = content == null ? "" : await content.ReadAsStringAsync();
                }
            }

            string uri = request.RequestUri.PathAndQuery;
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            string nonce = Path.GetRandomFileName();

            string message = $"{method}\n{uri}\n{timestamp}\n{nonce}\n{body}\n";
            ////此处重构待测试
            //string signature = TenPaySignHelper.CreateSign(message, privateKey);

            //return $"mchid=\"{merchantId}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",serial_no=\"{serialNo}\",signature=\"{signature}\"";

            //TODO:此处重构使用ISenparcWeixinSettingForTenpayV3
            if (_brandApiCredentials != null)
            {
                var brandSignature = TenPaySignHelper.CreateSign(CertType.RSA,
                    message, _brandApiCredentials.BrandPrivateKey);
                return $"brand_id=\"{_brandApiCredentials.BrandId}\"," +
                       $"nonce_str=\"{nonce}\",timestamp=\"{timestamp}\"," +
                       $"serial_no=\"{_brandApiCredentials.BrandSerialNumber}\"," +
                       $"signature=\"{brandSignature}\"";
            }

            string signature = string.Empty;
            if (_tenpayV3Setting.EncryptionType == CertType.SM)
            {
                byte[] keyData = Convert.FromBase64String(_tenpayV3Setting.TenPayV3_PrivateKey);

                ECPrivateKeyParameters eCPrivateKeyParameters = SMPemHelper.LoadPrivateKeyToParameters(keyData);

                byte[] signBytes = GmHelper.SignSm3WithSm2(eCPrivateKeyParameters, message);

                signature = Convert.ToBase64String(signBytes);
            }
            else
            {
                signature = TenPaySignHelper.CreateSign(_tenpayV3Setting.EncryptionType.Value, message, _tenpayV3Setting.TenPayV3_PrivateKey);
            }

            return $"mchid=\"{_tenpayV3Setting.TenPayV3_MchId}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",serial_no=\"{_tenpayV3Setting.TenPayV3_SerialNumber}\",signature=\"{signature}\"";
        }
    }
}
