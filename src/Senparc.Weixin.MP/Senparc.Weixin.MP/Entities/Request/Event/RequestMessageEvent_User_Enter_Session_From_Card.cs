/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageEvent_User_Enter_Session_From_Card.cs
    文件功能描述：事件之从卡券进入公众号会话
    
    
    创建标识：Senparc - 20150722
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_User_Enter_Session_From_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 从卡券进入公众号会话
        /// </summary>
        public override Event Event
        {
            get { return Event.user_enter_session_from_card; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 卡券Code码
        /// </summary>
        public string UserCardCode { get; set; }
    }
}
