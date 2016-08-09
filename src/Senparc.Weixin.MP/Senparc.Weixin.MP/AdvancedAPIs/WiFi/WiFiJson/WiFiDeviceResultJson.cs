/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WiFiDeviceResultJson.cs
    文件功能描述：WiFi设备接口返回结果
    
    
    创建标识：Senparc - 20150709
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.WiFi
{
    /// <summary>
    /// 查询设备返回结果
    /// </summary>
    public class GetDeviceListResult : WxJsonResult
    {
        public DeviceList_Data data { get; set; }
    }

    public class DeviceList_Data
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int recordcount { get; set; }
        /// <summary>
        /// 分页下标
        /// </summary>
        public int pageindex { get; set; }
        /// <summary>
        /// 分页页数
        /// </summary>
        public int pagecount { get; set; }
        /// <summary>
        /// 当前页列表数组
        /// </summary>
        public List<DeviceList_Data_Record> records { get; set; }
    }

    public class DeviceList_Data_Record
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long shop_id { get; set; }
        /// <summary>
        /// 连网设备ssid
        /// </summary>
        public string ssid { get; set; }
        /// <summary>
        /// 无线MAC地址
        /// </summary>
        public string bssid { get; set; }
    }
}
