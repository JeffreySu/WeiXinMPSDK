/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SingleScancodePushButton.cs
    文件功能描述：调起扫一扫工具按钮
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20181005
    修改描述：菜单按钮类型（ButtonType）改为使用 Senparc.NeuChar.MenuButtonType
----------------------------------------------------------------*/
using Senparc.NeuChar;

namespace Senparc.Weixin.Work.Entities.Menu
{
    /// <summary>
    /// 单个按键
    /// </summary>
    public class SingleScancodePushButton : SingleButton
    {
        /// <summary>
        /// 类型为scancode_push时必须。
        /// 用户点击按钮后，微信客户端将调起扫一扫工具，完成扫码操作后显示扫描结果（如果是URL，将进入URL），且会将扫码的结果传给开发者，开发者可以下发消息。
        /// 仅支持微信iPhone5.4.1以上版本，和Android5.4以上版本的微信用户，旧版本微信用户点击后将没有回应，开发者也不能正常接收到事件推送。
        /// </summary>
        public string key { get; set; }

        public SingleScancodePushButton()
            : base(MenuButtonType.scancode_push.ToString())
        {
        }
    }
}
