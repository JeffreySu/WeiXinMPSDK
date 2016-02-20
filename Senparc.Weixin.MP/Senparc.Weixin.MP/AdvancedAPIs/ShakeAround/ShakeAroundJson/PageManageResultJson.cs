/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：PageManageResultJson.cs
    文件功能描述：页面管理返回结果
    
    
    创建标识：Senparc - 20150512
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 新增与编辑页面返回结果基类
    /// </summary>
    public class BasePageResultJson : WxJsonResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public BasePage_Data data { get; set; }
    }

    public class BasePage_Data
    {
        /// <summary>
        /// 页面id
        /// </summary>
        public long page_id { get; set; }
    }

    /// <summary>
    /// 新增页面返回结果
    /// </summary>
    public class AddPageResultJson : BasePageResultJson
    {
        
    }

    /// <summary>
    /// 编辑页面返回结果
    /// </summary>
    public class UpdatePageResultJson : BasePageResultJson
    {

    }

    /// <summary>
    /// 查询页面列表返回结果
    /// </summary>
    public class SearchPagesResultJson : WxJsonResult
    {
        /// <summary>
        /// 查询页面列表返回数据
        /// </summary>
        public SearchPages_Data data { get; set; }
    }

    public class SearchPages_Data
    {
        /// <summary>
        /// 页面基本信息
        /// </summary>
        public List<SearchPages_Data_Page> pages { get; set; }
        /// <summary>
        /// 商户名下的页面总数
        /// </summary>
        public int total_count { get; set; }
    }

    public class SearchPages_Data_Page : WxJsonResult
    {
        /// <summary>
        /// 页面的备注信息
        /// </summary>
        public string comment { get; set; }
        /// <summary>
        /// 在摇一摇页面展示的副标题
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 在摇一摇页面展示的图片
        /// </summary>
        public string icon_url { get; set; }
        /// <summary>
        /// 摇周边页面唯一ID
        /// </summary>
        public long page_id { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string page_url { get; set; }
        /// <summary>
        /// 在摇一摇页面展示的主标题
        /// </summary>
        public string title { get; set; }
    }
}