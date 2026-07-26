/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OpenHardwareAttendanceJson.cs
    文件功能描述：企业微信智慧硬件考勤门禁强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐智慧硬件考勤门禁强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OpenHardware
{
    /// <summary>上报考勤打卡记录请求。</summary>
    public class OpenHardwareReportCheckinDataRequest
    {
        /// <summary>待上报的考勤打卡记录。</summary>
        public List<OpenHardwareCheckinData> checkin_data { get; set; }
    }

    /// <summary>考勤打卡记录。</summary>
    public class OpenHardwareCheckinData
    {
        /// <summary>成员或访客 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>用户类型。</summary>
        public int user_type { get; set; }

        /// <summary>打卡时间，Unix 时间戳。</summary>
        public long timestamp { get; set; }
    }

    /// <summary>上报考勤打卡记录结果。</summary>
    public class OpenHardwareReportCheckinDataResult : WorkJsonResult
    {
        /// <summary>上报失败的考勤记录；全部成功时为空。</summary>
        public List<OpenHardwareCheckinData> fail_list { get; set; }
    }

    /// <summary>上报体温检测记录请求。</summary>
    public class OpenHardwareReportTemperatureDataRequest
    {
        /// <summary>待上报的体温检测记录。</summary>
        public List<OpenHardwareTemperatureData> temperature_data { get; set; }
    }

    /// <summary>体温检测记录。</summary>
    public class OpenHardwareTemperatureData
    {
        /// <summary>成员或访客 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>用户类型。</summary>
        public int user_type { get; set; }

        /// <summary>检测时间，Unix 时间戳。</summary>
        public long timestamp { get; set; }

        /// <summary>体温值字符串。</summary>
        public string temperature { get; set; }

        /// <summary>体温状态。</summary>
        public int status { get; set; }
    }

    /// <summary>上报体温检测记录结果。</summary>
    public class OpenHardwareReportTemperatureDataResult : WorkJsonResult
    {
        /// <summary>上报失败的体温记录；全部成功时为空。</summary>
        public List<OpenHardwareTemperatureData> fail_list { get; set; }
    }

    /// <summary>上报门禁通行记录请求。</summary>
    public class OpenHardwareReportAccessDataRequest
    {
        /// <summary>待上报的门禁通行记录。</summary>
        public List<OpenHardwareAccessData> access_data { get; set; }
    }

    /// <summary>门禁通行记录。</summary>
    public class OpenHardwareAccessData
    {
        /// <summary>成员或访客 OpenUserId。</summary>
        public string open_userid { get; set; }

        /// <summary>用户类型。</summary>
        public int user_type { get; set; }

        /// <summary>通行时间，Unix 时间戳。</summary>
        public long timestamp { get; set; }

        /// <summary>通行类型。</summary>
        public int pass_type { get; set; }

        /// <summary>通行方式。</summary>
        public int pass_method { get; set; }
    }

    /// <summary>上报门禁通行记录结果。</summary>
    public class OpenHardwareReportAccessDataResult : WorkJsonResult
    {
        /// <summary>上报失败的门禁记录；全部成功时为空。</summary>
        public List<OpenHardwareAccessData> fail_list { get; set; }
    }

    /// <summary>上报成员识别信息变化结果请求。</summary>
    public class OpenHardwareBiometricInfoResultRequest
    {
        /// <summary>企业微信下发指令的操作 ID。</summary>
        public string oper_id { get; set; }

        /// <summary>执行结果错误码，零表示成功。</summary>
        public int errcode { get; set; }

        /// <summary>执行结果错误描述。</summary>
        public string errmsg { get; set; }
    }

    /// <summary>上报远程开门结果请求。</summary>
    public class OpenHardwareRemoteOpenResultRequest
    {
        /// <summary>企业微信下发指令的操作 ID。</summary>
        public string oper_id { get; set; }

        /// <summary>本次开门指令的类型。</summary>
        public string type { get; set; }

        /// <summary>执行结果错误码，零表示成功。</summary>
        public int errcode { get; set; }

        /// <summary>执行结果错误描述。</summary>
        public string errmsg { get; set; }
    }
}
