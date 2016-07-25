/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ArticleAnalysisItemJson.cs
    文件功能描述：获取图文群发每日数据返回结果 单条数据类
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
  
    修改标识：Senparc - 20150310
    修改描述：修改类
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.Analysis
{
    /// <summary>
    /// 图文群发每日数据 单条数据
    /// </summary>
    public class ArticleSummaryItem : BaseAnalysisObject
    {
        /// <summary>
        /// 数据的日期
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 这里的msgid实际上是由msgid（图文消息id）和index（消息次序索引）组成， 例如12003_3， 其中12003是msgid，即一次群发的id消息的； 3为index，假设该次群发的图文消息共5个文章（因为可能为多图文）， 3表示5个中的第3个
        /// </summary>
        public string msgid { get; set; }
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图文页（点击群发图文卡片进入的页面）的阅读人数
        /// </summary>
        public int int_page_read_user { get; set; }
        /// <summary>
        /// 图文页的阅读次数
        /// </summary>
        public int int_page_read_count { get; set; }
        /// <summary>
        /// 原文页（点击图文页“阅读原文”进入的页面）的阅读人数，无原文页时此处数据为0
        /// </summary>
        public int ori_page_read_user { get; set; }
        /// <summary>
        /// 原文页的阅读次数
        /// </summary>
        public int ori_page_read_count { get; set; }
        /// <summary>
        /// 分享的人数
        /// </summary>
        public int share_user { get; set; }
        /// <summary>
        /// 分享的次数
        /// </summary>
        public int share_count { get; set; }
        /// <summary>
        /// 收藏的人数
        /// </summary>
        public int add_to_fav_user { get; set; }
        /// <summary>
        /// 收藏的次数
        /// </summary>
        public int add_to_fav_count { get; set; }
    }

    /// <summary>
    /// 图文群发总数据 单条数据
    /// </summary>
    public class ArticleTotalItem : BaseAnalysisObject
    {
        /// <summary>
        /// 数据的日期
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 这里的msgid实际上是由msgid（图文消息id）和index（消息次序索引）组成， 例如12003_3， 其中12003是msgid，即一次群发的id消息的； 3为index，假设该次群发的图文消息共5个文章（因为可能为多图文）， 3表示5个中的第3个
        /// </summary>
        public string msgid { get; set; }
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { get; set; }

        public List<ArticleTotal_Detail> details { get; set; }
    }

    public class ArticleTotal_Detail
    {
        /// <summary>
        /// 统计的日期，在getarticletotal接口中，ref_date指的是文章群发出日期， 而stat_date是数据统计日期
        /// </summary>
        public string stat_date { get; set; }
        /// <summary>
        /// 送达人数，一般约等于总粉丝数（需排除黑名单或其他异常情况下无法收到消息的粉丝）
        /// </summary>
        public int target_user { get; set; }
        /// <summary>
        /// 图文页（点击群发图文卡片进入的页面）的阅读人数
        /// </summary>
        public int int_page_read_user { get; set; }
        /// <summary>
        /// 图文页的阅读次数
        /// </summary>
        public int int_page_read_count { get; set; }
        /// <summary>
        /// 原文页（点击图文页“阅读原文”进入的页面）的阅读人数，无原文页时此处数据为0
        /// </summary>
        public int ori_page_read_user { get; set; }
        /// <summary>
        /// 原文页的阅读次数
        /// </summary>
        public int ori_page_read_count { get; set; }
        /// <summary>
        /// 分享的人数
        /// </summary>
        public int share_user { get; set; }
        /// <summary>
        /// 分享的次数
        /// </summary>
        public int share_count { get; set; }
        /// <summary>
        /// 收藏的人数
        /// </summary>
        public int add_to_fav_user { get; set; }
        /// <summary>
        /// 收藏的次数
        /// </summary>
        public int add_to_fav_count { get; set; }
    }
    
    /// <summary>
    /// 图文统计数据 单条数据
    /// </summary>
    public class UserReadItem : BaseAnalysisObject
    {
        /// <summary>
        /// 数据的日期
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 图文页（点击群发图文卡片进入的页面）的阅读人数
        /// </summary>
        public int int_page_read_user { get; set; }
        /// <summary>
        /// 图文页的阅读次数
        /// </summary>
        public int int_page_read_count { get; set; }
        /// <summary>
        /// 原文页（点击图文页“阅读原文”进入的页面）的阅读人数，无原文页时此处数据为0
        /// </summary>
        public int ori_page_read_user { get; set; }
        /// <summary>
        /// 原文页的阅读次数
        /// </summary>
        public int ori_page_read_count { get; set; }
        /// <summary>
        /// 分享的人数
        /// </summary>
        public int share_user { get; set; }
        /// <summary>
        /// 分享的次数
        /// </summary>
        public int share_count { get; set; }
        /// <summary>
        /// 收藏的人数
        /// </summary>
        public int add_to_fav_user { get; set; }
        /// <summary>
        /// 收藏的次数
        /// </summary>
        public int add_to_fav_count { get; set; }
    }

    /// <summary>
    /// 图文统计分时数据 单条数据
    /// </summary>
    public class UserReadHourItem : BaseAnalysisObject
    {
        /// <summary>
        /// 数据的日期
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 数据的小时，包括从000到2300，分别代表的是[000,100)到[2300,2400)，即每日的第1小时和最后1小时
        /// </summary>
        public int ref_hour { get; set; }
        /// <summary>
        /// 图文页（点击群发图文卡片进入的页面）的阅读人数
        /// </summary>
        public int int_page_read_user { get; set; }
        /// <summary>
        /// 图文页的阅读次数
        /// </summary>
        public int int_page_read_count { get; set; }
        /// <summary>
        /// 原文页（点击图文页“阅读原文”进入的页面）的阅读人数，无原文页时此处数据为0
        /// </summary>
        public int ori_page_read_user { get; set; }
        /// <summary>
        /// 原文页的阅读次数
        /// </summary>
        public int ori_page_read_count { get; set; }
        /// <summary>
        /// 分享的人数
        /// </summary>
        public int share_user { get; set; }
        /// <summary>
        /// 分享的次数
        /// </summary>
        public int share_count { get; set; }
        /// <summary>
        /// 收藏的人数
        /// </summary>
        public int add_to_fav_user { get; set; }
        /// <summary>
        /// 收藏的次数
        /// </summary>
        public int add_to_fav_count { get; set; }
    }

    /// <summary>
    /// 图文分享转发数据 单条数据
    /// </summary>
    public class UserShareItem : BaseAnalysisObject
    {
        /// <summary>
        /// 数据的日期
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 分享的场景
        ///1代表好友转发 2代表朋友圈 3代表腾讯微博 255代表其他
        /// </summary>
        public int share_scene { get; set; }
        /// <summary>
        /// 分享的人数
        /// </summary>
        public int share_count { get; set; }
        /// <summary>
        /// 分享的次数
        /// </summary>
        public int share_user { get; set; }
    }

    /// <summary>
    /// 图文分享转发分时数据 单条数据
    /// </summary>
    public class UserShareHourItem : BaseAnalysisObject
    {
        /// <summary>
        /// 数据的日期
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 数据的小时，包括从000到2300，分别代表的是[000,100)到[2300,2400)，即每日的第1小时和最后1小时
        /// </summary>
        public int ref_hour { get; set; }
        /// <summary>
        /// 分享的场景
        ///1代表好友转发 2代表朋友圈 3代表腾讯微博 255代表其他
        /// </summary>
        public int share_scene { get; set; }
        /// <summary>
        /// 分享的人数
        /// </summary>
        public int share_count { get; set; }
        /// <summary>
        /// 分享的次数
        /// </summary>
        public int share_user { get; set; }
    }
}
