using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.HttpHandlers
{
    /// <summary>
    /// 微信支付 HttpHandler
    /// </summary>
    public class TenPayHttpHandler : DelegatingHandler
    {
        private readonly string merchantId;
        private readonly string serialNo;
        private readonly string privateKey;

        public TenPayHttpHandler(string merchantId, string merchantSerialNo, string privateKey)
        {
            InnerHandler = new HttpClientHandler();

            this.merchantId = merchantId;
            this.serialNo = merchantSerialNo;
            this.privateKey = privateKey;
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
            string signature = Sign(message);
            return $"mchid=\"{merchantId}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",serial_no=\"{serialNo}\",signature=\"{signature}\"";
        }

        /// <summary>
        /// 签名
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay4_0.shtml</para>
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected string Sign(string message)
        {
            // NOTE： 私钥不包括私钥文件起始的-----BEGIN PRIVATE KEY-----
            //        亦不包括结尾的-----END PRIVATE KEY-----
            //string privateKey = "{你的私钥}";
            byte[] keyData = Convert.FromBase64String(privateKey);
            using (CngKey cngKey = CngKey.Import(keyData, CngKeyBlobFormat.Pkcs8PrivateBlob))
            using (RSACng rsa = new RSACng(cngKey))
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
                return Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }
        }
    }
}
