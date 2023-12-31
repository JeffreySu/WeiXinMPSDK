/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_OpenApprovalChange.cs
    文件功能描述：自建审批申请状态变化回调通知
    官方文档：https://developer.work.weixin.qq.com/document/path/90269
    
    
    创建标识：Senparc - 20220211

    修改标识：Senparc - 20230405
    修改描述：v3.15.17.1 修改 RequestMessageEvent_OpenApprovalChange 中 OpenTemplateId 参数类型为 string

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 审批申请状态变化回调通知
    /// </summary>
    public class RequestMessageEvent_OpenApprovalChange : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.OPEN_APPROVAL_CHANGE; }
        }

        /// <summary>
        /// 审批信息
        /// </summary>
        public OpenApprovalInfo ApprovalInfo { get; set; }
    }

    public class OpenApprovalInfo
    {
        /// <summary>
        /// 审批单编号，由开发者在发起申请时自定义
        /// </summary>
        public string ThirdNo { get; set; }

        /// <summary>
        /// 审批模板名称
        /// </summary>
        public string OpenSpName { get; set; }

        /// <summary>
        ///	审批模板id
        /// </summary>
       // public uint OpenTemplateId { get; set; }
        //企业微信自建应用审批模板id应用使用string类型，否则报错
        public string OpenTemplateId { get; set; }
        
        /// <summary>
        /// 申请单当前审批状态：1-审批中；2-已通过；3-已驳回；4-已撤销
        /// </summary>
        public byte OpenSpStatus { get; set; }

        /// <summary>
        /// 提交申请时间
        /// </summary>
        public uint ApplyTime { get; set; }

        /// <summary>
        /// 提交者姓名
        /// </summary>
        public string ApplyUserName { get; set; }

        /// <summary>
        /// 提交者userid
        /// </summary>
        public string ApplyUserId { get; set; }

        /// <summary>
        /// 提交者所在部门
        /// </summary>
        public string ApplyUserParty { get; set; }

        /// <summary>
        /// 提交者头像
        /// </summary>
        public string ApplyUserImage { get; set; }

        /// <summary>
        /// 审批流程信息
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute("ApprovalNode", IsNullable = false)]
        public OpenApprovalNode[] ApprovalNodes { get; set; }

        /// <summary>
        /// 抄送信息，可能有多个抄送人
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute("NotifyNode", IsNullable = false)]
        public OpenApprovalNotifyNode[] NotifyNodes { get; set; }

        /// <summary>
        /// 当前审批节点：0-第一个审批节点；1-第二个审批节点…以此类推
        /// </summary>
        public byte ApproverStep { get; set; }
    }

    public partial class OpenApprovalNode
    {
        /// <summary>
        /// 节点审批操作状态：1-审批中；2-已同意；3-已驳回；4-已转审
        /// </summary>
        public byte NodeStatus { get; set; }

        /// <summary>
        /// 审批节点属性：1-或签；2-会签
        /// </summary>
        public byte NodeAttr { get; set; }

        /// <summary>
        /// 审批节点类型：1-固定成员；2-标签；3-上级
        /// </summary>
        public byte NodeType { get; set; }

        /// <summary>
        /// 审批节点信息，当节点为标签或上级时，一个节点可能有多个分支
        /// </summary>
        [System.Xml.Serialization.XmlArrayItemAttribute("Item", IsNullable = false)]
        public OpenApprovalNodeItemsItem[] Items { get; set; }
    }

    public partial class OpenApprovalNodeItemsItem
    {
        /// <summary>
        /// 抄送人姓名
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 抄送人userid
        /// </summary>
        public string ItemUserid { get; set; }

        /// <summary>
        /// 抄送人所在部门
        /// </summary>
        public string ItemParty { get; set; }

        /// <summary>
        /// 抄送人头像
        /// </summary>
        public string ItemImage { get; set; }

        /// <summary>
        /// 分支审批审批操作状态：1-审批中；2-已同意；3-已驳回；4-已转审
        /// </summary>
        public byte ItemStatus { get; set; }

        /// <summary>
        /// 分支审批人审批意见
        /// </summary>
        public string ItemSpeech { get; set; }

        /// <summary>
        /// 分支审批人操作时间
        /// </summary>
        public byte ItemOpTime { get; set; }
    }

    public partial class OpenApprovalNotifyNode
    {
        /// <summary>
        /// 抄送人姓名
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 抄送人userid
        /// </summary>
        public string ItemUserid { get; set; }

        /// <summary>
        /// 抄送人所在部门
        /// </summary>
        public string ItemParty { get; set; }

        /// <summary>
        /// 抄送人头像
        /// </summary>
        public string ItemImage { get; set; }
    }



}
