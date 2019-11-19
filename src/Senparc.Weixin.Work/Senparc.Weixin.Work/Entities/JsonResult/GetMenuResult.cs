/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetMenuResult.cs
    文件功能描述：获取菜单返回的Json结果
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20190129
    修改描述：添加 WorkJsonResult 作为基类

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.Entities.Menu;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>
    /// GetMenu返回的Json结果
    /// </summary>
    public class GetMenuResult: WorkJsonResult
    {
        public ButtonGroup menu { get; set; }

        public GetMenuResult()
        {
            menu = new ButtonGroup();
        }
    }
}
