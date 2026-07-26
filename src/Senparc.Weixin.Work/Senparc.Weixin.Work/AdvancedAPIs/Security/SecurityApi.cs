/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SecurityApi.cs
    文件功能描述：SecurityApi 微信接口封装


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Threading.Tasks;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Security
{
    /// <summary>企业微信安全管理接口。</summary>
    public static class SecurityApi
    {
        /// <summary>
        /// 获取文件操作记录。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static FileOperationRecordResult GetFileOperationRecords(string token, FileOperationRecordRequest request, int timeOut = Config.TIME_OUT)
            => Post<FileOperationRecordResult>(token, "/cgi-bin/security/get_file_oper_record", request, timeOut);
        /// <summary>
        /// 异步获取文件操作记录。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<FileOperationRecordResult> GetFileOperationRecordsAsync(string token, FileOperationRecordRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<FileOperationRecordResult>(token, "/cgi-bin/security/get_file_oper_record", request, timeOut);

        /// <summary>
        /// 导入可信设备。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ImportTrustDeviceResult ImportTrustDevices(string token, ImportTrustDeviceRequest request, int timeOut = Config.TIME_OUT)
            => Post<ImportTrustDeviceResult>(token, "/cgi-bin/security/trustdevice/import", request, timeOut);
        /// <summary>
        /// 异步导入可信设备。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ImportTrustDeviceResult> ImportTrustDevicesAsync(string token, ImportTrustDeviceRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ImportTrustDeviceResult>(token, "/cgi-bin/security/trustdevice/import", request, timeOut);

        /// <summary>
        /// 获取可信设备列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static TrustDeviceListResult GetTrustDeviceList(string token, ListTrustDeviceRequest request, int timeOut = Config.TIME_OUT)
            => Post<TrustDeviceListResult>(token, "/cgi-bin/security/trustdevice/list", request, timeOut);
        /// <summary>
        /// 异步获取可信设备列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<TrustDeviceListResult> GetTrustDeviceListAsync(string token, ListTrustDeviceRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<TrustDeviceListResult>(token, "/cgi-bin/security/trustdevice/list", request, timeOut);

        /// <summary>
        /// 获取成员的可信设备。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static TrustDeviceListResult GetTrustDevicesByUser(string token, GetTrustDeviceByUserRequest request, int timeOut = Config.TIME_OUT)
            => Post<TrustDeviceListResult>(token, "/cgi-bin/security/trustdevice/get_by_user", request, timeOut);
        /// <summary>
        /// 异步获取成员的可信设备。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<TrustDeviceListResult> GetTrustDevicesByUserAsync(string token, GetTrustDeviceByUserRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<TrustDeviceListResult>(token, "/cgi-bin/security/trustdevice/get_by_user", request, timeOut);

        /// <summary>
        /// 删除可信设备。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static WorkJsonResult DeleteTrustDevices(string token, DeleteTrustDeviceRequest request, int timeOut = Config.TIME_OUT)
            => Post<WorkJsonResult>(token, "/cgi-bin/security/trustdevice/delete", request, timeOut);
        /// <summary>
        /// 异步删除可信设备。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<WorkJsonResult> DeleteTrustDevicesAsync(string token, DeleteTrustDeviceRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<WorkJsonResult>(token, "/cgi-bin/security/trustdevice/delete", request, timeOut);

        /// <summary>
        /// 同意可信设备申请。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ReviewTrustDeviceResult ApproveTrustDevices(string token, ReviewTrustDeviceRequest request, int timeOut = Config.TIME_OUT)
            => Post<ReviewTrustDeviceResult>(token, "/cgi-bin/security/trustdevice/approve", request, timeOut);
        /// <summary>
        /// 异步同意可信设备申请。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ReviewTrustDeviceResult> ApproveTrustDevicesAsync(string token, ReviewTrustDeviceRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ReviewTrustDeviceResult>(token, "/cgi-bin/security/trustdevice/approve", request, timeOut);

        /// <summary>
        /// 拒绝可信设备申请。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ReviewTrustDeviceResult RejectTrustDevices(string token, ReviewTrustDeviceRequest request, int timeOut = Config.TIME_OUT)
            => Post<ReviewTrustDeviceResult>(token, "/cgi-bin/security/trustdevice/reject", request, timeOut);
        /// <summary>
        /// 异步拒绝可信设备申请。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ReviewTrustDeviceResult> RejectTrustDevicesAsync(string token, ReviewTrustDeviceRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ReviewTrustDeviceResult>(token, "/cgi-bin/security/trustdevice/reject", request, timeOut);

        /// <summary>
        /// 获取截屏或录屏记录。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ScreenOperationRecordResult GetScreenOperationRecords(string token, ScreenOperationRecordRequest request, int timeOut = Config.TIME_OUT)
            => Post<ScreenOperationRecordResult>(token, "/cgi-bin/security/get_screen_oper_record", request, timeOut);
        /// <summary>
        /// 异步获取截屏或录屏记录。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ScreenOperationRecordResult> GetScreenOperationRecordsAsync(string token, ScreenOperationRecordRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ScreenOperationRecordResult>(token, "/cgi-bin/security/get_screen_oper_record", request, timeOut);

        /// <summary>
        /// 分配高级功能账号。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static SubmitVipJobResult AssignVipUsers(string token, VipUserListRequest request, int timeOut = Config.TIME_OUT)
            => Post<SubmitVipJobResult>(token, "/cgi-bin/security/vip/submit_batch_add_job", request, timeOut);
        /// <summary>
        /// 异步分配高级功能账号。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<SubmitVipJobResult> AssignVipUsersAsync(string token, VipUserListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SubmitVipJobResult>(token, "/cgi-bin/security/vip/submit_batch_add_job", request, timeOut);

        /// <summary>
        /// 查询高级功能账号分配结果。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static VipJobResult GetAssignVipJobResult(string token, VipJobResultRequest request, int timeOut = Config.TIME_OUT)
            => Post<VipJobResult>(token, "/cgi-bin/security/vip/batch_add_job_result", request, timeOut);
        /// <summary>
        /// 异步查询高级功能账号分配结果。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<VipJobResult> GetAssignVipJobResultAsync(string token, VipJobResultRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<VipJobResult>(token, "/cgi-bin/security/vip/batch_add_job_result", request, timeOut);

        /// <summary>
        /// 取消高级功能账号。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static SubmitVipJobResult CancelVipUsers(string token, VipUserListRequest request, int timeOut = Config.TIME_OUT)
            => Post<SubmitVipJobResult>(token, "/cgi-bin/security/vip/submit_batch_del_job", request, timeOut);
        /// <summary>
        /// 异步取消高级功能账号。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<SubmitVipJobResult> CancelVipUsersAsync(string token, VipUserListRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<SubmitVipJobResult>(token, "/cgi-bin/security/vip/submit_batch_del_job", request, timeOut);

        /// <summary>
        /// 查询高级功能账号取消结果。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static VipJobResult GetCancelVipJobResult(string token, VipJobResultRequest request, int timeOut = Config.TIME_OUT)
            => Post<VipJobResult>(token, "/cgi-bin/security/vip/batch_del_job_result", request, timeOut);
        /// <summary>
        /// 异步查询高级功能账号取消结果。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<VipJobResult> GetCancelVipJobResultAsync(string token, VipJobResultRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<VipJobResult>(token, "/cgi-bin/security/vip/batch_del_job_result", request, timeOut);

        /// <summary>
        /// 获取高级功能账号列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static ListVipUserResult GetVipUserList(string token, ListVipUserRequest request, int timeOut = Config.TIME_OUT)
            => Post<ListVipUserResult>(token, "/cgi-bin/security/vip/list", request, timeOut);
        /// <summary>
        /// 异步获取高级功能账号列表。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<ListVipUserResult> GetVipUserListAsync(string token, ListVipUserRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<ListVipUserResult>(token, "/cgi-bin/security/vip/list", request, timeOut);

        /// <summary>
        /// 获取成员操作记录。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static OperationLogResult GetMemberOperationLogs(string token, OperationLogRequest request, int timeOut = Config.TIME_OUT)
            => Post<OperationLogResult>(token, "/cgi-bin/security/member_oper_log/list", request, timeOut);
        /// <summary>
        /// 异步获取成员操作记录。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<OperationLogResult> GetMemberOperationLogsAsync(string token, OperationLogRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<OperationLogResult>(token, "/cgi-bin/security/member_oper_log/list", request, timeOut);

        /// <summary>
        /// 获取管理员操作记录。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static OperationLogResult GetAdminOperationLogs(string token, OperationLogRequest request, int timeOut = Config.TIME_OUT)
            => Post<OperationLogResult>(token, "/cgi-bin/security/admin_oper_log/list", request, timeOut);
        /// <summary>
        /// 异步获取管理员操作记录。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="request">接口请求参数。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<OperationLogResult> GetAdminOperationLogsAsync(string token, OperationLogRequest request, int timeOut = Config.TIME_OUT)
            => PostAsync<OperationLogResult>(token, "/cgi-bin/security/admin_oper_log/list", request, timeOut);

        /// <summary>
        /// 获取微信回调服务器 IP。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static GetServerDomainIpResult GetServerDomainIp(string token, int timeOut = Config.TIME_OUT)
            => Post<GetServerDomainIpResult>(token, "/cgi-bin/security/get_server_domain_ip", new { }, timeOut);
        /// <summary>
        /// 异步获取微信回调服务器 IP。
        /// </summary>
        /// <param name="token">微信客服接口调用凭证。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）。</param>
        /// <returns>微信接口返回结果。</returns>
        public static Task<GetServerDomainIpResult> GetServerDomainIpAsync(string token, int timeOut = Config.TIME_OUT)
            => PostAsync<GetServerDomainIpResult>(token, "/cgi-bin/security/get_server_domain_ip", new { }, timeOut);

        private static T Post<T>(string token, string path, object request, int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApi(accessToken => CommonJsonSend.Send<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut), token);

        private static Task<T> PostAsync<T>(string token, string path, object request, int timeOut) where T : WorkJsonResult, new()
            => ApiHandlerWapper.TryCommonApiAsync(accessToken => CommonJsonSend.SendAsync<T>(accessToken,
                Config.ApiWorkHost + path + "?access_token={0}", request, CommonJsonSendType.POST, timeOut), token);
    }
}
