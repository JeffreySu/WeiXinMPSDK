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
 
    文件名：TenPayNotifyHandler.cs
    文件功能描述：微信支付V3 回调请求handler
    
    创建标识：Senparc - 20210811

    修改标识：Senparc - 20210819
    修改描述：重构使用TenPaySignHelper类验证签名

----------------------------------------------------------------*/

using Microsoft.AspNetCore.Http;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Helpers;
using System;
using System.IO;
using System.Threading.Tasks;


namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 微信支付通知消息处理器
    /// </summary>
    public class TenPayNotifyHandler
    {
        readonly private NotifyRequest NotifyRequest;
        readonly private string Body;

        private ISenparcWeixinSettingForTenpayV3 _tenpayV3Setting { get; }
        private HttpContext _httpContext;


        /// <summary>
        /// 构造函数
        /// 注意:.NetCore环境必须传入HttpContext实例，不能传Null，这个接口调试特别困难，千万别出错！
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="senparcWeixinSettingForTenpayV3"></param>
        public TenPayNotifyHandler(HttpContext httpContext, ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            _ = httpContext ?? throw new ArgumentNullException(nameof(httpContext));

            _httpContext = httpContext;
            _tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;

            // 获得body
            if (_httpContext.Request.Method == "POST"
                || _httpContext.Request.Method == "PUT"
                || _httpContext.Request.Method == "PATCH")
            {
                using (var reader = new StreamReader(_httpContext.Request.Body))
                {
                    Body = reader.ReadToEndAsync().GetAwaiter().GetResult();
                    NotifyRequest = Body.GetObject<NotifyRequest>();
                }
            }
        }

        /// <summary>
        /// 将返回的结果中的ciphertext进行AEAD_AES_256_GCM解反序列化为实体
        /// 签名规则见微信官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_5.shtml
        /// </summary>
        /// <param name="aes_key">这里需要传入apiv3秘钥进行AEAD_AES_256_GCM解密 可空</param>
        /// <param name="nonce">加密的随机串 可空</param>
        /// <param name="associated_data">附加数据包 可空</param>
        /// <returns></returns>
        // TODO: 本方法持续测试
        public async Task<T> AesGcmDecryptGetObjectAsync<T>(string aes_key = null, string nonce = null, string associated_data = null) where T : ReturnJsonBase, new()
        {
            aes_key ??= _tenpayV3Setting.TenPayV3_APIv3Key;
            nonce ??= NotifyRequest.resource.nonce;
            associated_data ??= NotifyRequest.resource.associated_data;

            var decrypted_string = SecurityHelper.AesGcmDecryptCiphertext(aes_key, nonce, associated_data, NotifyRequest.resource.ciphertext);

            T result = decrypted_string.GetObject<T>();

            //验证请求签名
            var wechatpayTimestamp = _httpContext.Request.Headers?["Wechatpay-Timestamp"];
            var wechatpayNonce = _httpContext.Request.Headers?["Wechatpay-Nonce"];
            var wechatpaySignature = _httpContext.Request.Headers?["Wechatpay-Signature"];
            var wechatpaySerial = _httpContext.Request.Headers?["Wechatpay-Serial"];

            result.VerifySignSuccess = await TenPaySignHelper.VerifyTenpaySign(wechatpayTimestamp, wechatpayNonce, wechatpaySignature, Body, wechatpaySerial, this._tenpayV3Setting);
            result.ResultCode = new TenPayApiResultCode($"{_httpContext.Response.StatusCode} / {_httpContext.Request.Method}", "", "", "", result.VerifySignSuccess == true);

            return result;
        }
    }
}
