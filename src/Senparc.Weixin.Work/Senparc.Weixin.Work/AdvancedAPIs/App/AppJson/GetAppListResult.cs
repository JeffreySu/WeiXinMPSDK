/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetAppListResult.cs
    文件功能描述：获取应用概况列表返回结果
    
    
    创建标识：Bemguin - 20150614
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.App
{
    /// <summary>
    /// 设置企业号应用需要Post的数据
    /// </summary>
    public class GetAppListResult : WorkJsonResult
    {
        public List<GetAppList_AppInfo> agentlist { get; set; }
    }

    /// <summary>
    /// GetAppList_AppInfo【QY移植修改】
    /// </summary>
    public class GetAppList_AppInfo {
        /// <summary>
        /// 企业应用id
        /// </summary>
        public string agentid { get; set; }
        /// <summary>
        /// 企业应用名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 企业应用方形头像
        /// </summary>
        public string square_logo_url { get; set; }
        ///// <summary>
        ///// 企业应用圆形头像
        ///// </summary>
        //public string round_logo_url { get; set; }
    }
}
