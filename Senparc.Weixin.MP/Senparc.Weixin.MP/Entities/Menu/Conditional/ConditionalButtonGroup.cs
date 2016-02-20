/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ConditionalButtonGroup.cs
    文件功能描述：个性化菜单按钮设置（可以直接用ConditionalButtonGroup实例返回JSON对象）
    
    
    创建标识：Senparc - 20151222

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.MP.Entities.Menu;

namespace Senparc.Weixin.MP.Entities.Menu
{
    /// <summary>
    /// 个性化菜单按钮集合
    /// </summary>
    public class ConditionalButtonGroup :ButtonGroupBase, IButtonGroupBase
    {
        public MenuMatchRule matchrule { get; set; }
        /// <summary>
        /// 菜单Id，只在获取的时候自动填充，提交“菜单创建”请求时不需要设置
        /// </summary>
        public long menuid { get; set; }
    }
}
