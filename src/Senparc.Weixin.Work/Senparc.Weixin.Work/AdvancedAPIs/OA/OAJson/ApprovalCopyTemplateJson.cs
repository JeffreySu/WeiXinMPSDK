/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：ApprovalCopyTemplateJson.cs
    文件功能描述：企业微信复制第三方应用审批模板请求与结果模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 补齐复制第三方应用审批模板强类型模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 复制第三方应用审批模板请求。
    /// </summary>
    public class ApprovalCopyTemplateRequest
    {
        /// <summary>
        /// 服务商提供的审批模板 ID。
        /// </summary>
        public string open_template_id { get; set; }
    }

    /// <summary>
    /// 复制第三方应用审批模板结果。
    /// </summary>
    public class ApprovalCopyTemplateResult : WorkJsonResult
    {
        /// <summary>
        /// 复制到当前企业后生成的审批模板 ID。
        /// </summary>
        public string template_id { get; set; }
    }
}
