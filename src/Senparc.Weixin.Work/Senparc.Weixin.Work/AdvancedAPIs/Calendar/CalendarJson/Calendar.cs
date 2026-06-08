/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：Calendar.cs
    文件功能描述：日历信息
    
    
    创建标识：Copilot - 20260608
    
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Calendar.CalendarJson
{
    /// <summary>
    /// 日历信息
    /// </summary>
    public class Calendar
    {
        /// <summary>
        /// 日历管理员ID列表
        /// </summary>
        public List<string> admins { get; set; }

        /// <summary>
        /// 是否设置为默认日历。0-否；1-是
        /// </summary>
        public int? set_as_default { get; set; }

        /// <summary>
        /// 日历标题
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 日历颜色（格式：#RRGGBB）
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// 日历描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 是否公共日历。0-否；1-是
        /// </summary>
        public int? is_public { get; set; }

        /// <summary>
        /// 公共范围
        /// </summary>
        public PublicRange public_range { get; set; }

        /// <summary>
        /// 是否全员日历。0-否；1-是
        /// </summary>
        public int? is_corp_calendar { get; set; }

        /// <summary>
        /// 共享者列表
        /// </summary>
        public List<Share> shares { get; set; }
    }

    /// <summary>
    /// 更新日历信息
    /// </summary>
    public class CalendarUpdate : Calendar
    {
        /// <summary>
        /// 日历ID
        /// </summary>
        public string cal_id { get; set; }
    }

    /// <summary>
    /// 公共范围
    /// </summary>
    public class PublicRange
    {
        /// <summary>
        /// 可见成员列表
        /// </summary>
        public List<string> userids { get; set; }

        /// <summary>
        /// 可见部门ID列表
        /// </summary>
        public List<long> partyids { get; set; }
    }

    /// <summary>
    /// 日历共享者
    /// </summary>
    public class Share
    {
        /// <summary>
        /// 成员ID
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public int permission { get; set; }
    }

    /// <summary>
    /// 创建日历请求参数
    /// </summary>
    public class CalendarAdd
    {
        /// <summary>
        /// 日历信息
        /// </summary>
        public Calendar calendar { get; set; }

        /// <summary>
        /// 授权方安装的应用agentid
        /// </summary>
        public int agentid { get; set; }
    }

    /// <summary>
    /// 更新日历请求参数
    /// </summary>
    public class CalendarUpdateData
    {
        /// <summary>
        /// 是否跳过更新可订阅范围。0-否；1-是
        /// </summary>
        public int? skip_public_range { get; set; }

        /// <summary>
        /// 日历信息
        /// </summary>
        public CalendarUpdate calendar { get; set; }
    }
}
