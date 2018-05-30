/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：SenparcWeixinSetting.cs
    文件功能描述：Senparc.Weixin JSON 配置
    
    
    创建标识：Senparc - 20170302
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// Senparc.Weixin JSON 配置
    /// </summary>
    public class SenparcWeixinSetting
    {
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

        #endregion

        #region 企业微信

        /// <summary>
        /// 企业微信CorpId
        /// </summary>
        public virtual string WeixinCorpId { get; set; }
        /// <summary>
        /// 企业微信CorpSecret
        /// </summary>
        public virtual string WeixinCorpSecret { get; set; }

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
        /// MchId
        /// </summary>
        public virtual string TenPayV3_MchId { get; set; }
        /// <summary>
        /// MchKey
        /// </summary>
        public virtual string TenPayV3_Key { get; set; }
        /// <summary>
        /// 微信支付AppId
        /// </summary>
        public virtual string TenPayV3_AppId { get; set; }
        /// <summary>
        /// 微信支付AppKey
        /// </summary>
        public virtual string TenPayV3_AppSecret { get; set; }
        /// <summary>
        /// 微信支付TenpayNotify
        /// </summary>
        public virtual string TenPayV3_TenpayNotify { get; set; }

        #endregion

        #endregion

        #region 开放平台

        /// <summary>
        /// Component_Appid
        /// </summary>
        public string Component_Appid { get; set; }
        /// <summary>
        /// Component_Secret
        /// </summary>
        public string Component_Secret { get; set; }
        /// <summary>
        /// 全局统一的 Component_Token（非必须）
        /// </summary>
        public string Component_Token { get; set; }
        /// <summary>
        /// 全局统一的 Component_EncodingAESKey（非必须）
        /// </summary>
        public string Component_EncodingAESKey { get; set; }

        #endregion

        #region 分布式缓存

        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public string Cache_Redis_Configuration { get; set; }

        #endregion
    }
}
