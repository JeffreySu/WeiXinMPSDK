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
    /// （普通自定义菜单）整个按钮设置（可以直接用ButtonGroup实例返回JSON对象）
    /// </summary>
    public class ButtonGroup : ButtonGroupBase, IButtonGroupBase
    {
        public ButtonGroup()
        {
        }
    }
}
