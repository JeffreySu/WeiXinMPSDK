/************************************************************************ 
 * 项目名称 :  Senparc.Weixin.MP.AdvancedAPIs.WiFi.WiFiJson   
 * 项目描述 :      
 * 类 名 称 :  WifiOutletResultJson 
 * 版 本 号 :  v1.0.0.0  
 * 说    明 :      
 * 作    者 :  Victor_Lo 
 * 创建时间 :  2015/7/24 9:37:44 
 * 更新时间 :  2015/7/24 9:37:44 
************************************************************************ 
 * Copyright @ Vapps 2015. All rights reserved. 
************************************************************************/


using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs.WiFi.WiFiJson
{
    public class GetWifiOutletResult : WxJsonResult
    {
        public WifiOutletResult_Data data{get;set;}
    }

    public class WifiOutletResult_Data{
        public WifiOutletResult_Data()
        {
            this.records = new List<OutletWifi>();
        }

        /// <summary>
        /// 总数
        /// </summary>
        public int totalcount { get; set; }

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
        public List<OutletWifi> records { get; set; }
    }

    public class OutletWifi
    {

        public string shop_id { get; set; }

        public string shop_name { get; set; }

        public string ssid { get; set; }

        public int protocol_type { get; set; }
    }
}
