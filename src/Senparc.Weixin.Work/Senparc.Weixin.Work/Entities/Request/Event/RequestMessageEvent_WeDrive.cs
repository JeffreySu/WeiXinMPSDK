/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_WeDrive.cs
    文件功能描述：企业微信微盘回调事件强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 接入微盘容量、空间和文件变更事件

----------------------------------------------------------------*/

using System.Linq;
using System.Xml.Linq;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 微盘容量不足事件。企业微盘容量使用率超过 90% 时，企业微信按日检测并通知有权限的第三方应用。
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/97898</para>
    /// </summary>
    public class RequestMessageEvent_WeDrive_Insufficient_Capacity : RequestMessageEventBase,
        IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型，固定为 <see cref="Event.wedrive_insufficient_capacity"/>。
        /// </summary>
        public override Event Event => Event.wedrive_insufficient_capacity;
    }

    /// <summary>
    /// 微盘空间变更事件，包括空间解散、成员变更和安全设置变更。
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/97899</para>
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/97901</para>
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/97902</para>
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/97903</para>
    /// </summary>
    public class RequestMessageEvent_WeDrive_Space_Change : RequestMessageEventBase,
        IRequestMessageEventBase
    {
        /// <summary>
        /// 创建微盘空间变更事件，并解析可重复出现的 SpaceId 节点。
        /// </summary>
        /// <param name="root">回调 XML 根节点；可为 <see langword="null"/>。</param>
        public RequestMessageEvent_WeDrive_Space_Change(XElement root = null)
        {
            SpaceIds = root?.Elements("SpaceId").Select(element => element.Value).ToArray()
                       ?? new string[0];
        }

        /// <summary>
        /// 事件类型，固定为 <see cref="Event.wedrive_space_change"/>。
        /// </summary>
        public override Event Event => Event.wedrive_space_change;

        /// <summary>
        /// 变更类型：dismiss_space、space_member_change 或 space_security_settings_change。
        /// </summary>
        public string ChangeType { get; set; }

        /// <summary>
        /// 本次发生变更的微盘空间 ID 列表，对应回调中可重复出现的 SpaceId 节点。
        /// </summary>
        public string[] SpaceIds { get; }
    }

    /// <summary>
    /// 微盘文件变更事件，包括文件创建、重命名、内容更新、删除和移动。
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/97900</para>
    /// </summary>
    public class RequestMessageEvent_WeDrive_File_Change : RequestMessageEventBase,
        IRequestMessageEventBase
    {
        /// <summary>
        /// 创建微盘文件变更事件，并解析可重复出现的 FileId 节点。
        /// </summary>
        /// <param name="root">回调 XML 根节点；可为 <see langword="null"/>。</param>
        public RequestMessageEvent_WeDrive_File_Change(XElement root = null)
        {
            FileIds = root?.Elements("FileId").Select(element => element.Value).ToArray()
                      ?? new string[0];
        }

        /// <summary>
        /// 事件类型，固定为 <see cref="Event.wedrive_file_change"/>。
        /// </summary>
        public override Event Event => Event.wedrive_file_change;

        /// <summary>
        /// 变更类型：create_file、rename_file、update_file、delete_file 或 move_file。
        /// </summary>
        public string ChangeType { get; set; }

        /// <summary>
        /// 本次发生变更的微盘文件 ID 列表，对应回调中可重复出现的 FileId 节点。
        /// </summary>
        public string[] FileIds { get; }
    }
}
