/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SingleViewButton.cs
    文件功能描述：Url按钮
    
    
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
    /// Url按键
    /// </summary>
    public class SingleViewButton : SingleButton
    {
        /// <summary>
        /// 类型为view时必须
        /// 网页链接，用户点击按钮可打开链接，不超过256字节
        /// </summary>
        public string url { get; set; }

        public SingleViewButton()
            : base(MenuButtonType.view.ToString())
        {
        }
    }
}
