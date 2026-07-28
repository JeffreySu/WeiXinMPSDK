#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：FundAppAuthorizationModels.cs
    文件功能描述：商家转账免确认收款授权请求与返回模型


    创建标识：Senparc - 20260728

    修改标识：Senparc - 20260728
    修改描述：v2.6.0 新增免确认收款授权、授权查询及授权后转账模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using Senparc.Weixin.TenPayV3.Attributes;

namespace Senparc.Weixin.TenPayV3.Apis.FundApp
{
    /// <summary>
    /// 预受理免确认收款转账请求。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4014399293</para>
    /// </summary>
    public class PreTransferWithAuthorizationRequestData
    {
        /// <summary>商户 AppID，必须与商户号存在绑定关系。</summary>
        public string appid { get; set; }

        /// <summary>商户系统内部唯一的商户单号。</summary>
        public string out_bill_no { get; set; }

        /// <summary>商户平台申请的转账场景 ID。</summary>
        public string transfer_scene_id { get; set; }

        /// <summary>收款用户在商户 AppID 下的 OpenID。</summary>
        public string openid { get; set; }

        /// <summary>收款用户姓名明文；SDK 在发送请求前自动加密。</summary>
        [FieldEncrypt]
        public string user_name { get; set; }

        /// <summary>转账金额，单位为分。</summary>
        public int transfer_amount { get; set; }

        /// <summary>用户可见的转账备注。</summary>
        public string transfer_remark { get; set; }

        /// <summary>转账结果通知地址，必须为公网可访问的 HTTPS URL。</summary>
        public string notify_url { get; set; }

        /// <summary>用户收款时展示的收款原因。</summary>
        public string user_recv_perception { get; set; }

        /// <summary>按转账场景要求填写的报备信息。</summary>
        public Transfer_Scene_Report_Info[] transfer_scene_report_infos { get; set; }

        /// <summary>本次免确认收款授权信息。</summary>
        public TransferAuthorizationInfo authorization_info { get; set; }

        /// <summary>出资商户号；服务商模式按官方规则填写。</summary>
        public string sponsor_mchid { get; set; }
    }

    /// <summary>商户侧免确认收款授权信息。</summary>
    public class TransferAuthorizationInfo
    {
        /// <summary>用户确认授权时展示的商户名称。</summary>
        public string user_display_name { get; set; }

        /// <summary>商户系统内部唯一的授权单号。</summary>
        public string out_authorization_no { get; set; }

        /// <summary>授权结果通知地址，必须为公网可访问的 HTTPS URL。</summary>
        public string authorization_notify_url { get; set; }
    }

    /// <summary>预受理免确认收款转账返回数据。</summary>
    public class PreTransferWithAuthorizationReturnJson : ReturnJsonBase
    {
        /// <summary>商户单号。</summary>
        public string out_bill_no { get; set; }

        /// <summary>微信转账单号。</summary>
        public string transfer_bill_no { get; set; }

        /// <summary>单据创建时间，格式为 RFC 3339。</summary>
        public string create_time { get; set; }

        /// <summary>转账单状态。</summary>
        public string state { get; set; }

        /// <summary>用于调起微信授权确认页的跳转信息。</summary>
        public string package_info { get; set; }

        /// <summary>用户确认授权时展示的商户名称。</summary>
        public string user_display_name { get; set; }

        /// <summary>商户授权单号。</summary>
        public string out_authorization_no { get; set; }
    }

    /// <summary>
    /// 创建用户确认免确认收款授权请求。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4015901167</para>
    /// </summary>
    public class CreateUserConfirmAuthorizationRequestData
    {
        /// <summary>商户系统内部唯一的授权单号。</summary>
        public string out_authorization_no { get; set; }

        /// <summary>商户 AppID。</summary>
        public string appid { get; set; }

        /// <summary>用户在商户 AppID 下的 OpenID。</summary>
        public string openid { get; set; }

        /// <summary>商户平台申请的转账场景 ID。</summary>
        public string transfer_scene_id { get; set; }

        /// <summary>用户确认授权时展示的商户名称。</summary>
        public string user_display_name { get; set; }

        /// <summary>用户收款时展示的收款原因。</summary>
        public string user_recv_perception { get; set; }

        /// <summary>授权结果通知地址，必须为公网可访问的 HTTPS URL。</summary>
        public string authorization_notify_url { get; set; }

        /// <summary>用户确认授权时的客户端场景信息。</summary>
        public TransferAuthorizationSceneInfo scene_info { get; set; }
    }

    /// <summary>用户确认授权的客户端场景信息。</summary>
    public class TransferAuthorizationSceneInfo
    {
        /// <summary>用户终端 IP 地址。</summary>
        public string client_ip { get; set; }

        /// <summary>商户侧设备标识。</summary>
        public string device_id { get; set; }

        /// <summary>设备类型：IOS、ANDROID、HARMONY 或 OTHER。</summary>
        public string device_type { get; set; }
    }

