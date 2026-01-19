/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：ApprovalCreateTemplateResult.cs
    文件功能描述：创建审批模板返回结果
    
    
    创建标识：Senparc - 20251223
    
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 创建审批模板返回结果
    /// </summary>
    public class ApprovalCreateTemplateResult : WorkJsonResult
    {
        /// <summary>
        /// 模板id
        /// </summary>
        public string template_id { get; set; }
    }
}

