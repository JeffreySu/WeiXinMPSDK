/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：AnalysisAPI.cs
    文件功能描述：分析数据接口


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/*
    图文分析数据接口详见：http://mp.weixin.qq.com/wiki/8/c0453610fb5131d1fcb17b4e87c82050.html
    接口分析数据接口详见：http://mp.weixin.qq.com/wiki/8/30ed81ae38cf4f977194bf1a5db73668.html
    消息分析数据接口详见：http://mp.weixin.qq.com/wiki/12/32d42ad542f2e4fc8a8aa60e1bce9838.html
    用户分析数据接口详见：http://mp.weixin.qq.com/wiki/3/ecfed6e1a0a03b5f35e5efac98e864b7.html
 */

using Senparc.Weixin.MP.AdvancedAPIs.Analysis;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 分析数据接口
    /// 最大时间跨度是指一次接口调用时最大可获取数据的时间范围，如最大时间跨度为7是指最多一次性获取7天的数据。
    /// 注意：所有的日期请使用【yyyy-MM-dd】的格式
    /// </summary>
    public static class AnalysisApi
    {
        /// <summary>
        /// 获取图文群发每日数据（getarticlesummary）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<ArticleSummaryItem> GetArticleSummary(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getarticlesummary?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<ArticleSummaryItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取图文群发总数据（getarticletotal）
        /// 请注意，details中，每天对应的数值为该文章到该日为止的总量（而不是当日的量）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<ArticleTotalItem> GetArticleTotal(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getarticletotal?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<ArticleTotalItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取图文统计数据（getuserread）
        /// 最大时间跨度 3
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UserReadItem> GetUserRead(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getuserread?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UserReadItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取图文统计分时数据（getuserreadhour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UserReadHourItem> GetUserReadHour(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getuserreadhour?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UserReadHourItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取图文分享转发数据（getusershare）
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UserShareItem> GetUserShare(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getusershare?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UserShareItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取图文分享转发分时数据（getusersharehour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UserShareHourItem> GetUserShareHour(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getusersharehour?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UserShareHourItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取接口分析数据（getinterfacesummary）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<InterfaceSummaryItem> GetInterfaceSummary(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getinterfacesummary?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<InterfaceSummaryItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取接口分析分时数据（getinterfacesummaryhour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<InterfaceSummaryHourItem> GetInterfaceSummaryHour(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getinterfacesummaryhour?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<InterfaceSummaryHourItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
        /// <summary>
        /// 获取消息发送概况数据（getupstreammsg）
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UpStreamMsgItem> GetUpStreamMsg(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsg?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UpStreamMsgItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取消息分送分时数据（getupstreammsghour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UpStreamMsgHourItem> GetUpStreamMsgHour(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsghour?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UpStreamMsgHourItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取消息发送周数据（getupstreammsgweek）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UpStreamMsgWeekItem> GetUpStreamMsgWeek(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsgweek?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UpStreamMsgWeekItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取消息发送月数据（getupstreammsgmonth）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UpStreamMsgMonthItem> GetUpStreamMsgMonth(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsgmonth?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UpStreamMsgMonthItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取消息发送分布数据（getupstreammsgdist）
        /// 最大时间跨度 15
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UpStreamMsgDistItem> GetUpStreamMsgDist(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsgdist?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UpStreamMsgDistItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取消息发送分布周数据（getupstreammsgdistweek）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UpStreamMsgDistWeekItem> GetUpStreamMsgDistWeek(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsgdistweek?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UpStreamMsgDistWeekItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取消息发送分布月数据（getupstreammsgdistmonth）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UpStreamMsgDistMonthItem> GetUpStreamMsgDistMonth(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getupstreammsgdistmonth?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UpStreamMsgDistMonthItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取用户增减数据
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UserSummaryItem> GetUserSummary(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getusersummary?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UserSummaryItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 获取累计用户数据
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static AnalysisResultJson<UserCumulateItem> GetUserCumulate(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = "https://api.weixin.qq.com/datacube/getusercumulate?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UserCumulateItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }
    }
}