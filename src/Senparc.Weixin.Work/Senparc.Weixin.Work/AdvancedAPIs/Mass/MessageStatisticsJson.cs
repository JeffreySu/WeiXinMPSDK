/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MessageStatisticsJson.cs
    文件功能描述：企业微信应用消息发送统计强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加应用消息发送统计请求与结果模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Mass
{
    /// <summary>
    /// 获取应用消息发送统计请求。
    /// <para><see href="https://developer.work.weixin.qq.com/document/path/92369">企业微信官方文档</see></para>
    /// </summary>
    public class MessageStatisticsRequest
    {
        /// <summary>
        /// 查询时间类型；留空时使用企业微信接口的默认统计周期。
        /// </summary>
        public int? time_type { get; set; }
    }

    /// <summary>
    /// 单个企业应用的消息发送统计。
    /// </summary>
    public class MessageStatisticsItem
    {
        /// <summary>
        /// 企业应用 ID。
        /// </summary>
        public int agentid { get; set; }

        /// <summary>
        /// 企业应用名称。
        /// </summary>
        public string app_name { get; set; }

        /// <summary>
        /// 消息发送成功人次。
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// 获取应用消息发送统计结果。
    /// </summary>
    public class MessageStatisticsResult : WorkJsonResult
    {
        /// <summary>
        /// 各企业应用的统计数据。
        /// </summary>
        public MessageStatisticsItem[] statistics { get; set; }
    }
}
