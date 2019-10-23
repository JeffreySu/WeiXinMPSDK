using Senparc.Weixin.Entities;
using System.Collections.Generic;
namespace Senparc.Weixin.MP.AdvancedAPIs.Wxa.MerchantJson
{
    /// <summary>
    /// 地图中的门店信息
    /// </summary>
    public class PoiInMap
    {
        /// <summary>
        /// 品牌名
        /// </summary>
        public string branch_name { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double latitude { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 从腾讯地图换取的位置点id， 即创建门店接口中的map_poi_id参数
        /// </summary>
        public string sosomap_poi_uid { get; set; }

        public int data_supply { get; set; }
        /// <summary>
        /// 图片列表
        /// </summary>
        public IEnumerable<string> pic_urls { get; set; }
        /// <summary>
        /// 卡券列表
        /// </summary>
        public IEnumerable<string> card_id_list { get; set; }
    }


    public class MapPoiData
    {
        public IEnumerable<PoiInMap> item { get; set; }
    }


    public class SearchMapPoiJson : WxJsonResult
    {
        public MapPoiData data { get; set; }
    }
}
