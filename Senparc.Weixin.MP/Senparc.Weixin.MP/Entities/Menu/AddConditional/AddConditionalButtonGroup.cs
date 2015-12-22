/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：AddConditionalButtonGroup.cs
    文件功能描述：个性化菜单按钮设置（可以直接用AddConditionalButtonGroup实例返回JSON对象）
    
    
    创建标识：Senparc - 20151222

----------------------------------------------------------------*/

using Senparc.Weixin.MP.Entities.Menu.AddConditional;

namespace Senparc.Weixin.MP.Entities.Menu
{
    public class AddConditionalButtonGroup : ButtonGroup
    {
        public MenuMatchRule matchrule { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public int menuid { get; set; }
    }
}
