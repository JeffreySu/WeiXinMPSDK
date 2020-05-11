#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc

    文件名：AnalysisAPI.cs
    文件功能描述：分析数据接口


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
 
    修改标识：Senparc - 20160718
    修改描述：将其接口增加了异步方法

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

----------------------------------------------------------------*/

/*
    图文分析数据接口详见：http://mp.weixin.qq.com/wiki/8/c0453610fb5131d1fcb17b4e87c82050.html
    接口分析数据接口详见：http://mp.weixin.qq.com/wiki/8/30ed81ae38cf4f977194bf1a5db73668.html
    消息分析数据接口详见：http://mp.weixin.qq.com/wiki/12/32d42ad542f2e4fc8a8aa60e1bce9838.html
    用户分析数据接口详见：http://mp.weixin.qq.com/wiki/3/ecfed6e1a0a03b5f35e5efac98e864b7.html
 */



using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
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
        #region 同步方法

        #region 图文分析数据接口

        /// <summary>
        /// 获取图文群发每日数据（getarticlesummary）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetArticleSummary", true)]
        public static AnalysisResultJson<ArticleSummaryItem> GetArticleSummary(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getarticlesummary?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetArticleTotal", true)]
        public static AnalysisResultJson<ArticleTotalItem> GetArticleTotal(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getarticletotal?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserRead", true)]
        public static AnalysisResultJson<UserReadItem> GetUserRead(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getuserread?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserReadHour", true)]
        public static AnalysisResultJson<UserReadHourItem> GetUserReadHour(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getuserreadhour?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserShare", true)]
        public static AnalysisResultJson<UserShareItem> GetUserShare(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getusershare?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserShareHour", true)]
        public static AnalysisResultJson<UserShareHourItem> GetUserShareHour(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getusersharehour?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UserShareHourItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 接口分析数据接口

        /// <summary>
        /// 获取接口分析数据（getinterfacesummary）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetInterfaceSummary", true)]
        public static AnalysisResultJson<InterfaceSummaryItem> GetInterfaceSummary(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getinterfacesummary?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetInterfaceSummaryHour", true)]
        public static AnalysisResultJson<InterfaceSummaryHourItem> GetInterfaceSummaryHour(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getinterfacesummaryhour?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<InterfaceSummaryHourItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 消息分析数据接口

        /// <summary>
        /// 获取消息发送概况数据（getupstreammsg）
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsg", true)]
        public static AnalysisResultJson<UpStreamMsgItem> GetUpStreamMsg(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsg?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgHour", true)]
        public static AnalysisResultJson<UpStreamMsgHourItem> GetUpStreamMsgHour(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsghour?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgWeek", true)]
        public static AnalysisResultJson<UpStreamMsgWeekItem> GetUpStreamMsgWeek(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsgweek?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgMonth", true)]
        public static AnalysisResultJson<UpStreamMsgMonthItem> GetUpStreamMsgMonth(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsgmonth?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgDist", true)]
        public static AnalysisResultJson<UpStreamMsgDistItem> GetUpStreamMsgDist(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsgdist?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgDistWeek", true)]
        public static AnalysisResultJson<UpStreamMsgDistWeekItem> GetUpStreamMsgDistWeek(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsgdistweek?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgDistMonth", true)]
        public static AnalysisResultJson<UpStreamMsgDistMonthItem> GetUpStreamMsgDistMonth(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsgdistmonth?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UpStreamMsgDistMonthItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        #endregion

        #region 用户分析数据接口

        /// <summary>
        /// 获取用户增减数据
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserSummary", true)]
        public static AnalysisResultJson<UserSummaryItem> GetUserSummary(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getusersummary?access_token={0}";

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
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserCumulate", true)]
        public static AnalysisResultJson<UserCumulateItem> GetUserCumulate(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getusercumulate?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return CommonJsonSend.Send<AnalysisResultJson<UserCumulateItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #endregion

        #region 异步方法

        #region 图文分析数据接口

        /// <summary>
        /// 【异步方法】获取图文群发每日数据（getarticlesummary）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetArticleSummaryAsync", true)]
        public static async Task<AnalysisResultJson<ArticleSummaryItem>> GetArticleSummaryAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getarticlesummary?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await
                    Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<ArticleSummaryItem>>(
                        accessToken, urlFormat, data,
                        timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 【异步方法】获取图文群发总数据（getarticletotal）
        /// 请注意，details中，每天对应的数值为该文章到该日为止的总量（而不是当日的量）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetArticleTotalAsync", true)]
        public static async Task<AnalysisResultJson<ArticleTotalItem>> GetArticleTotalAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getarticletotal?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<ArticleTotalItem>>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取图文统计数据（getuserread）
        /// 最大时间跨度 3
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserReadAsync", true)]
        public static async Task<AnalysisResultJson<UserReadItem>> GetUserReadAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getuserread?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UserReadItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        ///【异步方法】 获取图文统计分时数据（getuserreadhour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserReadHourAsync", true)]
        public static async Task<AnalysisResultJson<UserReadHourItem>> GetUserReadHourAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getuserreadhour?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UserReadHourItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取图文分享转发数据（getusershare）
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserShareAsync", true)]
        public static async Task<AnalysisResultJson<UserShareItem>> GetUserShareAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getusershare?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UserShareItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }



        /// <summary>
        /// 【异步方法】获取图文分享转发分时数据（getusersharehour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserShareHourAsync", true)]
        public static async Task<AnalysisResultJson<UserShareHourItem>> GetUserShareHourAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getusersharehour?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UserShareHourItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #region 接口分析数据接口


        /// <summary>
        /// 【异步方法】获取接口分析数据（getinterfacesummary）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetInterfaceSummaryAsync", true)]
        public static async Task<AnalysisResultJson<InterfaceSummaryItem>> GetInterfaceSummaryAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getinterfacesummary?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<InterfaceSummaryItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取接口分析分时数据（getinterfacesummaryhour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetInterfaceSummaryHourAsync", true)]
        public static async Task<AnalysisResultJson<InterfaceSummaryHourItem>> GetInterfaceSummaryHourAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getinterfacesummaryhour?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<InterfaceSummaryHourItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #region 消息分析数据接口


        /// <summary>
        /// 【异步方法】获取消息发送概况数据（getupstreammsg）
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgAsync", true)]
        public static async Task<AnalysisResultJson<UpStreamMsgItem>> GetUpStreamMsgAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsg?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UpStreamMsgItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取消息分送分时数据（getupstreammsghour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgHourAsync", true)]
        public static async Task<AnalysisResultJson<UpStreamMsgHourItem>> GetUpStreamMsgHourAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsghour?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UpStreamMsgHourItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取消息发送周数据（getupstreammsgweek）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgWeekAsync", true)]
        public static async Task<AnalysisResultJson<UpStreamMsgWeekItem>> GetUpStreamMsgWeekAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsgweek?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UpStreamMsgWeekItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取消息发送月数据（getupstreammsgmonth）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgMonthAsync", true)]
        public static async Task<AnalysisResultJson<UpStreamMsgMonthItem>> GetUpStreamMsgMonthAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsgmonth?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UpStreamMsgMonthItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取消息发送分布数据（getupstreammsgdist）
        /// 最大时间跨度 15
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgDistAsync", true)]
        public static async Task<AnalysisResultJson<UpStreamMsgDistItem>> GetUpStreamMsgDistAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsgdist?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UpStreamMsgDistItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取消息发送分布周数据（getupstreammsgdistweek）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgDistWeekAsync", true)]
        public static async Task<AnalysisResultJson<UpStreamMsgDistWeekItem>> GetUpStreamMsgDistWeekAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsgdistweek?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UpStreamMsgDistWeekItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取消息发送分布月数据（getupstreammsgdistmonth）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUpStreamMsgDistMonthAsync", true)]
        public static async Task<AnalysisResultJson<UpStreamMsgDistMonthItem>> GetUpStreamMsgDistMonthAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getupstreammsgdistmonth?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UpStreamMsgDistMonthItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #region 用户分析数据接口

        /// <summary>
        ///【异步方法】 获取用户增减数据
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessTokenOrAppId">调用接口凭证</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserSummaryAsync", true)]
        public static async Task<AnalysisResultJson<UserSummaryItem>> GetUserSummaryAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getusersummary?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UserSummaryItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取累计用户数据
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_OfficialAccount, "AnalysisApi.GetUserCumulateAsync", true)]
        public static async Task<AnalysisResultJson<UserCumulateItem>> GetUserCumulateAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getusercumulate?access_token={0}";

                var data = new
                {
                    begin_date = beginDate,
                    end_date = endDate
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<AnalysisResultJson<UserCumulateItem>>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}