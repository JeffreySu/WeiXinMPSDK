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
  
    文件名：TenPayHttpClient.cs
    文件功能描述：TenPayHttpClient
    
    
    创建标识：Senparc - 20210920

    修改标识：Senparc - 20220511
    修改描述：v0.6.2.2 TenPayHttpClient 赋值问题

    
----------------------------------------------------------------*/

using Client.TenPayHttpClient.Signer;
using Senparc.CO2NET;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.HttpUtility;
using Senparc.CO2NET.Trace;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.TenPayHttpClient.Verifier;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.TenPayHttpClient
{

    public class BasePayApis2
    {
        private readonly SenparcHttpClient _httpClient;
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;
        private readonly CertType _certType;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public BasePayApis2(SenparcHttpClient httpClient, ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null, CertType certType = CertType.RSA)
        {
            this._httpClient = httpClient;
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
            this._certType = certType;
        }

        /// <summary>
        /// 返回可用的微信支付地址（自动判断是否使用沙箱）
        /// </summary>
        /// <param name="urlFormat">如：<code>https://api.mch.weixin.qq.com/{0}pay/unifiedorder</code></param>
        /// <returns></returns>
        private static string ReturnPayApiUrl(string urlFormat)
        {
            //注意：目前微信支付 V3 还没有支持沙箱，此处只是预留
            return string.Format(urlFormat, Senparc.Weixin.Config.UseSandBoxPay ? "sandboxnew/" : "");
        }

        /// <summary>
        /// JSAPI下单接口
        /// <para>在微信支付服务后台生成JSAPI预支付交易单，返回预支付交易会话标识</para>
        /// </summary>
        /// <param name="data">微信支付需要POST的Data数据</param>
        /// <param name="timeOut">超时时间，单位为ms </param>
        /// <returns></returns>
        public async Task<JsApiReturnJson> JsApiAsync(TransactionsRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReturnPayApiUrl(Senparc.Weixin.Config.TenPayV3Host + "/{0}v3/pay/transactions/jsapi");

            HttpClient hc = null;//注入
            TenPayHttpClient tenPayApiRequest = new(_httpClient, _tenpayV3Setting);
            return await tenPayApiRequest.SendAsync<JsApiReturnJson>(url, data, timeOut);
        }
    }

    public class TenPayHttpClient
    {
        private readonly SenparcHttpClient _httpClient;
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;
        private readonly HttpClient _client;
        private readonly ISigner _signer;
        private readonly IVerifier _verifier;

        public TenPayHttpClient(SenparcHttpClient httpClient, ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null, CertType certType = CertType.RSA)
        {
            this._httpClient = httpClient;
            this._client = this._httpClient.Client;
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;

            //从工厂获得签名和验签的方法类
            _signer = TenPayCertFactory.GetSigner(certType);
            _verifier = TenPayCertFactory.GetVerifier(certType);

            #region 配置UA

            //ACCEPT header
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            //User-Agent header
            var userAgentValues = UserAgentValues.Instance;
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Senparc.Weixin.TenPayV3-C#", userAgentValues.TenPayV3Version));
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue($"(Senparc.Weixin {userAgentValues.SenparcWeixinVersion})"));
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(".NET", userAgentValues.RuntimeVersion));
            _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue($"({userAgentValues.OSVersion})"));

            #endregion
        }

        public async Task<T> SendAsync<T>(string url, object data, int timeOut = Senparc.Weixin.Config.TIME_OUT, ApiRequestMethod requestMethod = ApiRequestMethod.POST, bool checkSign = true, Func<T> createDefaultInstance = null)
                   where T : ReturnJsonBase/*, new()*/
        {
            T result = null;
            HttpMethod method = GetHttpMethod(requestMethod);

            try
            {
                var request = new HttpRequestMessage(method, url);

                //设置超时时间
                _client.Timeout = TimeSpan.FromMilliseconds(timeOut);

                //设置请求 Json 字符串
                string jsonString = data != null
                    ? data.ToJson(false, new Newtonsoft.Json.JsonSerializerSettings() { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore })
                    : "";
                WeixinTrace.SendApiPostDataLog(url, jsonString); //记录Post的Json数据
                request.Content = new StringContent(jsonString, Encoding.UTF8, mediaType: "application/json");

                // 进行签名
                var authorization = await GenerateAuthorizationHeader(request);
                request.Headers.Add("Authorization", $"WECHATPAY2-{_signer.GetAlgorithm()} {authorization}");

                // 发送请求
                var responseMessage = await _client.SendAsync(request);

                //获取响应结果
                string content = await responseMessage.Content.ReadAsStringAsync();//TODO:如果不正确也要返回详情
#if DEBUG
                Console.WriteLine("Content:" + content + ",,Headers:" + responseMessage.Headers.ToString());
#endif

                //检查响应代码
                TenPayApiResultCode resutlCode = TenPayApiResultCode.TryGetCode(responseMessage.StatusCode, content);

                if (resutlCode.Success)
                {
                    result = content.GetObject<T>();

                    if (checkSign)
                    {
                        result.VerifySignSuccess = await VerifyResponseMessage(responseMessage, content);
                    }
                }
                else
                {
                    result = createDefaultInstance?.Invoke() ?? GetInstance<T>(true);
                    resutlCode.Additional = content;
                }
                result.ResultCode = resutlCode;

                return result;
            }
            catch (Exception ex)
            {
                SenparcTrace.BaseExceptionLog(ex);
                result = createDefaultInstance?.Invoke() ?? GetInstance<T>(false);
                if (result != null)
                {
                    result.ResultCode = new() { ErrorMessage = ex.Message };
                }

                return result;
            }
        }

        protected HttpMethod GetHttpMethod(ApiRequestMethod requestMethod)
        {
            return requestMethod switch
            {
                ApiRequestMethod.GET => HttpMethod.Get,
                ApiRequestMethod.POST => HttpMethod.Post,
                ApiRequestMethod.PUT => HttpMethod.Put,
                ApiRequestMethod.PATCH => HttpMethod.Patch,
                ApiRequestMethod.DELETE => HttpMethod.Delete,
                _ => throw new ArgumentOutOfRangeException(nameof(requestMethod)),
            };
        }

        protected async Task<string> GenerateAuthorizationHeader(HttpRequestMessage request)
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

            string signature = _signer.Sign(message, _tenpayV3Setting.TenPayV3_PrivateKey);

            return $"mchid=\"{_tenpayV3Setting.TenPayV3_MchId}\",nonce_str=\"{nonce}\",timestamp=\"{timestamp}\",serial_no=\"{_tenpayV3Setting.TenPayV3_SerialNumber}\",signature=\"{signature}\"";
        }

        protected async Task<bool> VerifyResponseMessage(HttpResponseMessage responseMessage, string content)
        {
            var wechatpayTimestamp = responseMessage.Headers.GetValues("Wechatpay-Timestamp").First();
            var wechatpayNonce = responseMessage.Headers.GetValues("Wechatpay-Nonce").First();
            var wechatpaySignatureBase64 = responseMessage.Headers.GetValues("Wechatpay-Signature").First();//后续需要base64解码
            var wechatpaySerial = responseMessage.Headers.GetValues("Wechatpay-Serial").First();

            try
            {
                var pubKey = await TenPayV3InfoCollection.GetAPIv3PublicKeyAsync(this._tenpayV3Setting, wechatpaySerial);
                return _verifier.Verify(wechatpayTimestamp, wechatpayNonce, wechatpaySignatureBase64, content, pubKey);
            }
            catch (Exception ex)
            {
                throw new TenpayApiRequestException("RequestAsync 签名验证失败：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="throwIfFaild"></param>
        /// <returns></returns>
        private T GetInstance<T>(bool throwIfFaild)
            where T : ReturnJsonBase
        {
            if (typeof(T).IsClass)
            {
                return Senparc.CO2NET.Helpers.ReflectionHelper.CreateInstance<T>(typeof(T).FullName, typeof(T).Assembly.GetName().Name);
            }
            else if (throwIfFaild)
            {
                throw new TenpayApiRequestException("GetInstance 失败，此类型无法自动生成：" + typeof(T).FullName);
            }
            return null;
        }
    }
}
