/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：StoreLocationData.cs
    文件功能描述：卡券 门店地址信息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    public class StoreLocationData
    {
        /// <summary>
        /// 门店地址信息
        /// </summary>
        public List<Store_Location> location_list { get; set; }
    }

    /// <summary>
    /// 单条门店地址信息
    /// </summary>
    public class Store_Location
    {
        /// <summary>
        /// 门店名称
        /// 必填
        /// </summary>
        public string business_name { get; set; }
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
        /// 必填
        /// </summary>
        public string district { get; set; }
        /// <summary>
        /// 门店所在的详细街道地址
        /// 必填
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 门店的电话
        /// 必填
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 门店的类型（酒店、餐饮、购物...）
        /// 必填
        /// </summary>
        public string category { get; set; }
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
}
