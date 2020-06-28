using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.Wxa.MerchantJson
{
    /// <summary>
    /// 在腾讯地图中创建门店
    /// </summary>
    public class CreateMapPoiData
    {
        /// <summary>
        /// 名字，必填
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 经度，必填
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 纬度，必填
        /// </summary>
        public string latitude { get; set; }

        /// <summary>
        /// 省份，必填
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 城市，必填
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 区，必填
        /// </summary>
        public string district { get; set; }

        /// <summary>
        /// 详细地址，必填
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 类目，比如美食:中餐厅，必填
        /// </summary>
        public string category { get; set; }

        /// <summary>
        /// 电话，可多个，使用英文分号间隔 010-6666666-111;   010-6666666; 010- 6666666-222，必填
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// 门店图片url，必填
        /// </summary>
        public string photo { get; set; }

        /// <summary>
        /// 营业执照url，必填
        /// </summary>
        public string license { get; set; }

        /// <summary>
        /// 介绍,必填
        /// </summary>
        public string introduct { get; set; }

        /// <summary>
        /// 腾讯地图拉取省市区信息接口返回的id，必填
        /// </summary>
        public string districtid { get; set; }

        /// <summary>
        /// 如果是迁移门店， 必须填 poi_id字段,选填
        /// </summary>
        //public string poi_id { get; set; }
    }

    /// <summary>
    /// 在腾讯地图中创建门店返回结果
    /// </summary>
    public class CreateMapPoiJsonResult : WxJsonResult
    {
        /// <summary>
        /// 指出错误原因
        /// </summary>
        public string error { get; set; }

        public CreateMapPoiJsonResult_Data data { get; set; }
    }

    public class CreateMapPoiJsonResult_Data
    {
        /// <summary>
        /// 审核单id
        /// </summary>
        public long base_id { get; set; }

        public long rich_id { get; set; }
    }
}
