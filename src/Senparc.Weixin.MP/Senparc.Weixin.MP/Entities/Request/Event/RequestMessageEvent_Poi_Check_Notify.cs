/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageEvent_Poi_Check_Notify.cs
    文件功能描述：事件之审核结果事件推送
    
    
    创建标识：Senparc - 20150513
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之审核结果事件推送
    /// </summary>
    public class RequestMessageEvent_Poi_Check_Notify : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.poi_check_notify; }
        }

        /// <summary>
        /// 商户自己内部ID，即字段中的sid
        /// </summary>
        public string UniqId { get; set; }

        /// <summary>
        /// 微信的门店ID，微信内门店唯一标示ID
        /// </summary>
        public string PoiId { get; set; }

        /// <summary>
        /// 审核结果，成功succ 或失败fail
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 成功的通知信息，或审核失败的驳回理由
        /// </summary>
        public string Msg { get; set; }
    }
}
