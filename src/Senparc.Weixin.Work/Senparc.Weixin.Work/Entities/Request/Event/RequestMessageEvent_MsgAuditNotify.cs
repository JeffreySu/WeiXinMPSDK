/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_MsgAuditNotify.cs
    文件功能描述：企业微信会话存档-产生会话回调事件
    
    
    创建标识：IcedMango - 20240229

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// 企业微信会话存档-产生会话回调事件
    /// </summary>
    public class RequestMessageEvent_MsgAuditNotify : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event => Event.MSGAUDIT_NOTIFY;
    }
}
