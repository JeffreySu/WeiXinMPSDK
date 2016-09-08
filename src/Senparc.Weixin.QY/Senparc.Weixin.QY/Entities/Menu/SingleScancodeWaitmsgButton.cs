/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SingleScancodeWaitmsgButton.cs
    文件功能描述：调起扫一扫工具，然后弹出“消息接收中”提示框
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

namespace Senparc.Weixin.QY.Entities.Menu
{
    /// <summary>
    /// 单个按键
    /// </summary>
    public class SingleScancodeWaitmsgButton : SingleButton
    {
        /// <summary>
        /// 类型为scancode_waitmsg时必须。
        /// 用户点击按钮后，微信客户端将调起扫一扫工具，完成扫码操作后，将扫码的结果传给开发者，同时收起扫一扫工具，然后弹出“消息接收中”提示框，随后可能会收到开发者下发的消息。
        /// 仅支持微信iPhone5.4.1以上版本，和Android5.4以上版本的微信用户，旧版本微信用户点击后将没有回应，开发者也不能正常接收到事件推送。
        /// </summary>
        public string key { get; set; }

        public SingleScancodeWaitmsgButton()
            : base(ButtonType.scancode_waitmsg.ToString())
        {
        }
    }
}
