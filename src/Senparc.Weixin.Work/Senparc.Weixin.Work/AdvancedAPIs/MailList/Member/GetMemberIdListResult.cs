/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetMemberIdListResult.cs
    文件功能描述：通讯录同步接口
    
    
    创建标识：IcedMango - 20220919
     
    修改标识：IcedMango - 20221106
    修改描述：v3.15.10 增加企业微信获取成员ID列表的接口

----------------------------------------------------------------*/



using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.MailList.Member;

/// <summary>
/// 获取成员ID列表返回数据
/// </summary>
public class GetMemberIdListResult : WorkJsonResult
{
    /// <summary>
    /// 分页游标，下次请求时填写以获取之后分页的记录。如果该字段返回空则表示已没有更多数据
    /// </summary>
    public string next_cursor { get; set; }
    
    /// <summary>
    /// 用户-部门关系列表
    /// </summary>
    public List<DeptUser> dept_user { get; set; }
}

/// <summary>
/// 用户-部门关系列表
/// </summary>
public class DeptUser
{
    /// <summary>
    /// 用户userid，当用户在多个部门下时会有多条记录
    /// </summary>
    public string userid { get; set; }
    
    /// <summary>
    /// 用户所属部门
    /// </summary>
    public int department { get; set; }
}