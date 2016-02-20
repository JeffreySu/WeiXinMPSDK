/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：StoreResultJson.cs
    文件功能描述：门店相关接口返回结果
    
    
    创建标识：Senparc - 20150513
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Poi
{
    /// <summary>
    /// 查询门店信息返回结果
    /// </summary>
    public class GetStoreResultJson : WxJsonResult
    {
        /// <summary>
        /// 门店信息
        /// </summary>
        public GetStore_Business business { get; set; }
    }

    public class GetStore_Business
    {
        /// <summary>
        /// 查询门店基础信息
        /// </summary>
        public GetStoreBaseInfo base_info { get; set; }
    }

    /// <summary>
    /// 查询门店基础信息
    /// </summary>
    public class GetStoreBaseInfo : StoreBaseInfo
    {
        /// <summary>
        /// 门店是否可用状态。1 表示系统错误、2 表示审核中、3 审核通过、4 审核驳回。当该字段为1、2、4 状态时，poi_id 为空
        /// </summary>
        public int available_state { get; set; }
        /// <summary>
        /// 扩展字段是否正在更新中。1 表示扩展字段正在更新中，尚未生效，不允许再次更新； 0 表示扩展字段没有在更新中或更新已生效，可以再次更新
        /// </summary>
        public int update_status { get; set; }
    }

    /// <summary>
    /// 查询门店列表返回结果
    /// </summary>
    public class GetStoreListResultJson : WxJsonResult
    {
        public List<GetStoreList_Business> business_list { get; set; }
        /// <summary>
        /// 门店总数量
        /// </summary>
        public string total_count { get; set; }
    }

    public class GetStoreList_Business
    {
        public GetStoreList_BaseInfo base_info { get; set; }
    }

    public class GetStoreList_BaseInfo
    {
        public string sid { get; set; }
        /// <summary>
        /// 审核通过才会返回此字段
        /// </summary>
        public string poi_id { get; set; }
        /// <summary>
        /// 门店名称（仅为商户名，如：国美、麦当劳，不应包含地区、店号等信息，错误示例：北京国美）
        /// </summary>
        public string business_name { get; set; }
        /// <summary>
        /// 分店名称（不应包含地区信息、不应与门店名重复，错误示例：北京王府井店）
        /// </summary>
        public string branch_name { get; set; }
        /// <summary>
        /// 门店所在的详细街道地址（不要填写省市信息）
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 门店是否可用状态。1 表示系统错误、2 表示审核中、3 审核通过、4 审核驳回。当该字段为1、2、4 状态时，poi_id 为空
        /// </summary>
        public int available_state { get; set; }
    }

    /// <summary>
    /// 获取门店类目表返回结果
    /// </summary>
    public class GetCategoryResult : WxJsonResult
    {
        public List<string> category_list { get; set; } 
    }
}
