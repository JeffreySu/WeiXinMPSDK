/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：RobotUserIdConversionJson.cs
    文件功能描述：企业微信成员与智能机器人 OpenUserId 批量转换强类型模型


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加自建应用与第三方应用机器人账号转换模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Asynchronous
{
    /// <summary>
    /// 企业智能机器人 OpenUserId 批量转换为企业成员 ID 的请求。
    /// </summary>
    public class BatchOpenUserIdToUserIdRequest
    {
        /// <summary>
        /// 企业智能机器人返回的密文 OpenUserId 列表，最多 1000 个。
        /// </summary>
        public string[] open_userid_list { get; set; }
    }

    /// <summary>
    /// 企业智能机器人 OpenUserId 与企业成员 ID 的对应关系。
    /// </summary>
    public class BatchOpenUserIdToUserIdItem
    {
        /// <summary>
        /// 企业智能机器人返回的密文 OpenUserId。
        /// </summary>
        public string open_userid { get; set; }

        /// <summary>
        /// 转换后的企业内部成员 ID。
        /// </summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 企业智能机器人 OpenUserId 批量转换为企业成员 ID 的结果。
    /// </summary>
    public class BatchOpenUserIdToUserIdResult : WorkJsonResult
    {
        /// <summary>
        /// 成功转换的 OpenUserId 与企业成员 ID 对应关系。
        /// </summary>
        public BatchOpenUserIdToUserIdItem[] userid_list { get; set; }

        /// <summary>
        /// 无法转换的 OpenUserId 列表；传入明文成员 ID 时也会在此返回。
        /// </summary>
        public string[] invalid_open_userid_list { get; set; }
    }

    /// <summary>
    /// 服务商批量转换企业智能机器人加密成员 ID 的请求。
    /// </summary>
    public class ServiceBatchUserIdToOpenUserIdRequest
    {
        /// <summary>
        /// 企业智能机器人范围内的加密成员 ID 列表，最多 1000 个。
        /// </summary>
        public string[] open_userid_list { get; set; }

        /// <summary>
        /// 产生这些加密成员 ID 的企业智能机器人 ID。
        /// </summary>
        public string source_botid { get; set; }
    }

    /// <summary>
    /// 企业智能机器人加密成员 ID 与服务商 OpenUserId 的对应关系。
    /// </summary>
    public class ServiceBatchUserIdToOpenUserIdItem
    {
        /// <summary>
        /// 企业智能机器人范围内的加密成员 ID；字段名遵循官方协议。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 转换后的服务商范围 OpenUserId。
        /// </summary>
        public string open_userid { get; set; }
    }

    /// <summary>
    /// 服务商批量转换企业智能机器人加密成员 ID 的结果。
    /// </summary>
    public class ServiceBatchUserIdToOpenUserIdResult : WorkJsonResult
    {
        /// <summary>
        /// 成功转换的企业侧成员 ID 与服务商 OpenUserId 对应关系。
        /// </summary>
        public ServiceBatchUserIdToOpenUserIdItem[] items { get; set; }

        /// <summary>
        /// 来源企业智能机器人所在企业的 OpenCorpId。
        /// </summary>
        public string open_corpid { get; set; }

        /// <summary>
        /// 无法转换的企业侧加密成员 ID 列表；传入明文成员 ID 时也会在此返回。
        /// </summary>
        public string[] invalid_open_userid_list { get; set; }
    }
}
