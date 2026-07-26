/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_Change_School_Contact.cs
    文件功能描述：企业微信家校通讯录变更事件强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 接入家校通讯录成员和部门变更事件

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 家校通讯录成员或部门变更事件。
    /// <para>成员变更文档：https://developer.work.weixin.qq.com/document/path/92032</para>
    /// <para>部门变更文档：https://developer.work.weixin.qq.com/document/path/92052</para>
    /// </summary>
    public class RequestMessageEvent_Change_School_Contact : RequestMessageEventBase,
        IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型，固定为 <see cref="Event.change_school_contact"/>。
        /// </summary>
        public override Event Event => Event.change_school_contact;

        /// <summary>
        /// 变更类型。成员事件包括学生/家长创建、更新、删除及家长关注、取消关注；
        /// 部门事件包括创建、更新和删除。官方部门 XML 示例同时存在 department 与
        /// deparmtment 两种拼写，因此保留原始字符串。
        /// </summary>
        public string ChangeType { get; set; }

        /// <summary>
        /// 发生变更的学生、家长家校通讯录 UserId，或部门 ID。
        /// </summary>
        public string Id { get; set; }
    }
}
