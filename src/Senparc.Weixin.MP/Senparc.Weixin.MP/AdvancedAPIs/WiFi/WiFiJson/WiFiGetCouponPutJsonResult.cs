/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WiFiGetCouponPutJsonResult.cs
    文件功能描述：查询门店卡券投放信息的返回结果
    
    创建标识：Senparc - 20160520
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.WiFi
{
    /// <summary>
    /// 查询门店卡券投放信息的返回结果
    /// </summary>
    public class WiFiGetCouponPutJsonResult : WxJsonResult 
    {
        public GetCouponPut_Data data { get; set; }

       
    }
    public class GetCouponPut_Data
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public string shop_id { get; set; }
        /// <summary>
        /// 卡券投放状态（0表示生效中，1表示未生效，2表示已过期）
        /// </summary>
        public int card_status { get; set; }
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 卡券描述
        /// </summary>
        public string card_describe { get; set; }
        /// <summary>
        /// 卡券投放开始时间（单位是秒）
        /// </summary>
        public string start_date { get; set; }
        /// <summary>
        /// 卡券投放结束时间（单位是秒）
        /// </summary>
        public string end_date { get; set; }
    }
}
