/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_MeetingRoom.cs
    文件功能描述：企业微信会议室预定与取消回调强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐会议室预定与取消回调事件模型

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>会议室预定事件。</summary>
    public class RequestMessageEvent_Book_Meeting_Room : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>事件类型。</summary>
        public override Event Event => Event.book_meeting_room;

        /// <summary>会议室 ID。</summary>
        public long MeetingRoomId { get; set; }

        /// <summary>会议室预定 ID。</summary>
        public string BookingId { get; set; }
    }

    /// <summary>会议室取消预定事件。</summary>
    public class RequestMessageEvent_Cancel_Meeting_Room : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>事件类型。</summary>
        public override Event Event => Event.cancel_meeting_room;

        /// <summary>会议室 ID。</summary>
        public long MeetingRoomId { get; set; }

        /// <summary>会议室预定 ID。</summary>
        public string BookingId { get; set; }
    }
}
