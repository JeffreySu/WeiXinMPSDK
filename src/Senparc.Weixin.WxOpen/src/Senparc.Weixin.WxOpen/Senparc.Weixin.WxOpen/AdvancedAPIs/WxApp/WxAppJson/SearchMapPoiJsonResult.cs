using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    /// 在腾讯地图中搜索门店结果
    /// </summary>
    public class SearchMapPoiJsonResult : WxJsonResult
    {
        public Items data { get; set; }
    }
    [Serializable]
    public class Items
    {
        public List<MapPoiItem> item { get; set; } = new List<MapPoiItem>();
    }
    [Serializable]
    public class MapPoiItem
    {
        /// <summary>
        /// 门店名称
        /// </summary>
        public string branch_name { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public float longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public float latitude { get; set; }

        /// <summary>
        /// 客服电话
        /// </summary>
        public string telephone { get; set; }

        public string category { get; set; }

        /// <summary>
        /// 从腾讯地图换取的位置点id， 即后面创建门店接口中的map_poi_id参数
        /// </summary>
        public string sosomap_poi_uid { get; set; }

        public int data_supply { get; set; }

        public List<string> pic_urls { get; set; } = new List<string>();

        public List<string> card_id_list { get; set; } = new List<string>();
    }
}
