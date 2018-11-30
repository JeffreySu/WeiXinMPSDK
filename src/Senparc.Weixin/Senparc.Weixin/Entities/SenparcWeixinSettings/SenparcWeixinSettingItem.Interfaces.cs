using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// SenparcWeixinSetting基础接口
    /// </summary>
    public interface ISenparcWeixinSettingBase
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        string ItemKey { get; set; }
    }

    /// <summary>
    /// 公众号
    /// </summary>
    public interface ISenparcWeixinSettingForMP : ISenparcWeixinSettingBase
    {
        /// <summary>
        /// 公众号Token
        /// </summary>
        string Token { get; set; }
        /// <summary>
        /// 公众号消息加密Key
        /// </summary>
        string EncodingAESKey { get; set; }
        /// <summary>
        /// 公众号AppId
        /// </summary>
        string WeixinAppId { get; set; }
        /// <summary>
        /// 公众号AppSecret
        /// </summary>
        string WeixinAppSecret { get; set; }
    }

    /// <summary>
    /// 小程序
    /// </summary>
    public interface ISenparcWeixinSettingForWxOpen : ISenparcWeixinSettingBase
    {
        /// <summary>
        /// 小程序AppId
        /// </summary>
        string WxOpenAppId { get; set; }
        /// <summary>
        /// 小程序AppSecret
        /// </summary>
        string WxOpenAppSecret { get; set; }
        /// <summary>
        /// 小程序 Token
        /// </summary>
        string WxOpenToken { get; set; }
        /// <summary>
        /// 小程序EncodingAESKey 
        /// </summary>
        string WxOpenEncodingAESKey { get; set; }
    }

    /// <summary>
    /// 企业号
    /// </summary>
    public interface ISenparcWeixinSettingForWork : ISenparcWeixinSettingBase
    {
        /// <summary>
        /// 企业微信CorpId
        /// </summary>
        string WeixinCorpId { get; set; }
        /// <summary>
        /// 企业微信CorpSecret
        /// </summary>
        string WeixinCorpSecret { get; set; }
    }

    /// <summary>
    /// 微信支付（旧版）
    /// </summary>
    public interface ISenparcWeixinSettingForOldTenpay : ISenparcWeixinSettingBase
    {
        /// <summary>
        /// WeixinPay_PartnerId（微信支付V2）
        /// </summary>
        string WeixinPay_PartnerId { get; set; }
        /// <summary>
        /// WeixinPay_Key（微信支付V2）
        /// </summary>
        string WeixinPay_Key { get; set; }
        /// <summary>
        /// WeixinPay_AppId（微信支付V2）
        /// </summary>
        string WeixinPay_AppId { get; set; }
        /// <summary>
        /// WeixinPay_AppKey（微信支付V2）
        /// </summary>
        string WeixinPay_AppKey { get; set; }
        /// <summary>
        /// WeixinPay_TenpayNotify（微信支付V2）
        /// </summary>
        string WeixinPay_TenpayNotify { get; set; }
    }

    /// <summary>
    /// 微信支付V3
    /// </summary>
    public interface ISenparcWeixinSettingForTenpayV3 : ISenparcWeixinSettingBase
    {
        /// <summary>
        /// MchId
        /// </summary>
        string TenPayV3_MchId { get; set; }
        /// <summary>
        /// MchKey
        /// </summary>
        string TenPayV3_Key { get; set; }
        /// <summary>
        /// 微信支付AppId
        /// </summary>
        string TenPayV3_AppId { get; set; }
        /// <summary>
        /// 微信支付AppKey
        /// </summary>
        string TenPayV3_AppSecret { get; set; }
        /// <summary>
        /// 微信支付TenpayNotify
        /// </summary>
        string TenPayV3_TenpayNotify { get; set; }
        /// <summary>
        /// 小程序微信支付WxOpenTenpayNotify
        /// </summary>
        string TenPayV3_WxOpenTenpayNotify { get; set; }
    }

    /// <summary>
    /// 开放平台
    /// </summary>
    public interface ISenparcWeixinSettingForOpen : ISenparcWeixinSettingBase
    {
        /// <summary>
        /// Component_Appid
        /// </summary>
        string Component_Appid { get; set; }
        /// <summary>
        /// Component_Secret
        /// </summary>
        string Component_Secret { get; set; }
        /// <summary>
        /// 全局统一的 Component_Token（非必须）
        /// </summary>
        string Component_Token { get; set; }
        /// <summary>
        /// 全局统一的 Component_EncodingAESKey（非必须）
        /// </summary>
        string Component_EncodingAESKey { get; set; }
    }

    /// <summary>
    /// 扩展
    /// </summary>
    public interface ISenparcWeixinSettingForExtension : ISenparcWeixinSettingBase
    {
        string AgentUrl { get; set; }
        string AgentToken { get; set; }
        string SenparcWechatAgentKey { get; set; }
    }

}