    /// <summary>创建用户确认免确认收款授权返回数据。</summary>
    public class CreateUserConfirmAuthorizationReturnJson : ReturnJsonBase
    {
        /// <summary>商户授权单号。</summary>
        public string out_authorization_no { get; set; }

        /// <summary>授权状态，创建成功时通常为 WAIT_USER_CONFIRM。</summary>
        public string state { get; set; }

        /// <summary>授权单创建时间，格式为 RFC 3339。</summary>
        public string create_time { get; set; }

        /// <summary>用于调起微信授权确认页的跳转信息。</summary>
        public string package_info { get; set; }
    }

    /// <summary>用户免确认收款授权详情。</summary>
    public class UserConfirmAuthorizationReturnJson : ReturnJsonBase
    {
        /// <summary>商户授权单号。</summary>
        public string out_authorization_no { get; set; }

        /// <summary>商户 AppID。</summary>
        public string appid { get; set; }

        /// <summary>用户在商户 AppID 下的 OpenID。</summary>
        public string openid { get; set; }

        /// <summary>用户确认授权时展示的商户名称。</summary>
        public string user_display_name { get; set; }

        /// <summary>微信侧授权 ID。</summary>
        public string authorization_id { get; set; }

        /// <summary>授权状态。</summary>
        public string state { get; set; }

        /// <summary>授权成功时间，格式为 RFC 3339。</summary>
        public string authorize_time { get; set; }

        /// <summary>授权关闭信息。</summary>
        public TransferAuthorizationCloseInfo close_info { get; set; }

        /// <summary>商户平台申请的转账场景 ID。</summary>
        public string transfer_scene_id { get; set; }

        /// <summary>用户收款时展示的收款原因。</summary>
        public string user_recv_perception { get; set; }

        /// <summary>授权单创建时间，格式为 RFC 3339。</summary>
        public string create_time { get; set; }

        /// <summary>用于调起微信授权确认页的跳转信息。</summary>
        public string package_info { get; set; }
    }

    /// <summary>授权关闭信息。</summary>
    public class TransferAuthorizationCloseInfo
    {
        /// <summary>授权关闭时间，格式为 RFC 3339。</summary>
        public string close_time { get; set; }

        /// <summary>授权关闭原因。</summary>
        public string close_reason { get; set; }
    }

    /// <summary>
    /// 使用免确认收款授权发起转账的请求。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4014399371</para>
    /// </summary>
    public class TransferWithAuthorizationRequestData
    {
        /// <summary>商户 AppID。</summary>
        public string appid { get; set; }

        /// <summary>商户系统内部唯一的商户单号。</summary>
        public string out_bill_no { get; set; }

        /// <summary>商户平台申请的转账场景 ID。</summary>
        public string transfer_scene_id { get; set; }

        /// <summary>收款用户姓名明文；SDK 在发送请求前自动加密。</summary>
        [FieldEncrypt]
        public string user_name { get; set; }

        /// <summary>转账金额，单位为分。</summary>
        public int transfer_amount { get; set; }

        /// <summary>用户可见的转账备注。</summary>
        public string transfer_remark { get; set; }

        /// <summary>用户收款时展示的收款原因。</summary>
        public string user_recv_perception { get; set; }

        /// <summary>按转账场景要求填写的报备信息。</summary>
        public Transfer_Scene_Report_Info[] transfer_scene_report_infos { get; set; }

        /// <summary>微信侧授权 ID；与 out_authorization_no 按官方规则二选一。</summary>
        public string authorization_id { get; set; }

        /// <summary>商户授权单号；与 authorization_id 按官方规则二选一。</summary>
        public string out_authorization_no { get; set; }

        /// <summary>出资商户号；服务商模式按官方规则填写。</summary>
        public string sponsor_mchid { get; set; }
    }

    /// <summary>使用免确认收款授权发起转账的返回数据。</summary>
    public class TransferWithAuthorizationReturnJson : ReturnJsonBase
    {
        /// <summary>商户号。</summary>
        public string mch_id { get; set; }

        /// <summary>商户单号。</summary>
        public string out_bill_no { get; set; }

        /// <summary>微信转账单号。</summary>
        public string transfer_bill_no { get; set; }

        /// <summary>商户 AppID。</summary>
        public string appid { get; set; }

        /// <summary>转账单状态。</summary>
        public string state { get; set; }

        /// <summary>转账金额，单位为分。</summary>
        public int transfer_amount { get; set; }

        /// <summary>用户可见的转账备注。</summary>
        public string transfer_remark { get; set; }

        /// <summary>转账失败原因。</summary>
        public string fail_reason { get; set; }

        /// <summary>收款用户 OpenID。</summary>
        public string openid { get; set; }

        /// <summary>收款用户姓名密文。</summary>
        public string user_name { get; set; }

        /// <summary>单据创建时间，格式为 RFC 3339。</summary>
        public string create_time { get; set; }

        /// <summary>最后更新时间，格式为 RFC 3339。</summary>
        public string update_time { get; set; }
    }
}
