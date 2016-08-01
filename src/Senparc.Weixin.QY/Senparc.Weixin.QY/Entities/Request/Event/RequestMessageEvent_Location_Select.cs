/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：RequestMessageEvent_LocationSelect.cs
    文件功能描述：事件之弹出地理位置选择器（location_select）
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities
{
    /// <summary>
    /// 事件之弹出地理位置选择器（location_select）
    /// </summary>
    public class RequestMessageEvent_Location_Select : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.LOCATION_SELECT; }
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 发送的位置信息
        /// </summary>
        public SendLocationInfo SendLocationInfo { get; set; }
    }
}
