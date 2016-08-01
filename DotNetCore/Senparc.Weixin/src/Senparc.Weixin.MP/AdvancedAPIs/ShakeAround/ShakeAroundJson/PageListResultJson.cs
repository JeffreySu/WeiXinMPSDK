/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：PageListResultJson.cs
    文件功能描述：批量查询页面统计数据的返回结果
    
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
    /// <summary>
    /// 批量查询页面统计数据的返回结果
    /// </summary>
    public class PageListResultJson : WxJsonResult 
    {
        public PageList_Data data { get; set; }

       
    }
    public class PageList_Data
    {
        public List<PageList_Pages> pages { get; set; }


        /// <summary>
        /// 所查询的日期时间戳，单位为秒
        /// </summary>
        public long date { get; set; }
        /// <summary>
        /// 页面总数
        /// </summary>
        public int total_count { get; set; }
        /// <summary>
        /// 所查询的结果页序号；返回结果按摇周边人数降序排序，每50条记录为一页
        /// </summary>
        public int page_index { get; set; }
    }
    public class PageList_Pages
    {
        /// <summary>
        /// 页面ID
        /// </summary>
        public int page_id { get; set; }
        /// <summary>
        /// 点击摇周边消息的次数
        /// </summary>
        public int click_pv { get; set; }
        /// <summary>
        /// 点击摇周边消息的人数
        /// </summary>
        public int click_uv { get; set; }
        /// <summary>
        /// 摇周边的次数
        /// </summary>
        public int shake_pv { get; set; }
        /// <summary>
        /// 摇周边的人数
        /// </summary>
        public int shake_uv { get; set; }
    }
}
