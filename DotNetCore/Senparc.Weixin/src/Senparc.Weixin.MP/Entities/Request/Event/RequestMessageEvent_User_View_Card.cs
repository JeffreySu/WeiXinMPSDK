/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageEvent_User_View_Card.cs
    文件功能描述：事件之进入会员卡
    
    
    创建标识：Senparc - 20150722
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_User_View_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 进入会员卡
        /// </summary>
        public override Event Event
        {
            get { return Event.user_view_card; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 商户自定义code值。非自定code推送为空串。
        /// </summary>
        public string UserCardCode { get; set; }
    }
}
