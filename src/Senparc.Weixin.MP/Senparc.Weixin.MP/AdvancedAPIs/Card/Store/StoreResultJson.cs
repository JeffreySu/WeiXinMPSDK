/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：StoreResultJson.cs
    文件功能描述：门店相关接口返回结果
    
    
    创建标识：Senparc - 20181008
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 拉取门店小程序类目返回结果
    /// </summary>
    public class GetMerchantCategoryResultJson : WxJsonResult
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public All_Category_Info all_category_info { get; set; }
    }

    public class All_Category_Info
    {
        public Category[] categories { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int?[] children { get; set; }
        public int father { get; set; }
        public Qualify qualify { get; set; }
        public int scene { get; set; }
        public int sensitive_type { get; set; }
    }

    public class Qualify
    {
        public Exter_List[] exter_list { get; set; }
    }

    public class Exter_List
    {
        public Inner_List[] inner_list { get; set; }
    }

    public class Inner_List
    {
        public string name { get; set; }
    }

    /// <summary>
    /// 查询门店小程序审核结果
    /// </summary>
    public class GetMerchantAuditInfoResultJson : WxJsonResult
    {
        public GetMerchantAuditInfo data { get; set; }
    }

    public class GetMerchantAuditInfo
    {
        public int audit_id { get; set; }
        public int status { get; set; }
        public string reason { get; set; }
    }

    /// <summary>
    /// 从腾讯地图拉取省市区信息
    /// </summary>
    public class GetDistrictResultJson : WxJsonResult
    {
        public int status { get; set; }
        public string message { get; set; }
        public string data_version { get; set; }
        public Result[][] result { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public string[] pinyin { get; set; }
        public Location location { get; set; }
        public int[] cidx { get; set; }
    }

    public class Location
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    /// <summary>
    /// 在腾讯地图中搜索门店返回结果
    /// </summary>
    public class SearchMapPoiResultJson : WxJsonResult
    {
        public SearchMapPoidata data { get; set; }
    }

    public class SearchMapPoidata
    {
        public SearchMapPoiItem[] item { get; set; }
    }

    /// <summary>
    /// 门店信息
    /// </summary>
    public class SearchMapPoiItem
    {
        public string branch_name { get; set; }
        public string address { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public string telephone { get; set; }
        public string category { get; set; }
        public string sosomap_poi_uid { get; set; }
        public int data_supply { get; set; }
        public object[] pic_urls { get; set; }
        public object[] card_id_list { get; set; }
    }

    /// <summary>
    /// 在腾讯地图中创建门店返回结果
    /// </summary>
    public class CreateMapPoiResultJson : WxJsonResult
    {
        public string error { get; set; }
        public CreateMapPoiResultJsonData data { get; set; }
    }

    public class CreateMapPoiResultJsonData
    {
        public int base_id { get; set; }
        public int rich_id { get; set; }
    }

    /// <summary>
    /// 添加门店返回结果
    /// </summary>
    public class AddStoreResultJson : WxJsonResult
    {
        public AddStoreResultData data { get; set; }
    }

    public class AddStoreResultData
    {
        public int audit_id { get; set; }
    }

    /// <summary>
    /// 更新门店信息返回结果
    /// </summary>
    public class UpdateStoreResultJson : WxJsonResult
    {
        public UpdateStoreResultData data { get; set; }
    }

    public class UpdateStoreResultData
    {
        /// <summary>
        /// 表示是否需要审核(1表示需要，0表示不需要)
        /// </summary>
        public int has_audit_id { get; set; }
        /// <summary>
        /// 表示具体的审核单id
        /// </summary>
        public string audit_id { get; set; }
    }

    /// <summary>
    /// 获取单个门店信息返回结果
    /// </summary>
    public class GetStoreInfoResultJson : WxJsonResult
    {
        public Business business { get; set; }
    }

    /// <summary>
    /// 获取门店信息列表返回结果
    /// </summary>
    public class GetStoreListResultJson : WxJsonResult
    {
        public List<Business> business_list { get; set; }
    }

    public class Business
    {
        public Base_Info base_info { get; set; }
    }

    public class Base_Info
    {
        public string business_name { get; set; }
        public string address { get; set; }
        public string telephone { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
        public Photo_List[] photo_list { get; set; }
        public string open_time { get; set; }
        public string poi_id { get; set; }
        public int status { get; set; }
        public string district { get; set; }
        public string qualification_num { get; set; }
        public string qualification_name { get; set; }
    }

    public class Photo_List
    {
        public string photo_url { get; set; }
    }

    /// <summary>
    /// 获取门店小程序配置的卡券返回结果
    /// </summary>
    public class GetStoreCardResultJson : WxJsonResult
    {
        public string card_id { get; set; }
    }

}
