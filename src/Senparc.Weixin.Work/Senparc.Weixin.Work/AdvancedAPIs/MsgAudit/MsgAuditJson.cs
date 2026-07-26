/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MsgAuditJson.cs
    文件功能描述：企业微信会话内容存档 HTTP 接口强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260726
    修改描述：v3.32.1 补齐会话内容存档成员、内部群和同意状态模型；补齐会话内容存档机器人信息模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.MsgAudit
{
    /// <summary>
    /// 会话内容存档机器人信息。
    /// </summary>
    public class MsgAuditRobotInfo
    {
        /// <summary>获取或设置机器人 ID。</summary>
        public string robot_id { get; set; }

        /// <summary>获取或设置机器人名称。</summary>
        public string name { get; set; }

        /// <summary>获取或设置创建机器人的成员 UserId。</summary>
        public string creator_userid { get; set; }
    }

    /// <summary>
    /// 获取会话内容存档机器人信息的结果。
    /// </summary>
    public class GetMsgAuditRobotInfoResult : WorkJsonResult
    {
        /// <summary>获取或设置机器人信息。</summary>
        public MsgAuditRobotInfo data { get; set; }
    }

    /// <summary>
    /// 查询单聊会话内容存档同意状态的请求。
    /// </summary>
    public class CheckSingleAgreeRequest
    {
        /// <summary>
        /// 待查询的企业成员与外部成员会话列表。
        /// </summary>
        public IList<MsgAuditConversationInfo> info { get; set; }
    }

    /// <summary>
    /// 单聊会话双方的标识信息。
    /// </summary>
    public class MsgAuditConversationInfo
    {
        /// <summary>
        /// 企业成员 UserID。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 外部成员在会话内容存档场景中的 OpenID。
        /// <para>属性名沿用企业微信协议中的 <c>exteranalopenid</c> 拼写。</para>
        /// </summary>
        public string exteranalopenid { get; set; }
    }

    /// <summary>
    /// 会话内容存档群聊查询请求。
    /// </summary>
    public class MsgAuditRoomRequest
    {
        /// <summary>
        /// 待查询的群聊 ID。
        /// </summary>
        public string roomid { get; set; }
    }

    /// <summary>
    /// 已开启会话内容存档的成员列表结果。
    /// </summary>
    public class GetPermitUserListResult : WorkJsonResult
    {
        /// <summary>
        /// 已开启会话内容存档的企业成员 UserID 列表。
        /// </summary>
        public IList<string> ids { get; set; }
    }

    /// <summary>
    /// 会话内容存档内部群信息结果。
    /// </summary>
    public class GetGroupChatResult : WorkJsonResult
    {
        /// <summary>
        /// 群聊名称。
        /// </summary>
        public string roomname { get; set; }

        /// <summary>
        /// 群主的企业成员 UserID。
        /// </summary>
        public string creator { get; set; }

        /// <summary>
        /// 群聊创建时间，Unix 时间戳（秒）。
        /// </summary>
        public long room_create_time { get; set; }

        /// <summary>
        /// 群公告。
        /// </summary>
        public string notice { get; set; }

        /// <summary>
        /// 群成员列表。
        /// </summary>
        public IList<MsgAuditGroupChatMember> members { get; set; }
    }

    /// <summary>
    /// 会话内容存档内部群成员信息。
    /// </summary>
    public class MsgAuditGroupChatMember
    {
        /// <summary>
        /// 群成员 ID；企业成员为 UserID，外部成员为外部联系人标识。
        /// </summary>
        public string memberid { get; set; }

        /// <summary>
        /// 加入群聊的时间，Unix 时间戳（秒）。
        /// </summary>
        public long jointime { get; set; }
    }

    /// <summary>
    /// 会话内容存档同意状态查询结果。
    /// </summary>
    public class CheckAgreeResult : WorkJsonResult
    {
        /// <summary>
        /// 各会话成员的同意状态列表。
        /// </summary>
        public IList<MsgAuditAgreeInfo> agreeinfo { get; set; }
    }

    /// <summary>
    /// 会话内容存档同意状态信息。
    /// </summary>
    public class MsgAuditAgreeInfo
    {
        /// <summary>
        /// 企业成员 UserID。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 外部成员在会话内容存档场景中的 OpenID。
        /// <para>属性名沿用企业微信协议中的 <c>exteranalopenid</c> 拼写。</para>
        /// </summary>
        public string exteranalopenid { get; set; }

        /// <summary>
        /// 会话内容存档同意状态，取值由企业微信协议返回。
        /// </summary>
        public string agree_status { get; set; }

        /// <summary>
        /// 同意状态最后变更时间，Unix 时间戳（秒）。
        /// </summary>
        public long status_change_time { get; set; }
    }
}
