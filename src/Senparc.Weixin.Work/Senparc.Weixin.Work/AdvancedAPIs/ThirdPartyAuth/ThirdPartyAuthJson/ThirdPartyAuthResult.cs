/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ThirdPartyAuthResult.cs
    文件功能描述：第三方应用授权返回结果
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
    
    修改标识：Senparc - 2019620
    修改描述：v3.5.6 添加 GetPermanentCodeResult.auth_user_info 属性

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.ThirdPartyAuth
{
    /// <summary>
    /// 设置授权应用可见范围返回结果
    /// </summary>
    public class SetScopeResult : WorkJsonResult
    {
        public string[] invaliduser { get; set; }
        public int[] invalidparty { get; set; }
        public int[] invalidtag { get; set; }
    }

    /// <summary>
    /// 查询注册状态返回结果
    /// </summary>
    public class GetRegisterInfoResult : WorkJsonResult
    {
        public string corpid { get; set; }
        public Contact_Sync contact_sync { get; set; }
        public Auth_User_Info auth_user_info { get; set; }
        public string state { get; set; }
    }

    public class Contact_Sync
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }

    public class Auth_User_Info
    {
        public string userid { get; set; }
    }

    /// <summary>
    /// 获取注册码返回结果
    /// </summary>
    public class GetRegisterCodeResult : WorkJsonResult
    {
        public string register_code { get; set; }
        public int expires_in { get; set; }
    }

    /// <summary>
    /// 第三方使用user_ticket获取成员详情返回结果
    /// </summary>
    public class GetUserInfoByTicketResult : WorkJsonResult
    {
        public string corpid { get; set; }
        public string userid { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public string qr_code { get; set; }
    }


    /// <summary>
    /// 第三方根据code获取企业成员信息返回结果
    /// </summary>
    public class GetUserInfoResult : WorkJsonResult
    {
        public string CorpId { get; set; }
        public string OpenId { get; set; }
        public string UserId { get; set; }
        public string DeviceId { get; set; }
        public string user_ticket { get; set; }
        public int expires_in { get; set; }
    }


    /// <summary>
    /// 获取应用的管理员列表返回结果
    /// </summary>
    public class GetAdminListResult : WorkJsonResult
    {
        public AdminItem[] admin { get; set; }
    }

    public class AdminItem
    {
        public string userid { get; set; }
        public int auth_type { get; set; }
    }


    /// <summary>
    /// 获取应用套件令牌返回结果
    /// </summary>
    public class GetSuiteTokenResult : WorkJsonResult
    {
        /// <summary>
        /// 应用套件access_token
        /// </summary>
        public string suite_access_token { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public int expires_in { get; set; }
    }

    /// <summary>
    /// 获取预授权码返回结果
    /// </summary>
    public class GetPreAuthCodeResult : WorkJsonResult
    {
        /// <summary>
        /// 预授权码,最长为512字节
        /// </summary>
        public string pre_auth_code { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public int expires_in { get; set; }
    }

    public class GetPermanentCodeResult : WorkJsonResult
    {
        /// <summary>
        /// 授权方（企业）access_token
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 授权方（企业）access_token超时时间
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 企业号永久授权码
        /// </summary>
        public string permanent_code { get; set; }

        /// <summary>
        /// 授权方企业信息
        /// </summary>
        public ThirdParty_AuthCorpInfo auth_corp_info { get; set; }

        /// <summary>
        /// 授权信息
        /// </summary>
        public ThirdParty_AuthInfo auth_info { get; set; }

        /// <summary>
        /// 授权管理员的信息
        /// </summary>
        public GetPermanentCodeResult_AuthUserInfo auth_user_info { get; set; }

    }

    /// <summary>
    /// 授权管理员的信息
    /// </summary>
    public class GetPermanentCodeResult_AuthUserInfo
    {
        /// <summary>
        /// 授权管理员的userid，可能为空（内部管理员一定有，不可更改）
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 授权管理员的name，可能为空（内部管理员一定有，不可更改）
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 授权管理员的头像url
        /// </summary>
        public string avatar { get; set; }
    }


    /// <summary>
    /// ThirdParty_AuthCorpInfo【QY移植修改】
    /// </summary>
    public class ThirdParty_AuthCorpInfo
    {
        /// <summary>
        /// 授权方企业号id
        /// </summary>
        public string corpid { get; set; }

        /// <summary>
        /// 授权方企业号名称
        /// </summary>
        public string corp_name { get; set; }

        /// <summary>
        /// 授权方企业号类型，认证号：verified, 注册号：unverified，体验号：test
        /// </summary>
        public string corp_type { get; set; }

        ///// <summary>
        ///// 授权方企业号圆形头像
        ///// </summary>
        //public string corp_round_logo_url { get; set; }

        /// <summary>
        /// 授权方企业号方形头像
        /// </summary>
        public string corp_square_logo_url { get; set; }

        /// <summary>
        /// 授权方企业号用户规模
        /// </summary>
        public string corp_user_max { get; set; }

        /// <summary>
        /// 授权方企业号应用规模
        /// </summary>
        public string corp_agent_max { get; set; }

        /// <summary>
        /// 所绑定的企业微信主体名称
        /// </summary>
        public string corp_full_name { get; set; }

        /// <summary>
        /// verified_end_time（UNIX时间）
        /// </summary>
        public long verified_end_time { get; set; }

        /// <summary>
        /// 企业类型，1. 企业; 2. 政府以及事业单位; 3. 其他组织, 4.团队号
        /// </summary>
        public int subject_type { get; set; }

        /// <summary>
        /// 授权方企业微信二维码
        /// </summary>
        public string corp_wxqrcode { get; set; }
    }

    public class ThirdParty_AuthInfo
    {
        /// <summary>
        /// 授权的应用信息
        /// </summary>
        public List<ThirdParty_Agent> agent { get; set; }

        ///// <summary>
        ///// 授权的通讯录部门
        ///// </summary>
        //public List<ThirdParty_Department> department { get; set; }

        /// <summary>
        /// 	授权信息
        /// </summary>
        public ThirdParty_AuthUserInfo auth_user_info { get; set; }
    }

    public class ThirdParty_Agent
    {
        /// <summary>
        /// 授权方应用id
        /// </summary>
        public string agentid { get; set; }

        /// <summary>
        /// 授权方应用名字
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 授权方应用方形头像
        /// </summary>
        public string square_logo_url { get; set; }

        /// <summary>
        /// 授权方应用圆形头像
        /// </summary>
        public string round_logo_url { get; set; }

        /// <summary>
        /// 服务商套件中的对应应用id
        /// </summary>
        public string appid { get; set; }

        ///// <summary>
        ///// 授权方应用敏感权限组，目前仅有get_location，表示是否有权限设置应用获取地理位置的开关
        ///// </summary>
        //public string[] api_group { get; set; }

        /// <summary>
        /// 应用对应的权限
        /// </summary>
        public ThirdParty_Agent_Privilege privilege { get; set; }

    }

    /// <summary>
    /// ThirdParty_Agent_Privilege【QY移植新增】
    /// </summary>
    public class ThirdParty_Agent_Privilege
    {
        /// <summary>
        /// 权限等级。
        /// 1:通讯录基本信息只读
        /// 2:通讯录全部信息只读
        /// 3:通讯录全部信息读写
        /// 4:单个基本信息只读
        /// 5:通讯录全部信息只写
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 应用可见范围（部门）
        /// </summary>
        public int[] allow_party { get; set; }
        /// <summary>
        /// 应用可见范围（成员）
        /// </summary>
        public string[] allow_user { get; set; }
        /// <summary>
        /// 应用可见范围（标签）
        /// </summary>
        public int[] allow_tag { get; set; }
        /// <summary>
        /// 额外通讯录（部门）
        /// </summary>
        public int[] extra_party { get; set; }
        /// <summary>
        /// 额外通讯录（成员）
        /// </summary>
        public string[] extra_user { get; set; }
        /// <summary>
        /// 额外通讯录（标签）
        /// </summary>
        public int[] extra_tag { get; set; }
    }

    //public class ThirdParty_Department
    //{
    //    /// <summary>
    //    /// 部门id
    //    /// </summary>
    //    public string id { get; set; }

    //    /// <summary>
    //    /// 部门名称
    //    /// </summary>
    //    public string name { get; set; }

    //    /// <summary>
    //    /// 父部门id
    //    /// </summary>
    //    public string parentid { get; set; }

    //    /// <summary>
    //    /// 是否具有该部门的写权限
    //    /// </summary>
    //    public string writable { get; set; }
    //}

    public class ThirdParty_AuthUserInfo
    {
        /// <summary>
        /// 授权管理员的邮箱，可能为空（外部管理员一定有，不可更改）
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 授权管理员的手机号，可能为空（内部管理员一定有，可更改）
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 授权管理员的userid，可能为空（内部管理员一定有，不可更改）
        /// </summary>
        public string userid { get; set; }
    }

    /// <summary>
    /// 获取企业号的授权信息返回结果
    /// </summary>
    public class GetAuthInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 授权方企业信息
        /// </summary>
        public ThirdParty_AuthCorpInfo auth_corp_info { get; set; }

        /// <summary>
        /// 授权信息
        /// </summary>
        public ThirdParty_AuthInfo auth_info { get; set; }
    }

    public class GetAgentResult : WorkJsonResult
    {
        /// <summary>
        /// 授权方企业应用id
        /// </summary>
        public string agentid { get; set; }

        /// <summary>
        /// 授权方企业应用名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 授权方企业应用方形头像
        /// </summary>
        public string square_logo_url { get; set; }

        /// <summary>
        /// 授权方企业应用圆形头像
        /// </summary>
        public string round_logo_url { get; set; }

        /// <summary>
        /// 授权方企业应用详情
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 授权方企业应用可见范围（人员），其中包括userid和关注状态state
        /// </summary>
        public ThirdParty_AllowUserinfos allow_userinfos { get; set; }

        /// <summary>
        /// 授权方企业应用可见范围（部门）
        /// </summary>
        public ThirdParty_AllowPartys allow_partys { get; set; }

        /// <summary>
        /// 授权方企业应用可见范围（标签）
        /// </summary>
        public ThirdParty_AllowTags allow_tags { get; set; }

        /// <summary>
        /// 授权方企业应用是否被禁用
        /// </summary>
        public int close { get; set; }

        /// <summary>
        /// 授权方企业应用可信域名
        /// </summary>
        public string redirect_domain { get; set; }

        /// <summary>
        /// 授权方企业应用是否打开地理位置上报 0：不上报；1：进入会话上报；2：持续上报
        /// </summary>
        public int report_location_flag { get; set; }

        /// <summary>
        /// 是否接收用户变更通知。0：不接收；1：接收
        /// </summary>
        public int isreportuser { get; set; }
    }

    public class ThirdParty_AllowUserinfos
    {
        public List<ThirdParty_User> user { get; set; }
    }

    public class ThirdParty_User
    {
        public string userid { get; set; }
        public string status { get; set; }
    }

    public class ThirdParty_AllowPartys
    {
        public int[] partyid { get; set; }
    }

    public class ThirdParty_AllowTags
    {
        public int[] tagid { get; set; }
    }

    /// <summary>
    /// 获取企业号access_token返回结果
    /// </summary>
    public class GetCorpTokenResult : WorkJsonResult
    {
        /// <summary>
        /// 授权方（企业）access_token,最长为512字节
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 授权方（企业）access_token超时时间
        /// </summary>
        public int expires_in { get; set; }
    }
}


