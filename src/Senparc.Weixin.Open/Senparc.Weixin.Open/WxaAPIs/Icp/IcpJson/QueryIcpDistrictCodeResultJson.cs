/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：QueryIcpDistrictCodeResultJson.cs
    文件功能描述：获取区域信息 接口返回消息
    
    
    创建标识：Senparc - 20230905

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs.Icp.IcpJson
{
    /// <summary>
    /// 获取区域信息
    /// </summary>
    public class QueryIcpDistrictCodeResultJson : WxJsonResult
    {
        public List<ProvinceModel> items { get; set; }
    }

    public class ProvinceModel : DistrictCodeItem
    {
        public List<CityModel> sub_list { get; set; }
    }

    public class CityModel :DistrictCodeItem
    {
        public List<DistrictCodeItem> sub_list { get; set; }
    }

    /// <summary>
    /// 区域信息
    /// </summary>
    public class DistrictCodeItem
    {
        //区域类型: 1 -- 省份，2 -- 市，3 -- 区
        public int type { get; set; }

        /// <summary>
        /// 区域代码
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string name { get; set; }
    }
}
