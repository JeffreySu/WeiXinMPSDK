using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    /// 从腾讯地图拉取省市区信息结果
    /// </summary>
    public class GetDistrictJsonResult : WxJsonResult
    {
        public string data_version { get; set; }

        /// <summary>
        /// 三维数组，第一维表示省的信息，第二维表示市的信息，第三维表示区的信息
        /// </summary>
        public List<DistrictItem[]> result { get; set; } = new List<DistrictItem[]>();
    }
    [Serializable]
    public class DistrictItem
    {
        /// <summary>
        /// 区域id，也叫做 districtid
        /// </summary>
        public string id { get; set; }

        public string name { get; set; }

        /// <summary>
        /// 省市区的名字
        /// </summary>
        public string fullname { get; set; }

        public List<string> pinyin { get; set; } = new List<string>();

        public Loction location { get; set; }

        /// <summary>
        /// 通过省的cidx，可以在 result[1] 中找到省下的所有市
        /// </summary>
        public List<int> cidx { get; set; } = new List<int>();
    }
    [Serializable]
    public class Loction
    {
        public float lat { get; set; }

        public float lng { get; set; }
    }
}
