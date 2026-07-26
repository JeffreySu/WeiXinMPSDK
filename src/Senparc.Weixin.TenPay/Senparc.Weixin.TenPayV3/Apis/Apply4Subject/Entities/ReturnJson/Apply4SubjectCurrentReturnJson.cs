#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：Apply4SubjectCurrentReturnJson.cs
    文件功能描述：微信支付 V3 现行商户开户意愿确认返回模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Apply4Subject
{
    /// <summary>
    /// 商户开户意愿确认申请提交结果。
    /// </summary>
    public class Apply4SubjectApplicationResultJson : ReturnJsonBase
    {
        /// <summary>微信支付分配的申请单编号。</summary>
        public long applyment_id { get; set; }
    }

    /// <summary>
    /// 商户开户意愿确认申请审核结果。
    /// </summary>
    public class Apply4SubjectAuditResultJson : ReturnJsonBase
    {
        /// <summary>申请单状态，例如 APPLYMENT_STATE_PASSED 或 APPLYMENT_STATE_REJECTED。</summary>
        public string applyment_state { get; set; }

        /// <summary>待确认或审核通过时返回的小程序码 Base64 图片数据。</summary>
        public string qrcode_data { get; set; }

        /// <summary>审核驳回或冻结时返回的被驳回字段名。</summary>
        public string reject_param { get; set; }

        /// <summary>审核驳回或冻结时返回的具体原因。</summary>
        public string reject_reason { get; set; }
    }

    /// <summary>
    /// 特约商户开户意愿确认授权状态。
    /// </summary>
    public class Apply4SubjectAuthorizationStateResultJson : ReturnJsonBase
    {
        /// <summary>授权状态：AUTHORIZE_STATE_UNAUTHORIZED 或 AUTHORIZE_STATE_AUTHORIZED。</summary>
        public string authorize_state { get; set; }
    }

    /// <summary>
    /// 商户开户材料图片上传结果。
    /// </summary>
    public class Apply4SubjectMediaUploadResultJson : ReturnJsonBase
    {
        /// <summary>微信支付返回的媒体文件标识，可用于申请资料中的图片字段。</summary>
        public string media_id { get; set; }
    }
}
