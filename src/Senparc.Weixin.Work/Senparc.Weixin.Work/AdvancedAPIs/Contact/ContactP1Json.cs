/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ContactP1Json.cs
    文件功能描述：ContactP1Json 强类型数据模型


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Contact
{
    /// <summary>
    /// ConvertTemporaryExternalUserId 接口请求参数。
    /// </summary>
    public class ConvertTemporaryExternalUserIdRequest
    {
        public int business_type { get; set; }
        public int user_type { get; set; }
        public IList<string> tmp_external_userid_list { get; set; }
    }

    /// <summary>
    /// ConvertTemporaryExternalUserId 接口返回结果。
    /// </summary>
    public class ConvertTemporaryExternalUserIdResult : WorkJsonResult
    {
        public IList<ConvertedExternalUserId> results { get; set; }
        public IList<string> invalid_tmp_external_userid_list { get; set; }
    }

    /// <summary>
    /// ConvertedExternalUserId 微信接口数据模型。
    /// </summary>
    public class ConvertedExternalUserId
    {
        public string tmp_external_userid { get; set; }
        public string external_userid { get; set; }
        public string corpid { get; set; }
        public string userid { get; set; }
    }

    /// <summary>
    /// ContactRuleRange 微信接口数据模型。
    /// </summary>
    public class ContactRuleRange
    {
        public IList<string> userid { get; set; }
        public IList<long> partyid { get; set; }
        public IList<long> tagid { get; set; }
    }

    /// <summary>
    /// ContactRule 微信接口数据模型。
    /// </summary>
    public class ContactRule
    {
        public long? rule_id { get; set; }
        public int rule_type { get; set; }
        public ContactRuleRange range { get; set; }
        public ContactRuleRange whitelist { get; set; }
        public ContactRuleRange exclude { get; set; }
        public bool? is_allowed_search { get; set; }
        public bool? is_allowed_conversation { get; set; }
    }

    /// <summary>
    /// ContactRule 接口请求参数。
    /// </summary>
    public class ContactRuleRequest
    {
        public IList<ContactRule> rules { get; set; }
    }

    /// <summary>
    /// DeleteContactRule 接口请求参数。
    /// </summary>
    public class DeleteContactRuleRequest
    {
        public IList<long> rule_ids { get; set; }
    }

    /// <summary>
    /// CreateContactRule 接口返回结果。
    /// </summary>
    public class CreateContactRuleResult : WorkJsonResult
    {
        public IList<long> rule_ids { get; set; }
    }

    /// <summary>
    /// GetContactRuleList 接口返回结果。
    /// </summary>
    public class GetContactRuleListResult : WorkJsonResult
    {
        public IList<ContactRule> rules { get; set; }
    }

    /// <summary>
    /// ExportContact 接口请求参数。
    /// </summary>
    public class ExportContactRequest
    {
        public string encoding_aeskey { get; set; }
        public int? block_size { get; set; }
    }

    /// <summary>
    /// ExportTagContact 接口请求参数。
    /// </summary>
    public class ExportTagContactRequest : ExportContactRequest
    {
        public long tagid { get; set; }
    }

    /// <summary>
    /// ExportContactJob 接口返回结果。
    /// </summary>
    public class ExportContactJobResult : WorkJsonResult
    {
        public string jobid { get; set; }
    }

    /// <summary>
    /// GetExportContact 接口返回结果。
    /// </summary>
    public class GetExportContactResult : WorkJsonResult
    {
        public int status { get; set; }
        public IList<ExportContactData> data_list { get; set; }
    }

    /// <summary>
    /// ExportContact 数据。
    /// </summary>
    public class ExportContactData
    {
        public string url { get; set; }
        public long size { get; set; }
        public string md5 { get; set; }
    }
}
