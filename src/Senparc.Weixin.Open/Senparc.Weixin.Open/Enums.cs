#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：Enums.cs
    文件功能描述：枚举类型
    
    
    创建标识：Senparc - 20150430
 
    修改标识：Senparc - 20160726
    修改描述：RequestInfoType中加了updateauthorized，authorized

    修改标识：Senparc - 20170601
    修改描述：v2.5.0 添加ModifyDomainAction

    修改标识：Senparc - 20170726
    修改描述：完成接口开放平台-代码管理及小程序码获取

    修改标识：Senparc - 20180121
    修改描述：v2.8.6 完善 FuncscopeCategory 枚举

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
        unauthorized,
        /// <summary>
        /// 更新授权
        /// </summary>
        updateauthorized,
        /// <summary>
        /// 授权成功通知
        /// </summary>
        authorized
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
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        订阅号 = 0,
        由历史老帐号升级后的订阅号 = 1,
        服务号 = 2
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }

    /// <summary>
    /// 授权方认证类型
    /// </summary>
    public enum VerifyType
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        未认证 = -1,
        微信认证 = 0,
        新浪微博认证 = 1,
        腾讯微博认证 = 2,
        已资质认证通过但还未通过名称认证 = 3,
        已资质认证通过还未通过名称认证但通过了新浪微博认证 = 4,
        已资质认证通过还未通过名称认证但通过了腾讯微博认证 = 5
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }

    /// <summary>
    /// <para>公众号/小程序授权给开发者的权限集列表(1-15为公众号权限,17-19为小程序权限)。</para>
    /// <para>请注意：1）该字段的返回不会考虑公众号是否具备该权限集的权限（因为可能部分具备），请根据公众号的帐号类型和认证情况，来判断公众号的接口权限。</para>
    /// </summary>
    public enum FuncscopeCategory
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        消息管理权限 = 1,
        用户管理权限 = 2,
        帐号服务权限 = 3,
        网页服务权限 = 4,
        微信小店权限 = 5,
        微信多客服权限 = 6,
        群发与通知权限 = 7,
        微信卡券权限 = 8,
        微信扫一扫权限 = 9,
        微信连WIFI权限 = 10,
        素材管理权限 = 11,
        微信摇周边权限 = 12,
        微信门店权限 = 13,
        微信支付权限 = 14,
        自定义菜单权限 = 15,
        获取认证状态及信息 = 16,
        帐号管理权限_小程序 = 17,
        开发管理权限_小程序 = 18,
        客服消息管理权限_小程序 = 19,
        微信登录权限_小程序 = 20,
        数据分析权限_小程序 = 21,
        城市服务接口权限 = 22,
        广告管理权限 = 23,
        开放平台帐号管理权限 = 24,
        开放平台帐号管理权限_小程序 = 25,
        微信电子发票权限 = 26,
        快速注册小程序权限 = 27,
        小程序管理权限 = 33,
        微信卡路里权限 = 35
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
    }

    /// <summary>
    /// 小程序“修改服务器地址”接口的action类型
    /// </summary>
    public enum ModifyDomainAction
    {
        /// <summary>
        /// 添加
        /// </summary>
        add,
        /// <summary>
        /// 删除
        /// </summary>
        delete,
        /// <summary>
        /// 覆盖
        /// </summary>
        set,
        /// <summary>
        /// 获取
        /// </summary>
        get
    }

    /// <summary>
    /// 小程序“线上代码的可见状态”接口的action类型
    /// </summary>
    public enum ChangVisitStatusAction
    {
        open,
        close
    }

    /// <summary>
    /// 帐号类型（1：订阅号，2：服务号，3：小程序）
    /// </summary>
    public enum AccountType
    {
        订阅号 = 1,
        服务号 = 2,
        小程序 = 3
    }

    /// <summary>
    /// 主体类型（1：企业）
    /// </summary>
    public enum PrincipalType
    {
        企业 = 1
    }

    /// <summary>
    /// 1：实名验证成功，2：实名验证中，3：实名验证失败
    /// </summary>
    public enum RealNameStatus
    {
        实名验证成功 = 1,
        实名验证中 = 2,
        实名验证失败 = 3
    }

    /// <summary>
    /// 小程序昵称审核状态，1：审核中，2：审核失败，3：审核成功
    /// </summary>
    public enum AuditStat
    {
        审核中 = 1,
        审核失败 = 2,
        审核成功 = 3
    }

    /// <summary>
    /// 小程序类目审核状态，1：审核中，2：审核失败，3：审核成功
    /// </summary>
    public enum AuditStatus
    {
        审核中 = 1,
        审核不通过 = 2,
        审核通过 = 3
    }

    /// <summary>
    /// 要授权的帐号类型
    /// </summary>
    public enum LoginAuthType
    {
        默认,
        仅展示公众号 = 1,
        仅展示小程序 = 2,
        表示公众号和小程序都展示 = 3
    }
}
