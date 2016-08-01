/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：StoreBaseInfo.cs
    文件功能描述：门店基础信息
    
    
    创建标识：Senparc - 20150513
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Poi
{
    /// <summary>
    /// 可以被修改的门店基础信息
    /// </summary>
    public class StoreBaseInfoCanBeUpdate
    {
        /// <summary>
        /// 门店的电话
        /// 必填
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 图片列表，url 形式，可以有多张图片，尺寸为640*340px。必须为上一接口生成的url
        /// 必填
        /// </summary>
        public List<Store_Photo> photo_list { get; set; }
        /// <summary>
        /// 推荐品，餐厅可为推荐菜；酒店为推荐套房；景点为推荐游玩景点等，针对自己行业的推荐内容
        /// 非必填
        /// </summary>
        public string recommend { get; set; }
        /// <summary>
        /// 特色服务，如免费wifi，免费停车，送货上门等商户能提供的特色功能或服务
        /// 必填
        /// </summary>
        public string special { get; set; }
        /// <summary>
        /// 商户简介，主要介绍商户信息等
        /// 非必填
        /// </summary>
        public string introduction { get; set; }
        /// <summary>
        /// 营业时间，24 小时制表示，用“-”连接，如8:00-20:00
        /// 必填
        /// </summary>
        public string open_time { get; set; }
        /// <summary>
        /// 人均价格，大于0 的整
        /// 非必填
        /// </summary>
        public int avg_price { get; set; }
    }

    /// <summary>
    /// 门店基础信息
    /// </summary>
    public class StoreBaseInfo : StoreBaseInfoCanBeUpdate
    {
        /// <summary>
        /// 商户自己的id，用于后续审核通过收到poi_id 的通知时，做对应关系。请商户自己保证唯一识别性
        /// 非必填
        /// </summary>
        public string sid { get; set; }
        /// <summary>
        /// 门店名称
        /// 必填
        /// </summary>
        public string business_name { get; set; }
        /// <summary>
        /// 分店名称
        /// 非必填
        /// </summary>
        public string branch_name { get; set; }
        /// <summary>
        /// 门店所在的省
        /// 必填
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 门店所在的市
        /// 必填
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 门店所在的区
        /// 非必填
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// 门店所在的详细街道地址
        /// 必填
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 门店的类型（酒店、餐饮、购物...）
        /// 必填
        /// </summary>
        public string[] categories { get; set; }
        /// <summary>
        /// 坐标类型，1 为火星坐标（目前只能选1）
        /// </summary>
        public int offset_type { get; set; }
        /// <summary>
        /// 门店所在地理位置的经度（建议使用腾讯地图定位经纬度）
        /// 必填
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// 门店所在地理位置的纬度
        /// 必填
        /// </summary>
        public string latitude { get; set; }
    }

    public class Store_Photo
    {
        /// <summary>
        /// 图片Url
        /// </summary>
        public string photo_url { get; set; }
    }
}
