/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：PublishedArticleAnalysisItemJson.cs
    文件功能描述：PublishedArticleAnalysisItemJson 强类型数据模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v16.25.1 补齐公众号 openApi、统计、图像、医疗、非税和一物一码官方接口

----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Analysis
{
    /// <summary>
    /// 新版发表内容统计结果
    /// </summary>
    public class PublishedArticleAnalysisResult<T> : AnalysisResultJson<T>
    {
        public bool is_delay { get; set; }
    }

    /// <summary>
    /// PublishedArticleRead 数据项。
    /// </summary>
    public class PublishedArticleReadItem : BaseAnalysisObject
    {
        public string ref_date { get; set; }
        public string msgid { get; set; }
        public PublishedArticleReadDetail detail { get; set; }
    }

    /// <summary>
    /// PublishedArticleShare 数据项。
    /// </summary>
    public class PublishedArticleShareItem : BaseAnalysisObject
    {
        public string ref_date { get; set; }
        public string msgid { get; set; }
        public PublishedArticleShareDetail detail { get; set; }
    }

    /// <summary>
    /// PublishedArticleBizSummary 数据项。
    /// </summary>
    public class PublishedArticleBizSummaryItem : BaseAnalysisObject
    {
        public string ref_date { get; set; }
        public PublishedArticleBizSummaryDetail detail { get; set; }
    }

    /// <summary>
    /// PublishedArticleTotalDetail 数据项。
    /// </summary>
    public class PublishedArticleTotalDetailItem : BaseAnalysisObject
    {
        public string ref_date { get; set; }
        public string msgid { get; set; }
        public int publish_type { get; set; }
        public string title { get; set; }
        public string content_url { get; set; }
        public PublishedArticleDailyDetail[] detail_list { get; set; }
    }

    /// <summary>
    /// PublishedArticleReadDetail 微信接口数据模型。
    /// </summary>
    public class PublishedArticleReadDetail
    {
        public long read_user { get; set; }
        public PublishedArticleReadSource[] read_user_source { get; set; }
    }

    /// <summary>
    /// PublishedArticleShareDetail 微信接口数据模型。
    /// </summary>
    public class PublishedArticleShareDetail
    {
        public long share_user { get; set; }
    }

    /// <summary>
    /// PublishedArticleBizSummaryDetail 微信接口数据模型。
    /// </summary>
    public class PublishedArticleBizSummaryDetail : PublishedArticleReadDetail
    {
        public long share_user { get; set; }
        public long zaikan_user { get; set; }
        public long like_user { get; set; }
        public long comment_count { get; set; }
        public long collection_user { get; set; }
        public long redirect_ori_page_user { get; set; }
        public long send_page_count { get; set; }
    }

    /// <summary>
    /// PublishedArticleDailyDetail 微信接口数据模型。
    /// </summary>
    public class PublishedArticleDailyDetail : PublishedArticleBizSummaryDetail
    {
        public string stat_date { get; set; }
        public long praise_money { get; set; }
        public long read_subscribe_user { get; set; }
        public double read_delivery_rate { get; set; }
        public double read_finish_rate { get; set; }
        public double read_avg_activetime { get; set; }
        public PublishedArticleJumpPosition[] read_jump_position { get; set; }
    }

    /// <summary>
    /// PublishedArticleReadSource 微信接口数据模型。
    /// </summary>
    public class PublishedArticleReadSource
    {
        public long user_count { get; set; }
        public string scene_desc { get; set; }
    }

    /// <summary>
    /// PublishedArticleJumpPosition 微信接口数据模型。
    /// </summary>
    public class PublishedArticleJumpPosition
    {
        public int position { get; set; }
        public double rate { get; set; }
    }
}
