/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：DataIntelligenceApi.cs
    文件功能描述：企业微信数据与智能专区接口
    
    
    创建标识：Senparc - 20241128
    
    修改标识：Senparc - 20241128
    修改描述：新增数据与智能专区相关接口，包括获取会话记录等功能
    
----------------------------------------------------------------*/

/*
    官方文档：https://developer.work.weixin.qq.com/document/path/99864
              https://developer.work.weixin.qq.com/document/path/99824
 */

using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.DataIntelligence;
using System;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    /// <summary>
    /// 数据与智能专区接口
    /// 官方文档：https://developer.work.weixin.qq.com/document/path/99864
    /// https://developer.work.weixin.qq.com/document/path/99824
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class DataIntelligenceApi
    {
        private static string _urlFormatGetConversationRecords = Config.ApiWorkHost + "/cgi-bin/data/get_conversation_records?access_token={0}";
        private static string _urlFormatGetMessageStatistics = Config.ApiWorkHost + "/cgi-bin/data/get_message_statistics?access_token={0}";

        #region 同步方法

        /// <summary>
        /// 获取会话记录
        /// 通过此接口可以获取企业微信中的会话记录数据，用于数据分析和智能化应用
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话ID，可通过会话创建接口获得</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="cursor">分页拉取的cursor，初始传入为空</param>
        /// <param name="limit">分页拉取的数量限制，最大为1000，默认为100</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static GetConversationRecordsResult GetConversationRecords(string accessTokenOrAppKey, string chatId, DateTime startTime, DateTime endTime, string cursor = "", int limit = 100, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new GetConversationRecordsRequest
                {
                    chatid = chatId,
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    cursor = cursor ?? "",
                    limit = Math.Min(limit, 1000) // 确保不超过最大限制
                };

                return CommonJsonSend.Send<GetConversationRecordsResult>(accessToken, _urlFormatGetConversationRecords, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取会话记录（使用请求对象）
        /// 通过此接口可以获取企业微信中的会话记录数据，用于数据分析和智能化应用
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="request">获取会话记录请求参数</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static GetConversationRecordsResult GetConversationRecords(string accessTokenOrAppKey, GetConversationRecordsRequest request, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                // 确保不超过最大限制
                if (request.limit > 1000)
                {
                    request.limit = 1000;
                }

                return CommonJsonSend.Send<GetConversationRecordsResult>(accessToken, _urlFormatGetConversationRecords, request, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取消息统计数据
        /// 通过此接口可以获取企业微信中的消息统计信息，用于分析沟通趋势和活跃度
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="startTime">统计开始时间</param>
        /// <param name="endTime">统计结束时间</param>
        /// <param name="type">统计类型：day(按天), week(按周), month(按月)</param>
        /// <param name="agentId">应用ID，为空时统计全部应用</param>
        /// <param name="userIds">用户ID列表，为空时统计全部用户</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static GetMessageStatisticsResult GetMessageStatistics(string accessTokenOrAppKey, DateTime startTime, DateTime endTime, string type = "day", string agentId = null, string[] userIds = null, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var data = new GetMessageStatisticsRequest
                {
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    type = type ?? "day",
                    agentid = agentId,
                    userids = userIds
                };

                return CommonJsonSend.Send<GetMessageStatisticsResult>(accessToken, _urlFormatGetMessageStatistics, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取消息统计数据（使用请求对象）
        /// 通过此接口可以获取企业微信中的消息统计信息，用于分析沟通趋势和活跃度
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="request">获取消息统计请求参数</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static GetMessageStatisticsResult GetMessageStatistics(string accessTokenOrAppKey, GetMessageStatisticsRequest request, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                return CommonJsonSend.Send<GetMessageStatisticsResult>(accessToken, _urlFormatGetMessageStatistics, request, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        #endregion

        #region 异步方法

        /// <summary>
        /// 【异步方法】获取会话记录
        /// 通过此接口可以获取企业微信中的会话记录数据，用于数据分析和智能化应用
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="chatId">会话ID，可通过会话创建接口获得</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="cursor">分页拉取的cursor，初始传入为空</param>
        /// <param name="limit">分页拉取的数量限制，最大为1000，默认为100</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static async Task<GetConversationRecordsResult> GetConversationRecordsAsync(string accessTokenOrAppKey, string chatId, DateTime startTime, DateTime endTime, string cursor = "", int limit = 100, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new GetConversationRecordsRequest
                {
                    chatid = chatId,
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    cursor = cursor ?? "",
                    limit = Math.Min(limit, 1000) // 确保不超过最大限制
                };

                return await CommonJsonSend.SendAsync<GetConversationRecordsResult>(accessToken, _urlFormatGetConversationRecords, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取会话记录（使用请求对象）
        /// 通过此接口可以获取企业微信中的会话记录数据，用于数据分析和智能化应用
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="request">获取会话记录请求参数</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static async Task<GetConversationRecordsResult> GetConversationRecordsAsync(string accessTokenOrAppKey, GetConversationRecordsRequest request, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                // 确保不超过最大限制
                if (request.limit > 1000)
                {
                    request.limit = 1000;
                }

                return await CommonJsonSend.SendAsync<GetConversationRecordsResult>(accessToken, _urlFormatGetConversationRecords, request, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取消息统计数据
        /// 通过此接口可以获取企业微信中的消息统计信息，用于分析沟通趋势和活跃度
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="startTime">统计开始时间</param>
        /// <param name="endTime">统计结束时间</param>
        /// <param name="type">统计类型：day(按天), week(按周), month(按月)</param>
        /// <param name="agentId">应用ID，为空时统计全部应用</param>
        /// <param name="userIds">用户ID列表，为空时统计全部用户</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static async Task<GetMessageStatisticsResult> GetMessageStatisticsAsync(string accessTokenOrAppKey, DateTime startTime, DateTime endTime, string type = "day", string agentId = null, string[] userIds = null, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var data = new GetMessageStatisticsRequest
                {
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    type = type ?? "day",
                    agentid = agentId,
                    userids = userIds
                };

                return await CommonJsonSend.SendAsync<GetMessageStatisticsResult>(accessToken, _urlFormatGetMessageStatistics, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取消息统计数据（使用请求对象）
        /// 通过此接口可以获取企业微信中的消息统计信息，用于分析沟通趋势和活跃度
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="request">获取消息统计请求参数</param>
        /// <param name="timeOut">超时时间</param>
        /// <returns></returns>
        public static async Task<GetMessageStatisticsResult> GetMessageStatisticsAsync(string accessTokenOrAppKey, GetMessageStatisticsRequest request, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                return await CommonJsonSend.SendAsync<GetMessageStatisticsResult>(accessToken, _urlFormatGetMessageStatistics, request, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        #endregion
    }
}
