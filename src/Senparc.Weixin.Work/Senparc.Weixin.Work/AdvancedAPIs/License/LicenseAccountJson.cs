/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：LicenseAccountJson.cs
    文件功能描述：企业微信服务商许可账号管理强类型模型


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐许可账号管理强类型模型

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.License
{
    /// <summary>单账号激活请求。</summary>
    public class LicenseActivateAccountRequest
    {
        /// <summary>账号激活码。</summary>
        public string active_code { get; set; }

        /// <summary>激活码所属企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>待绑定激活的企业成员 UserId。</summary>
        public string userid { get; set; }
    }

    /// <summary>批量账号激活请求。</summary>
    public class LicenseBatchActivateAccountRequest
    {
        /// <summary>激活码所属企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>需要激活的账号列表，最多一千项。</summary>
        public List<LicenseActivationItem> active_list { get; set; }
    }

    /// <summary>批量激活中的单个账号。</summary>
    public class LicenseActivationItem
    {
        /// <summary>账号激活码。</summary>
        public string active_code { get; set; }

        /// <summary>待绑定激活的企业成员 UserId。</summary>
        public string userid { get; set; }
    }

    /// <summary>批量激活账号结果。</summary>
    public class LicenseBatchActivateAccountResult : WorkJsonResult
    {
        /// <summary>逐项激活结果。</summary>
        public List<LicenseActivationResultItem> active_result { get; set; }
    }

    /// <summary>单个账号激活结果。</summary>
    public class LicenseActivationResultItem : LicenseActivationItem
    {
        /// <summary>该成员的激活错误码，零表示成功。</summary>
        public int errcode { get; set; }
    }

    /// <summary>按账号类型激活请求。</summary>
    public class LicenseActivateAccountByTypeRequest
    {
        /// <summary>账号类型：1 基础账号，2 互通账号。</summary>
        public int type { get; set; }

        /// <summary>激活码所属企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>待绑定激活的企业成员 UserId。</summary>
        public string userid { get; set; }
    }

    /// <summary>获取单个激活码详情请求。</summary>
    public class LicenseGetActiveInfoByCodeRequest
    {
        /// <summary>激活码所属企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>待查询账号激活码。</summary>
        public string active_code { get; set; }
    }

    /// <summary>批量获取激活码详情请求。</summary>
    public class LicenseBatchGetActiveInfoByCodeRequest
    {
        /// <summary>激活码所属企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>待查询账号激活码列表。</summary>
        public List<string> active_code_list { get; set; }
    }

    /// <summary>获取单个激活码详情结果。</summary>
    public class LicenseGetActiveInfoResult : WorkJsonResult
    {
        /// <summary>激活码详情。</summary>
        public LicenseActiveInfo active_info { get; set; }
    }

    /// <summary>批量获取激活码详情结果。</summary>
    public class LicenseBatchGetActiveInfoResult : WorkJsonResult
    {
        /// <summary>有效激活码详情列表。</summary>
        public List<LicenseActiveInfo> active_info_list { get; set; }

        /// <summary>无效激活码列表。</summary>
        public List<string> invalid_active_code_list { get; set; }
    }

    /// <summary>许可激活码及其绑定状态详情。</summary>
    public class LicenseActiveInfo
    {
        /// <summary>账号激活码。</summary>
        public string active_code { get; set; }

        /// <summary>账号类型：1 基础账号，2 互通账号。</summary>
        public int type { get; set; }

        /// <summary>激活码状态。</summary>
        public int status { get; set; }

        /// <summary>已绑定的企业成员 UserId，未绑定时为空。</summary>
        public string userid { get; set; }

        /// <summary>激活码创建时间，Unix 时间戳。</summary>
        public long create_time { get; set; }

        /// <summary>账号激活时间，Unix 时间戳。</summary>
        public long? active_time { get; set; }

        /// <summary>账号过期时间，Unix 时间戳。</summary>
        public long? expire_time { get; set; }

        /// <summary>激活码合并流转信息。</summary>
        public LicenseMergeInfo merge_info { get; set; }

        /// <summary>激活码企业间分配信息。</summary>
        public LicenseShareInfo share_info { get; set; }
    }

    /// <summary>许可激活码合并信息。</summary>
    public class LicenseMergeInfo
    {
        /// <summary>合并后的目标激活码。</summary>
        public string to_active_code { get; set; }

        /// <summary>被合并的来源激活码。</summary>
        public string from_active_code { get; set; }
    }

    /// <summary>许可激活码企业间分配信息。</summary>
    public class LicenseShareInfo
    {
        /// <summary>接收激活码的企业 CorpId。</summary>
        public string to_corpid { get; set; }

        /// <summary>分配激活码的来源企业 CorpId。</summary>
        public string from_corpid { get; set; }
    }

    /// <summary>分页获取企业已激活账号请求。</summary>
    public class LicenseListActivatedAccountRequest
    {
        /// <summary>待查询企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>分页游标，首次请求不填。</summary>
        public string cursor { get; set; }

        /// <summary>每页数量。</summary>
        public int? limit { get; set; }
    }

    /// <summary>企业已激活账号分页结果。</summary>
    public class LicenseActivatedAccountListResult : WorkJsonResult
    {
        /// <summary>下一页游标。</summary>
        public string next_cursor { get; set; }

        /// <summary>是否还有更多数据，零表示没有，一表示有。</summary>
        public int has_more { get; set; }

        /// <summary>已激活账号列表。</summary>
        public List<LicenseAccountInfo> account_list { get; set; }
    }

    /// <summary>企业成员的许可账号信息。</summary>
    public class LicenseAccountInfo
    {
        /// <summary>企业成员 UserId。</summary>
        public string userid { get; set; }

        /// <summary>账号激活码；部分列表接口不返回。</summary>
        public string active_code { get; set; }

        /// <summary>账号类型：1 基础账号，2 互通账号。</summary>
        public int type { get; set; }

        /// <summary>账号激活时间，Unix 时间戳。</summary>
        public long active_time { get; set; }

        /// <summary>账号过期时间，Unix 时间戳。</summary>
        public long expire_time { get; set; }
    }

    /// <summary>获取成员激活详情请求。</summary>
    public class LicenseGetActiveInfoByUserRequest
    {
        /// <summary>成员所属企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>待查询企业成员 UserId。</summary>
        public string userid { get; set; }
    }

    /// <summary>获取成员激活详情结果。</summary>
    public class LicenseGetActiveInfoByUserResult : WorkJsonResult
    {
        /// <summary>成员整体激活状态。</summary>
        public int active_status { get; set; }

        /// <summary>成员已激活的基础账号和互通账号列表。</summary>
        public List<LicenseAccountInfo> active_info_list { get; set; }
    }

    /// <summary>批量账号继承请求。</summary>
    public class LicenseTransferRequest
    {
        /// <summary>成员所属企业 CorpId。</summary>
        public string corpid { get; set; }

        /// <summary>待继承的离职成员和接替成员列表。</summary>
        public List<LicenseTransferItem> transfer_list { get; set; }
    }

    /// <summary>单项许可账号继承关系。</summary>
    public class LicenseTransferItem
    {
        /// <summary>离职交接成员 UserId。</summary>
        public string handover_userid { get; set; }

        /// <summary>接替成员 UserId。</summary>
        public string takeover_userid { get; set; }
    }

    /// <summary>批量账号继承结果。</summary>
    public class LicenseTransferResult : WorkJsonResult
    {
        /// <summary>逐项继承结果。</summary>
        public List<LicenseTransferResultItem> transfer_result { get; set; }
    }

    /// <summary>单项许可账号继承结果。</summary>
    public class LicenseTransferResultItem : LicenseTransferItem
    {
        /// <summary>该继承关系的错误码，零表示成功。</summary>
        public int errcode { get; set; }
    }

    /// <summary>批量分配激活码请求。</summary>
    public class LicenseShareActiveCodeRequest
    {
        /// <summary>激活码来源企业 CorpId。</summary>
        public string from_corpid { get; set; }

        /// <summary>接收激活码的目标企业 CorpId。</summary>
        public string to_corpid { get; set; }

        /// <summary>待分配激活码列表。</summary>
        public List<LicenseActiveCodeItem> share_list { get; set; }

        /// <summary>企业关联类型。</summary>
        public int corp_link_type { get; set; }
    }

    /// <summary>单个待分配许可激活码。</summary>
    public class LicenseActiveCodeItem
    {
        /// <summary>账号激活码。</summary>
        public string active_code { get; set; }
    }

    /// <summary>批量分配激活码结果。</summary>
    public class LicenseShareActiveCodeResult : WorkJsonResult
    {
        /// <summary>逐项分配结果。</summary>
        public List<LicenseShareActiveCodeResultItem> share_result { get; set; }
    }

    /// <summary>单个激活码分配结果。</summary>
    public class LicenseShareActiveCodeResultItem : LicenseActiveCodeItem
    {
        /// <summary>该激活码的分配错误码，零表示成功。</summary>
        public int errcode { get; set; }

        /// <summary>该激活码的错误说明。</summary>
        public string errmsg { get; set; }
    }
}
