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
  
    文件名：TenPaySignHelper.cs
    文件功能描述：微信支付V3签名Helper类 可用于创建签名 验证签名
    
    
    创建标识：Senparc - 20210819

    修改标识：Senparc - 20211002
    修改描述：v0.3.500.4-preview4.3 TenPaySignHelper.CreateSign() 支持 Linux 和 Windows 环境
    
    修改标识：Senparc - 20231010
    修改描述：v1.1.0 TenPaySignHelper.GetJsApiUiPackage() 方法添加 senparcWeixinSettingForTenpayV3 参数
    
----------------------------------------------------------------*/


using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Helpers
{
    public class TenPaySignHelper
    {
        /// <summary>
        /// 创建签名
        /// <para>https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay4_0.shtml</para>
        /// </summary>
        /// <param name="message">签名串</param>
        /// <param name="privateKey">签名私钥 可为空</param>
        /// <returns></returns>
        public static string CreateSign(string message, string privateKey = null)
        {
            privateKey ??= Senparc.Weixin.Config.SenparcWeixinSetting.TenPayV3_PrivateKey;

            // NOTE： 私钥不包括私钥文件起始的-----BEGIN PRIVATE KEY-----
            //        亦不包括结尾的-----END PRIVATE KEY-----
            //string privateKey = "{你的私钥}";

            byte[] keyData = Convert.FromBase64String(privateKey);

            #region 以下方法不兼容 Linux
            //using (CngKey cngKey = CngKey.Import(keyData, CngKeyBlobFormat.Pkcs8PrivateBlob))
            //using (RSACng rsa = new RSACng(cngKey))
            //{
            //    byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
            //    return Convert.ToBase64String(rsa.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            //}
            #endregion

            using (var rsa = System.Security.Cryptography.RSA.Create())
            {
                rsa.ImportPkcs8PrivateKey(keyData, out _);
                byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
                return Convert.ToBase64String(rsa.SignData(data, 0, data.Length, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
            }
        }

        /// <summary>
        /// 获取调起支付所需的签名
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="nonceStr">随机串</param>
        /// <param name="package">格式：prepay_id={0}</param>
        /// <param name="senparcWeixinSettingForTenpayV3">可为空 为空将从Senparc.Weixin.Config获取</param>
        /// <returns></returns>
        public static string CreatePaySign(string timeStamp, string nonceStr, string package, ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            senparcWeixinSettingForTenpayV3 ??= Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;

            var appId = senparcWeixinSettingForTenpayV3.TenPayV3_AppId;
            var privateKey = senparcWeixinSettingForTenpayV3.TenPayV3_PrivateKey;

            return CreatePaySign(timeStamp, nonceStr, package, appId, privateKey);
        }

        /// <summary>
        /// 获取调起支付所需的签名
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="nonceStr">随机串</param>
        /// <param name="package">格式：prepay_id={0}</param>
        /// <param name="privateKey">商户证书私钥</param>
        /// <returns></returns>
        public static string CreatePaySign(string timeStamp, string nonceStr, string package, string appId, string privateKey)
        {
            string contentForSign = $"{appId}\n{timeStamp}\n{nonceStr}\n{package}\n";
            return CreateSign(contentForSign, privateKey);
        }

        /// <summary>
        /// 检验签名，以确保回调是由微信支付发送。
        /// 签名规则见微信官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay4_1.shtml。
        /// return bool
        /// </summary>
        /// <param name="wechatpayTimestamp">HTTP头中的应答时间戳</param>
        /// <param name="wechatpayNonce">HTTP头中的应答随机串</param>
        /// <param name="wechatpaySignatureBase64">HTTP头中的应答签名（Base64）</param>
        /// <param name="content">应答报文主体</param>
        /// <param name="pubKey">平台公钥（必须是Unwrap的公钥）</param>
        /// <returns></returns>
        public static bool VerifyTenpaySign(string wechatpayTimestamp, string wechatpayNonce, string wechatpaySignatureBase64, string content, string pubKey)
        {
            //验签名串
            string contentForSign = $"{wechatpayTimestamp}\n{wechatpayNonce}\n{content}\n";

            //Base64 解码 pubKey（必须已经使用 ApiSecurityHelper.GetUnwrapCertKey() 方法进行 Unwrap）
            var bs = Convert.FromBase64String(pubKey);
            //使用 X509Certificate2 证书
            var x509 = new X509Certificate2(bs);
            //AsymmetricAlgorithm对象
            var key = x509.PublicKey.Key;

            //RSAPKCS1SignatureDeformatter 对象
            RSAPKCS1SignatureDeformatter df = new RSAPKCS1SignatureDeformatter(key);
            //指定 SHA256
            df.SetHashAlgorithm("SHA256");
            //SHA256Managed 方法已弃用，使用 SHA256.Create() 生成 SHA256 对象
            var sha256 = SHA256.Create();
            //应答签名
            byte[] signature = Convert.FromBase64String(wechatpaySignatureBase64);
            //对比签名
            byte[] compareByte = sha256.ComputeHash(Encoding.UTF8.GetBytes(contentForSign));
            //验证签名
            var result = df.VerifySignature(compareByte, signature);

            return result;
        }

        /// <summary>
        /// 检验签名，以确保回调是由微信支付发送。
        /// 签名规则见微信官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/wechatpay/wechatpay4_1.shtml。
        /// return bool
        /// </summary>
        /// <param name="wechatpayTimestamp">HTTP头中的应答时间戳</param>
        /// <param name="wechatpayNonce">HTTP头中的应答随机串</param>
        /// <param name="wechatpaySignature">HTTP头中的应答签名</param>
        /// <param name="content">应答报文主体</param>
        /// <param name="pubKey">平台公钥 可为空</param>
        /// <returns></returns>
        public static async Task<bool> VerifyTenpaySign(string wechatpayTimestamp, string wechatpayNonce, string wechatpaySignature, string content, string serialNumber, ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3)
        {
            //string contentForSign = $"{wechatpayTimestamp}\n{wechatpayNonce}\n{content}\n";

            var tenpayV3InfoKey = TenPayHelper.GetRegisterKey(senparcWeixinSettingForTenpayV3.TenPayV3_MchId, senparcWeixinSettingForTenpayV3.TenPayV3_SubMchId);
            var pubKey = await TenPayV3InfoCollection.Data[tenpayV3InfoKey].GetPublicKeyAsync(serialNumber, senparcWeixinSettingForTenpayV3);
            return VerifyTenpaySign(wechatpayTimestamp, wechatpayNonce, wechatpaySignature, content, pubKey);
        }

        /// <summary>
        /// 获取给 JsApi UI 使用的打包签名信息
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="prepayId"></param>
        /// <returns></returns>
        public static JsApiUiPackage GetJsApiUiPackage(string appId, string prepayId, ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
        {
            var timeStamp = TenPayV3Util.GetTimestamp();
            var nonceStr = TenPayV3Util.GetNoncestr();
            var prepayIdPackage = prepayId.Contains("prepay_id=") ? prepayId : string.Format("prepay_id={0}", prepayId);
            var sign = TenPaySignHelper.CreatePaySign(timeStamp, nonceStr, prepayIdPackage, senparcWeixinSettingForTenpayV3);

            JsApiUiPackage jsApiUiPackage = new(appId, timeStamp, nonceStr, prepayIdPackage, sign);
            return jsApiUiPackage;
        }
    }
}
