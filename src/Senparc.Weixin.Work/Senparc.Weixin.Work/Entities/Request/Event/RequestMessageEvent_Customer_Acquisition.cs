/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_Customer_Acquisition.cs
    文件功能描述：RequestMessageEvent_Customer_Acquisition 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>获客助手额度、链接和客户行为事件。</summary>
    public class RequestMessageEvent_Customer_Acquisition : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event => Event.CUSTOMER_ACQUISITION;

        /// <summary>事件类型，用于区分额度、链接和客户行为通知。</summary>
        public string ChangeType { get; set; }
        /// <summary>获客链接 ID。</summary>
        public string LinkId { get; set; }
        /// <summary>额度到期时间（Unix 时间戳）。</summary>
        public long ExpireTime { get; set; }
        /// <summary>即将到期的额度数量。</summary>
        public long ExpireQuotaNum { get; set; }
        /// <summary>创建获客链接时透传的自定义参数。</summary>
        public string State { get; set; }
        /// <summary>接待成员 UserId。</summary>
        public string UserID { get; set; }
        /// <summary>外部联系人 UserId。</summary>
        public string ExternalUserID { get; set; }
        /// <summary>会话序号。</summary>
        public int ChatSeq { get; set; }
        /// <summary>用于查询会话内容的 ChatKey。</summary>
        public string ChatKey { get; set; }
    }
}
