/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SchoolApi.Service.cs
    文件功能描述：企业微信家校健康、直播、缴费与应用范围接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐家校健康、直播、缴费与应用范围接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.Work.AdvancedAPIs.School
{
    /// <summary>企业微信家校健康、直播、缴费与应用范围接口。</summary>
    public static partial class SchoolApi
    {
        private const string GetTeacherHealthInfoPath = "/cgi-bin/school/user/get_teacher_customize_health_info";
        private const string GetStudentHealthInfoPath = "/cgi-bin/school/user/get_student_customize_health_info";
        private const string GetHealthQrCodePath = "/cgi-bin/school/user/get_health_qrcode";
        private const string GetSchoolLivingInfoPath = "/cgi-bin/school/living/get_living_info";
        private const string GetSchoolWatchStatisticsPath = "/cgi-bin/school/living/get_watch_stat";
        private const string GetSchoolUnwatchStatisticsPath = "/cgi-bin/school/living/get_unwatch_stat";
        private const string GetSchoolWatchStatisticsV2Path = "/cgi-bin/school/living/get_watch_stat_v2";
        private const string GetSchoolUnwatchStatisticsV2Path = "/cgi-bin/school/living/get_unwatch_stat_v2";
        private const string GetSchoolPaymentResultPath = "/cgi-bin/school/get_payment_result";
        private const string GetSchoolTradePath = "/cgi-bin/school/get_trade";
        private const string GetSchoolAllowScopePath = "/cgi-bin/school/agent/get_allow_scope";

        /// <summary>获取老师健康信息。<see href="https://developer.work.weixin.qq.com/document/path/93744"/></summary>
        public static SchoolHealthInfoResult GetTeacherHealthInfo(string accessTokenOrAppKey,
            SchoolHealthInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolHealthInfoResult>(accessTokenOrAppKey, GetTeacherHealthInfoPath, request, timeOut);

        /// <summary>异步获取老师健康信息。<see href="https://developer.work.weixin.qq.com/document/path/93744"/></summary>
        public static Task<SchoolHealthInfoResult> GetTeacherHealthInfoAsync(string accessTokenOrAppKey,
            SchoolHealthInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolHealthInfoResult>(accessTokenOrAppKey, GetTeacherHealthInfoPath, request, timeOut);

        /// <summary>获取学生健康信息。<see href="https://developer.work.weixin.qq.com/document/path/93745"/></summary>
        public static SchoolHealthInfoResult GetStudentHealthInfo(string accessTokenOrAppKey,
            SchoolHealthInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolHealthInfoResult>(accessTokenOrAppKey, GetStudentHealthInfoPath, request, timeOut);

        /// <summary>异步获取学生健康信息。<see href="https://developer.work.weixin.qq.com/document/path/93745"/></summary>
        public static Task<SchoolHealthInfoResult> GetStudentHealthInfoAsync(string accessTokenOrAppKey,
            SchoolHealthInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolHealthInfoResult>(accessTokenOrAppKey, GetStudentHealthInfoPath, request, timeOut);

        /// <summary>获取师生健康码。<see href="https://developer.work.weixin.qq.com/document/path/93746"/></summary>
        public static SchoolHealthQrCodeResult GetHealthQrCode(string accessTokenOrAppKey,
            SchoolHealthQrCodeRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolHealthQrCodeResult>(accessTokenOrAppKey, GetHealthQrCodePath, request, timeOut);

        /// <summary>异步获取师生健康码。<see href="https://developer.work.weixin.qq.com/document/path/93746"/></summary>
        public static Task<SchoolHealthQrCodeResult> GetHealthQrCodeAsync(string accessTokenOrAppKey,
            SchoolHealthQrCodeRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolHealthQrCodeResult>(accessTokenOrAppKey, GetHealthQrCodePath, request, timeOut);

        /// <summary>获取家校直播详情。<see href="https://developer.work.weixin.qq.com/document/path/93740"/></summary>
        public static SchoolLivingInfoResult GetSchoolLivingInfo(string accessTokenOrAppKey, string livingId,
            int timeOut = Config.TIME_OUT)
            => Get<SchoolLivingInfoResult>(accessTokenOrAppKey, GetSchoolLivingInfoPath,
                "livingid=" + livingId.AsUrlData(), timeOut);

        /// <summary>异步获取家校直播详情。<see href="https://developer.work.weixin.qq.com/document/path/93740"/></summary>
        public static Task<SchoolLivingInfoResult> GetSchoolLivingInfoAsync(string accessTokenOrAppKey,
            string livingId, int timeOut = Config.TIME_OUT)
            => GetAsync<SchoolLivingInfoResult>(accessTokenOrAppKey, GetSchoolLivingInfoPath,
                "livingid=" + livingId.AsUrlData(), timeOut);

        /// <summary>获取家校直播观看统计。<see href="https://developer.work.weixin.qq.com/document/path/93741"/></summary>
        public static SchoolLivingWatchResult GetSchoolWatchStatistics(string accessTokenOrAppKey,
            SchoolLivingStatisticsRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolLivingWatchResult>(accessTokenOrAppKey, GetSchoolWatchStatisticsPath, request, timeOut);

        /// <summary>异步获取家校直播观看统计。<see href="https://developer.work.weixin.qq.com/document/path/93741"/></summary>
        public static Task<SchoolLivingWatchResult> GetSchoolWatchStatisticsAsync(string accessTokenOrAppKey,
            SchoolLivingStatisticsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolLivingWatchResult>(accessTokenOrAppKey, GetSchoolWatchStatisticsPath, request, timeOut);

        /// <summary>获取家校直播未观看统计。<see href="https://developer.work.weixin.qq.com/document/path/93742"/></summary>
        public static SchoolLivingUnwatchResult GetSchoolUnwatchStatistics(string accessTokenOrAppKey,
            SchoolLivingStatisticsRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolLivingUnwatchResult>(accessTokenOrAppKey, GetSchoolUnwatchStatisticsPath, request, timeOut);

        /// <summary>异步获取家校直播未观看统计。<see href="https://developer.work.weixin.qq.com/document/path/93742"/></summary>
        public static Task<SchoolLivingUnwatchResult> GetSchoolUnwatchStatisticsAsync(string accessTokenOrAppKey,
            SchoolLivingStatisticsRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolLivingUnwatchResult>(accessTokenOrAppKey, GetSchoolUnwatchStatisticsPath, request, timeOut);

        /// <summary>获取新版家校直播观看统计。<see href="https://developer.work.weixin.qq.com/document/path/95793"/></summary>
        public static SchoolLivingWatchV2Result GetSchoolWatchStatisticsV2(string accessTokenOrAppKey,
            SchoolLivingStatisticsV2Request request, int timeOut = Config.TIME_OUT)
            => Post<SchoolLivingWatchV2Result>(accessTokenOrAppKey, GetSchoolWatchStatisticsV2Path, request, timeOut);

        /// <summary>异步获取新版家校直播观看统计。<see href="https://developer.work.weixin.qq.com/document/path/95793"/></summary>
        public static Task<SchoolLivingWatchV2Result> GetSchoolWatchStatisticsV2Async(string accessTokenOrAppKey,
            SchoolLivingStatisticsV2Request request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolLivingWatchV2Result>(accessTokenOrAppKey, GetSchoolWatchStatisticsV2Path, request, timeOut);

        /// <summary>获取新版家校直播未观看统计。<see href="https://developer.work.weixin.qq.com/document/path/95795"/></summary>
        public static SchoolLivingUnwatchV2Result GetSchoolUnwatchStatisticsV2(string accessTokenOrAppKey,
            SchoolLivingStatisticsV2Request request, int timeOut = Config.TIME_OUT)
            => Post<SchoolLivingUnwatchV2Result>(accessTokenOrAppKey, GetSchoolUnwatchStatisticsV2Path, request, timeOut);

        /// <summary>异步获取新版家校直播未观看统计。<see href="https://developer.work.weixin.qq.com/document/path/95795"/></summary>
        public static Task<SchoolLivingUnwatchV2Result> GetSchoolUnwatchStatisticsV2Async(string accessTokenOrAppKey,
            SchoolLivingStatisticsV2Request request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolLivingUnwatchV2Result>(accessTokenOrAppKey, GetSchoolUnwatchStatisticsV2Path, request, timeOut);

        /// <summary>获取学生付款结果。<see href="https://developer.work.weixin.qq.com/document/path/94470"/></summary>
        public static SchoolPaymentResult GetSchoolPaymentResult(string accessTokenOrAppKey,
            SchoolPaymentRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolPaymentResult>(accessTokenOrAppKey, GetSchoolPaymentResultPath, request, timeOut);

        /// <summary>异步获取学生付款结果。<see href="https://developer.work.weixin.qq.com/document/path/94470"/></summary>
        public static Task<SchoolPaymentResult> GetSchoolPaymentResultAsync(string accessTokenOrAppKey,
            SchoolPaymentRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolPaymentResult>(accessTokenOrAppKey, GetSchoolPaymentResultPath, request, timeOut);

        /// <summary>获取学生缴费订单详情。<see href="https://developer.work.weixin.qq.com/document/path/94471"/></summary>
        public static SchoolTradeResult GetSchoolTrade(string accessTokenOrAppKey, SchoolTradeRequest request,
            int timeOut = Config.TIME_OUT)
            => Post<SchoolTradeResult>(accessTokenOrAppKey, GetSchoolTradePath, request, timeOut);

        /// <summary>异步获取学生缴费订单详情。<see href="https://developer.work.weixin.qq.com/document/path/94471"/></summary>
        public static Task<SchoolTradeResult> GetSchoolTradeAsync(string accessTokenOrAppKey,
            SchoolTradeRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolTradeResult>(accessTokenOrAppKey, GetSchoolTradePath, request, timeOut);

        /// <summary>获取应用可使用的家长范围。<see href="https://developer.work.weixin.qq.com/document/path/94895"/></summary>
        public static SchoolAllowScopeResult GetSchoolAllowScope(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => Get<SchoolAllowScopeResult>(accessTokenOrAppKey, GetSchoolAllowScopePath, null, timeOut);

        /// <summary>异步获取应用可使用的家长范围。<see href="https://developer.work.weixin.qq.com/document/path/94895"/></summary>
        public static Task<SchoolAllowScopeResult> GetSchoolAllowScopeAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => GetAsync<SchoolAllowScopeResult>(accessTokenOrAppKey, GetSchoolAllowScopePath, null, timeOut);
    }
}
