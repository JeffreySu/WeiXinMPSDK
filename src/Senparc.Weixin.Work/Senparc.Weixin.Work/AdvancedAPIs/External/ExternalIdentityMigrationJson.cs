/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ExternalIdentityMigrationJson.cs
    文件功能描述：企业微信客户联系身份转换与迁移强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐客户联系身份转换、可见范围与迁移模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    /// 客户联系 UnionId 转 ExternalUserId 请求。
    /// </summary>
    public class ExternalContactUnionIdConvertRequest
    {
        /// <summary>获取或设置待转换的微信 UnionId。</summary>
        public string unionid { get; set; }
    }

    /// <summary>
    /// 客户联系 UnionId 转 ExternalUserId 结果。
    /// </summary>
    public class ExternalContactUnionIdConvertResult : WorkJsonResult
    {
        /// <summary>获取或设置客户联系 ExternalUserId。</summary>
        public string external_userid { get; set; }
    }

    /// <summary>
    /// 批量将手机号转换为 ExternalUserId 的请求。
    /// </summary>
    public class BatchMobileToExternalUserIdRequest
    {
        /// <summary>获取或设置待转换的手机号列表。</summary>
        public List<string> mobiles { get; set; }
    }

    /// <summary>
    /// 手机号转换 ExternalUserId 的逐项结果。
    /// </summary>
    public class MobileExternalUserIdConvertItem
    {
        /// <summary>获取或设置本项错误码。</summary>
        public int errcode { get; set; }

        /// <summary>获取或设置本项错误信息。</summary>
        public string errmsg { get; set; }

        /// <summary>获取或设置转换得到的 ExternalUserId。</summary>
        public string external_userid { get; set; }

        /// <summary>获取或设置导入家长时指定的业务关键字。</summary>
        public string foreign_key { get; set; }

        /// <summary>获取或设置手机号。</summary>
        public string mobile { get; set; }
    }

    /// <summary>
    /// 批量将手机号转换为 ExternalUserId 的结果。
    /// </summary>
    public class BatchMobileToExternalUserIdResult : WorkJsonResult
    {
        /// <summary>获取或设置转换成功的逐项结果。</summary>
        public List<MobileExternalUserIdConvertItem> success_list { get; set; }

        /// <summary>获取或设置转换失败的逐项结果。</summary>
        public List<MobileExternalUserIdConvertItem> fail_list { get; set; }
    }

    /// <summary>
    /// 代开发自建应用 ExternalUserId 转服务商范围 ExternalUserId 请求。
    /// </summary>
    public class ServiceExternalUserIdConvertRequest
    {
        /// <summary>获取或设置代开发自建应用的 ExternalUserId。</summary>
        public string external_userid { get; set; }
    }

    /// <summary>
    /// 代开发自建应用 ExternalUserId 转服务商范围 ExternalUserId 结果。
    /// </summary>
    public class ServiceExternalUserIdConvertResult : WorkJsonResult
    {
        /// <summary>获取或设置服务商范围的 ExternalUserId。</summary>
        public string external_userid { get; set; }
    }

    /// <summary>
    /// 获客助手应用可使用范围结果。
    /// </summary>
    public class CustomerAcquisitionAppPermitResult : WorkJsonResult
    {
        /// <summary>获取或设置可使用获客助手的成员 UserId 列表。</summary>
        public List<string> user_list { get; set; }

        /// <summary>获取或设置可使用获客助手的部门 ID 列表。</summary>
        public List<long> department_list { get; set; }

        /// <summary>获取或设置可使用获客助手的标签 ID 列表。</summary>
        public List<long> tag_list { get; set; }
    }

    /// <summary>
    /// 批量获取企业合并后新 ExternalUserId 的请求。
    /// </summary>
    public class NewExternalUserIdRequest
    {
        /// <summary>获取或设置企业合并前的 ExternalUserId 列表。</summary>
        public List<string> external_userid_list { get; set; }
    }

    /// <summary>
    /// 按客户群批量获取企业合并后新 ExternalUserId 的请求。
    /// </summary>
    public class NewGroupChatExternalUserIdRequest : NewExternalUserIdRequest
    {
        /// <summary>获取或设置客户群 ID。</summary>
        public string chat_id { get; set; }
    }

    /// <summary>
    /// 企业合并前后 ExternalUserId 的对应关系。
    /// </summary>
    public class NewExternalUserIdItem
    {
        /// <summary>获取或设置企业合并前的 ExternalUserId。</summary>
        public string external_userid { get; set; }

        /// <summary>获取或设置企业合并后的新 ExternalUserId。</summary>
        public string new_external_userid { get; set; }
    }

    /// <summary>
    /// 批量获取企业合并后新 ExternalUserId 的结果。
    /// </summary>
    public class NewExternalUserIdResult : WorkJsonResult
    {
        /// <summary>获取或设置新旧 ExternalUserId 的对应关系。</summary>
        public List<NewExternalUserIdItem> items { get; set; }
    }
}
