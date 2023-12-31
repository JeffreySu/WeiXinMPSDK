/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_SysApprovalChange.cs
    文件功能描述：系统审批申请状态变化回调通知
    官方文档：https://developer.work.weixin.qq.com/document/path/91815
    
    
    创建标识：Senparc - 20220208
    
----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 审批申请状态变化回调通知
    /// </summary>
    public class RequestMessageEvent_SysApprovalChange : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.SYS_APPROVAL_CHANGE; }
        }

        /// <summary>
        /// 申请信息
        /// </summary>
        public SysApprovalInfo ApprovalInfo { get; set; }
    }

    /// <summary>
    /// 申请信息
    /// </summary>
    public partial class SysApprovalInfo
    {
        /// <summary>
        /// 审批编号
        /// </summary>
        public ulong SpNo { get; set; }

        /// <summary>
        /// 审批申请类型名称（审批模板名称）
        /// </summary>
        public string SpName { get; set; }

        /// <summary>
        /// 申请单状态：1-审批中；2-已通过；3-已驳回；4-已撤销；6-通过后撤销；7-已删除；10-已支付
        /// </summary>
        public byte SpStatus { get; set; }

        /// <summary>
        /// 审批模板id。可在“获取审批申请详情”、“审批状态变化回调通知”中获得，也可在审批模板的模板编辑页面链接中获得。
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// 审批申请提交时间,Unix时间戳
        /// </summary>
        public uint ApplyTime { get; set; }

        /// <summary>
        /// 申请人信息
        /// </summary>
        public ApprovalInfoApplyer Applyer { get; set; }

        /// <summary>
        /// 审批流程信息，可能有多个审批节点。
        /// </summary>
        //[System.Xml.Serialization.XmlArrayAttribute("SpRecord", IsNullable = false)]
        [System.Xml.Serialization.XmlElement("SpRecord")]
        public ApprovalInfoSpRecord[] SpRecords { get; set; }


        /// <summary>
        /// 抄送信息，可能有多个抄送节点
        /// </summary>
        [System.Xml.Serialization.XmlElement("Notifyer")]
        public ApprovalInfoNotifyer[] Notifyers { get; set; }

        /// <summary>
        /// 审批申请备注信息，可能有多个备注节点
        /// TODO：官方示例中XML只支持一个节点
        /// </summary>
        [System.Xml.Serialization.XmlElement("Comments")]
        public ApprovalInfoComments[] Comments { get; set; }

        /// <summary>
        /// 审批申请状态变化类型：1-提单；2-同意；3-驳回；4-转审；5-催办；6-撤销；8-通过后撤销；10-添加备注
        /// </summary>
        public byte StatuChangeEvent { get; set; }
    }

    public partial class ApprovalInfoApplyer
    {
        /// <summary>
        /// 申请人userid
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 申请人所在部门pid
        /// </summary>
        public string Party { get; set; }
    }

    public partial class ApprovalInfoSpRecord
    {
        /// <summary>
        /// 审批节点状态：1-审批中；2-已同意；3-已驳回；4-已转审
        /// </summary>
        public byte SpStatus { get; set; }

        /// <summary>
        /// 节点审批方式：1-或签；2-会签
        /// </summary>
        public byte ApproverAttr { get; set; }

        /// <summary>
        /// 审批节点详情。当节点为标签或上级时，一个节点可能有多个分支
        /// TODO：官方示例中XML只支持一个节点
        /// </summary>
        [System.Xml.Serialization.XmlElement("Details")]
        public ApprovalInfoSpRecordDetails[] Details { get; set; }
    }

    /// <summary>
    /// 审批节点详情。当节点为标签或上级时，一个节点可能有多个分支
    /// </summary>
    public partial class ApprovalInfoSpRecordDetails
    {
        /// <summary>
        /// 分支审批人
        /// </summary>
        public ApprovalInfoSpRecordDetailsApprover Approver { get; set; }

        /// <summary>
        /// 审批意见字段
        /// </summary>
        public string Speech { get; set; }

        /// <summary>
        /// 分支审批人审批状态：1-审批中；2-已同意；3-已驳回；4-已转审
        /// </summary>
        public byte SpStatus { get; set; }

        /// <summary>
        /// 节点分支审批人审批操作时间，0为尚未操作
        /// </summary>
        public uint SpTime { get; set; }

        /// <summary>
        /// 节点分支审批人审批意见附件，赋值为media_id具体使用请参考：<see href="https://developer.work.weixin.qq.com/document/path/90254">文档-获取临时素材</see>
        /// </summary>
        public string Attach { get; set; }
    }

    public partial class ApprovalInfoSpRecordDetailsApprover
    {
        /// <summary>
        /// 分支审批人userid
        /// </summary>
        public string UserId { get; set; }
    }

    public partial class ApprovalInfoNotifyer
    {
        /// <summary>
        /// 节点抄送人userid
        /// </summary>
        public string UserId { get; set; }
    }

    public partial class ApprovalInfoComments
    {
        /// <summary>
        /// 备注人信息
        /// </summary>
        public ApprovalInfoCommentsCommentUserInfo CommentUserInfo { get; set; }

        /// <summary>
        /// 备注提交时间
        /// </summary>
        public ulong CommentTime { get; set; }

        /// <summary>
        /// 备注文本内容
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// 备注id
        /// </summary>
        public string CommentId { get; set; }

        /// <summary>
        /// 节点分支审批人审批意见附件，赋值为media_id具体使用请参考：<see href="https://developer.work.weixin.qq.com/document/path/90254">文档-获取临时素材</see>
        /// </summary>
        public string Attach { get; set; }
    }

    public partial class ApprovalInfoCommentsCommentUserInfo
    {
        /// <summary>
        /// 备注人userid
        /// </summary>
        public string UserId { get; set; }
    }


}
