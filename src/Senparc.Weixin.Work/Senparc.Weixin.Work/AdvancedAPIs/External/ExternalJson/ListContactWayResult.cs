/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：ListContactWayResult.cs
    文件功能描述：获取企业已配置的「联系我」列表
    
    
    创建标识：Senparc - 20220918
    
----------------------------------------------------------------*/


using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 获取企业已配置的「联系我」方式 返回数据
    /// </summary>
    public class ListContactWayResult : WorkJsonResult
    {
        public Contact_Way[] contact_way { get; set; }
        /// <summary>
        /// 分页参数，用于查询下一个分页的数据，为空时表示没有更多的分页
        /// </summary>
        public string next_cursor { get; set; }

        public class Contact_Way
        {
            /// <summary>
            /// 联系方式的配置id
            /// </summary>
            public string config_id { get; set; }
        }
    }

}
