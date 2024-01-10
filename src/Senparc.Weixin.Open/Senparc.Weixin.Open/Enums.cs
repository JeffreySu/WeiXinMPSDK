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

    修改标识：mc7246 - 20211209
    修改描述：v4.13.2 添加“小程序违规和申诉管理”接口

    修改标识：Senparc - 20220107
    修改描述：v4.13.4 完善“公众号权限集”

    修改标识：mc7246 - 20220402
    修改描述：v4.13.9 添加试用小程序接口及事件

    修改标识：mc7246 - 20220514
    修改描述：v4.14.3 补充小程序/公众号获取基本信息字段（PrincipalType、CustomerType）

    修改标识：Senparc - 20230207
    修改描述：v4.14.15 完善“第三方平台业务域名”，添加枚举：ModifyWxaJumpDomain_Action #2767 #2789

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
        authorized,
        /// <summary>
        /// 小程序注册审核事件推送
        /// </summary>
        notify_third_fasteregister,
        /// <summary>
        /// 小程序名称设置及改名审核事件推送
        /// </summary>
        wxa_nickname_audit,
        /// <summary>
        /// 试用小程序快速认证事件推送
        /// </summary>
        notify_third_fastverifybetaapp,
        /// <summary>
        /// 创建试用小程序成功/失败的事件推送
        /// </summary>
        notify_third_fastregisterbetaapp,
        /// <summary>
        /// 小程序管理员人脸核身完成事件
        /// </summary>
        notify_icpfiling_verify_result,
        /// <summary>
        /// 当备案审核被驳回或通过时会推送该事件
        /// </summary>
        notify_apply_icpfiling_result,
        /// <summary>
        /// 微信认证推送事件
        /// </summary>
        notify_3rd_wxa_auth,
        /// <summary>
        /// 小程序认证年审和过期能力限制提醒推送事件
        /// </summary>
        notify_3rd_wxa_wxverify

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
        /// <summary>
        /// <see href="https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/product/offical_account_authority.html">文档</see>中已取消
        /// </summary>
        微信支付权限 = 14,
        自定义菜单权限 = 15,
        获取认证状态及信息 = 16,
        帐号管理权限_小程序 = 17,
        开发管理与数据分析权限_小程序 = 18,
        客服消息管理权限_小程序 = 19,
        微信登录权限_小程序 = 20,
        数据分析权限_小程序 = 21,
        城市服务接口权限 = 22,
        广告管理权限 = 23,
        开放平台帐号管理权限 = 24,
        微信开放平台帐号管理权限_小程序 = 25,
        微信电子发票权限 = 26,
        快速注册小程序权限 = 27,
        小程序基本信息设置权限=30,
        小程序认证名称检测=31,
        小程序管理权限 = 33,
        微信商品库权限 = 34,
        微信公众号卡路里权限 = 35,
        微信小程序卡路里权限 = 36,
        小程序附近地点权限集 =37,
        小程序插件管理权限集=40,
        好物圈管理 = 41,
        好物圈权限 = 44,
        微信物流服务=45,
        微信一物一码权限 = 46,
        微信财政电子票据权限 = 47,
        微信财政电子票据管理 = 48,
        云开发管理权限集 =49,
        即时配送权限=51,
        小程序直播权限集=52,
        服务号对话权限 = 54,
        广告管理权限_小程序=65,
        服务平台管理权限 = 66,
        服务平台管理权限_小程序=67,
        商品管理权限=70,
        订单与物流管理权限=71,
        标准版交易组件接入权限=73,
        违规和申诉管理权限=76,
        快速体验小程序权限=81,
        优惠券权限集=84,
        自定义版交易组件=85,
        小商店装修=86,
        获取小程序链接=88,
        订阅通知权限 = 89,
        小程序联盟权限=93,
        云开发短信服务 = 99,
        云开发微信支付=102,
        标准版组件资金链路=104,
        城市服务=105,
        连接器电商场景接入 = 107,
        红包封面管理 = 112,
        获取自定义版交易组件数据 = 116,
        小程序图像处理 = 117,
        硬件服务 = 118,
        小程序支付管理服务 = 119,
        小程序购物订单 = 120,
        公众号购物订单 = 121,
        视频号小店商品管理 = 129,
        视频号小店物流管理 = 130,
        视频号小店订单与售后管理 = 131,
        视频号小店优惠券管理 = 132,
        视频号商品橱窗管理 = 133,
        流量主代运营权限集 = 135,
        视频号小店资金管理 = 138,
        小程序运费险 = 139,
        小程序推广员 = 140,
        视频号小店优选联盟 = 141,
        小程序发货管理服务 = 142,
        微信学生身份验证权限集 = 144,
        小程序交易保障 = 151,
        微短剧小程序资源管理 = 153,
        小程序备案服务 = 156,
        小程序虚拟支付管理权限 = 157,
        视频号电子面单管理 = 159,
        微信客服管理 = 161

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
    /// 小程序“修改业务域名”接口的action类型
    /// </summary>
    public enum SetWebViewDomainAction
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
        个人 = 0,
        企业 = 1,
        媒体 = 2,
        政府 = 3,
        其他组织 = 4
    }

    /// <summary>
    /// 认证类型
    /// </summary>
    public enum CustomerType
    {
        未认证 = 0,
        企业 = 1,
        企业媒体 = 2,
        政府 = 3,
        非盈利组织 = 4,
        民营非企业 = 5,
        盈利组织 = 6,
        社会团体 = 8,
        事业媒体 = 9,
        事业单位 = 11,
        个体工商户 = 12,
        海外企业 = 13
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

    /// <summary>
    /// 企业代码类型 1：统一社会信用代码（18位） 2：组织机构代码（9位xxxxxxxx-x） 3：营业执照注册号(15位)
    /// </summary>
    public enum CodeType
    {
        统一社会信用代码 =1,
        组织机构代码=2,
        营业执照注册号=3
    }

    /// <summary>
    /// “设置第三方平台服务器域名”接口，action 参数枚举
    /// </summary>
    public enum ModifyWxaServerDomain_Action
    {
        /// <summary>
        /// 	添加
        /// </summary>
        add,
        /// <summary>
        /// 删除。说明，删除不存在的域名会视为成功，返回 errcode 为0
        /// </summary>
        delete,
        /// <summary>
        /// 覆盖
        /// </summary>
        set,
        /// <summary>
        /// 获取 ，action=get时，会同时返回测试版和全网发布版的“小程序服务器域名”值。
        /// </summary>
        get
    }

    /// <summary>
    /// “设置第三方平台业务域名”接口，action 参数枚举
    /// </summary>
    public enum ModifyWxaJumpDomain_Action
    {
        /// <summary>
        /// 	添加
        /// </summary>
        add,
        /// <summary>
        /// 删除。说明，删除不存在的域名会视为成功，返回 errcode 为0
        /// </summary>
        delete,
        /// <summary>
        /// 覆盖
        /// </summary>
        set,
        /// <summary>
        /// 获取 ，action=get时，会同时返回测试版和全网发布版的“小程序服务器域名”值。
        /// </summary>
        get
    }
}
