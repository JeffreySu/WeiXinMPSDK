/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_Change_Chain.cs
    文件功能描述：企业微信上下游变更回调强类型模型

    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增上下游空间、分组与企业变更回调模型
----------------------------------------------------------------*/

using System.Xml.Serialization;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 上下游空间、分组或企业变更事件。
    /// </summary>
    public class RequestMessageEvent_Change_Chain : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型。
        /// </summary>
        public override Event Event => Event.change_chain;

        /// <summary>
        /// 变更类型，包括 create_chain、update_chain、delete_chain、create_group、
        /// update_group、delete_group、corp_join、update_corp 和 remove_corp。
        /// </summary>
        public string ChangeType { get; set; }

        /// <summary>
        /// 上下游 ID。
        /// </summary>
        public string ChainId { get; set; }

        /// <summary>
        /// 发生变更的分组 ID 列表。
        /// </summary>
        public ChangeChainGroupIds GroupIds { get; set; }

        /// <summary>
        /// 发生变更的企业 ID 列表。
        /// </summary>
        public ChangeChainCorpIds CorpIds { get; set; }
    }

    /// <summary>
    /// 上下游变更事件中的分组 ID 容器。
    /// </summary>
    public class ChangeChainGroupIds
    {
        /// <summary>
        /// 分组 ID。
        /// </summary>
        [XmlElement("GroupId")]
        public long[] Items { get; set; }
    }

    /// <summary>
    /// 上下游变更事件中的企业 ID 容器。
    /// </summary>
    public class ChangeChainCorpIds
    {
        /// <summary>
        /// 企业 ID。
        /// </summary>
        [XmlElement("CorpId")]
        public string[] Items { get; set; }
    }
}
