/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：OaDataOpenApi.CheckinP2.cs
    文件功能描述：企业微信打卡增量接口

    创建标识：Senparc - 20260724

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信打卡月报、排班、补卡、人脸、硬件及规则管理接口
----------------------------------------------------------------*/

using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen.OaDataOpenJson;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen
{
    /// <summary>
    /// OA 数据开放接口（打卡增量）
    /// </summary>
    public partial class OaDataOpenApi
    {
        #region 同步方法

        /// <summary>获取企业全部打卡规则。</summary>
        public static GetCorpCheckinOptionJsonResult GetCorpCheckinOption(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return SendCheckin<GetCorpCheckinOptionJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/getcorpcheckinoption", new { }, timeOut);
        }

        /// <summary>获取打卡月报数据。</summary>
        public static GetCheckinMonthDataJsonResult GetCheckinMonthData(string accessTokenOrAppKey, CheckinStatisticsRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckin<GetCheckinMonthDataJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/getcheckin_monthdata", data, timeOut);
        }

        /// <summary>获取成员排班信息。</summary>
        public static GetCheckinScheduleListJsonResult GetCheckinScheduleList(string accessTokenOrAppKey, CheckinStatisticsRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckin<GetCheckinScheduleListJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/getcheckinschedulist", data, timeOut);
        }

        /// <summary>设置成员排班信息。</summary>
        public static WorkJsonResult SetCheckinScheduleList(string accessTokenOrAppKey, SetCheckinScheduleListRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckin<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/setcheckinschedulist", data, timeOut);
        }

        /// <summary>为成员补卡。</summary>
        public static WorkJsonResult PunchCorrection(string accessTokenOrAppKey, PunchCorrectionRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckin<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/punch_correction", data, timeOut);
        }

        /// <summary>录入成员人脸。</summary>
        public static WorkJsonResult AddCheckinUserFace(string accessTokenOrAppKey, AddCheckinUserFaceRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckin<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/addcheckinuserface", data, timeOut);
        }

        /// <summary>获取硬件打卡数据。</summary>
        public static GetHardwareCheckinDataJsonResult GetHardwareCheckinData(string accessTokenOrAppKey, GetHardwareCheckinDataRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckin<GetHardwareCheckinDataJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/hardware/get_hardware_checkin_data", data, timeOut);
        }

        /// <summary>新增打卡规则。</summary>
        public static WorkJsonResult AddCheckinOption(string accessTokenOrAppKey, CheckinOptionRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckin<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/add_checkin_option", data, timeOut);
        }

        /// <summary>更新打卡规则。</summary>
        public static WorkJsonResult UpdateCheckinOption(string accessTokenOrAppKey, CheckinOptionRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckin<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/update_checkin_option", data, timeOut);
        }

        /// <summary>清空打卡规则中的数组字段。</summary>
        public static WorkJsonResult ClearCheckinOptionArrayField(string accessTokenOrAppKey, ClearCheckinOptionArrayFieldRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckin<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/clear_checkin_option_array_field", data, timeOut);
        }

        /// <summary>删除打卡规则。</summary>
        public static WorkJsonResult DeleteCheckinOption(string accessTokenOrAppKey, DeleteCheckinOptionRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckin<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/del_checkin_option", data, timeOut);
        }

        #endregion

        #region 异步方法

        /// <summary>异步获取企业全部打卡规则。</summary>
        public static Task<GetCorpCheckinOptionJsonResult> GetCorpCheckinOptionAsync(string accessTokenOrAppKey, int timeOut = Config.TIME_OUT)
        {
            return SendCheckinAsync<GetCorpCheckinOptionJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/getcorpcheckinoption", new { }, timeOut);
        }

        /// <summary>异步获取打卡月报数据。</summary>
        public static Task<GetCheckinMonthDataJsonResult> GetCheckinMonthDataAsync(string accessTokenOrAppKey, CheckinStatisticsRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckinAsync<GetCheckinMonthDataJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/getcheckin_monthdata", data, timeOut);
        }

        /// <summary>异步获取成员排班信息。</summary>
        public static Task<GetCheckinScheduleListJsonResult> GetCheckinScheduleListAsync(string accessTokenOrAppKey, CheckinStatisticsRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckinAsync<GetCheckinScheduleListJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/getcheckinschedulist", data, timeOut);
        }

        /// <summary>异步设置成员排班信息。</summary>
        public static Task<WorkJsonResult> SetCheckinScheduleListAsync(string accessTokenOrAppKey, SetCheckinScheduleListRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckinAsync<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/setcheckinschedulist", data, timeOut);
        }

        /// <summary>异步为成员补卡。</summary>
        public static Task<WorkJsonResult> PunchCorrectionAsync(string accessTokenOrAppKey, PunchCorrectionRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckinAsync<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/punch_correction", data, timeOut);
        }

        /// <summary>异步录入成员人脸。</summary>
        public static Task<WorkJsonResult> AddCheckinUserFaceAsync(string accessTokenOrAppKey, AddCheckinUserFaceRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckinAsync<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/addcheckinuserface", data, timeOut);
        }

        /// <summary>异步获取硬件打卡数据。</summary>
        public static Task<GetHardwareCheckinDataJsonResult> GetHardwareCheckinDataAsync(string accessTokenOrAppKey, GetHardwareCheckinDataRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckinAsync<GetHardwareCheckinDataJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/hardware/get_hardware_checkin_data", data, timeOut);
        }

        /// <summary>异步新增打卡规则。</summary>
        public static Task<WorkJsonResult> AddCheckinOptionAsync(string accessTokenOrAppKey, CheckinOptionRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckinAsync<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/add_checkin_option", data, timeOut);
        }

        /// <summary>异步更新打卡规则。</summary>
        public static Task<WorkJsonResult> UpdateCheckinOptionAsync(string accessTokenOrAppKey, CheckinOptionRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckinAsync<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/update_checkin_option", data, timeOut);
        }

        /// <summary>异步清空打卡规则中的数组字段。</summary>
        public static Task<WorkJsonResult> ClearCheckinOptionArrayFieldAsync(string accessTokenOrAppKey, ClearCheckinOptionArrayFieldRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckinAsync<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/clear_checkin_option_array_field", data, timeOut);
        }

        /// <summary>异步删除打卡规则。</summary>
        public static Task<WorkJsonResult> DeleteCheckinOptionAsync(string accessTokenOrAppKey, DeleteCheckinOptionRequest data, int timeOut = Config.TIME_OUT)
        {
            return SendCheckinAsync<WorkJsonResult>(accessTokenOrAppKey,
                "/cgi-bin/checkin/del_checkin_option", data, timeOut);
        }

        #endregion

        private static T SendCheckin<T>(string accessTokenOrAppKey, string path, object data, int timeOut)
            where T : WorkJsonResult, new()
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
                CommonJsonSend.Send<T>(accessToken,
                    Config.ApiWorkHost + path + "?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut), accessTokenOrAppKey);
        }

        private static Task<T> SendCheckinAsync<T>(string accessTokenOrAppKey, string path, object data, int timeOut)
            where T : WorkJsonResult, new()
        {
            return ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
                await CommonJsonSend.SendAsync<T>(accessToken,
                    Config.ApiWorkHost + path + "?access_token={0}", data,
                    CommonJsonSendType.POST, timeOut).ConfigureAwait(false), accessTokenOrAppKey);
        }
    }
}
