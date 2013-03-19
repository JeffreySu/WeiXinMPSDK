using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// 整个按钮设置（可以直接用ButtonGroup实例返回JSON对象）
    /// </summary>
    public class ButtonGroup
    {
        /// <summary>
        /// 按钮对象，1-3个
        /// </summary>
        public List<Button> button { get; set; }
    }
}
