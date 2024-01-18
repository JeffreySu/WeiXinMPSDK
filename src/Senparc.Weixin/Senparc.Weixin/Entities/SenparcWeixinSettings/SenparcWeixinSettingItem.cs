#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Suzhou Senparc Network Technology Co.,Ltd.

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
    
    文件名：SenparcWeixinSettingItem.cs
    文件功能描述：Senparc.Weixin SDK 中单个公众号配置信息
    
    
    创建标识：Senparc - 20180707

	修改标识：Senparc - 20180802
    修改描述：MP v15.2.0 SenparcWeixinSetting 添加 TenPayV3_WxOpenTenpayNotify 属性，用于设置小程序支付回调地址

	修改标识：Senparc - 20190521
    修改描述：v6.4.4 .NET Core 添加多证书注册功能；增加 ISenparcWeixinSettingForTenpayV3 接口中的新属性

	修改标识：Senparc - 20191003
    修改描述：v6.6.102 提供 SenparcWeixinSettingItem 快速创建构造函数

	修改标识：Senparc - 20191004
    修改描述：添加新的 Work（企业微信）的参数

----------------------------------------------------------------*/

using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// Senparc.Weixin SDK 中单个公众号配置信息
    /// </summary>
    public record class SenparcWeixinSettingItem : ISenparcWeixinSettingForMP,
        ISenparcWeixinSettingForWxOpen, ISenparcWeixinSettingForWork,
        ISenparcWeixinSettingForOldTenpay, ISenparcWeixinSettingForTenpayV3,
        ISenparcWeixinSettingForOpen, ISenparcWeixinSettingForExtension
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public virtual string ItemKey { get; set; }

        #region 构造函数

        public SenparcWeixinSettingItem()
        {
        }

        public SenparcWeixinSettingItem(ISenparcWeixinSettingForMP setting)
        {
            ItemKey = setting.ItemKey;

            Token = setting.Token;
            EncodingAESKey = setting.EncodingAESKey;
            WeixinAppId = setting.WeixinAppId;
            WeixinAppSecret = setting.WeixinAppSecret;
        }

        public SenparcWeixinSettingItem(ISenparcWeixinSettingForWxOpen setting, bool isDebug = false)
        {
            ItemKey = setting.ItemKey;

            WxOpenAppId = setting.WxOpenAppId;
            WxOpenAppSecret = setting.WxOpenAppSecret;
            WxOpenEncodingAESKey = setting.WxOpenEncodingAESKey;
            WxOpenToken = setting.WxOpenToken;
        }

        public SenparcWeixinSettingItem(ISenparcWeixinSettingForWork setting, bool isDebug = false)
        {
            ItemKey = setting.ItemKey;

            WeixinCorpId = setting.WeixinCorpId;
            WeixinCorpAgentId = setting.WeixinCorpAgentId;
            WeixinCorpSecret = setting.WeixinCorpSecret;
            WeixinCorpToken = setting.WeixinCorpToken;
            WeixinCorpEncodingAESKey = setting.WeixinCorpEncodingAESKey;
        }

        public SenparcWeixinSettingItem(ISenparcWeixinSettingForOldTenpay setting, bool isDebug = false)
        {
            ItemKey = setting.ItemKey;

            WeixinPay_AppId = setting.WeixinPay_AppId;
            WeixinPay_AppKey = setting.WeixinPay_AppKey;
            WeixinPay_Key = setting.WeixinPay_Key;
            WeixinPay_PartnerId = setting.WeixinPay_PartnerId;
            WeixinPay_TenpayNotify = setting.WeixinPay_TenpayNotify;
        }

        public SenparcWeixinSettingItem(ISenparcWeixinSettingForTenpayV3 setting, bool isDebug = false)
        {
            ItemKey = setting.ItemKey;

            TenPayV3_AppId = setting.TenPayV3_AppId;
            TenPayV3_AppSecret = setting.TenPayV3_AppSecret;
            TenPayV3_CertPath = setting.TenPayV3_CertPath;
            TenPayV3_CertSecret = setting.TenPayV3_CertSecret;
            TenPayV3_Key = setting.TenPayV3_Key;
            TenPayV3_MchId = setting.TenPayV3_MchId;
            TenPayV3_SubAppId = setting.TenPayV3_SubAppId;
            TenPayV3_SubAppSecret = setting.TenPayV3_SubAppSecret;
            TenPayV3_SubMchId = setting.TenPayV3_SubMchId;
            TenPayV3_TenpayNotify = setting.TenPayV3_TenpayNotify;
            TenPayV3_APIv3Key = setting.TenPayV3_APIv3Key;
            TenPayV3_PrivateKey = setting.TenPayV3_PrivateKey;
            TenPayV3_SerialNumber = setting.TenPayV3_SerialNumber;
            TenPayV3_WxOpenTenpayNotify = setting.TenPayV3_WxOpenTenpayNotify;
        }

        public SenparcWeixinSettingItem(ISenparcWeixinSettingForOpen setting, bool isDebug = false)
        {
            ItemKey = setting.ItemKey;

            Component_Appid = setting.Component_Appid;
            Component_EncodingAESKey = setting.Component_EncodingAESKey;
            Component_Secret = setting.Component_Secret;
            Component_Token = setting.Component_Token;
        }

        public SenparcWeixinSettingItem(ISenparcWeixinSettingForExtension setting, bool isDebug = false)
        {
            ItemKey = setting.ItemKey;

            AgentUrl = setting.AgentUrl;
            AgentToken = setting.AgentToken;
            SenparcWechatAgentKey = setting.SenparcWechatAgentKey;
        }
        #endregion

        #region 公众号

        /// <summary>
        /// 公众号Token
        /// </summary>
        public virtual string Token { get; set; }
        /// <summary>
        /// 公众号消息加密Key
        /// </summary>
        public virtual string EncodingAESKey { get; set; }
        /// <summary>
        /// 公众号AppId
        /// </summary>
        public virtual string WeixinAppId { get; set; }
        /// <summary>
        /// 公众号AppSecret
        /// </summary>
        public virtual string WeixinAppSecret { get; set; }

        #endregion

        #region 小程序

        /// <summary>
        /// 小程序AppId
        /// </summary>
        public virtual string WxOpenAppId { get; set; }
        /// <summary>
        /// 小程序AppSecret
        /// </summary>
        public virtual string WxOpenAppSecret { get; set; }
        /// <summary>
        /// 小程序 Token
        /// </summary>
        public virtual string WxOpenToken { get; set; }
        /// <summary>
        /// 小程序EncodingAESKey 
        /// </summary>
        public virtual string WxOpenEncodingAESKey { get; set; }

        #endregion

        #region 企业微信

        /// <summary>
        /// 企业微信CorpId（全局）
        /// </summary>
        public virtual string WeixinCorpId { get; set; }

        /// <summary>
        /// 企业微信 AgentId（单个应用的Id），一般为数字
        /// </summary>
        public virtual string WeixinCorpAgentId { get; set; }

        /// <summary>
        /// 企业微信CorpSecret（和 AgentId对应）
        /// </summary>
        public virtual string WeixinCorpSecret { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public virtual string WeixinCorpToken { get; set; }

        /// <summary>
        /// EncodingAESKey
        /// </summary>
        public virtual string WeixinCorpEncodingAESKey { get; set; }

        #endregion

        #region 微信支付

        #region 微信支付V2（旧版）
        /// <summary>
        /// WeixinPay_PartnerId（微信支付V2）
        /// </summary>
        public virtual string WeixinPay_PartnerId { get; set; }
        /// <summary>
        /// WeixinPay_Key（微信支付V2）
        /// </summary>
        public virtual string WeixinPay_Key { get; set; }
        /// <summary>
        /// WeixinPay_AppId（微信支付V2）
        /// </summary>
        public virtual string WeixinPay_AppId { get; set; }
        /// <summary>
        /// WeixinPay_AppKey（微信支付V2）
        /// </summary>
        public virtual string WeixinPay_AppKey { get; set; }
        /// <summary>
        /// WeixinPay_TenpayNotify（微信支付V2）
        /// </summary>
        public virtual string WeixinPay_TenpayNotify { get; set; }

        #endregion

        #region 微信支付V3（新版）

        /// <summary>
        /// MchId（商户ID）
        /// </summary>
        public virtual string TenPayV3_MchId { get; set; }
        /// <summary>
        /// 特约商户微信支付 子商户ID，没有可留空
        /// </summary>
        public virtual string TenPayV3_SubMchId { get; set; }
        /// <summary>
        /// MchKey
        /// </summary>
        public virtual string TenPayV3_Key { get; set; }
        /// <summary>
        /// 微信支付证书位置（物理路径），在 .NET Core 下执行 TenPayV3InfoCollection.Register() 方法会为 HttpClient 自动添加证书
        /// </summary>
        public virtual string TenPayV3_CertPath { get; set; }
        /// <summary>
        /// 微信支付证书密码，在 .NET Core 下执行 TenPayV3InfoCollection.Register() 方法会为 HttpClient 自动添加证书
        /// </summary>
        public virtual string TenPayV3_CertSecret { get; set; }
        /// <summary>
        /// 微信支付AppId
        /// </summary>
        public virtual string TenPayV3_AppId { get; set; }
        /// <summary>
        /// 微信支付AppSecert
        /// </summary>
        public virtual string TenPayV3_AppSecret { get; set; }
        /// <summary>
        /// 特约商户微信支付 子商户AppID
        /// </summary>
        public virtual string TenPayV3_SubAppId { get; set; }
        /// <summary>
        /// 特约商户微信支付 子商户AppSecret
        /// </summary>
        public virtual string TenPayV3_SubAppSecret { get; set; }
        /// <summary>
        /// 微信支付TenpayNotify
        /// </summary>
        public virtual string TenPayV3_TenpayNotify { get; set; }

        #region 新版微信支付 V3 新增
        private string _tenPayV3_PrivateKey;
        /// <summary>
        /// 微信支付（V3）证书私钥
        /// <para>获取途径：apiclient_key.pem</para>
        /// </summary>
        public virtual string TenPayV3_PrivateKey
        {
            get
            {
                return TenPayHelper.TryGetPrivateKeyFromFile(ref _tenPayV3_PrivateKey);
            }
            set
            {

                _tenPayV3_PrivateKey = value;
            }
        }

        /// <summary>
        /// 微信支付（V3）证书序列号
        /// <para>查看地址：https://pay.weixin.qq.com/index.php/core/cert/api_cert#/api-cert-manage</para>
        /// </summary>
        public virtual string TenPayV3_SerialNumber { get; set; }
        /// <summary>
        /// APIv3 密钥。在微信支付后台设置：https://pay.weixin.qq.com/index.php/core/cert/api_cert#/
        /// </summary>
        public string TenPayV3_APIv3Key { get; set; }
        #endregion

        /// <summary>
        /// 小程序微信支付WxOpenTenpayNotify
        /// </summary>
        public virtual string TenPayV3_WxOpenTenpayNotify { get; set; }


        #endregion

        #endregion

        #region 开放平台

        /// <summary>
        /// Component_Appid
        /// </summary>
        public virtual string Component_Appid { get; set; }
        /// <summary>
        /// Component_Secret
        /// </summary>
        public virtual string Component_Secret { get; set; }
        /// <summary>
        /// 全局统一的 Component_Token（非必须）
        /// </summary>
        public virtual string Component_Token { get; set; }
        /// <summary>
        /// 全局统一的 Component_EncodingAESKey（非必须）
        /// </summary>
        public virtual string Component_EncodingAESKey { get; set; }

        #endregion

        #region 扩展

        public virtual string AgentUrl { get; set; }
        public virtual string AgentToken { get; set; }
        public virtual string SenparcWechatAgentKey { get; set; }

        #endregion
    }
}
