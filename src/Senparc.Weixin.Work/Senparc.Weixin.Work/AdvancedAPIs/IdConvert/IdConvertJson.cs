/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：IdConvertJson.cs
    文件功能描述：企业微信账号与群聊 ID 转换强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐账号、标签、客服账号及群聊 ID 转换模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.IdConvert
{
    /// <summary>
    /// UnionId 或 OpenId 转外部联系人账号请求。
    /// </summary>
    public class UnionIdToExternalUserIdRequest
    {
        /// <summary>获取或设置用户的微信 UnionId；与 OpenId 按业务场景选择填写。</summary>
        public string unionid { get; set; }

        /// <summary>获取或设置用户的微信 OpenId；与 UnionId 按业务场景选择填写。</summary>
        public string openid { get; set; }

        /// <summary>获取或设置主体类型，取值定义以企业微信官方文档为准。</summary>
        public int subject_type { get; set; }
    }

    /// <summary>
    /// UnionId 或 OpenId 转外部联系人账号结果。
    /// </summary>
    public class UnionIdToExternalUserIdResult : WorkJsonResult
    {
        /// <summary>获取或设置外部联系人账号。</summary>
        public string external_userid { get; set; }

        /// <summary>获取或设置暂未完成迁移时返回的 PendingId。</summary>
        public string pending_id { get; set; }
    }

    /// <summary>
    /// 批量将外部联系人账号转换为 PendingId 的请求。
    /// </summary>
    public class BatchExternalUserIdToPendingIdRequest
    {
        /// <summary>获取或设置外部联系人所在的群聊 ID。</summary>
        public string chat_id { get; set; }

        /// <summary>获取或设置待转换的外部联系人账号列表。</summary>
        public List<string> external_userid { get; set; }
    }

    /// <summary>
    /// 外部联系人账号与 PendingId 的转换项。
    /// </summary>
    public class ExternalUserIdPendingIdItem
    {
        /// <summary>获取或设置外部联系人账号。</summary>
        public string external_userid { get; set; }

        /// <summary>获取或设置迁移中的 PendingId。</summary>
        public string pending_id { get; set; }
    }

    /// <summary>
    /// 批量将外部联系人账号转换为 PendingId 的结果。
    /// </summary>
    public class BatchExternalUserIdToPendingIdResult : WorkJsonResult
    {
        /// <summary>获取或设置外部联系人账号与 PendingId 的对应关系。</summary>
        public List<ExternalUserIdPendingIdItem> result { get; set; }
    }

    /// <summary>
    /// 企业客户标签 ID 转换请求。
    /// </summary>
    public class ExternalTagIdConvertRequest
    {
        /// <summary>获取或设置待转换的企业客户标签 ID 列表。</summary>
        public List<string> external_tagid_list { get; set; }
    }

    /// <summary>
    /// 企业客户标签 ID 转换项。
    /// </summary>
    public class ExternalTagIdConvertItem
    {
        /// <summary>获取或设置企业范围内的客户标签 ID。</summary>
        public string external_tagid { get; set; }

        /// <summary>获取或设置服务商范围内的客户标签 ID。</summary>
        public string open_external_tagid { get; set; }
    }

    /// <summary>
    /// 企业客户标签 ID 转换结果。
    /// </summary>
    public class ExternalTagIdConvertResult : WorkJsonResult
    {
        /// <summary>获取或设置成功转换的标签 ID 对应关系。</summary>
        public List<ExternalTagIdConvertItem> items { get; set; }

        /// <summary>获取或设置无效的企业客户标签 ID 列表。</summary>
        public List<string> invalid_external_tagid_list { get; set; }
    }

    /// <summary>
    /// 企业客服账号 ID 转换请求。
    /// </summary>
    public class OpenKfIdConvertRequest
    {
        /// <summary>获取或设置待转换的企业客服账号 ID 列表。</summary>
        public List<string> open_kfid_list { get; set; }
    }

    /// <summary>
    /// 企业客服账号 ID 转换项。
    /// </summary>
    public class OpenKfIdConvertItem
    {
        /// <summary>获取或设置企业范围内的客服账号 ID。</summary>
        public string open_kfid { get; set; }

        /// <summary>获取或设置转换后的服务商范围客服账号 ID。</summary>
        public string new_open_kfid { get; set; }
    }

    /// <summary>
    /// 企业客服账号 ID 转换结果。
    /// </summary>
    public class OpenKfIdConvertResult : WorkJsonResult
    {
        /// <summary>获取或设置成功转换的客服账号 ID 对应关系。</summary>
        public List<OpenKfIdConvertItem> items { get; set; }

        /// <summary>获取或设置无效的企业客服账号 ID 列表。</summary>
        public List<string> invalid_open_kfid_list { get; set; }
    }

    /// <summary>
    /// 申请升级群聊 ID 的请求。
    /// </summary>
    public class ApplyToUpgradeChatIdRequest
    {
        /// <summary>获取或设置计划完成升级的 Unix 时间戳。</summary>
        public long upgrade_time { get; set; }
    }

    /// <summary>
    /// 申请升级群聊 ID 的结果。
    /// </summary>
    public class ApplyToUpgradeChatIdResult : WorkJsonResult
    {
    }

    /// <summary>
    /// 批量转换群聊 ID 的请求。
    /// </summary>
    public class ChatIdConvertRequest
    {
        /// <summary>获取或设置升级前的群聊 ID 列表。</summary>
        public List<string> chat_id_list { get; set; }
    }

    /// <summary>
    /// 群聊 ID 转换项。
    /// </summary>
    public class ChatIdConvertItem
    {
        /// <summary>获取或设置升级前的群聊 ID。</summary>
        public string chat_id { get; set; }

        /// <summary>获取或设置升级后的群聊 ID。</summary>
        public string new_chat_id { get; set; }
    }

    /// <summary>
    /// 批量转换群聊 ID 的结果。
    /// </summary>
    public class ChatIdConvertResult : WorkJsonResult
    {
        /// <summary>获取或设置新旧群聊 ID 的对应关系。</summary>
        public List<ChatIdConvertItem> items { get; set; }

        /// <summary>获取或设置无效的群聊 ID 列表。</summary>
        public List<string> invalid_chat_id_list { get; set; }
    }

    /// <summary>
    /// 使用第三方应用套件凭证为新企业升级群聊 ID 的请求。
    /// </summary>
    public class UpgradeChatIdForNewCorpRequest
    {
    }

    /// <summary>
    /// 使用第三方应用套件凭证为新企业升级群聊 ID 的结果。
    /// </summary>
    public class UpgradeChatIdForNewCorpResult : WorkJsonResult
    {
    }
}
