/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_AdvancedFeature.cs
    文件功能描述：企业微信高级功能成员申请回调强类型模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐高级功能成员申请回调事件模型

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>成员提交高级功能账号申请事件。</summary>
    public class RequestMessageEvent_Submit_Vip_Account_Approval : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>事件类型。</summary>
        public override Event Event => Event.submit_vip_account_approval;

        /// <summary>申请原因。</summary>
        public string ApplyReason { get; set; }

        /// <summary>高级账号类型：1 邮件、2 文档、3 微盘、4 会议。</summary>
        public uint BusinessType { get; set; }

        /// <summary>申请 ID。</summary>
        public string ApplyId { get; set; }
    }

    /// <summary>成员高级功能账号申请终止事件。</summary>
    public class RequestMessageEvent_Finish_Vip_Account_Approval : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>事件类型。</summary>
        public override Event Event => Event.finish_vip_account_approval;

        /// <summary>终止类型：1 管理员驳回、2 高级账号分配、3 成员删除申请。</summary>
        public uint FinishType { get; set; }

        /// <summary>申请 ID。</summary>
        public string ApplyId { get; set; }
    }
}
