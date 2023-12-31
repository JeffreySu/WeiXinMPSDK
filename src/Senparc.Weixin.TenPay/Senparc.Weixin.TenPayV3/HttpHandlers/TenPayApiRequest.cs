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
  
    文件名：TenPayApiRequest.cs
    文件功能描述：微信支付V3接口请求
    
    
    创建标识：Senparc - 20210815

    修改标识：Senparc - 20210822
    修改描述：重构使用ISenparcWeixinSettingForTenpayV3初始化实例

    修改标识：Senparc - 20211225
    修改描述：v0.5.2 发布版本删除调试代码
    
----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 微信支付 API 请求
    /// </summary>
    public class TenPayApiRequest
    {
        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;
        private Action<HttpClient> _setHeaderAction;

        public TenPayApiRequest(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null, Action<HttpClient> setHeaderAction = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
            _setHeaderAction = setHeaderAction;
        }

        /// <summary>
        /// 设置 HTTP 请求头
        /// </summary>
        /// <param name="client"></param>
        public void SetHeader(HttpClient client)
        {
            //ACCEPT header
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            //User-Agent header
            var userAgentValues = UserAgentValues.Instance;
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Senparc.Weixin.TenPayV3-C#", userAgentValues.TenPayV3Version));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue($"(Senparc.Weixin {userAgentValues.SenparcWeixinVersion})"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(".NET", userAgentValues.RuntimeVersion));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue($"({userAgentValues.OSVersion})"));

            // 外部
            if (_setHeaderAction != null)
                _setHeaderAction(client);
        }

        /// <summary>
        /// 获取 HttpResponseMessage 对象
        /// </summary> 
        /// <param name="url"></param>
        /// <param name="data">如果为 GET 请求，此参数可为 null</param>
        /// <param name="timeOut"></param>
        /// <param name="requestMethod"></param>
        /// <param name="checkDataNotNull">非 GET 请求情况下，是否强制检查 data 参数不能为 null，默认为 true</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetHttpResponseMessageAsync(string url, object data, int timeOut = Config.TIME_OUT, ApiRequestMethod requestMethod = ApiRequestMethod.POST, bool checkDataNotNull = true)
        {
            try
            {
                //var co2netHttpClient = CO2NET.HttpUtility.RequestUtility.HttpPost_Common_NetCore(serviceProvider, url, out var hc, contentType: "application/json");

                ////设置参数
                //var mchid = _tenpayV3Setting.TenPayV3_MchId;
                //var ser_no = _tenpayV3Setting.TenPayV3_SerialNumber;
                //var privateKey = _tenpayV3Setting.TenPayV3_PrivateKey;

                ////使用微信支付参数，配置 HttpHandler
                //TenPayHttpHandler httpHandler = new(mchid, ser_no, privateKey);

                //TODO:此处重构使用ISenparcWeixinSettingForTenpayV3
                TenPayHttpHandler httpHandler = new(_tenpayV3Setting);

                //创建 HttpClient
                HttpClient client = new HttpClient(httpHandler);//TODO: 有资源消耗和效率问题
                //设置超时时间
                client.Timeout = TimeSpan.FromMilliseconds(timeOut);

                //设置 HTTP 请求头
                SetHeader(client);

                HttpResponseMessage responseMessage = null;
                switch (requestMethod)
                {
                    case ApiRequestMethod.GET:
                        responseMessage = await client.GetAsync(url);
                        WeixinTrace.Log(url); //记录Get的Json数据
                        break;
                    //TODO: 此处新增DELETE方法 待测试是否有问题
                    case ApiRequestMethod.DELETE:
                        responseMessage = await client.DeleteAsync(url);
                        WeixinTrace.Log(url); //记录Delete的Json数据
                        break;
                    case ApiRequestMethod.POST:
                    case ApiRequestMethod.PUT:
                    case ApiRequestMethod.PATCH:
                        //检查是否为空
                        if (checkDataNotNull)
                        {
                            _ = data ?? throw new ArgumentNullException($"{nameof(data)} 不能为 null！");
                        }

                        //设置请求 Json 字符串
                        //var jsonString = SerializerHelper.GetJsonString(data, new CO2NET.Helpers.Serializers.JsonSetting(true));
                        string jsonString = data != null
                            ? data.ToJson(false, new Newtonsoft.Json.JsonSerializerSettings() { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore })
                            : "";
                        WeixinTrace.SendApiPostDataLog(url, jsonString); //记录Post的Json数据

                        //设置 HttpContent
                        var hc = new StringContent(jsonString, Encoding.UTF8, mediaType: "application/json");
                        //获取响应结果
                        responseMessage = requestMethod switch
                        {
                            ApiRequestMethod.POST => await client.PostAsync(url, hc),
                            ApiRequestMethod.PUT => await client.PutAsync(url, hc),
                            ApiRequestMethod.PATCH => await client.PatchAsync(url, hc),
                            _ => throw new ArgumentOutOfRangeException(nameof(requestMethod))
                        };
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(requestMethod));
                }

                return responseMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 请求参数，获取结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data">如果为 GET 请求，此参数可为 null</param>
        /// <param name="timeOut"></param>
        /// <param name="requestMethod"></param>
        /// <param name="checkSign"></param>
        /// <param name="createDefaultInstance"></param>
        /// <returns></returns>
        public async Task<T> RequestAsync<T>(string url, object data, int timeOut = Config.TIME_OUT, ApiRequestMethod requestMethod = ApiRequestMethod.POST, bool checkSign = true, Func<T> createDefaultInstance = null)
            where T : ReturnJsonBase, new()
        {
            T result = null;

            try
            {
                HttpResponseMessage responseMessage = await GetHttpResponseMessageAsync(url, data, timeOut, requestMethod);

                //获取响应结果
                string content = await responseMessage.Content.ReadAsStringAsync();//TODO:如果不正确也要返回详情

#if DEBUG
                Console.WriteLine("Content:" + content + ",,Headers:" + responseMessage.Headers.ToString());
#endif

                //检查响应代码
                TenPayApiResultCode resultCode = TenPayApiResultCode.TryGetCode(responseMessage.StatusCode, content);

                if (resultCode.Success)
                {
                    if (resultCode.StateCode == ((int)HttpStatusCode.NoContent).ToString())
                    {
                        result = new T();
                        result.VerifySignSuccess = true;
                    }
                    else
                    {
                        //TODO:待测试
                        //验证微信签名
                        //result.Signed = VerifyTenpaySign(responseMessage.Headers, content);
                        var wechatpayTimestamp = responseMessage.Headers.GetValues("Wechatpay-Timestamp").First();
                        var wechatpayNonce = responseMessage.Headers.GetValues("Wechatpay-Nonce").First();
                        var wechatpaySignatureBase64 = responseMessage.Headers.GetValues("Wechatpay-Signature").First();//后续需要base64解码
                        var wechatpaySerial = responseMessage.Headers.GetValues("Wechatpay-Serial").First();

                        result = content.GetObject<T>();

                        if (checkSign)
                        {
                            try
                            {
                                var pubKey = await TenPayV3InfoCollection.GetAPIv3PublicKeyAsync(this._tenpayV3Setting, wechatpaySerial);
                                result.VerifySignSuccess = TenPaySignHelper.VerifyTenpaySign(wechatpayTimestamp, wechatpayNonce, wechatpaySignatureBase64, content, pubKey);
                            }
                            catch (Exception ex)
                            {
                                throw new TenpayApiRequestException("RequestAsync 签名验证失败：" + ex.Message, ex);
                            }
                        }
                    }
                }
                else
                {
                    result = createDefaultInstance?.Invoke() ?? GetInstance<T>(true);
                    resultCode.Additional = content;
                }
                //T result = resultCode.Success ? (await responseMessage.Content.ReadAsStringAsync()).GetObject<T>() : new T();
                result.ResultCode = resultCode;

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
