/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：UrgentNoticeJson.cs
    文件功能描述：企业微信紧急通知应用强类型模型


    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐紧急通知强类型请求响应模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.UrgentNotice
{
    /// <summary>
    /// 发起紧急通知语音电话请求。
    /// </summary>
    public class StartUrgentCallRequest
    {
        /// <summary>
        /// 需要呼叫的成员 ID 列表，不可为空。
        /// </summary>
        public IList<string> callee_userid { get; set; }
    }

    /// <summary>
    /// 单个成员的呼叫发起结果。
    /// </summary>
    public class UrgentCallStartState
    {
        /// <summary>
        /// 呼叫结果状态，0 表示成功发起呼叫。
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 呼叫唯一 ID。
        /// </summary>
        public string callid { get; set; }

        /// <summary>
        /// 被叫成员 ID。
        /// </summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 发起紧急通知语音电话结果。
    /// </summary>
    public class StartUrgentCallResult : WorkJsonResult
    {
        /// <summary>
        /// 各成员的呼叫发起结果。
        /// </summary>
        public IList<UrgentCallStartState> states { get; set; }
    }

    /// <summary>
    /// 获取语音电话接听状态请求。
    /// </summary>
    public class GetUrgentCallStateRequest
    {
        /// <summary>
        /// 被叫成员 ID。
        /// </summary>
        public string callee_userid { get; set; }

        /// <summary>
        /// 发起语音电话时返回的呼叫 ID；仅支持查询七天内的记录。
        /// </summary>
        public string callid { get; set; }
    }

    /// <summary>
    /// 获取语音电话接听状态结果。
    /// </summary>
    public class GetUrgentCallStateResult : WorkJsonResult
    {
        /// <summary>
        /// 是否已接听：0 表示未接听，1 表示已接听。
        /// </summary>
        public int istalked { get; set; }

        /// <summary>
        /// 呼叫发起时间戳。
        /// </summary>
        public long calltime { get; set; }

        /// <summary>
        /// 通话时长，单位为秒。
        /// </summary>
        public int talktime { get; set; }

        /// <summary>
        /// 呼叫状态或结束原因代码。
        /// </summary>
        public int reason { get; set; }
    }
}
