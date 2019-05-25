/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：StoreLocationData.cs
    文件功能描述：门店接口POST数据
    
    
    创建标识：Senparc - 20181008


----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 创建门店小程序数据
    /// </summary>
    public class ApplyMerchantData
    {
        public int first_catid { get; set; }
        public int second_catid { get; set; }
        public string qualification_list { get; set; }
        public string headimg_mediaid { get; set; }
        public string nickname { get; set; }
        public string intro { get; set; }
        public string org_code { get; set; }
        public string other_files { get; set; }
    }

    /// <summary>
    /// 创建门店数据
    /// </summary>
    public class CreateMapPoiData
    {
        public string name { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string address { get; set; }
        public string category { get; set; }
        public string telephone { get; set; }
        public string photo { get; set; }
        public string license { get; set; }
        public string introduct { get; set; }
        public string districtid { get; set; }
    }

    /// <summary>
    /// 添加门店数据
    /// </summary>
    public class AddStoreData
    {
        public string poi_id { get; set; }
        public string map_poi_id { get; set; }
        public string pic_list { get; set; }
        public string contract_phone { get; set; }
        public string credential { get; set; }
        public string qualification_list { get; set; }
    }

    /// <summary>
    /// 更新门店信息
    /// </summary>
    public class UpdateStoreData
    {
        public string map_poi_id { get; set; }
        public string poi_id { get; set; }
        public string hour { get; set; }
        public string contract_phone { get; set; }
        public string pic_list { get; set; }
    }

}
