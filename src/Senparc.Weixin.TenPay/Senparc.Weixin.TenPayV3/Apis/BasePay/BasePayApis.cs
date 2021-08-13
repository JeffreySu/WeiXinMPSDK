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
  
    文件名：BasePayApis.cs
    文件功能描述：新微信支付V3基础接口
    
    
    创建标识：Senparc - 20210804

    修改标识：Senparc - 20210811
    修改描述：完成JsApi支付签名方法
    
----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.HttpUtility;
using Senparc.Weixin.TenPayV3.Apis.BasePay;
using Senparc.Weixin.TenPayV3.Apis.BasePay.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Senparc.NeuChar.Helpers;

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

        /// <summary>
        /// JSAPI下单接口
        /// 在微信支付服务后台生成 JSAPI 预支付交易单，返回预支付交易会话标识
        /// </summary>
        /// <param name="dataInfo">微信支付需要post的Data数据</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static JsApiNotifyJson JsApi(IServiceProvider serviceProvider, JsApiRequestData data,
            int timeOut = Config.TIME_OUT)
        {
            var url = ReurnPayApiUrl("https://api.mch.weixin.qq.com/{0}v3/pay/transactions/jsapi");
            var jsonString = SerializerHelper.GetJsonString(data, null);
            using (MemoryStream ms = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);

                WeixinTrace.SendApiPostDataLog(url, jsonString); //记录Post的Json数据

                //PostGetJson方法中将使用WeixinTrace记录结果
                return Post.PostGetJson<JsApiNotifyJson>(serviceProvider, url, null, ms,
                    timeOut: timeOut,
                    afterReturnText: null,
                    checkValidationResult: false);
            }
        }

        /// <summary>
        /// 获取UI使用的JS支付签名
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="timeStamp"></param>
        /// <param name="nonceStr"></param>
        /// <param name="package">格式：prepay_id={0}</param>
        /// <returns></returns>
        public static string GetJsApiPaySign(string appId, string timeStamp, string nonceStr, string package, string privateKey)
        {
            string contentForSign = $"{appId}\n{timeStamp}\n{nonceStr}\n{package}\n";

            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            //签名返回
            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                var signData = rsa.SignData(Encoding.UTF8.GetBytes(contentForSign), sha256);
                return Convert.ToBase64String(signData);
            }
        }
    }
}
