using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.TenPayV3.Apis.BasePay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.HttpHandlers;
using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using System.Reflection.PortableExecutable;
using System.Reflection;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 微信支付 API 请求
    /// </summary>
    public class TenPayApiRequest
    {
        /// <summary>
        /// 当前模块版本号
        /// </summary>
        private static string SenparcWeixinVersion = typeof(Senparc.Weixin.Config).Assembly.GetName().Version.ToString();
        /// <summary>
        /// 当前模块版本
        /// </summary>
        private static string TenPayV3Version = typeof(TenPayApiRequest).Assembly.GetName().Version.ToString();
        /// <summary>
        /// 操作系统名称及版本
        /// </summary>
        private static string OSVersion = Environment.OSVersion.ToString();
        /// <summary>
        /// 当前 .NET 运行时版本
        /// </summary>
        private static string RuntimeVersion = Environment.Version.ToString();

        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting;

        public TenPayApiRequest(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;
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
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Senparc.Weixin.TenPayV3-C#", TenPayV3Version));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue($"(Senparc.Weixin {SenparcWeixinVersion})"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(".NET", RuntimeVersion));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue($"({OSVersion})"));
        }

        /// <summary>
        /// Post 参数，获取结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data">如果为 GET 请求，此参数可为 null</param>
        /// <returns></returns>
        public async Task<T> RequestAsync<T>(string url, object data, int timeOut = Config.TIME_OUT, ApiRequestMethod requestMethod = ApiRequestMethod.POST) where T : ReturnJsonBase, new()
        {
            //var co2netHttpClient = CO2NET.HttpUtility.RequestUtility.HttpPost_Common_NetCore(serviceProvider, url, out var hc, contentType: "application/json");

            //设置参数
            var mchid = _tenpayV3Setting.TenPayV3_MchId;
            var ser_no = _tenpayV3Setting.TenPayV3_SerialNumber;
            var privateKey = _tenpayV3Setting.TenPayV3_PrivateKey;

            //使用微信支付参数，配置 HttpHandler
            TenPayHttpHandler httpHandler = new(mchid, ser_no, privateKey);

            //创建 HttpClient
            HttpClient client = new HttpClient(httpHandler);
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
                case ApiRequestMethod.POST:
                case ApiRequestMethod.PUT:
                case ApiRequestMethod.PATCH:
                    //检查是否为空
                    _ = data ?? throw new ArgumentNullException($"{nameof(data)} 不能为 null！");

                    //设置请求 Json 字符串
                    //var jsonString = SerializerHelper.GetJsonString(data, new CO2NET.Helpers.Serializers.JsonSetting(true));
                    string jsonString = data.ToJson(false, new Newtonsoft.Json.JsonSerializerSettings() { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore });
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

            //检查响应代码
            TenPayApiResultCode resutlCode = TenPayApiResultCode.TryGetCode(responseMessage.StatusCode);

            //获取响应结果
            T result = resutlCode.Success ? (await responseMessage.Content.ReadAsStringAsync()).GetObject<T>() : new T();
            result.ResultCode = resutlCode;

            return result;
        }
    }
}
