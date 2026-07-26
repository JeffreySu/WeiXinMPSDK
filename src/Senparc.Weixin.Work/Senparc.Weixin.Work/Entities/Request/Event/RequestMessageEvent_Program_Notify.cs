/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_Program_Notify.cs
    文件功能描述：企业微信数据与智能专区程序通知应用事件模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 接入专区程序通知应用事件

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 数据与智能专区程序通知应用事件。
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/99998</para>
    /// </summary>
    public class RequestMessageEvent_Program_Notify : RequestMessageEventBase,
        IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型，固定为 <see cref="Event.program_notify"/>。
        /// </summary>
        public override Event Event => Event.program_notify;

        /// <summary>
        /// 十分钟内有效的通知 ID，可用于应用同步调用专区程序。
        /// </summary>
        public string NotifyId { get; set; }

        /// <summary>
        /// 专区程序调用应用时指定的通知场景值。
        /// </summary>
        public int NotifyScene { get; set; }
    }
}
