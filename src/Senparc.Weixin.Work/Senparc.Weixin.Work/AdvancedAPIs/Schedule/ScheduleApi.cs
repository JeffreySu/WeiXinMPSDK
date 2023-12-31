/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：ScheduleApi.cs
    文件功能描述：日程相关API
    
    
    创建标识：lishewen - 20191226
    
    修改标识：Senparc - 20230226
    修改描述：v3.15.16 Add 方法修改 ScheduleAdd 参数类型

----------------------------------------------------------------*/

using Senparc.NeuChar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Work.AdvancedAPIs.Schedule.ScheduleJson;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Schedule
{
    /// <summary>
    /// 日程相关API
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class ScheduleApi
    {
        #region 同步方法
        /// <summary>
        /// 创建日程（每个应用每天最多可创建1万个日程。）
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="schedule">日程信息</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AddScheduleJsonResult Add(string accessTokenOrAppKey, ScheduleAdd data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/schedule/add?access_token={0}";

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<AddScheduleJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 更新日程（注意，更新日程，不可变更组织者）
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="schedule">日程信息</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult Update(string accessTokenOrAppKey, ScheduleUpdate schedule, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/schedule/update?access_token={0}";

                var data = new
                {
                    schedule
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 取消日程
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="schedule_id">日程ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult Del(string accessTokenOrAppKey, string schedule_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/schedule/del?access_token={0}";

                var data = new
                {
                    schedule_id
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取日程(注意，被取消的日程也可以拉取详情，调用者需要检查status)
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="schedule_id_list">日程ID列表。一次最多拉取1000条</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetScheduleJsonResult Get(string accessTokenOrAppKey, List<string> schedule_id_list, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/schedule/get?access_token={0}";

                var data = new
                {
                    schedule_id_list
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetScheduleJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 创建日程（每个应用每天最多可创建1万个日程。）
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="schedule">日程信息</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<AddScheduleJsonResult> AddAsync(string accessTokenOrAppKey, ScheduleAdd data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/schedule/add?access_token={0}";

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<AddScheduleJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 更新日程（注意，更新日程，不可变更组织者）
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="schedule">日程信息</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> UpdateAsync(string accessTokenOrAppKey, ScheduleUpdate schedule, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/schedule/update?access_token={0}";

                var data = new
                {
                    schedule
                };

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 取消日程
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="schedule_id">日程ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> DelAsync(string accessTokenOrAppKey, string schedule_id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/schedule/del?access_token={0}";

                var data = new
                {
                    schedule_id
                };

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取日程(注意，被取消的日程也可以拉取详情，调用者需要检查status)
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="schedule_id_list">日程ID列表。一次最多拉取1000条</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetScheduleJsonResult> GetAsync(string accessTokenOrAppKey, List<string> schedule_id_list, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/schedule/get?access_token={0}";

                var data = new
                {
                    schedule_id_list
                };

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetScheduleJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        #endregion
    }
}
