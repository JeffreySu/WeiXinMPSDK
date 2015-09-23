/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：StoreResultJson.cs
    文件功能描述：门店相关接口返回结果
    
    
    创建标识：Senparc - 20150513
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public int total_count { get; set; }
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
		
		/// <summary>
        /// 门店所在的省
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 门店所在的市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 门店所在的区
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// 门店的类型（酒店、餐饮、购物...）
        /// </summary>
        public string[] categories { get; set; }
        /// <summary>
        /// 坐标类型，1 为火星坐标（目前只能选1）
        /// </summary>
        public int offset_type { get; set; }
        /// <summary>
        /// 门店所在地理位置的经度（建议使用腾讯地图定位经纬度）
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// 门店所在地理位置的纬度
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// 门店的电话
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 图片列表，url 形式，可以有多张图片，尺寸为640*340px。必须为上一接口生成的url
        /// </summary>
        public List<Store_Photo> photo_list { get; set; }
        /// <summary>
        /// 推荐品，餐厅可为推荐菜；酒店为推荐套房；景点为推荐游玩景点等，针对自己行业的推荐内容
        /// </summary>
        public string recommend { get; set; }
        /// <summary>
        /// 特色服务，如免费wifi，免费停车，送货上门等商户能提供的特色功能或服务
        /// </summary>
        public string special { get; set; }
        /// <summary>
        /// 商户简介，主要介绍商户信息等
        /// </summary>
        public string introduction { get; set; }
        /// <summary>
        /// 营业时间，24 小时制表示，用“-”连接，如8:00-20:00
        /// </summary>
        public string open_time { get; set; }
        /// <summary>
        /// 人均价格，大于0 的整
        /// </summary>
        public int avg_price { get; set; }
		
    }

    /// <summary>
    /// 获取门店类目表返回结果
    /// </summary>
    public class GetCategoryResult : WxJsonResult
    {
        public List<string> category_list { get; set; } 
    }
}
