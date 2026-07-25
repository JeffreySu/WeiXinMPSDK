/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SecurityJson.cs
    文件功能描述：SecurityJson 强类型数据模型


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Security
{
    /// <summary>
    /// SecurityOperation 微信接口数据模型。
    /// </summary>
    public class SecurityOperation
    {
        public int type { get; set; }
        public int? source { get; set; }
    }

    /// <summary>
    /// SecurityPaged 接口请求参数。
    /// </summary>
    public class SecurityPagedRequest
    {
        public long start_time { get; set; }
        public long end_time { get; set; }
        public IList<string> userid_list { get; set; }
        public string cursor { get; set; }
        public int? limit { get; set; }
    }

    /// <summary>
    /// FileOperationRecord 接口请求参数。
    /// </summary>
    public class FileOperationRecordRequest : SecurityPagedRequest
    {
        public SecurityOperation operation { get; set; }
    }

    /// <summary>
    /// FileOperationRecord 接口返回结果。
    /// </summary>
    public class FileOperationRecordResult : WorkJsonResult
    {
        public bool has_more { get; set; }
        public string next_cursor { get; set; }
        public IList<FileOperationRecord> record_list { get; set; }
    }

    /// <summary>
    /// FileOperationRecord 微信接口数据模型。
    /// </summary>
    public class FileOperationRecord
    {
        public long time { get; set; }
        public string userid { get; set; }
        public SecurityExternalUser external_user { get; set; }
        public SecurityOperation operation { get; set; }
        public string file_info { get; set; }
        public long? file_size { get; set; }
        public string file_md5 { get; set; }
        public string applicant_name { get; set; }
        public int? device_type { get; set; }
        public string device_code { get; set; }
    }

    /// <summary>
    /// SecurityExternalUser 微信接口数据模型。
    /// </summary>
    public class SecurityExternalUser
    {
        public int type { get; set; }
        public string name { get; set; }
        public string corp_name { get; set; }
    }

    /// <summary>
    /// TrustDevice 微信接口数据模型。
    /// </summary>
    public class TrustDevice
    {
        public string device_code { get; set; }
        public string system { get; set; }
        public IList<string> mac_addr { get; set; }
        public string motherboard_uuid { get; set; }
        public IList<string> harddisk_uuid { get; set; }
        public string domain { get; set; }
        public string pc_name { get; set; }
        public string seq_no { get; set; }
        public long? last_login_time { get; set; }
        public string last_login_userid { get; set; }
        public long? confirm_timestamp { get; set; }
        public string confirm_userid { get; set; }
        public string approved_userid { get; set; }
        public int? source { get; set; }
        public int? status { get; set; }
    }

    /// <summary>
    /// ImportTrustDevice 接口请求参数。
    /// </summary>
    public class ImportTrustDeviceRequest
    {
        public IList<TrustDevice> device_list { get; set; }
    }

    /// <summary>
    /// ImportTrustDevice 接口返回结果。
    /// </summary>
    public class ImportTrustDeviceResult : WorkJsonResult
    {
        public IList<ImportTrustDeviceItem> result { get; set; }
    }

    /// <summary>
    /// ImportTrustDevice 数据项。
    /// </summary>
    public class ImportTrustDeviceItem
    {
        public int device_index { get; set; }
        public string device_code { get; set; }
        public string duplicated_device_code { get; set; }
        public int status { get; set; }
    }

    /// <summary>
    /// ListTrustDevice 接口请求参数。
    /// </summary>
    public class ListTrustDeviceRequest
    {
        public string cursor { get; set; }
        public int? limit { get; set; }
        public int type { get; set; }
    }

    /// <summary>
    /// GetTrustDeviceByUser 接口请求参数。
    /// </summary>
    public class GetTrustDeviceByUserRequest
    {
        public string last_login_userid { get; set; }
        public int type { get; set; }
    }

    /// <summary>
    /// TrustDeviceList 接口返回结果。
    /// </summary>
    public class TrustDeviceListResult : WorkJsonResult
    {
        public IList<TrustDevice> device_list { get; set; }
        public string next_cursor { get; set; }
    }

    /// <summary>
    /// DeleteTrustDevice 接口请求参数。
    /// </summary>
    public class DeleteTrustDeviceRequest
    {
        public int type { get; set; }
        public IList<string> device_code_list { get; set; }
    }

    /// <summary>
    /// ReviewTrustDevice 接口请求参数。
    /// </summary>
    public class ReviewTrustDeviceRequest
    {
        public IList<string> device_code_list { get; set; }
    }

    /// <summary>
    /// ReviewTrustDevice 接口返回结果。
    /// </summary>
    public class ReviewTrustDeviceResult : WorkJsonResult
    {
        public IList<string> success_list { get; set; }
        public IList<string> fail_list { get; set; }
    }

    /// <summary>
    /// ScreenOperationRecord 接口请求参数。
    /// </summary>
    public class ScreenOperationRecordRequest : SecurityPagedRequest
    {
        public IList<long> department_id_list { get; set; }
        public int? screen_shot_type { get; set; }
    }

    /// <summary>
    /// ScreenOperationRecord 接口返回结果。
    /// </summary>
    public class ScreenOperationRecordResult : WorkJsonResult
    {
        public bool has_more { get; set; }
        public string next_cursor { get; set; }
        public IList<ScreenOperationRecord> record_list { get; set; }
    }

    /// <summary>
    /// ScreenOperationRecord 微信接口数据模型。
    /// </summary>
    public class ScreenOperationRecord
    {
        public long time { get; set; }
        public string userid { get; set; }
        public long department_id { get; set; }
        public int screen_shot_type { get; set; }
        public string screen_shot_content { get; set; }
        public string system { get; set; }
    }

    /// <summary>
    /// VipUserList 接口请求参数。
    /// </summary>
    public class VipUserListRequest
    {
        public IList<string> userid_list { get; set; }
    }

    /// <summary>
    /// SubmitVipJob 接口返回结果。
    /// </summary>
    public class SubmitVipJobResult : WorkJsonResult
    {
        public string jobid { get; set; }
        public IList<string> invalid_userid_list { get; set; }
    }

    /// <summary>
    /// VipJobResult 接口请求参数。
    /// </summary>
    public class VipJobResultRequest
    {
        public string jobid { get; set; }
    }

    /// <summary>
    /// VipJob 接口返回结果。
    /// </summary>
    public class VipJobResult : WorkJsonResult
    {
        public VipJobResultDetail job_result { get; set; }
    }

    /// <summary>
    /// VipJobResultDetail 微信接口数据模型。
    /// </summary>
    public class VipJobResultDetail
    {
        public IList<string> succ_userid_list { get; set; }
        public IList<string> fail_userid_list { get; set; }
    }

    /// <summary>
    /// ListVipUser 接口请求参数。
    /// </summary>
    public class ListVipUserRequest
    {
        public string cursor { get; set; }
        public int? limit { get; set; }
    }

    /// <summary>
    /// ListVipUser 接口返回结果。
    /// </summary>
    public class ListVipUserResult : WorkJsonResult
    {
        public bool has_more { get; set; }
        public string next_cursor { get; set; }
        public IList<string> userid_list { get; set; }
    }

    /// <summary>
    /// OperationLog 接口请求参数。
    /// </summary>
    public class OperationLogRequest
    {
        public long start_time { get; set; }
        public long end_time { get; set; }
        public int? oper_type { get; set; }
        public string userid { get; set; }
        public string cursor { get; set; }
        public int? limit { get; set; }
    }

    /// <summary>
    /// OperationLog 接口返回结果。
    /// </summary>
    public class OperationLogResult : WorkJsonResult
    {
        public bool has_more { get; set; }
        public string next_cursor { get; set; }
        public IList<OperationLogRecord> record_list { get; set; }
    }

    /// <summary>
    /// OperationLogRecord 微信接口数据模型。
    /// </summary>
    public class OperationLogRecord
    {
        public long time { get; set; }
        public string userid { get; set; }
        public int oper_type { get; set; }
        public int? detail_type { get; set; }
        public string detail_info { get; set; }
        public string ip { get; set; }
    }

    /// <summary>
    /// GetServerDomainIp 接口返回结果。
    /// </summary>
    public class GetServerDomainIpResult : WorkJsonResult
    {
        public IList<ServerDomainItem> domain_list { get; set; }
        public IList<ServerIpItem> ip_list { get; set; }
    }

    /// <summary>
    /// ServerNetwork 数据项。
    /// </summary>
    public class ServerNetworkItem
    {
        public string protocol { get; set; }
        public IList<int> port { get; set; }
        public int is_necessary { get; set; }
        public string description { get; set; }
    }

    /// <summary>
    /// ServerDomain 数据项。
    /// </summary>
    public class ServerDomainItem : ServerNetworkItem
    {
        public string domain { get; set; }
        public string universal_domian { get; set; }
    }

    /// <summary>
    /// ServerIp 数据项。
    /// </summary>
    public class ServerIpItem : ServerNetworkItem
    {
        public string ip { get; set; }
    }
}
