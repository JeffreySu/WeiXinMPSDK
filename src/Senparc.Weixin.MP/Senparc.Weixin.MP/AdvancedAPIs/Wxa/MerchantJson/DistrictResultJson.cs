using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Wxa.MerchantJson
{
    /// <summary>
    /// 从腾讯地图拉取省市区信息返回值
    /// </summary>
    public class DistrictResultJson : WxJsonResult
    {
        /// <summary>
        /// status
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// message
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string data_version { get; set; }
        /// <summary>
        /// result[0]是省 result[1]是市 result[2]是区
        /// </summary>
        public List<District> result { get; set; }
        //public List<District>[] result { get; set; }
    }

    /// <summary>
    /// 地区信息
    /// </summary>
    public class District
    {
        /// <summary>
        /// 地区id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 简称 只有该地区名字
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 全称 省市区的名字
        /// </summary>
        public string fullname { get; set; }
        /// <summary>
        /// 拼音
        /// </summary>
        public List<string> pinyin { get; set; }
        /// <summary>
        /// 经纬度
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 下级的地区id
        /// </summary>
        public List<int> cidx { get; set; }
    }

    public class Location
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double lat { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double lng { get; set; }
    }
}
