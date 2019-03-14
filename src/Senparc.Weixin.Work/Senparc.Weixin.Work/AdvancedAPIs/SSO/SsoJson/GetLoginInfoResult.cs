/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetLoginInfoResult.cs
    文件功能描述：获取企业号管理员登录信息返回结果
    
    
    创建标识：Senparc - 20150325

    -----------------------------------
    
    修改标识：Senparc - 20170617
    修改描述：从QY移植，同步Work接口，从Senparc.Weixin.Work.AdvancedAPIs.LoginAuth命名空间下移植而来

----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.SSO
{
    /// <summary>
    /// 获取企业号管理员登录信息返回结果
    /// </summary>
    public class GetLoginInfoResult : WorkJsonResult
    {
        ///// <summary>
        ///// 是否系统管理员
        ///// </summary>
        //public bool is_sys { get; set; }
        ///// <summary>
        ///// 是否内部管理员
        ///// </summary>
        //public bool is_inner { get; set; }

        /// <summary>
        /// 登录用户的类型：1.创建者 2.内部系统管理员 3.外部系统管理员 4.分级管理员
        /// </summary>
        public int usertype { get; set; }

        /// <summary>
        /// 登录管理员的信息
        /// </summary>
        public LoginInfo_UserInfo user_info { get; set; }

        /// <summary>
        /// 授权方企业信息
        /// </summary>
        public LoginInfo_CorpInfo corp_info { get; set; }

        /// <summary>
        /// 该管理员在该提供商中能使用的应用列表
        /// </summary>
        public List<LoginInfo_AgentItem> agent { get; set; }
        /// <summary>
        /// 该管理员拥有的通讯录权限
        /// </summary>
        public LoginInfo_AuthInfo auth_info { get; set; }
    }

    public class LoginInfo_UserInfo
    {
        /// <summary>
        /// 管理员邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 该管理员的userid（仅为内部管理员时展示）
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 该管理员的名字（仅为内部管理员时展示）
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 该管理员的头像（仅为内部管理员时展示）
        /// </summary>
        public string avatar { get; set; }
        ///// <summary>
        ///// 该管理员的手机（仅为内部管理员时展示）
        ///// </summary>
        //public string mobile { get; set; }
    }

    public class LoginInfo_CorpInfo
    {
        /// <summary>
        /// 授权方企业号id
        /// </summary>
        public string corpid { get; set; }
        ///// <summary>
        ///// 授权方企业号名称
        ///// </summary>
        //public string corp_name { get; set; }
        ///// <summary>
        ///// 授权方企业号类型，认证号：verified, 注册号：unverified，体验号：test
        ///// </summary>
        //public string corp_type { get; set; }
        ///// <summary>
        ///// 授权方企业号圆形头像
        ///// </summary>
        //public string corp_round_logo_url { get; set; }
        ///// <summary>
        ///// 授权方企业号方形头像
        ///// </summary>
        //public string corp_square_logo_url { get; set; }
        ///// <summary>
        ///// 授权方企业号用户规模
        ///// </summary>
        //public int corp_user_max { get; set; }
        ///// <summary>
        ///// 授权方企业号应用规模
        ///// </summary>
        //public int corp_agent_max { get; set; }
    }

    public class LoginInfo_AgentItem
    {
        /// <summary>
        /// 应用id
        /// </summary>
        public int agentid { get; set; }
        /// <summary>
        /// 该管理员对应用的权限：1.管理权限，0.使用权限
        /// </summary>
        public int auth_type { get; set; }
    }

    public class LoginInfo_AuthInfo
    {
        public List<LoginInfo_AuthInfo_DepartmentItem> department { get; set; }
    }

    public class LoginInfo_AuthInfo_DepartmentItem
    {
        public string id { get; set; }
        public string writable { get; set; }
    }
}
