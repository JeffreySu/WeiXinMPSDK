/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetCheckinDataJsonResult.cs
    文件功能描述：获取打卡数据返回结果
    
    
    创建标识：Senparc - 20170617


----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen
{
    public class GetCheckinDataJsonResult : WorkJsonResult
    {
        /// <summary>
        /// 打卡数据
        /// </summary>
        public GetCheckinDataJsonResult_Result[] checkindata { get; set; }
    }

    public class GetCheckinDataJsonResult_Result
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 打卡规则名称
        /// </summary>
        public string groupname { get; set; }
        /// <summary>
        /// 打卡类型
        /// </summary>
        public string checkin_type { get; set; }
        /// <summary>
        /// 异常类型，如果有多个异常，以分号间隔
        /// </summary>
        public string exception_type { get; set; }
        /// <summary>
        /// 打卡时间。UTC时间戳
        /// </summary>
        public long checkin_time { get; set; }
        /// <summary>
        /// 打卡地点title
        /// </summary>
        public string location_title { get; set; }
        /// <summary>
        /// 打卡地点详情
        /// </summary>
        public string location_detail { get; set; }
        /// <summary>
        /// 打卡wifi名称
        /// </summary>
        public string wifiname { get; set; }
        /// <summary>
        /// 打卡备注
        /// </summary>
        public string notes { get; set; }
        /// <summary>
        /// 打卡的MAC地址/bssid
        /// </summary>
        public string wifimac { get; set; }
        /// <summary>
        /// 打卡的附件media_id，可使用media/get获取附件
        /// </summary>
        public string[] mediaids { get; set; }
    }
}
