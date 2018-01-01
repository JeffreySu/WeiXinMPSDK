#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
    
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

        /// <summary>
        /// 概况趋势
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetWeAnalysisAppidDailySummaryTrendResultJson GetWeAnalysisAppidDailySummaryTrend(string accessTokenOrAppId,string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappiddailysummarytrend?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<GetWeAnalysisAppidDailySummaryTrendResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 访问分析：日趋势
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetWeAnalysisAppidDailyVisitTrendResultJson GetWeAnalysisAppidDailyVisitTrend(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
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
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetWeAnalysisAppidWeeklyVisitTrendResultJson GetWeAnalysisAppidWeeklyVisitTrend(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
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
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetWeAnalysisAppidMonthlyVisitTrendResultJson GetWeAnalysisAppidMonthlyVisitTrend(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidmonthlyvisittrend?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<GetWeAnalysisAppidMonthlyVisitTrendResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        /// <summary>
        /// 访问分析：访问分布
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetWeAnalysisAppidVisitDistributionResultJson GetWeAnalysisAppidVisitDistribution(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappidvisitdistribution?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return CommonJsonSend.Send<GetWeAnalysisAppidVisitDistributionResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }




        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】概况趋势
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="endDate">开始日期，如：20170313</param>
        /// <param name="beginDate">结束日期，限定查询1天数据，end_date允许设置的最大值为昨日，如：20170312</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetWeAnalysisAppidDailySummaryTrendResultJson> GetWeAnalysisAppidDailySummaryTrendAsync(string accessTokenOrAppId, string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                string urlFormat = Config.ApiMpHost + "/datacube/getweanalysisappiddailysummarytrend?access_token={0}";
                var data = new { begin_date = beginDate, end_date = endDate };
                return await CommonJsonSend.SendAsync<GetWeAnalysisAppidDailySummaryTrendResultJson>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

        #endregion
#endif
    }
}
