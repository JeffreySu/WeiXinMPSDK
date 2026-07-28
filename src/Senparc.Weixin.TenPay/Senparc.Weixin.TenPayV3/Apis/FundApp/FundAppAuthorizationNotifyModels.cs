#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：FundAppAuthorizationNotifyModels.cs
    文件功能描述：商家转账免确认收款授权与转账结果通知模型


    创建标识：Senparc - 20260728

    修改标识：Senparc - 20260728
    修改描述：v2.6.0 新增免确认收款授权及转账结果通知模型

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.FundApp
{
    /// <summary>商家转账免确认收款通知事件常量。</summary>
    public static class FundAppAuthorizationNotifyEventTypes
    {
        /// <summary>商家转账单已完成。</summary>
        public const string TransferBillFinished = "MCHTRANSFER.BILL.FINISHED";

        /// <summary>用户已确认免确认收款授权。</summary>
        public const string AuthorizationConfirmed =
            "MCHTRANSFER.AUTHORIZATION.CONFIRMED";

        /// <summary>免确认收款授权已关闭。</summary>
        public const string AuthorizationClosed =
            "MCHTRANSFER.AUTHORIZATION.CLOSED";

        /// <summary>转账结果通知资源的 original_type。</summary>
        public const string TransferOriginalType = "mch_payment";
    }

    /// <summary>
    /// 商家转账结果通知的解密资源。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4012712115</para>
    /// </summary>
    public class FundAppTransferResultNotifyJson : ReturnJsonBase
    {
        /// <summary>商户单号。</summary>
        public string out_bill_no { get; set; }

        /// <summary>微信转账单号。</summary>
        public string transfer_bill_no { get; set; }

        /// <summary>转账单状态。</summary>
        public string state { get; set; }

        /// <summary>商户号。</summary>
        public string mch_id { get; set; }

        /// <summary>转账金额，单位为分。</summary>
        public int transfer_amount { get; set; }

        /// <summary>收款用户 OpenID。</summary>
        public string openid { get; set; }

        /// <summary>转账失败原因。</summary>
        public string fail_reason { get; set; }

        /// <summary>单据创建时间，格式为 RFC 3339。</summary>
        public string create_time { get; set; }

        /// <summary>最后更新时间，格式为 RFC 3339。</summary>
        public string update_time { get; set; }

        /// <summary>付款方式类型。</summary>
        public string payment_method_type { get; set; }
    }

    /// <summary>
    /// 免确认收款授权结果通知的解密资源。
    /// <para>官方文档：https://pay.weixin.qq.com/doc/v3/merchant/4014512908</para>
    /// </summary>
    public class FundAppAuthorizationResultNotifyJson : ReturnJsonBase
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
    }
}
