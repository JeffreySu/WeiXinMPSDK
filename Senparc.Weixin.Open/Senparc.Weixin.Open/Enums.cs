/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：Enums.cs
    文件功能描述：枚举类型
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 选项设置信息选项名称
    /// </summary>
    public enum OptionName
    {
        /// <summary>
        /// 地理位置上报选项
        /// 0	无上报
        /// 1	进入会话时上报
        /// 2	每5s上报
        /// </summary>
        location_report,
        /// <summary>
        /// 语音识别开关选项
        /// 0	关闭语音识别
        /// 1	开启语音识别
        /// </summary>
        voice_recognize,
        /// <summary>
        /// 客服开关选项
        /// 0	关闭多客服
        /// 1	开启多客服
        /// </summary>
        customer_service
    }

    /// <summary>
    /// 公众号第三方平台推送消息类型
    /// </summary>
    public enum RequestInfoType
    {
        /// <summary>
        /// 推送component_verify_ticket协议
        /// </summary>
        component_verify_ticket,
        /// <summary>
        /// 推送取消授权通知
        /// </summary>
        unauthorized
    }

    /// <summary>
    /// 应用授权作用域
    /// </summary>
    public enum OAuthScope
    {
        /// <summary>
        /// 不弹出授权页面，直接跳转，只能获取用户openid
        /// </summary>
        snsapi_base,
        /// <summary>
        /// 弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息
        /// </summary>
        snsapi_userinfo,
        /// <summary>
        /// 网站应用授权登录
        /// </summary>
        snsapi_login,
    }

    /// <summary>
    /// 授权方公众号类型
    /// </summary>
    public enum ServiceType
    {
        订阅号 = 0,
        由历史老帐号升级后的订阅号 = 1,
        服务号 = 2
    }

    /// <summary>
    /// 授权方认证类型
    /// </summary>
    public enum VerifyType
    {
        未认证 = -1,
        微信认证 = 0,
        新浪微博认证 = 1,
        腾讯微博认证 = 2,
        已资质认证通过但还未通过名称认证 = 3,
        已资质认证通过还未通过名称认证但通过了新浪微博认证 = 4,
        已资质认证通过还未通过名称认证但通过了腾讯微博认证 = 5
    }

    /// <summary>
    /// 公众号授权给开发者的权限集列表
    /// </summary>
    public enum FuncscopeCategory
    {
        消息与菜单权限集 = 1,
        用户管理权限集 = 2,
        帐号管理权限集 = 3,
        网页授权权限集 = 4,
        微信小店权限集 = 5,
        多客服权限集 = 6,
        业务通知权限集 = 7,
        微信卡券权限集 = 8,
        素材管理权限集 = 9,
        摇一摇周边权限集 = 10,
        线下门店权限集 = 11,
        微信连WIFI权限集 = 12,
        未知类型 = 13
    }
}
