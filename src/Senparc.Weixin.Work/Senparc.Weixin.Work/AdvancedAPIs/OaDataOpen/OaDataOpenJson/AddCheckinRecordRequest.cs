using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen.OaDataOpenJson
{
    public class AddCheckinRecordRequest : WorkJsonResult
    {
        /// <summary>
        /// 打卡记录，一批最多200个
        /// </summary>
        public List<AddCheckinRecord_Info> records { get;set; }
    }

    public class AddCheckinRecord_Info
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 打卡时间。Unix时间戳
        /// </summary>
        public long checkin_time { get; set; }
        /// <summary>
        /// 打卡地点title，限制1024字符
        /// </summary>
        public string location_title { get; set; }
        /// <summary>
        /// 打卡地点详情限制1024字符
        /// </summary>
        public string location_detail {  get; set; }
        /// <summary>
        /// 打卡的附件media_id，可使用media/upload上传附件。当前最多只允许传1个
        /// </summary>
        public List<string> mediaids { get; set; }
        /// <summary>
        /// 打卡备注限制1024字符
        /// </summary>
        public string notes { get; set; }
        /// <summary>
        /// 打卡设备类型：1、门禁 2、考勤机（人脸识别、指纹识别） 3、其他；
        /// </summary>
        public int device_type { get; set; }
        /// <summary>
        /// 位置打卡地点纬度，是实际纬度的1000000倍，与腾讯地图一致采用GCJ-02坐标系统标准 范围 -90000000,90000000
        /// </summary>
        public long lat { get; set; }
        /// <summary>
        /// 位置打卡地点经度，是实际经度的1000000倍，与腾讯地图一致采用GCJ-02坐标系统标准 范围-180000000,180000000
        /// </summary>
        public long lng { get; set; }
        /// <summary>
        /// 打卡设备品牌：字符串写入（限制40个字符内）
        /// </summary>
        public string device_detail {  get; set; }
        /// <summary>
        /// 打卡wifi名称限制1024字符
        /// </summary>
        public string wifiname {  get; set; }
        /// <summary>
        /// 打卡的MAC地址/bssid 满足正则表达式^[A-Fa-f0-9]{2}:[A-Fa-f0-9]{2}:[A-Fa-f0-9]{2}:[A-Fa-f0-9]{2}:[A-Fa-f0-9]{2}:[A-Fa-f0-9]{2}$。传入wifiname时必填
        /// </summary>
        public string wifimac { get; set; }
    }
}
