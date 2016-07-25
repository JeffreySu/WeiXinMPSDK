/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SingleLocationSelectButton.cs
    文件功能描述：调起地理位置选择工具按钮
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// 单个按键
    /// </summary>
    public class SingleLocationSelectButton : SingleButton
    {
        /// <summary>
        /// 类型为location_select时必须。
        /// 用户点击按钮后，微信客户端将调起地理位置选择工具，完成选择操作后，将选择的地理位置发送给开发者的服务器，同时收起位置选择工具，随后可能会收到开发者下发的消息。
        /// 仅支持微信iPhone5.4.1以上版本，和Android5.4以上版本的微信用户，旧版本微信用户点击后将没有回应，开发者也不能正常接收到事件推送。
        /// </summary>
        public string key { get; set; }

        public SingleLocationSelectButton()
            : base(ButtonType.location_select.ToString())
        {
        }
    }
}
