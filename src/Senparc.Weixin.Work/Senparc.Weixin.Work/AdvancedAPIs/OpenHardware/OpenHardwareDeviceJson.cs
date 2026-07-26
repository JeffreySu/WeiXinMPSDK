/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenHardwareDeviceJson.cs
    文件功能描述：企业微信智慧硬件设备接入强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智慧硬件设备接入强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OpenHardware
{
    /// <summary>获取设备型号调用凭证请求。</summary>
    public class OpenHardwareGetModelTokenRequest
    {
        /// <summary>设备型号 ID。</summary>
        public string model_id { get; set; }

        /// <summary>设备型号 Secret。</summary>
        public string model_secret { get; set; }

        /// <summary>企业微信推送的设备型号 Ticket。</summary>
        public string model_ticket { get; set; }
    }

    /// <summary>设备型号调用凭证结果。</summary>
    public class OpenHardwareGetModelTokenResult : WorkJsonResult
    {
        /// <summary>设备型号调用凭证。</summary>
        public string model_access_token { get; set; }

        /// <summary>凭证有效期，单位为秒。</summary>
        public int expires_in { get; set; }
    }

    /// <summary>获取设备授权密钥请求。</summary>
    public class OpenHardwareGetDeviceSecretRequest
    {
        /// <summary>企业用户扫码授权后下发的授权码。</summary>
        public string auth_code { get; set; }
    }

    /// <summary>设备授权密钥结果。</summary>
    public class OpenHardwareGetDeviceSecretResult : WorkJsonResult
    {
        /// <summary>设备授权密钥。</summary>
        public string device_secret { get; set; }

        /// <summary>设备接口调用凭证。</summary>
        public string device_access_token { get; set; }

        /// <summary>设备凭证有效期，单位为秒。</summary>
        public int expires_in { get; set; }
    }

    /// <summary>获取设备调用凭证请求。</summary>
    public class OpenHardwareGetDeviceTokenRequest
    {
        /// <summary>设备序列号。</summary>
        public string device_sn { get; set; }

        /// <summary>设备授权密钥。</summary>
        public string device_secret { get; set; }
    }

    /// <summary>设备调用凭证结果。</summary>
    public class OpenHardwareGetDeviceTokenResult : WorkJsonResult
    {
        /// <summary>设备接口调用凭证。</summary>
        public string device_access_token { get; set; }

        /// <summary>设备凭证有效期，单位为秒。</summary>
        public int expires_in { get; set; }
    }

    /// <summary>仅包含设备序列号的请求。</summary>
    public class OpenHardwareDeviceSerialNumberRequest
    {
        /// <summary>设备序列号。</summary>
        public string device_sn { get; set; }
    }

    /// <summary>录入设备结果。</summary>
    public class OpenHardwareRegisterDeviceResult : WorkJsonResult
    {
        /// <summary>设备静态身份识别二维码内容。</summary>
        public string qr_code { get; set; }
    }

    /// <summary>设备详情结果。</summary>
    public class OpenHardwareGetDeviceDetailResult : WorkJsonResult
    {
        /// <summary>设备详情。</summary>
        public OpenHardwareDeviceDetail device_detail { get; set; }
    }

    /// <summary>智慧硬件设备详情。</summary>
    public class OpenHardwareDeviceDetail
    {
        /// <summary>设备静态身份识别二维码内容。</summary>
        public string qr_code { get; set; }

        /// <summary>设备型号名称。</summary>
        public string model_name { get; set; }

        /// <summary>设备出厂名称。</summary>
        public string default_name { get; set; }

        /// <summary>企业设置的设备备注名称。</summary>
        public string remark_name { get; set; }

        /// <summary>设备绑定状态。</summary>
        public int bind_status { get; set; }

        /// <summary>设备已绑定的企业信息；未绑定时为空。</summary>
        public OpenHardwareBoundCorpInfo bind_corpinfo { get; set; }
    }

    /// <summary>设备绑定企业信息。</summary>
    public class OpenHardwareBoundCorpInfo
    {
        /// <summary>服务商主体下的企业 OpenCorpId。</summary>
        public string open_corpid { get; set; }

        /// <summary>企业名称。</summary>
        public string corp_name { get; set; }
    }

    /// <summary>更新设备状态请求。</summary>
    public class OpenHardwareReportDeviceStatusRequest
    {
        /// <summary>设备在线状态。</summary>
        public int online_status { get; set; }

        /// <summary>当前固件版本。</summary>
        public string cur_version { get; set; }

        /// <summary>可升级固件版本。</summary>
        public string upgradable_version { get; set; }

        /// <summary>可升级固件版本描述。</summary>
        public string upgradable_version_desc { get; set; }
    }

    /// <summary>分页获取设备成员请求。</summary>
    public class OpenHardwareGetUserInfoByPageRequest
    {
        /// <summary>是否仅获取云端通讯录版本号。</summary>
        public bool? only_perm_version { get; set; }

        /// <summary>每页成员数量。</summary>
        public int? limit { get; set; }

        /// <summary>分页游标；首次请求可不填。</summary>
        public string cursor { get; set; }
    }

    /// <summary>获取指定设备成员请求。</summary>
    public class OpenHardwareGetUserInfoByIdsRequest
    {
        /// <summary>待查询的成员标识列表。</summary>
        public List<OpenHardwareUserIdentifier> user_item { get; set; }
    }

    /// <summary>智慧硬件成员标识。</summary>
    public class OpenHardwareUserIdentifier
    {
        /// <summary>成员或访客 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>用户类型。</summary>
        public int user_type { get; set; }
    }

    /// <summary>分页获取设备成员结果。</summary>
    public class OpenHardwareGetUserInfoByPageResult : WorkJsonResult
    {
        /// <summary>当前云端通讯录权限版本号。</summary>
        public int perm_version { get; set; }

        /// <summary>成员列表容器。</summary>
        public OpenHardwareUserInfoContainer userinfo { get; set; }

        /// <summary>下一页游标；没有更多数据时为空。</summary>
        public string next_cursor { get; set; }
    }

    /// <summary>获取指定设备成员结果。</summary>
    public class OpenHardwareGetUserInfoByIdsResult : WorkJsonResult
    {
        /// <summary>成员列表容器。</summary>
        public OpenHardwareUserInfoContainer userinfo { get; set; }
    }

    /// <summary>智慧硬件成员列表容器。</summary>
    public class OpenHardwareUserInfoContainer
    {
        /// <summary>成员列表。</summary>
        public List<OpenHardwareUserInfo> useritems { get; set; }
    }

    /// <summary>智慧硬件成员信息。</summary>
    public class OpenHardwareUserInfo
    {
        /// <summary>成员或访客 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>用户类型。</summary>
        public int user_type { get; set; }

        /// <summary>用户名称。</summary>
        public string user_name { get; set; }

        /// <summary>门禁放行规则。</summary>
        public OpenHardwarePassRuleContainer pass_rule { get; set; }
    }

    /// <summary>门禁规则列表容器。</summary>
    public class OpenHardwarePassRuleContainer
    {
        /// <summary>门禁规则列表。</summary>
        public List<OpenHardwarePassRule> rule_list { get; set; }
    }

    /// <summary>门禁放行规则。</summary>
    public class OpenHardwarePassRule
    {
        /// <summary>规则 ID。</summary>
        public long id { get; set; }

        /// <summary>放行规则表达式。</summary>
        public string rule { get; set; }

        /// <summary>规则生效时间，Unix 时间戳。</summary>
        public long effect_time { get; set; }
    }

    /// <summary>固件升级结果请求。</summary>
    public class OpenHardwareFirmwareUpgradeResultRequest
    {
        /// <summary>固件升级操作 ID。</summary>
        public string oper_id { get; set; }

        /// <summary>升级结果错误码，零表示成功。</summary>
        public int errcode { get; set; }

        /// <summary>升级结果错误描述。</summary>
        public string errmsg { get; set; }

        /// <summary>升级后的当前固件版本。</summary>
        public string cur_version { get; set; }
    }

    /// <summary>生成设备管理二维码请求。</summary>
    public class OpenHardwareGenerateLoginQrCodeRequest
    {
        /// <summary>扫码事件中透传回设备的状态值。</summary>
        public string state { get; set; }

        /// <summary>二维码类型：零为静态，一为动态。</summary>
        public int type { get; set; }
    }

    /// <summary>生成设备动态身份识别二维码请求。</summary>
    public class OpenHardwareGenerateIdQrCodeRequest
    {
        /// <summary>设备序列号。</summary>
        public string device_sn { get; set; }

        /// <summary>扫码事件中透传回设备的状态值。</summary>
        public string state { get; set; }
    }

    /// <summary>生成设备二维码结果。</summary>
    public class OpenHardwareQrCodeResult : WorkJsonResult
    {
        /// <summary>可编码为二维码的完整 URL。</summary>
        public string qrcode_content { get; set; }

        /// <summary>动态二维码有效期，单位为秒；静态二维码不返回。</summary>
        public int? expires_in { get; set; }
    }
}
