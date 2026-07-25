#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ComplaintP1RequestData.cs
    文件功能描述：ComplaintP1RequestData 强类型数据模型


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v2.5.1 补齐微信支付 V3 退款、投诉、停车、医保、品牌入驻和商户开户接口并增强 HTTP 与通知处理

----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.TenPayV3.Apis.Complaint
{
    /// <summary>
    /// UpdateRefundProgress 接口请求参数。
    /// </summary>
    public class UpdateRefundProgressRequestData
    {
        /// <summary>退款进度操作类型。</summary>
        public string action { get; set; }
        /// <summary>预计发起退款的天数；仅对应操作需要填写。</summary>
        public int? launch_refund_day { get; set; }
        /// <summary>拒绝退款原因。</summary>
        public string reject_reason { get; set; }
        /// <summary>拒绝退款的举证媒体文件 ID 列表。</summary>
        public IEnumerable<string> reject_media_list { get; set; }
        /// <summary>商户补充说明。</summary>
        public string remark { get; set; }
    }

    /// <summary>
    /// ImmediateService 接口请求参数。
    /// </summary>
    public class ImmediateServiceRequestData
    {
        /// <summary>被投诉的二级商户号；服务商模式使用。</summary>
        public string complainted_mchid { get; set; }
        /// <summary>即时服务消息。</summary>
        public ImmediateServiceMessage message { get; set; }
        /// <summary>幂等请求 ID，同一业务请求应保持不变。</summary>
        public string idempotent_id { get; set; }
    }

    /// <summary>
    /// ImmediateServiceMessage 微信接口数据模型。
    /// </summary>
    public class ImmediateServiceMessage
    {
        /// <summary>消息内容块列表。</summary>
        public IEnumerable<ImmediateServiceMessageBlock> blocks { get; set; }
        /// <summary>消息发送方身份。</summary>
        public string sender_identity { get; set; }
        /// <summary>商户自定义透传数据。</summary>
        public string custom_data { get; set; }
    }

    /// <summary>
    /// 即时服务消息块。各子对象与官方 TEXT、IMAGE、LINK、FAQ_LIST、BUTTON、BUTTON_GROUP 结构对应。
    /// 使用 object 保留官方后续扩展字段的兼容性。
    /// </summary>
    public class ImmediateServiceMessageBlock
    {
        /// <summary>消息块类型，例如 TEXT、IMAGE、LINK、FAQ_LIST、BUTTON 或 BUTTON_GROUP。</summary>
        public string type { get; set; }
        /// <summary>TEXT 类型数据。</summary>
        public object text { get; set; }
        /// <summary>IMAGE 类型数据。</summary>
        public object image { get; set; }
        /// <summary>LINK 类型数据。</summary>
        public object link { get; set; }
        /// <summary>FAQ_LIST 类型数据。</summary>
        public object faq_list { get; set; }
        /// <summary>BUTTON 类型数据。</summary>
        public object button { get; set; }
        /// <summary>BUTTON_GROUP 类型数据。</summary>
        public object button_group { get; set; }
    }
}
