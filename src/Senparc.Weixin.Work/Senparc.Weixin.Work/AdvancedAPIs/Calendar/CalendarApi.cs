/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：CalendarApi.cs
    文件功能描述：日历相关API
    
    
    创建标识：Copilot - 20260608
    
----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.Calendar.CalendarJson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Calendar
{
    /// <summary>
    /// 日历相关API
    /// </summary>
    [NcApiBind(NeuChar.PlatformType.WeChat_Work, true)]
    public static class CalendarApi
    {
        #region 同步方法
        /// <summary>
        /// 创建日历
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static AddCalendarJsonResult Add(string accessTokenOrAppKey, CalendarAdd data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/calendar/add?access_token={0}";

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<AddCalendarJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 更新日历
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult Update(string accessTokenOrAppKey, CalendarUpdateData data, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/calendar/update?access_token={0}";

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 获取日历
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="cal_id_list">日历ID列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static GetCalendarJsonResult Get(string accessTokenOrAppKey, List<string> cal_id_list, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/calendar/get?access_token={0}";

                var data = new
                {
                    cal_id_list
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetCalendarJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        /// <summary>
        /// 删除日历
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="cal_id">日历ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult Del(string accessTokenOrAppKey, string cal_id, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/calendar/del?access_token={0}";

                var data = new
                {
                    cal_id
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut);
            }, accessTokenOrAppKey);
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 创建日历
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<AddCalendarJsonResult> AddAsync(string accessTokenOrAppKey, CalendarAdd data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/calendar/add?access_token={0}";

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<AddCalendarJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 更新日历
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="data">请求参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> UpdateAsync(string accessTokenOrAppKey, CalendarUpdateData data, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/calendar/update?access_token={0}";

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 获取日历
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="cal_id_list">日历ID列表</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<GetCalendarJsonResult> GetAsync(string accessTokenOrAppKey, List<string> cal_id_list, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/calendar/get?access_token={0}";

                var data = new
                {
                    cal_id_list
                };

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCalendarJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        /// <summary>
        /// 删除日历
        /// </summary>
        /// <param name="accessTokenOrAppKey">接口调用凭证</param>
        /// <param name="cal_id">日历ID</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> DelAsync(string accessTokenOrAppKey, string cal_id, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/oa/calendar/del?access_token={0}";

                var data = new
                {
                    cal_id
                };

                return await Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(accessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);
            }, accessTokenOrAppKey).ConfigureAwait(false);
        }
        #endregion
    }
}
