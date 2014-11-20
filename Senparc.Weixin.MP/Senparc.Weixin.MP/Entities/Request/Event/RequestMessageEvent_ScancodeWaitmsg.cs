using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之扫码推事件且弹出“消息接收中”提示框(scancode_waitmsg)
    /// </summary>
    public class RequestMessageEvent_ScancodeWaitmsg : RequestMessageEventBase, IRequestMessageEventBase, IRequestMessageEventKey
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.SCANCODEWAITMSG; }
        }

        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 扫描信息
        /// </summary>
        public ScanCodeInfo ScanCodeInfo { get; set; }
    }
}
