using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    //接口详见：http://mp.weixin.qq.com/wiki/8/c0453610fb5131d1fcb17b4e87c82050.html
    
    /// <summary>
    /// 图文分析数据接口
    /// 最大时间跨度是指一次接口调用时最大可获取数据的时间范围，如最大时间跨度为7是指最多一次性获取7天的数据。
    /// 注意：所有的日期请使用【yyyy-MM-dd】的格式
    /// </summary>
    public static class ArticleAnalysis
    {
        /// <summary>
        /// 获取图文群发每日数据（getarticlesummary）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static ArticleSummaryResultJson GetArticleSummary(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getarticlesummary?access_token={0}";

            var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

            return CommonJsonSend.Send<ArticleSummaryResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取图文群发总数据（getarticletotal）
        /// 请注意，details中，每天对应的数值为该文章到该日为止的总量（而不是当日的量）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static ArticleTotalResultJson GetArticleTotal(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getarticletotal?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<ArticleTotalResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取图文统计数据（getuserread）
        /// 最大时间跨度 3
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UserReadResultJson GetUserRead(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getuserread?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<UserReadResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取图文统计分时数据（getuserreadhour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UserReadHourResultJson GetUserReadHour(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getuserreadhour?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<UserReadHourResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取图文分享转发数据（getusershare）
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UserShareResultJson GetUserShare(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getusershare?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<UserShareResultJson>(accessToken, urlFormat, data);
        }

        /// <summary>
        /// 获取图文分享转发分时数据（getusersharehour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static UserShareHourResultJson GetUserShareHour(string accessToken, string beginDate, string endDate)
        {
            string urlFormat = "https://api.weixin.qq.com/datacube/getusersharehour?access_token={0}";

            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };

            return CommonJsonSend.Send<UserShareHourResultJson>(accessToken, urlFormat, data);
        }
    }
}
