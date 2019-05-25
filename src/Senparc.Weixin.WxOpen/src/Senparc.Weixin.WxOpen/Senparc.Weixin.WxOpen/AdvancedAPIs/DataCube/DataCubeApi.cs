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
    
    文件名：DataCubeApi.cs
    文件功能描述：小程序“数据分析”接口
    
    
    创建标识：Senparc - 20180101
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.DataCube
{
    /// <summary>
    /// 小程序“数据分析”接口
    /// </summary>
    public static class DataCubeApi
    {

        #region 同步方法

        #region 概况趋势

        /// <summary>
        /// 概况趋势
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidDailySummaryTrend", true)]
        public static GetWeAnalysisAppidDailySummaryTrendResultJson GetWeAnalysisAppidDailySummaryTrend(string accessTokenOrAppId,string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappiddailysummarytrend?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<GetWeAnalysisAppidDailySummaryTrendResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        #endregion

        #region 访问趋势

        /// <summary>
        /// 访问分析：日趋势
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidDailyVisitTrend", true)]
        public static GetWeAnalysisAppidDailyVisitTrendResultJson GetWeAnalysisAppidDailyVisitTrend(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappiddailyvisittrend?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<GetWeAnalysisAppidDailyVisitTrendResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 访问分析：周趋势
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，为周一日期，如：20170306</param>
        /// <param name="beginDate">结束日期，为周日日期，限定查询一周数据，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidWeeklyVisitTrend", true)]
        public static GetWeAnalysisAppidWeeklyVisitTrendResultJson GetWeAnalysisAppidWeeklyVisitTrend(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidweeklyvisittrend?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<GetWeAnalysisAppidWeeklyVisitTrendResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 访问分析：月趋势
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，为自然月第一天，如：20170201/param>
        /// <param name="beginDate">结束日期，为自然月最后一天，限定查询一个月数据，如：20170228</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidMonthlyVisitTrend", true)]
        public static GetWeAnalysisAppidMonthlyVisitTrendResultJson GetWeAnalysisAppidMonthlyVisitTrend(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidmonthlyvisittrend?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<GetWeAnalysisAppidMonthlyVisitTrendResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        #endregion

        #region 访问分布

        /// <summary>
        /// 访问分析：访问分布
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidVisitDistribution", true)]
        public static GetWeAnalysisAppidVisitDistributionResultJson GetWeAnalysisAppidVisitDistribution(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidvisitdistribution?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<GetWeAnalysisAppidVisitDistributionResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #region 访问留存

        /// <summary>
        /// 访问分析：访问留存-日留存
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidDailyRetainInfo", true)]
        public static CommonGetWeAnalysisAppidRetainInfoResultJson GetWeAnalysisAppidDailyRetainInfo(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappiddailyretaininfo?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<CommonGetWeAnalysisAppidRetainInfoResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 访问分析：访问留存-周留存
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，为周一日期，如：20170306</param>
        /// <param name="beginDate">结束日期，为周日日期，限定查询一周数据，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidWeeklyRetainInfo", true)]
        public static CommonGetWeAnalysisAppidRetainInfoResultJson GetWeAnalysisAppidWeeklyRetainInfo(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidweeklyretaininfo?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<CommonGetWeAnalysisAppidRetainInfoResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 访问分析：访问留存-月留存
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，为自然月第一天，如：20170201</param>
        /// <param name="beginDate">结束日期，为自然月最后一天，限定查询一个月数据，如：20170228</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidMonthlyRetainInfo", true)]
        public static CommonGetWeAnalysisAppidRetainInfoResultJson GetWeAnalysisAppidMonthlyRetainInfo(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidmonthlyretaininfo?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<CommonGetWeAnalysisAppidRetainInfoResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #region 访问页面

        /// <summary>
        /// 访问分析：访问页面。
        /// 注意：目前只提供按 page_visit_pv 排序的 top200
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170313</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidVisitPage", true)]
        public static GetWeAnalysisAppidVisitPageResultJson GetWeAnalysisAppidVisitPage(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidvisitpage?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<GetWeAnalysisAppidVisitPageResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #endregion

        #region 用户画像

        /// <summary>
        /// 访问分析：用户画像。
        /// 注：
        /// 1、部分用户属性数据缺失，属性值可能出现 “未知”。
        /// 2、机型数据无 id 字段，暂只提供用户数最多的 top20。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：2017-06-11</param>
        /// <param name="beginDate">结束日期，开始日期与结束日期相差的天数限定为0/6/29，分别表示查询最近1/7/30天数据，end_date允许设置的最大值为昨日，如：2017-06-17</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidUserPortrait", true)]
        public static GetWeAnalysisAppidUserPortraitResultJson GetWeAnalysisAppidUserPortrait(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return WxOpenApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappiduserportrait?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<GetWeAnalysisAppidUserPortraitResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion

        #endregion


        #region 异步方法

        #region 概况趋势

        /// <summary>
        /// 【异步方法】概况趋势
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidDailySummaryTrendAsync", true)]
        public static async Task<GetWeAnalysisAppidDailySummaryTrendResultJson> GetWeAnalysisAppidDailySummaryTrendAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappiddailysummarytrend?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return await CommonJsonSend.SendAsync<GetWeAnalysisAppidDailySummaryTrendResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }
        #endregion

        #region 访问趋势

        /// <summary>
        /// 【异步方法】访问分析：日趋势
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidDailyVisitTrendAsync", true)]
        public static async Task<GetWeAnalysisAppidDailyVisitTrendResultJson> GetWeAnalysisAppidDailyVisitTrendAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappiddailyvisittrend?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return await CommonJsonSend.SendAsync<GetWeAnalysisAppidDailyVisitTrendResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】访问分析：周趋势
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，为周一日期，如：20170306</param>
        /// <param name="beginDate">结束日期，为周日日期，限定查询一周数据，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidWeeklyVisitTrendAsync", true)]
        public static async Task<GetWeAnalysisAppidWeeklyVisitTrendResultJson> GetWeAnalysisAppidWeeklyVisitTrendAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidweeklyvisittrend?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return await CommonJsonSend.SendAsync<GetWeAnalysisAppidWeeklyVisitTrendResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);
            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】访问分析：月趋势
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，为自然月第一天，如：20170201/param>
        /// <param name="beginDate">结束日期，为自然月最后一天，限定查询一个月数据，如：20170228</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidMonthlyVisitTrendAsync", true)]
        public static async Task<GetWeAnalysisAppidMonthlyVisitTrendResultJson> GetWeAnalysisAppidMonthlyVisitTrendAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidmonthlyvisittrend?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return await CommonJsonSend.SendAsync<GetWeAnalysisAppidMonthlyVisitTrendResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        #endregion

        #region 访问分布

        /// <summary>
        /// 【异步方法】访问分析：访问分布
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidVisitDistributionAsync", true)]
        public static async Task<GetWeAnalysisAppidVisitDistributionResultJson> GetWeAnalysisAppidVisitDistributionAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidvisitdistribution?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return await CommonJsonSend.SendAsync<GetWeAnalysisAppidVisitDistributionResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #region 访问留存

        /// <summary>
        /// 【异步方法】访问分析：访问留存-日留存
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidDailyRetainInfoAsync", true)]
        public static async Task<CommonGetWeAnalysisAppidRetainInfoResultJson> GetWeAnalysisAppidDailyRetainInfoAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappiddailyretaininfo?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return await CommonJsonSend.SendAsync<CommonGetWeAnalysisAppidRetainInfoResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】访问分析：访问留存-周留存
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，为周一日期，如：20170306</param>
        /// <param name="beginDate">结束日期，为周日日期，限定查询一周数据，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidWeeklyRetainInfoAsync", true)]
        public static async Task<CommonGetWeAnalysisAppidRetainInfoResultJson> GetWeAnalysisAppidWeeklyRetainInfoAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidweeklyretaininfo?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return await CommonJsonSend.SendAsync<CommonGetWeAnalysisAppidRetainInfoResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】访问分析：访问留存-月留存
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，为自然月第一天，如：20170201</param>
        /// <param name="beginDate">结束日期，为自然月最后一天，限定查询一个月数据，如：20170228</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidMonthlyRetainInfoAsync", true)]
        public static async Task<CommonGetWeAnalysisAppidRetainInfoResultJson> GetWeAnalysisAppidMonthlyRetainInfoAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidmonthlyretaininfo?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return await CommonJsonSend.SendAsync<CommonGetWeAnalysisAppidRetainInfoResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #region 访问页面

        /// <summary>
        /// 【异步方法】访问分析：访问页面。
        /// 注意：目前只提供按 page_visit_pv 排序的 top200
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170313</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidVisitPageAsync", true)]
        public static async Task<GetWeAnalysisAppidVisitPageResultJson> GetWeAnalysisAppidVisitPageAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidvisitpage?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return await CommonJsonSend.SendAsync<GetWeAnalysisAppidVisitPageResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #endregion

        #region 用户画像

        /// <summary>
        /// 【异步方法】访问分析：用户画像。
        /// 注：
        /// 1、部分用户属性数据缺失，属性值可能出现 “未知”。
        /// 2、机型数据无 id 字段，暂只提供用户数最多的 top20。
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：2017-06-11</param>
        /// <param name="beginDate">结束日期，开始日期与结束日期相差的天数限定为0/6/29，分别表示查询最近1/7/30天数据，end_date允许设置的最大值为昨日，如：2017-06-17</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_MiniProgram, "DataCubeApi.GetWeAnalysisAppidUserPortraitAsync", true)]
        public static async Task<GetWeAnalysisAppidUserPortraitResultJson> GetWeAnalysisAppidUserPortraitAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await WxOpenApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappiduserportrait?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return await CommonJsonSend.SendAsync<GetWeAnalysisAppidUserPortraitResultJson>(accessToken, urlFormat, data, timeOut: timeOut).ConfigureAwait(false);

            }, accessTokenOrAppId).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
