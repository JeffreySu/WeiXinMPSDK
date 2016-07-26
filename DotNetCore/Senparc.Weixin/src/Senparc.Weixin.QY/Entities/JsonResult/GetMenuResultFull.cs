/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：GetMenuResultFull.cs
    文件功能描述：获取菜单时候的完整结构，用于接收微信服务器返回的Json信息
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY
{
    /// <summary>
    /// 获取菜单时候的完整结构，用于接收微信服务器返回的Json信息
    /// </summary>
    public class GetMenuResultFull : QyJsonResult
    {
        public MenuFull_ButtonGroup menu { get; set; }
    }

    public class MenuFull_ButtonGroup
    {
        public List<MenuFull_RootButton> button { get; set; }
    }

    public class MenuFull_RootButton
    {
        public string type { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public List<MenuFull_RootButton> sub_button { get; set; }
    }
}
