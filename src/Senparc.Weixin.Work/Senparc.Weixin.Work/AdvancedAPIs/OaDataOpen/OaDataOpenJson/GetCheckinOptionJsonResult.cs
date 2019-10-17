#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetCheckinOptionJsonResult.cs
    文件功能描述：企业微信 获取打卡规则 接口返回结果
    
    
    创建标识：Senparc - 20171222
 
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen.OaDataOpenJson
{
    /// <summary>
    /// 企业微信 获取打卡规则 接口返回结果
    /// </summary>
    public class GetCheckinOptionJsonResult : WorkJsonResult
    {
        public Info[] info { get; set; }
    }

    public class Info
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string userid { get; set; }
        public Group group { get; set; }
    }

    public class Group
    {
        /// <summary>
        /// 打卡规则类型。1：固定时间上下班；2：按班次上下班；3：自由上下班 。
        /// </summary>
        public int grouptype { get; set; }
        /// <summary>
        /// 打卡规则id
        /// </summary>
        public int groupid { get; set; }
        /// <summary>
        /// 打卡时间
        /// </summary>
        public Checkindate[] checkindate { get; set; }
        /// <summary>
        /// 特殊日期
        /// </summary>
        public Spe_Workdays[] spe_workdays { get; set; }
        public Spe_Offdays[] spe_offdays { get; set; }
        /// <summary>
        /// 是否同步法定节假日
        /// </summary>
        public bool sync_holidays { get; set; }
        /// <summary>
        /// 打卡规则名称
        /// </summary>
        public string groupname { get; set; }
        /// <summary>
        /// 是否打卡必须拍照
        /// </summary>
        public bool need_photo { get; set; }
        /// <summary>
        /// WiFi打卡地点信息
        /// </summary>
        public Wifimac_Infos[] wifimac_infos { get; set; }
        /// <summary>
        /// 是否备注时允许上传本地图片
        /// </summary>
        public bool note_can_use_local_pic { get; set; }
        /// <summary>
        /// 是否非工作日允许打卡
        /// </summary>
        public bool allow_checkin_offworkday { get; set; }
        /// <summary>
        /// 是否允许异常打卡时提交申请
        /// </summary>
        public bool allow_apply_offworkday { get; set; }
        /// <summary>
        /// 位置打卡地点信息
        /// </summary>
        public Loc_Infos[] loc_infos { get; set; }
    }

    public class Checkindate
    {
        /// <summary>
        /// 工作日。若为固定时间上下班或按班次上下班，则1到7分别表示星期一到星期日；若为按班次上下班，则表示拉取班次的日期。
        /// </summary>
        public int[] workdays { get; set; }
        public Checkintime[] checkintime { get; set; }
        /// <summary>
        /// 弹性时间（毫秒）
        /// </summary>
        public int flex_time { get; set; }
        /// <summary>
        /// 下班不需要打卡
        /// </summary>
        public bool noneed_offwork { get; set; }
        /// <summary>
        /// 打卡时间限制（毫秒）
        /// </summary>
        public int limit_aheadtime { get; set; }
    }

    public class Checkintime
    {
        /// <summary>
        /// 上班时间，表示为距离当天0点的秒数。
        /// </summary>
        public int work_sec { get; set; }
        /// <summary>
        /// 下班时间，表示为距离当天0点的秒数。
        /// </summary>
        public int off_work_sec { get; set; }
        /// <summary>
        /// 上班提醒时间，表示为距离当天0点的秒数。
        /// </summary>
        public int remind_work_sec { get; set; }
        /// <summary>
        /// 下班提醒时间，表示为距离当天0点的秒数。
        /// </summary>
        public int remind_off_work_sec { get; set; }
    }

    public class Spe_Workdays
    {
        /// <summary>
        /// 特殊日期具体时间
        /// </summary>
        public int timestamp { get; set; }
        /// <summary>
        /// 特殊日期备注
        /// </summary>
        public string notes { get; set; }
        public Checkintime[] checkintime { get; set; }
    }

    public class Spe_Offdays
    {
        public int timestamp { get; set; }
        public string notes { get; set; }
        public Checkintime[] checkintime { get; set; }
    }

    public class Wifimac_Infos
    {
        /// <summary>
        /// WiFi打卡地点名称
        /// </summary>
        public string wifiname { get; set; }
        /// <summary>
        /// WiFi打卡地点MAC地址/bssid
        /// </summary>
        public string wifimac { get; set; }
    }

    public class Loc_Infos
    {
        /// <summary>
        /// 位置打卡地点经度
        /// </summary>
        public int lat { get; set; }
        /// <summary>
        /// 位置打卡地点纬度
        /// </summary>
        public int lng { get; set; }
        /// <summary>
        /// 位置打卡地点名称
        /// </summary>
        public string loc_title { get; set; }
        /// <summary>
        /// 位置打卡地点详情
        /// </summary>
        public string loc_detail { get; set; }
        /// <summary>
        /// 允许打卡范围（米）
        /// </summary>
        public int distance { get; set; }
    }
}
