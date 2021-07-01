using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    /// <summary>
    /// 添加地点返回结果
    /// </summary>
    public class GetNearbyDetailPageJsonResult : WxJsonResult
    {
    }
    [Serializable]
    public class NearbyDetail
    {
        public UInt64 poi_id { get; set; }

        public string map_poi_id { get; set; }

        public int status { get; set; }

        public string refuse_reason { get; set; }

        public float longitude { get; set; }

        public float latitude { get; set; }

        public string address { get; set; }

        public string province { get; set; }

        public string city { get; set; }

        public string district { get; set; }

        public string store_name { get; set; }

        public string branch_name { get; set; }

        public string pic_list_josn { get; set; }

        public string poi_qrcode { get; set; }

        public string contract_phone { get; set; }

        public string hour { get; set; }

        public string company_name { get; set; }

        public Qualification qualification_info { get; set; }

        public string checksum { get; set; }

        public int enable_show { get; set; }

        public string service_infos_json { get; set; }

        public bool is_comm_nearby { get; set; }

        public Kf kf_info { get; set; }

        public ServiceInfo service_info_limit { get; set; }

        public int is_miniprogram { get; set; }

        public int apply_status { get; set; }

        public int has_open_kf { get; set; }

        public string appid { get; set; }

        public int upgrade_status { get; set; }

        public string headimg { get; set; }

        public string remark { get; set; }
    }
    [Serializable]
    public  class ServiceInfo
    {
        public int official_limit { get; set; }

        public int user_define_limit { get; set; }
    }
    [Serializable]
    public class Kf
    {
        public string kf_headimg { get; set; }

        public string kf_name { get; set; }

        public bool open_kf { get; set; }
    }
    [Serializable]
    public class Qualification
    {
        public int principal_type { get; set; }

        public string qualification_num { get; set; }

        public string qualification_address { get; set; }

        public string[] mediaid_list { get; set; }

        public string name { get; set; }
    }
}
