/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：Page.cs
    文件功能描述：小程序搜索，页面参数
    
    
    创建标识：lishewen - 20191221
    
----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.Search
{
    public class Page
    {
        /// <summary>
        /// 页面路径
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 页面参数
        /// </summary>
        public string query { get; set; }
    }
}
