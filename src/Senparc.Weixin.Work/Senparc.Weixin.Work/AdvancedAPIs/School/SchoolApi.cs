/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SchoolApi.cs
    文件功能描述：企业微信家校沟通基础与学校部门接口


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增家校通知、外部联系人转换与学校部门管理接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.School
{
    /// <summary>企业微信家校沟通基础与学校部门接口。</summary>
    public static partial class SchoolApi
    {
        private const string GetSubscribeQrCodePath = "/cgi-bin/externalcontact/get_subscribe_qr_code";
        private const string SetSubscribeModePath = "/cgi-bin/externalcontact/set_subscribe_mode";
        private const string GetSubscribeModePath = "/cgi-bin/externalcontact/get_subscribe_mode";
        private const string SendNotificationPath = "/cgi-bin/externalcontact/message/send";
        private const string ConvertToOpenIdPath = "/cgi-bin/externalcontact/convert_to_openid";
        private const string CreateDepartmentPath = "/cgi-bin/school/department/create";
        private const string UpdateDepartmentPath = "/cgi-bin/school/department/update";
        private const string DeleteDepartmentPath = "/cgi-bin/school/department/delete";
        private const string GetDepartmentListPath = "/cgi-bin/school/department/list";
        private const string SetUpgradeInfoPath = "/cgi-bin/school/set_upgrade_info";

        /// <summary>获取“学校通知”二维码。<see href="https://developer.work.weixin.qq.com/document/path/92320"/></summary>
        public static SchoolSubscribeQrCodeResult GetSubscribeQrCode(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => Get<SchoolSubscribeQrCodeResult>(accessTokenOrAppKey, GetSubscribeQrCodePath, null, timeOut);

        /// <summary>异步获取“学校通知”二维码。<see href="https://developer.work.weixin.qq.com/document/path/92320"/></summary>
        public static Task<SchoolSubscribeQrCodeResult> GetSubscribeQrCodeAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => GetAsync<SchoolSubscribeQrCodeResult>(accessTokenOrAppKey, GetSubscribeQrCodePath, null, timeOut);

        /// <summary>设置家长关注“学校通知”的模式。<see href="https://developer.work.weixin.qq.com/document/path/92318"/></summary>
        public static WorkJsonResult SetSubscribeMode(string accessTokenOrAppKey,
            SchoolSubscribeModeRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SetSubscribeModePath, request, timeOut);

        /// <summary>异步设置家长关注“学校通知”的模式。<see href="https://developer.work.weixin.qq.com/document/path/92318"/></summary>
        public static Task<WorkJsonResult> SetSubscribeModeAsync(string accessTokenOrAppKey,
            SchoolSubscribeModeRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SetSubscribeModePath, request, timeOut);

        /// <summary>获取家长关注“学校通知”的模式。<see href="https://developer.work.weixin.qq.com/document/path/92318"/></summary>
        public static SchoolSubscribeModeResult GetSubscribeMode(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => Get<SchoolSubscribeModeResult>(accessTokenOrAppKey, GetSubscribeModePath, null, timeOut);

        /// <summary>异步获取家长关注“学校通知”的模式。<see href="https://developer.work.weixin.qq.com/document/path/92318"/></summary>
        public static Task<SchoolSubscribeModeResult> GetSubscribeModeAsync(string accessTokenOrAppKey,
            int timeOut = Config.TIME_OUT)
            => GetAsync<SchoolSubscribeModeResult>(accessTokenOrAppKey, GetSubscribeModePath, null, timeOut);

        /// <summary>发送“学校通知”。<see href="https://developer.work.weixin.qq.com/document/path/92321"/></summary>
        public static SchoolNotificationResult SendNotification(string accessTokenOrAppKey,
            SchoolNotificationRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolNotificationResult>(accessTokenOrAppKey, SendNotificationPath, request, timeOut);

        /// <summary>异步发送“学校通知”。<see href="https://developer.work.weixin.qq.com/document/path/92321"/></summary>
        public static Task<SchoolNotificationResult> SendNotificationAsync(string accessTokenOrAppKey,
            SchoolNotificationRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolNotificationResult>(accessTokenOrAppKey, SendNotificationPath, request, timeOut);

        /// <summary>将微信外部联系人的 external_userid 转为微信 OpenId。<see href="https://developer.work.weixin.qq.com/document/path/92323"/></summary>
        public static SchoolConvertToOpenIdResult ConvertToOpenId(string accessTokenOrAppKey,
            SchoolConvertToOpenIdRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolConvertToOpenIdResult>(accessTokenOrAppKey, ConvertToOpenIdPath, request, timeOut);

        /// <summary>异步将微信外部联系人的 external_userid 转为微信 OpenId。<see href="https://developer.work.weixin.qq.com/document/path/92323"/></summary>
        public static Task<SchoolConvertToOpenIdResult> ConvertToOpenIdAsync(string accessTokenOrAppKey,
            SchoolConvertToOpenIdRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolConvertToOpenIdResult>(accessTokenOrAppKey, ConvertToOpenIdPath, request, timeOut);

        /// <summary>创建学校部门。<see href="https://developer.work.weixin.qq.com/document/path/92340"/></summary>
        public static SchoolDepartmentCreateResult CreateDepartment(string accessTokenOrAppKey,
            SchoolDepartmentCreateRequest request, int timeOut = Config.TIME_OUT)
            => Post<SchoolDepartmentCreateResult>(accessTokenOrAppKey, CreateDepartmentPath, request, timeOut);

        /// <summary>异步创建学校部门。<see href="https://developer.work.weixin.qq.com/document/path/92340"/></summary>
        public static Task<SchoolDepartmentCreateResult> CreateDepartmentAsync(string accessTokenOrAppKey,
            SchoolDepartmentCreateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SchoolDepartmentCreateResult>(accessTokenOrAppKey, CreateDepartmentPath, request, timeOut);

        /// <summary>更新学校部门。<see href="https://developer.work.weixin.qq.com/document/path/92341"/></summary>
        public static WorkJsonResult UpdateDepartment(string accessTokenOrAppKey,
            SchoolDepartmentUpdateRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, UpdateDepartmentPath, request, timeOut);

        /// <summary>异步更新学校部门。<see href="https://developer.work.weixin.qq.com/document/path/92341"/></summary>
        public static Task<WorkJsonResult> UpdateDepartmentAsync(string accessTokenOrAppKey,
            SchoolDepartmentUpdateRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, UpdateDepartmentPath, request, timeOut);

        /// <summary>删除学校部门。<see href="https://developer.work.weixin.qq.com/document/path/92342"/></summary>
        public static WorkJsonResult DeleteDepartment(string accessTokenOrAppKey, long departmentId,
            int timeOut = Config.TIME_OUT)
            => Get<WorkJsonResult>(accessTokenOrAppKey, DeleteDepartmentPath, "id=" + departmentId, timeOut);

        /// <summary>异步删除学校部门。<see href="https://developer.work.weixin.qq.com/document/path/92342"/></summary>
        public static Task<WorkJsonResult> DeleteDepartmentAsync(string accessTokenOrAppKey, long departmentId,
            int timeOut = Config.TIME_OUT)
            => GetAsync<WorkJsonResult>(accessTokenOrAppKey, DeleteDepartmentPath, "id=" + departmentId, timeOut);

        /// <summary>获取学校部门列表；不指定上级部门时获取根部门。<see href="https://developer.work.weixin.qq.com/document/path/92343"/></summary>
        public static SchoolDepartmentListResult GetDepartmentList(string accessTokenOrAppKey,
            long? parentDepartmentId = null, int timeOut = Config.TIME_OUT)
            => Get<SchoolDepartmentListResult>(accessTokenOrAppKey, GetDepartmentListPath,
                parentDepartmentId.HasValue ? "id=" + parentDepartmentId.Value : null, timeOut);

        /// <summary>异步获取学校部门列表；不指定上级部门时获取根部门。<see href="https://developer.work.weixin.qq.com/document/path/92343"/></summary>
        public static Task<SchoolDepartmentListResult> GetDepartmentListAsync(string accessTokenOrAppKey,
            long? parentDepartmentId = null, int timeOut = Config.TIME_OUT)
            => GetAsync<SchoolDepartmentListResult>(accessTokenOrAppKey, GetDepartmentListPath,
                parentDepartmentId.HasValue ? "id=" + parentDepartmentId.Value : null, timeOut);

        /// <summary>设置学校自动升年级配置。<see href="https://developer.work.weixin.qq.com/document/path/92949"/></summary>
        public static WorkJsonResult SetUpgradeInfo(string accessTokenOrAppKey,
            SchoolUpgradeInfoRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(accessTokenOrAppKey, SetUpgradeInfoPath, request, timeOut);

        /// <summary>异步设置学校自动升年级配置。<see href="https://developer.work.weixin.qq.com/document/path/92949"/></summary>
        public static Task<WorkJsonResult> SetUpgradeInfoAsync(string accessTokenOrAppKey,
            SchoolUpgradeInfoRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(accessTokenOrAppKey, SetUpgradeInfoPath, request, timeOut);

        private static T Get<T>(string accessTokenOrAppKey, string path, string query, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                BuildGetUrl(path, query), null, CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);

        private static Task<T> GetAsync<T>(string accessTokenOrAppKey, string path, string query, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                BuildGetUrl(path, query), null, CommonJsonSendType.GET, timeOut), accessTokenOrAppKey);

        private static T Post<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        private static Task<T> PostAsync<T>(string accessTokenOrAppKey, string path, object request, int timeOut)
            where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut),
                accessTokenOrAppKey);

        private static string BuildGetUrl(string path, string query)
            => Config.ApiWorkHost + path + "?access_token={0}" +
               (string.IsNullOrEmpty(query) ? string.Empty : "&" + query);
    }
}
