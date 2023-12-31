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
 
    文件名：TenPayV3Info.cs

    
    创建标识：Senparc - 20210804

    修改标识：Senparc - 20210822
    修改描述：修改BasePayApis 此类型不再为静态类 使用ISenparcWeixinSettingForTenpayV3初始化实例

    修改标识：Senparc - 20210829
    修改描述：添加 V3 中新增加的属性

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Trace;
using Senparc.Weixin.Entities;
using Senparc.Weixin.TenPayV3.Apis;
using Senparc.Weixin.TenPayV3.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3
{
    /// <summary>
    /// 微信支付基础信息储存类
    /// </summary>
    public class TenPayV3Info
    {
        private PublicKeyCollection publicKeys;

        /// <summary>
        /// 第三方用户唯一凭证appid
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 第三方用户唯一凭证密钥，即appsecret
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 商户支付密钥Key。登录微信商户后台，进入栏目【账户设置】【密码安全】【API 安全】【API 密钥】
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 微信支付证书位置（物理路径），在 .NET Core 下执行 TenPayV3InfoCollection.Register() 方法会为 HttpClient 自动添加证书
        /// </summary>
        public string CertPath { get; set; }
        /// <summary>
        /// 微信支付证书密码
        /// </summary>
        public string CertSecret { get; set; }
        /// <summary>
        /// 支付完成后的回调处理页面
        /// </summary>
        public string TenPayV3Notify { get; set; } // = "http://localhost/payNotifyUrl.aspx";
        /// <summary>
        /// 小程序支付完成后的回调处理页面
        /// </summary>
        public string TenPayV3_WxOpenNotify { get; set; }

        /// <summary>
        /// 服务商模式下，特约商户的开发配置中的AppId
        /// </summary>
        public string Sub_AppId { get; set; }
        /// <summary>
        /// 服务商模式下，特约商户的开发配置中的AppSecret
        /// </summary>
        public string Sub_AppSecret { get; set; }
        /// <summary>
        /// 服务商模式下，特约商户的商户Id
        /// </summary>
        public string Sub_MchId { get; set; }


        #region 新版微信支付 V3 新增

        /// <summary>
        /// 微信支付（V3）证书私钥
        /// <para>获取途径：apiclient_key.pem</para>
        /// </summary>
        public string TenPayV3_PrivateKey { get; set; }
        /// <summary>
        /// 微信支付（V3）证书序列号
        /// <para>查看地址：https://pay.weixin.qq.com/index.php/core/cert/api_cert#/api-cert-manage</para>
        /// </summary>
        public string TenPayV3_SerialNumber { get; set; }
        /// <summary>
        /// APIv3 密钥。在微信支付后台设置：https://pay.weixin.qq.com/index.php/core/cert/api_cert#/
        /// </summary>
        public string TenPayV3_APIv3Key { get; set; }

        #endregion


        /// <summary>
        /// 普通服务商 微信支付 V3 参数 构造函数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="mchId"></param>
        /// <param name="key"></param>
        /// <param name="certPath"></param>
        /// <param name="certSecret"></param>
        /// <param name="tenPayV3Notify"></param>
        /// <param name="tenPayV3WxOpenNotify"></param>
        /// <param name="privateKey"></param>
        /// <param name="serialNumber"></param>
        /// <param name="apiV3Key"></param>
        public TenPayV3Info(string appId, string appSecret, string mchId, string key, string certPath, string certSecret, string tenPayV3Notify, string tenPayV3WxOpenNotify, string privateKey, string serialNumber, string apiV3Key)
            : this(appId, appSecret, mchId, key, certPath, certSecret, "", "", "", tenPayV3Notify, tenPayV3WxOpenNotify, privateKey, serialNumber, apiV3Key)
        {

        }
        /// <summary>
        /// 服务商户 微信支付 V3 参数 构造函数
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="mchId"></param>
        /// <param name="key"></param>
        /// <param name="certPath"></param>
        /// <param name="certSecret"></param>
        /// <param name="subAppId"></param>
        /// <param name="subAppSecret"></param>
        /// <param name="subMchId"></param>
        /// <param name="tenPayV3Notify"></param>
        /// <param name="tenPayV3WxOpenNotify"></param>
        /// <param name="privateKey"></param>
        /// <param name="serialNumber"></param>
        /// <param name="apiV3Key"></param>
        public TenPayV3Info(string appId, string appSecret, string mchId, string key, string certPath, string certSecret, string subAppId, string subAppSecret, string subMchId, string tenPayV3Notify, string tenPayV3WxOpenNotify, string privateKey, string serialNumber, string apiV3Key)
        {
            AppId = appId;
            AppSecret = appSecret;
            MchId = mchId;
            Key = key;
            CertPath = certPath;
            CertSecret = certSecret;
            TenPayV3Notify = tenPayV3Notify;
            TenPayV3_WxOpenNotify = tenPayV3WxOpenNotify;
            Sub_AppId = subAppId;
            Sub_AppSecret = subAppSecret;
            Sub_MchId = subMchId;
            TenPayV3_PrivateKey = privateKey;
            TenPayV3_SerialNumber = serialNumber;
            TenPayV3_APIv3Key = apiV3Key;
        }

        /// <summary>
        /// 微信支付 V3 参数 构造函数
        /// </summary>
        /// <param name="senparcWeixinSettingForTenpayV3">已经填充过微信支付参数的 SenparcWeixinSetting 对象</param>
        public TenPayV3Info(ISenparcWeixinSettingForTenpayV3 senparcWeixinSettingForTenpayV3 = null)
            : this(senparcWeixinSettingForTenpayV3.TenPayV3_AppId,
                  senparcWeixinSettingForTenpayV3.TenPayV3_AppSecret,
                  senparcWeixinSettingForTenpayV3.TenPayV3_MchId,
                  senparcWeixinSettingForTenpayV3.TenPayV3_Key,
                  senparcWeixinSettingForTenpayV3.TenPayV3_CertPath,
                  senparcWeixinSettingForTenpayV3.TenPayV3_CertSecret,
                  senparcWeixinSettingForTenpayV3.TenPayV3_SubAppId,
                  senparcWeixinSettingForTenpayV3.TenPayV3_SubAppSecret,
                  senparcWeixinSettingForTenpayV3.TenPayV3_SubMchId,
                  senparcWeixinSettingForTenpayV3.TenPayV3_TenpayNotify,
                  senparcWeixinSettingForTenpayV3.TenPayV3_WxOpenTenpayNotify,
                  senparcWeixinSettingForTenpayV3.TenPayV3_PrivateKey,
                  senparcWeixinSettingForTenpayV3.TenPayV3_SerialNumber,
                  senparcWeixinSettingForTenpayV3.TenPayV3_APIv3Key
                  )
        {
            //_tenpayV3Setting = senparcWeixinSettingForTenpayV3 ?? Senparc.Weixin.Config.SenparcWeixinSetting.TenpayV3Setting;

        }

        /// <summary>
        /// 获取当前支付账号下所有公钥信息
        /// </summary>
        public async Task<PublicKeyCollection> GetPublicKeysAsync(ISenparcWeixinSettingForTenpayV3 tenpayV3Setting)
        {
            //TODO:可以升级为从缓存读取

            if (publicKeys == null)
            {
                //获取最新的 Key
                var basePayApis = new BasePayApis(tenpayV3Setting);
                publicKeys = await basePayApis.GetPublicKeysAsync();
            }
            return publicKeys;
        }

        /// <summary>
        /// 获取单个公钥
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public async Task<string> GetPublicKeyAsync(string serialNumber, ISenparcWeixinSettingForTenpayV3 tenpayV3Setting)
        {
            var keys = await GetPublicKeysAsync(tenpayV3Setting);
            if (keys.TryGetValue(serialNumber, out string publicKey))
            {
                return publicKey;
            }

            SenparcTrace.BaseExceptionLog(new TenpaySecurityException($"公钥序列号不存在！serialNumber:{serialNumber},TenPayV3Info:{this.ToJson(true)}"));
            throw new TenpaySecurityException("公钥序列号不存在！请查看日志！", true);
        }
    }
}
