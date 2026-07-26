#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc
    
    文件名：GetCheckinOptionJsonResult.cs
    文件功能描述：企业微信 获取打卡规则 接口返回结果
    
    
    创建标识：Senparc - 20171222

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信打卡规则配置字段
 
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

        /// <summary>
        /// 打卡人员范围
        /// </summary>
        public CheckinRange range { get; set; }

        /// <summary>
        /// 规则创建时间（Unix 时间戳）
        /// </summary>
        public long create_time { get; set; }

        /// <summary>
        /// 无需打卡人员列表
        /// </summary>
        public string[] white_users { get; set; }

        /// <summary>
        /// 打卡方式
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 汇报对象信息
        /// </summary>
        public CheckinReporterInfo reporterinfo { get; set; }

        /// <summary>
        /// 旧版加班配置
        /// </summary>
        public CheckinOvertimeInfo ot_info { get; set; }

        /// <summary>
        /// 新版加班配置
        /// </summary>
        public CheckinOvertimeInfoV2 ot_info_v2 { get; set; }

        /// <summary>
        /// 每月允许补卡次数
        /// </summary>
        public int allow_apply_bk_cnt { get; set; }

        /// <summary>
        /// 允许补卡的天数限制
        /// </summary>
        public int allow_apply_bk_day_limit { get; set; }

        /// <summary>
        /// 是否允许次月补卡
        /// </summary>
        public bool buka_limit_next_month { get; set; }

        /// <summary>
        /// 是否允许范围外打卡
        /// </summary>
        public bool option_out_range { get; set; }

        /// <summary>
        /// 规则创建者 UserId
        /// </summary>
        public string create_userid { get; set; }

        /// <summary>
        /// 是否启用人脸识别
        /// </summary>
        public bool use_face_detect { get; set; }

        /// <summary>
        /// 是否启用活体检测
        /// </summary>
        public bool open_face_live_detect { get; set; }

        /// <summary>
        /// 规则更新者 UserId
        /// </summary>
        public string update_userid { get; set; }

        /// <summary>
        /// 排班列表
        /// </summary>
        public CheckinRuleSchedule[] schedulelist { get; set; }

        /// <summary>
        /// 下班间隔时间
        /// </summary>
        public int offwork_interval_time { get; set; }

        /// <summary>
        /// 补卡异常类型位掩码
        /// </summary>
        public ulong buka_restriction { get; set; }

        /// <summary>
        /// 自由上下班跨天时间
        /// </summary>
        public int span_day_time { get; set; }

        /// <summary>
        /// 标准工作时长（秒）
        /// </summary>
        public int standard_work_duration { get; set; }

        /// <summary>
        /// 是否开启审批打卡
        /// </summary>
        public bool open_sp_checkin { get; set; }

        /// <summary>
        /// 打卡交替方式
        /// </summary>
        public int checkin_method_type { get; set; }

        /// <summary>
        /// 是否同步外出打卡
        /// </summary>
        public bool sync_out_checkin { get; set; }

        /// <summary>
        /// 补卡提醒配置
        /// </summary>
        public CheckinCorrectionReminder buka_remind { get; set; }
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

        /// <summary>
        /// 是否允许弹性打卡
        /// </summary>
        public bool allow_flex { get; set; }

        /// <summary>
        /// 上班弹性时间
        /// </summary>
        public int flex_on_duty_time { get; set; }

        /// <summary>
        /// 下班弹性时间
        /// </summary>
        public int flex_off_duty_time { get; set; }

        /// <summary>
        /// 允许最早到达时长
        /// </summary>
        public int max_allow_arrive_early { get; set; }

        /// <summary>
        /// 允许最晚到达时长
        /// </summary>
        public int max_allow_arrive_late { get; set; }

        /// <summary>
        /// 迟到规则
        /// </summary>
        public CheckinLateRule late_rule { get; set; }

        /// <summary>
        /// 大小周配置
        /// </summary>
        public CheckinBiweekly biweekly { get; set; }
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

        /// <summary>
        /// 时段 ID
        /// </summary>
        public int time_id { get; set; }

        /// <summary>
        /// 是否允许休息
        /// </summary>
        public bool allow_rest { get; set; }

        /// <summary>
        /// 单个休息开始时间
        /// </summary>
        public int rest_begin_time { get; set; }

        /// <summary>
        /// 单个休息结束时间
        /// </summary>
        public int rest_end_time { get; set; }

        /// <summary>
        /// 多个休息时段
        /// </summary>
        public CheckinRestTime[] rest_times { get; set; }

        /// <summary>
        /// 上班最早可打卡时间
        /// </summary>
        public int earliest_work_sec { get; set; }

        /// <summary>
        /// 上班最晚可打卡时间
        /// </summary>
        public int latest_work_sec { get; set; }

        /// <summary>
        /// 下班最早可打卡时间
        /// </summary>
        public int earliest_off_work_sec { get; set; }

        /// <summary>
        /// 下班最晚可打卡时间
        /// </summary>
        public int latest_off_work_sec { get; set; }

        /// <summary>
        /// 是否无需上班打卡
        /// </summary>
        public bool no_need_checkon { get; set; }

        /// <summary>
        /// 是否无需下班打卡
        /// </summary>
        public bool no_need_checkoff { get; set; }
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
        /// <summary>
        /// 特殊日期类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 特殊日期开始时间
        /// </summary>
        public int begtime { get; set; }
        /// <summary>
        /// 特殊日期结束时间
        /// </summary>
        public int endtime { get; set; }
    }

    public class Spe_Offdays
    {
        public int timestamp { get; set; }
        public string notes { get; set; }
        public Checkintime[] checkintime { get; set; }
        /// <summary>
        /// 特殊日期类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 特殊日期开始时间
        /// </summary>
        public int begtime { get; set; }
        /// <summary>
        /// 特殊日期结束时间
        /// </summary>
        public int endtime { get; set; }
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
