using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// 单个按键
    /// </summary>
    public class SingleScancodePushButton : SingleButton
    {
        /// <summary>
        /// 类型为scancode_push时必须。
        /// 用户点击按钮后，微信客户端将调起扫一扫工具，完成扫码操作后显示扫描结果（如果是URL，将进入URL），且会将扫码的结果传给开发者，开发者可以下发消息。
        /// </summary>
        public string key { get; set; }

        public SingleScancodePushButton()
            : base(ButtonType.scancode_push.ToString())
        {
        }
    }
}
