/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：LivingManagementJson.cs
    文件功能描述：企业微信预约直播管理与微信观众信息强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐直播创建、修改、取消、回放、观看凭证与分享信息模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Living.LivingJson
{
    /// <summary>活动直播详情。</summary>
    public class LivingActivityDetail
    {
        /// <summary>活动直播简介。</summary>
        public string description { get; set; }

        /// <summary>活动直播附图 media_id 列表，最多 5 张。</summary>
        public List<string> image_list { get; set; }
    }

    /// <summary>创建预约直播请求。</summary>
    public class CreateLivingRequest
    {
        /// <summary>直播发起者 UserID。</summary>
        public string anchor_userid { get; set; }

        /// <summary>直播标题。</summary>
        public string theme { get; set; }

        /// <summary>预约开始时间，Unix 时间戳。</summary>
        public long living_start { get; set; }

        /// <summary>预约直播时长，单位秒。</summary>
        public int living_duration { get; set; }

        /// <summary>直播简介。</summary>
        public string description { get; set; }

        /// <summary>直播类型：0 通用、1 小班课、2 大班课、3 企业培训、4 活动直播。</summary>
        public int? type { get; set; }

        /// <summary>旧第三方多应用套件的应用 AgentID。</summary>
        public int? agentid { get; set; }

        /// <summary>开始前提醒秒数。</summary>
        public int? remind_time { get; set; }

        /// <summary>活动直播封面图 media_id。</summary>
        public string activity_cover_mediaid { get; set; }

        /// <summary>活动直播分享卡片图 media_id。</summary>
        public string activity_share_mediaid { get; set; }

        /// <summary>活动直播详情。</summary>
        public LivingActivityDetail activity_detail { get; set; }
    }

    /// <summary>创建预约直播结果。</summary>
    public class CreateLivingResult : WorkJsonResult
    {
        /// <summary>直播 ID。</summary>
        public string livingid { get; set; }
    }

    /// <summary>修改预约直播请求。</summary>
    public class ModifyLivingRequest
    {
        /// <summary>直播 ID。</summary>
        public string livingid { get; set; }

        /// <summary>直播标题。</summary>
        public string theme { get; set; }

        /// <summary>预约开始时间，Unix 时间戳。</summary>
        public long? living_start { get; set; }

        /// <summary>预约直播时长，单位秒。</summary>
        public int? living_duration { get; set; }

        /// <summary>直播简介。</summary>
        public string description { get; set; }

        /// <summary>直播类型。</summary>
        public int? type { get; set; }

        /// <summary>开始前提醒秒数。</summary>
        public int? remind_time { get; set; }
    }

    /// <summary>只包含直播 ID 的请求。</summary>
    public class LivingIdRequest
    {
        /// <summary>直播 ID。</summary>
        public string livingid { get; set; }
    }

    /// <summary>获取微信观看直播凭证请求。</summary>
    public class GetLivingCodeRequest : LivingIdRequest
    {
        /// <summary>微信用户 OpenID。</summary>
        public string openid { get; set; }
    }

    /// <summary>获取微信观看直播凭证结果。</summary>
    public class GetLivingCodeResult : WorkJsonResult
    {
        /// <summary>五分钟内可重复使用的微信观看直播凭证。</summary>
        public string living_code { get; set; }
    }

    /// <summary>获取跳转小程序商城的直播观众信息请求。</summary>
    public class GetLivingShareInfoRequest
    {
        /// <summary>小程序路径携带的五分钟有效分享码。</summary>
        public string ww_share_code { get; set; }
    }

    /// <summary>获取跳转小程序商城的直播观众信息结果。</summary>
    public class GetLivingShareInfoResult : WorkJsonResult
    {
        /// <summary>直播 ID。</summary>
        public string livingid { get; set; }

        /// <summary>内部成员观众 UserID。</summary>
        public string viewer_userid { get; set; }

        /// <summary>外部观众 ExternalUserID。</summary>
        public string viewer_external_userid { get; set; }

        /// <summary>内部成员邀请人 UserID。</summary>
        public string invitor_userid { get; set; }

        /// <summary>外部邀请人 ExternalUserID。</summary>
        public string invitor_external_userid { get; set; }
    }
}
