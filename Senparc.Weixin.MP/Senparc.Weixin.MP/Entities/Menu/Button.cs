using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 单个按钮
    /// </summary>
    public class Button:BaseButton
    {
        /// <summary>
        /// 子菜单，1-3个
        /// </summary>
        public List<SubButton> sub_button { get; set; }

        public Button()
        {
            sub_button=new List<SubButton>();
        }
    }
}
