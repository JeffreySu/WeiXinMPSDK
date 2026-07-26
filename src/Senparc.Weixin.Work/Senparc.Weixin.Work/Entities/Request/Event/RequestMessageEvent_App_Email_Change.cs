/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RequestMessageEvent_App_Email_Change.cs
    文件功能描述：企业微信应用邮箱新邮件变更回调强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 接入应用邮箱新邮件变更回调

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 应用邮箱新邮件变更事件。
    /// </summary>
    public class RequestMessageEvent_App_Email_Change : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型。
        /// </summary>
        public override Event Event => Event.app_email_change;

        /// <summary>
        /// 变更类型，例如 receive_email。
        /// </summary>
        public string ChangeType { get; set; }

        /// <summary>
        /// 本次新增邮件数量。
        /// </summary>
        public int Amount { get; set; }
    }
}
