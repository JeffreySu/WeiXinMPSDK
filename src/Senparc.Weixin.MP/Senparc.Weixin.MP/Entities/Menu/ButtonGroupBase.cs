/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ButtonGroup.cs
    文件功能描述：整个按钮设置（可以直接用ButtonGroup实例返回JSON对象）
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// 整个按钮设置（可以直接用ButtonGroup实例返回JSON对象）
    /// </summary>
    public abstract class ButtonGroupBase : IButtonGroupBase
    {
        /// <summary>
        /// 按钮数组，按钮个数应为1-3个
        /// </summary>
        public List<BaseButton> button { get; set; }

        public ButtonGroupBase()
        {
            button = new List<BaseButton>();
        }
    }
}
