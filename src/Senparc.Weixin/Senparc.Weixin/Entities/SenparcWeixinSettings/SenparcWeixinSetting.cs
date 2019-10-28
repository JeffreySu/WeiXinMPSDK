﻿/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SenparcWeixinSetting.cs
    文件功能描述：Senparc.Weixin JSON 配置
    
    
    创建标识：Senparc - 20170302

    修改标识：Senparc - 20180622
    修改描述：v5.0.3.1 SenparcWeixinSetting 添加 Cache_Memcached_Configuration 属性
    
    修改标识：Senparc - 20180622
    修改描述：v5.0.6.2 WeixinRegister.UseSenparcWeixin() 方法去除 isDebug 参数，提供扩展缓存自动扫描添加功能

    修改标识：Senparc - 20180622
    修改描述：v5.0.8 SenparcWeixinSetting 构造函数提供 isDebug 参数

    修改标识：Senparc - 20180802
    修改描述：MP v15.2.0 SenparcWeixinSetting 添加 TenPayV3_WxOpenTenpayNotify 属性，用于设置小程序支付回调地址

    修改标识：Senparc - 20191002
    修改描述：v6.6.102 提供 SenparcWeixinSetting[key] 快捷索引器，对 Items 内容进行索引

    修改标识：Senparc - 20191004
    修改描述：添加 SenparcWeixinSetting.MpSetting 等一系列属性，用于快速筛选（限定）不同模块的指定参数

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// <para>Senparc.Weixin JSON 配置</para>
    /// </summary>
    public class SenparcWeixinSetting : SenparcWeixinSettingItem//继承 SenparcWeixinSettingItem 是为了可以得到一组默认的参数，方便访问
    {
        #region 微信全局

        /// <summary>
        /// 是否处于 Debug 状态（仅限微信范围）
        /// </summary>
        public bool IsDebug { get; set; }

        #endregion

        /// <summary>
        /// 系统中所有微信设置的参数，默认已经添加一个 Key 为“Default”的对象
        /// </summary>
        public SenparcWeixinSettingItemCollection Items { get; set; }

        /// <summary>
        /// 从 Items 中获取对应键的参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SenparcWeixinSettingItem this[string key]
        {
            get
            {
                return Items[key];
            }
            set
            {
                Items[key] = value;
            }
        }

        public ISenparcWeixinSettingForMP MpSetting => this;
        public ISenparcWeixinSettingForWxOpen WxOpenSetting => this;
        public ISenparcWeixinSettingForWork WorkSetting => this;
        public ISenparcWeixinSettingForOpen OpenSetting => this;
        public ISenparcWeixinSettingForOldTenpay OldTenpaySettting => this;
        public ISenparcWeixinSettingForTenpayV3 TenpayV3Setting => this;
        public ISenparcWeixinSettingForExtension ExtensionSetting => this;

        /// <summary>
        /// SenparcWeixinSetting 构造函数
        /// </summary>
        public SenparcWeixinSetting() : this(false)
        { }

        /// <summary>
        /// SenparcWeixinSetting 构造函数
        /// </summary>
        /// <param name="isDebug">是否开启 Debug 模式</param>
        /// <param name="isRoot">是否是根节点，如果是，将创建下级 Items 节点</param>
        public SenparcWeixinSetting(bool isDebug)
        {
            IsDebug = isDebug;

            Items = new SenparcWeixinSettingItemCollection();
            Items["Default"] = this;//储存第一个默认参数
        }


#if NET45
        /// <summary>
        /// 从 Web.Config 文件自动生成 SenparcWeixinSetting
        /// </summary>
        /// <param name="isDebug">设置微信的 Debug 状态 </param>
        /// <returns></returns>
        public static SenparcWeixinSetting BuildFromWebConfig(bool isDebug)
        {
            var senparcWeixinSetting = new SenparcWeixinSetting(isDebug);

            //微信公众号URL对接信息
            senparcWeixinSetting.Token = System.Configuration.ConfigurationManager.AppSettings["WeixinToken"];
            senparcWeixinSetting.EncodingAESKey = System.Configuration.ConfigurationManager.AppSettings["WeixinEncodingAESKey"];
            //高级接口信息
            senparcWeixinSetting.WeixinAppId = System.Configuration.ConfigurationManager.AppSettings["WeixinAppId"];
            senparcWeixinSetting.WeixinAppSecret = System.Configuration.ConfigurationManager.AppSettings["WeixinAppSecret"];
            //SDK提供的代理功能设置
            senparcWeixinSetting.AgentUrl = System.Configuration.ConfigurationManager.AppSettings["WeixinAgentUrl"];
            senparcWeixinSetting.AgentToken = System.Configuration.ConfigurationManager.AppSettings["WeixinAgentToken"];
            senparcWeixinSetting.SenparcWechatAgentKey = System.Configuration.ConfigurationManager.AppSettings["SenparcWechatAgentKey"];
            //微信支付相关参数
            //微信支付V2
            //senparcWeixinSetting.WeixinPay_Tenpay = System.Configuration.ConfigurationManager.AppSettings["WeixinPay_Tenpay"];
            senparcWeixinSetting.WeixinPay_PartnerId = System.Configuration.ConfigurationManager.AppSettings["WeixinPay_PartnerId"];
            senparcWeixinSetting.WeixinPay_Key = System.Configuration.ConfigurationManager.AppSettings["WeixinPay_Key"];
            senparcWeixinSetting.WeixinPay_AppId = System.Configuration.ConfigurationManager.AppSettings["WeixinPay_AppId"];
            senparcWeixinSetting.WeixinPay_AppKey = System.Configuration.ConfigurationManager.AppSettings["WeixinPay_AppKey"];
            senparcWeixinSetting.WeixinPay_TenpayNotify = System.Configuration.ConfigurationManager.AppSettings["WeixinPay_TenpayNotify"];
            //微信支付V3
            senparcWeixinSetting.TenPayV3_MchId = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_MchId"];
            senparcWeixinSetting.TenPayV3_SubMchId = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_SubMchId"];
            senparcWeixinSetting.TenPayV3_Key = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_Key"];
            senparcWeixinSetting.TenPayV3_AppId = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_AppId"];
            senparcWeixinSetting.TenPayV3_AppSecret = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_AppSecret"];
            senparcWeixinSetting.TenPayV3_SubAppId = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_SubAppId"];
            senparcWeixinSetting.TenPayV3_SubAppSecret = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_SubAppSecret"];
            senparcWeixinSetting.TenPayV3_TenpayNotify = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_TenpayNotify"];
            senparcWeixinSetting.TenPayV3_WxOpenTenpayNotify = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_WxOpenTenpayNotify"];
            senparcWeixinSetting.TenPayV3_CertPath = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_CertPath"];//.net 4.5 实际不需要使用此配置
            senparcWeixinSetting.TenPayV3_CertSecret = System.Configuration.ConfigurationManager.AppSettings["TenPayV3_CertSecret"];//.net 4.5 实际不需要使用此配置
            if (string.IsNullOrEmpty(senparcWeixinSetting.TenPayV3_WxOpenTenpayNotify))
            {
                senparcWeixinSetting.TenPayV3_WxOpenTenpayNotify = senparcWeixinSetting.TenPayV3_TenpayNotify + "WxOpen";//设置默认值
            }

            //开放平台
            senparcWeixinSetting.Component_Appid = System.Configuration.ConfigurationManager.AppSettings["Component_Appid"];
            senparcWeixinSetting.Component_Secret = System.Configuration.ConfigurationManager.AppSettings["Component_Secret"];
            senparcWeixinSetting.Component_Token = System.Configuration.ConfigurationManager.AppSettings["Component_Token"];
            senparcWeixinSetting.Component_EncodingAESKey = System.Configuration.ConfigurationManager.AppSettings["Component_EncodingAESKey"];
            //微信企业号
            senparcWeixinSetting.WeixinCorpId = System.Configuration.ConfigurationManager.AppSettings["WeixinCorpId"];
            senparcWeixinSetting.WeixinCorpAgentId = System.Configuration.ConfigurationManager.AppSettings["WeixinCorpAgentId"];
            senparcWeixinSetting.WeixinCorpSecret = System.Configuration.ConfigurationManager.AppSettings["WeixinCorpSecret"];
            senparcWeixinSetting.WeixinCorpToken = System.Configuration.ConfigurationManager.AppSettings["WeixinCorpToken"];
            senparcWeixinSetting.WeixinCorpEncodingAESKey = System.Configuration.ConfigurationManager.AppSettings["WeixinCorpEncodingAESKey"];

            //小程序
            //小程序消息URL对接信息
            senparcWeixinSetting.WxOpenToken = System.Configuration.ConfigurationManager.AppSettings["WxOpenToken"];
            senparcWeixinSetting.WxOpenEncodingAESKey = System.Configuration.ConfigurationManager.AppSettings["WxOpenEncodingAESKey"];
            //小程序秘钥信息
            senparcWeixinSetting.WxOpenAppId = System.Configuration.ConfigurationManager.AppSettings["WxOpenAppId"];
            senparcWeixinSetting.WxOpenAppSecret = System.Configuration.ConfigurationManager.AppSettings["WxOpenAppSecret"];

            ////Cache.Redis连接配置
            //senparcWeixinSetting.Cache_Redis_Configuration = System.Configuration.ConfigurationManager.AppSettings["Cache_Redis_Configuration"];
            ////Cache.Redis连接配置
            //senparcWeixinSetting.Cache_Memcached_Configuration = System.Configuration.ConfigurationManager.AppSettings["Cache_Memcached_Configuration"];

            return senparcWeixinSetting;
        }
#endif
    }
}
