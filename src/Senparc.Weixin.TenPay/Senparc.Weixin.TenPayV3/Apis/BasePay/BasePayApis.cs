using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Apis.BasePay.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis
{
    public static class BasePayApis
    {
        //private readonly IServiceProvider _serviceProvider;

        //public BasePayApis(IServiceProvider serviceProvider)
        //{
        //    this._serviceProvider = serviceProvider;
        //}

        /// <summary>
        /// 返回可用的微信支付地址（自动判断是否使用沙箱）
        /// </summary>
        /// <param name="urlFormat">如：<code>https://api.mch.weixin.qq.com/{0}pay/unifiedorder</code></param>
        /// <returns></returns>
        private static string ReurnPayApiUrl(string urlFormat)
        {
            return string.Format(urlFormat, Senparc.Weixin.Config.UseSandBoxPay ? "sandboxnew/" : "");
        }

        public static JsApiReturnJson JsApi(IServiceProvider serviceProvider, JsApiRequestData data, int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/pay/transactions/jsapi");
            var jsonString = SerializerHelper.GetJsonString(data, null);
            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);

                WeixinTrace.SendApiPostDataLog(url, jsonString);//记录Post的Json数据

                //PostGetJson方法中将使用WeixinTrace记录结果
                return Post.PostGetJson<JsApiReturnJson>(serviceProvider, url, null, ms,
                    timeOut: timeOut,
                    afterReturnText: null,
                    checkValidationResult: false);
            }
        }
    }
}
