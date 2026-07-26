/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_Public_Email_Change.cs
    文件功能描述：企业微信公共邮箱接收邮件事件强类型模型


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 接入公共邮箱接收邮件事件

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 公共邮箱接收邮件事件。
    /// <para>文档：https://developer.work.weixin.qq.com/document/path/100180</para>
    /// </summary>
    public class RequestMessageEvent_Public_Email_Change : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型，固定为 <see cref="Event.public_email_change"/>。
        /// </summary>
        public override Event Event => Event.public_email_change;

        /// <summary>
        /// 变更类型，当前固定为 receive_email。
        /// </summary>
        public string ChangeType { get; set; }

        /// <summary>
        /// 公共邮箱 ID。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 公共邮箱当前的新邮件数量。
        /// </summary>
        public int Amount { get; set; }
    }
}
