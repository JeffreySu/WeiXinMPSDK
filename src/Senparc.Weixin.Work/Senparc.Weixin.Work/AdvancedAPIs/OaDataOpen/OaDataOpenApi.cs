/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：SsoApi.cs
    文件功能描述：OA数据开放接口（Work中新增）
    
    
    创建标识：Senparc - 20170617

    修改标识：Senparc - 20170709
    修改描述：v0.3.1 修复OaDataOpenApi接口AccessToken传递问题

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造

----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers;
using Senparc.NeuChar;
using Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen.OaDataOpenJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen
{
    /// <summary>
    /// OA数据开放接口
    /// </summary>
    public class OaDataOpenApi
    {
        /// <summary>
        /// 打卡类型
        /// </summary>
        public enum OpenCheckinDataType
        {
            上下班打卡 = 1,
            外出打卡 = 2,
            全部打卡 = 3
        }

        #region 同步方法

        /// <summary>
        /// 获取打卡数据【QY移植新增】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="openCheckinDataType">打卡类型</param>
        /// <param name="startTime">获取打卡记录的开始时间</param>
        /// <param name="endTime">获取打卡记录的结束时间</param>
        /// <param name="userIdList">需要获取打卡记录的用户列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "OaDataOpenApi.GetCheckinData", true)]
        public static GetCheckinDataJsonResult GetCheckinData(string accessTokenOrAppKey, OpenCheckinDataType openCheckinDataType, DateTime startTime, DateTime endTime, string[] userIdList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/checkin/getcheckindata?access_token={0}";

                var data = new
                {
                    opencheckindatatype = (int)openCheckinDataType,
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    useridlist = userIdList
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetCheckinDataJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取打卡规则
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="datetime">需要获取规则的日期当天0点的Unix时间戳</param>
        /// <param name="userIdList">需要获取打卡规则的用户列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "OaDataOpenApi.GetCheckinOption", true)]
        public static GetCheckinOptionJsonResult GetCheckinOption(string accessTokenOrAppKey, DateTime datetime, string[] userIdList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/checkin/getcheckinoption?access_token={0}";

                var data = new
                {
                    datetime = DateTimeHelper.GetUnixDateTime(datetime),
                    useridlist = userIdList
                };

                return Weixin.CommonAPIs.CommonJsonSend.Send<GetCheckinOptionJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }

        /// <summary>
        /// 获取审批数据【QY移植新增】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="startTime">获取打卡记录的开始时间</param>
        /// <param name="endTime">获取打卡记录的结束时间</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "OaDataOpenApi.GetApprovalData", true)]
        public static GetApprovalDataJsonResult GetApprovalData(string accessTokenOrAppKey, DateTime startTime, DateTime endTime, long next_spnum, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/corp/getapprovaldata?access_token={0}";

                var data = new
                {
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    next_spnum = next_spnum
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetApprovalDataJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 获取公费电话拨打记录
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="startTime">获取打卡记录的开始时间</param>
        /// <param name="endTime">获取打卡记录的结束时间</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "OaDataOpenApi.GetDialRecord", true)]
        public static GetDialRecordJsonResult GetDialRecord(string accessTokenOrAppKey, DateTime startTime, DateTime endTime, int offset, int limit, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/dial/get_dial_record?access_token={0}";

                var data = new
                {
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    offset = offset,
                    limit = limit
                };

                return Weixin.CommonAPIs.CommonJsonSend.Send<GetDialRecordJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }

        /// <summary>
        /// 查询自建应用审批单当前状态
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="startTime">获取打卡记录的开始时间</param>
        /// <param name="endTime">获取打卡记录的结束时间</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "OaDataOpenApi.GetOpenApprovalData", true)]
        public static GetOpenApprovalDataJsonResult GetOpenApprovalData(string accessTokenOrAppKey, string thirdId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/corp/getopenapprovaldata?access_token={0}";

                var data = new
                {
                    thirdId
                };

                return Weixin.CommonAPIs.CommonJsonSend.Send<GetOpenApprovalDataJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);

        }

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】获取打卡规则
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="datetime">需要获取规则的日期当天0点的Unix时间戳</param>
        /// <param name="userIdList">需要获取打卡规则的用户列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "OaDataOpenApi.GetCheckinOptionAsync", true)]
        public static async Task<GetCheckinOptionJsonResult> GetCheckinOptionAsync(string accessTokenOrAppKey, DateTime datetime, string[] userIdList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/checkin/getcheckinoption?access_token={0}";

                var data = new
                {
                    datetime = DateTimeHelper.GetUnixDateTime(datetime),
                    useridlist = userIdList
                };

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCheckinOptionJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】获取打卡数据【QY移植新增】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="openCheckinDataType">打卡类型</param>
        /// <param name="startTime">获取打卡记录的开始时间</param>
        /// <param name="endTime">获取打卡记录的结束时间</param>
        /// <param name="userIdList">需要获取打卡记录的用户列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "OaDataOpenApi.GetCheckinDataAsync", true)]
        public static async Task<GetCheckinDataJsonResult> GetCheckinDataAsync(string accessTokenOrAppKey, OpenCheckinDataType openCheckinDataType, DateTime startTime, DateTime endTime, string[] userIdList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/checkin/getcheckindata?access_token={0}";

                var data = new
                {
                    opencheckindatatype = (int)openCheckinDataType,
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    useridlist = userIdList
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCheckinDataJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】获取审批数据【QY移植新增】
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="openCheckinDataType">打卡类型</param>
        /// <param name="startTime">获取打卡记录的开始时间</param>
        /// <param name="endTime">获取打卡记录的结束时间</param>
        /// <param name="userIdList">需要获取打卡记录的用户列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "OaDataOpenApi.GetApprovalDataAsync", true)]
        public static async Task<GetApprovalDataJsonResult> GetApprovalDataAsync(string accessTokenOrAppKey, DateTime startTime, DateTime endTime, long next_spnum, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/corp/getapprovaldata?access_token={0}";

                var data = new
                {
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    next_spnum = next_spnum
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetApprovalDataJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);


        }

        /// <summary>
        /// 【异步方法】获取公费电话拨打记录
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="startTime">获取打卡记录的开始时间</param>
        /// <param name="endTime">获取打卡记录的结束时间</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "OaDataOpenApi.GetDialRecordAsync", true)]
        public static async Task<GetDialRecordJsonResult> GetDialRecordAsync(string accessTokenOrAppKey, DateTime startTime, DateTime endTime, int offset, int limit, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/dial/get_dial_record?access_token={0}";

                var data = new
                {
                    starttime = DateTimeHelper.GetUnixDateTime(startTime),
                    endtime = DateTimeHelper.GetUnixDateTime(endTime),
                    offset = offset,
                    limit = limit
                };

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetDialRecordJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }

        /// <summary>
        /// 【异步方法】查询自建应用审批单当前状态
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="startTime">获取打卡记录的开始时间</param>
        /// <param name="endTime">获取打卡记录的结束时间</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "OaDataOpenApi.GetOpenApprovalDataAsync", true)]
        public static async Task<GetOpenApprovalDataJsonResult> GetOpenApprovalDataAsync(string accessTokenOrAppKey, string thirdId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/corp/getopenapprovaldata?access_token={0}";

                var data = new
                {
                    thirdId
                };

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetOpenApprovalDataJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);

        }

        #endregion
    }
}
