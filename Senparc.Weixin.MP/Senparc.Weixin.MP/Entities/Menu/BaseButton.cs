using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 所有按钮基类
    /// </summary>
    public class BaseButton
    {
        /// <summary>
        /// 按钮类型
        /// </summary>
        public ButtonType type { get; set; }
        /// <summary>
        /// 按钮描述，既按钮名字，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 类型为click时必须。
        /// 按钮KEY值，用于消息接口(event类型)推送，不超过128字节
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 类型为view时必须
        /// 网页链接，用户点击按钮可打开链接，不超过256字节
        /// </summary>
        public string url { get; set; }
    }
}
