﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.ShakeAround
{
   public  class DeviceListResultJson : WxJsonResult
    {
       /// <summary>
       /// 
       /// </summary>
       public List<DevicesList_DevicesItem> devices { get; set; }
       /// <summary>
       /// 所查询的日期时间戳
       /// </summary>
       public long  date { get; set; }
       /// <summary>
       /// 设备总数
       /// </summary>
       public string total_count { get; set; }
       /// <summary>
       /// 所查询的结果页序号；返回结果按摇周边人数降序排序，每50条记录为一页   
       /// </summary>
       public string page_index { get; set; }
     }

    public class DevicesList_DevicesItem
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string device_id { get; set; }
        /// <summary>
        /// major
        /// </summary>
        public string major { get; set; }
        /// <summary>
        /// minor
        /// </summary>
        public string minor { get; set; }
        /// <summary>
        /// uuid
        /// </summary>
        public string uuid { get; set; }
        /// <summary>
        /// 摇周边的次数
        /// </summary>
        public int shake_pv { get; set; }
        /// <summary>
        /// 摇周边的人数
        /// </summary>
        public int shake_uv { get; set; }
        /// <summary>
        /// 点击摇周边消息的次数
        /// </summary>
        public int click_pv { get; set; }
        /// <summary>
        /// 点击摇周边消息的人数
        /// </summary>
        public int click_uv { get; set; }

    }
}
