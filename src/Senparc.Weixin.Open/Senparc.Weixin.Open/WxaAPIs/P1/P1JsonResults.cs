#region Apache License Version 2.0
/*----------------------------------------------------------------
Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.
Licensed under the Apache License, Version 2.0 (the "License").
----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：P1JsonResults.cs
    文件功能描述：P1JsonResults 强类型数据模型


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v4.24.4 补齐开放平台基础管理、流量主代运营和微信云托管接口

    修改标识：Senparc - 20260725
    修改描述：v4.24.4 增加开放平台账号绑定状态返回模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.P1
{
    /// <summary>
    /// FetchDataSetting 接口返回结果。
    /// </summary>
    public class FetchDataSettingJsonResult : WxJsonResult
    {
        public bool is_pre_fetch_open { get; set; }
        public int pre_fetch_type { get; set; }
        public string pre_fetch_url { get; set; }
        public string pre_env { get; set; }
        public string pre_function_name { get; set; }
        public bool is_period_fetch_open { get; set; }
        public int period_fetch_type { get; set; }
        public string period_fetch_url { get; set; }
        public string period_env { get; set; }
        public string period_function_name { get; set; }
    }

    /// <summary>
    /// 查询是否绑定开放平台账号接口返回结果。
    /// </summary>
    public class BindOpenAccountJsonResult : WxJsonResult
    {
        /// <summary>
        /// 是否已绑定开放平台账号。
        /// </summary>
        public bool have_open { get; set; }
    }

    /// <summary>
    /// SameEntity 接口返回结果。
    /// </summary>
    public class SameEntityJsonResult : WxJsonResult
    {
        public bool same_entity { get; set; }
    }

    /// <summary>
    /// CategoriesByType 接口返回结果。
    /// </summary>
    public class CategoriesByTypeJsonResult : WxJsonResult
    {
        public CategoriesByTypeData categories_list { get; set; }
    }

    /// <summary>
    /// CategoriesByType 数据。
    /// </summary>
    public class CategoriesByTypeData
    {
        public List<CategoryByTypeItem> categories { get; set; }
    }

    /// <summary>
    /// CategoryByType 数据项。
    /// </summary>
    public class CategoryByTypeItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int father { get; set; }
        public List<CategoryByTypeItem> children { get; set; }
        public int sensitive_type { get; set; }
        public object qualify { get; set; }
    }

    /// <summary>
    /// VisitStatus 接口返回结果。
    /// </summary>
    public class VisitStatusJsonResult : WxJsonResult
    {
        public int status { get; set; }
    }

    /// <summary>
    /// CodePrivacyInfo 接口返回结果。
    /// </summary>
    public class CodePrivacyInfoJsonResult : WxJsonResult
    {
        public List<string> without_auth_list { get; set; }
        public List<string> without_conf_list { get; set; }
    }

    /// <summary>
    /// SubmitAuthAndIcp 接口返回结果。
    /// </summary>
    public class SubmitAuthAndIcpJsonResult : WxJsonResult
    {
        public List<string> hints { get; set; }
        public string procedure_id { get; set; }
        public string pay_url { get; set; }
    }

    /// <summary>
    /// QueryAuthAndIcp 接口返回结果。
    /// </summary>
    public class QueryAuthAndIcpJsonResult : WxJsonResult
    {
        public int procedure_status { get; set; }
        public object orderid { get; set; }
        public string refill_reason { get; set; }
        public string fail_reason { get; set; }
        public object icp_audit { get; set; }
    }

    /// <summary>
    /// ApplyLiveInfo 接口返回结果。
    /// </summary>
    public class ApplyLiveInfoJsonResult : WxJsonResult
    {
        public string action { get; set; }
    }

    /// <summary>
    /// WeDataLoginConfig 接口返回结果。
    /// </summary>
    public class WeDataLoginConfigJsonResult : WxJsonResult
    {
        public string component_appid { get; set; }
        public string component_nickname { get; set; }
        public string recheck_url { get; set; }
        public List<WeDataAppInfo> appinfo { get; set; }
    }

    /// <summary>
    /// WeDataApp 信息。
    /// </summary>
    public class WeDataAppInfo
    {
        public string appid { get; set; }
        public string nickname { get; set; }
    }

    /// <summary>
    /// WeDataPermissionList 接口返回结果。
    /// </summary>
    public class WeDataPermissionListJsonResult : WxJsonResult
    {
        public List<WeDataPermission> perm { get; set; }
    }

    /// <summary>
    /// WeDataPermission 微信接口数据模型。
    /// </summary>
    public class WeDataPermission
    {
        public string perm_id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
    }

    /// <summary>
    /// WeDataBindList 接口返回结果。
    /// </summary>
    public class WeDataBindListJsonResult : WxJsonResult
    {
        public List<WeDataBindInfo> info { get; set; }
    }

    /// <summary>
    /// WeDataBind 信息。
    /// </summary>
    public class WeDataBindInfo
    {
        public string uid { get; set; }
        public long create_time { get; set; }
        public long update_time { get; set; }
        public string nickname { get; set; }
        public string head_url { get; set; }
        public int is_bind { get; set; }
        public List<WeDataPermission> perm { get; set; }
    }

    /// <summary>
    /// WeDataLogin 接口返回结果。
    /// </summary>
    public class WeDataLoginJsonResult : WxJsonResult
    {
        public WeDataBaseResponse base_resp { get; set; }
        public string redirect_url { get; set; }
        public long expire_at { get; set; }
    }

    /// <summary>
    /// WeDataBaseResponse 微信接口数据模型。
    /// </summary>
    public class WeDataBaseResponse
    {
        public int ret { get; set; }
        public string err_msg { get; set; }
    }
}
