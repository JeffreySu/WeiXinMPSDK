using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Wxa.MerchantJson
{
    public class DistrictResultJson 
    {
        public int status { get; set; }

        public string message { get; set; }

        public string data_version { get; set; }
        /// <summary>
        /// result[0]是省 result[1]是市 result[2]是区
        /// </summary>
        public IEnumerable<District>[] result { get; set; }
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
        public IEnumerable<string> pinyin { get; set; }
        /// <summary>
        /// 经纬度
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 下级的地区id
        /// </summary>
        public IEnumerable<int> cidx { get; set; }
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
