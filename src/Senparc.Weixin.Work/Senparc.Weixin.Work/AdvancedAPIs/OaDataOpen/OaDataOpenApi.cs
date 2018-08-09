/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：SsoApi.cs
    文件功能描述：OA数据开放接口（Work中新增）
    
    
    创建标识：Senparc - 20170617

    修改标识：Senparc - 20170709
    修改描述：v0.3.1 修复OaDataOpenApi接口AccessToken传递问题

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造

----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers;
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
        public static GetCheckinDataJsonResult GetCheckinData(string accessTokenOrAppKey, OpenCheckinDataType openCheckinDataType, DateTime startTime, DateTime endTime, string[] userIdList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/checkin/getcheckindata?access_token={0}";

                var data = new
                {
                    opencheckindatatype = (int)openCheckinDataType,
                    starttime = DateTimeHelper.GetWeixinDateTime(startTime),
                    endtime = DateTimeHelper.GetWeixinDateTime(endTime),
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
        public static GetCheckinOptionJsonResult GetCheckinOption(string accessTokenOrAppKey, DateTime datetime, string[] userIdList, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/checkin/getcheckinoption?access_token={0}";

                var data = new
                {
                    datetime = DateTimeHelper.GetWeixinDateTime(datetime),
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
        public static GetApprovalDataJsonResult GetApprovalData(string accessTokenOrAppKey, DateTime startTime, DateTime endTime, long next_spnum, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/corp/getapprovaldata?access_token={0}";

                var data = new
                {
                    starttime = DateTimeHelper.GetWeixinDateTime(startTime),
                    endtime = DateTimeHelper.GetWeixinDateTime(endTime),
                    next_spnum = next_spnum
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetApprovalDataJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }



        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】获取打卡规则
        /// </summary>
        /// <param name="accessTokenOrAppKey">调用接口凭证（AccessToken）或AppKey（根据AccessTokenContainer.BuildingKey(corpId, corpSecret)方法获得）</param>
        /// <param name="datetime">需要获取规则的日期当天0点的Unix时间戳</param>
        /// <param name="userIdList">需要获取打卡规则的用户列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetCheckinOptionJsonResult> GetCheckinOptionAsync(string accessTokenOrAppKey, DateTime datetime, string[] userIdList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/checkin/getcheckinoption?access_token={0}";

                var data = new
                {
                    datetime = DateTimeHelper.GetWeixinDateTime(datetime),
                    useridlist = userIdList
                };

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCheckinOptionJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
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
        public static async Task<GetCheckinDataJsonResult> GetCheckinDataAsync(string accessTokenOrAppKey, OpenCheckinDataType openCheckinDataType, DateTime startTime, DateTime endTime, string[] userIdList, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/checkin/getcheckindata?access_token={0}";

                var data = new
                {
                    opencheckindatatype = (int)openCheckinDataType,
                    starttime = DateTimeHelper.GetWeixinDateTime(startTime),
                    endtime = DateTimeHelper.GetWeixinDateTime(endTime),
                    useridlist = userIdList
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCheckinDataJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
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
        public static async Task<GetApprovalDataJsonResult> GetApprovalDataAsync(string accessTokenOrAppKey, DateTime startTime, DateTime endTime, long next_spnum, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/corp/getapprovaldata?access_token={0}";

                var data = new
                {
                    starttime = DateTimeHelper.GetWeixinDateTime(startTime),
                    endtime = DateTimeHelper.GetWeixinDateTime(endTime),
                    next_spnum = next_spnum
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetApprovalDataJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);


        }

        #endregion
#endif
    }
}
