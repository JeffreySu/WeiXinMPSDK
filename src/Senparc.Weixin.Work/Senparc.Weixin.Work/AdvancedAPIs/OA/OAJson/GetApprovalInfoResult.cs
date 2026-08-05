/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：GetApprovalInfoResult.cs
    文件功能描述：批量获取审批单号返回结果


    创建标识：Senparc - 20230224

    修改标识：Senparc - 20260805
    修改描述：v3.32.2 修复企业微信审批分页字段并兼容新版游标

----------------------------------------------------------------*/


using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 
    /// </summary>
    public class GetApprovalInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> sp_no_list { get; set; }

        /// <summary>
        /// 新版分页查询游标；返回结果没有该字段时表示审批单已经拉取完。
        /// </summary>
        public string new_next_cursor { get; set; }

        /// <summary>
        /// 旧版分页查询游标，兼容官方待废弃的旧协议字段。
        /// </summary>
        public int? next_cursor { get; set; }
    }
}
