/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MassCallTicketJson.cs
    文件功能描述：企业微信接口高频调用凭据结果模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增接口高频调用凭据结果模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.IdConvert
{
    /// <summary>获取接口高频调用凭据结果。</summary>
    public class ApplyMassCallTicketResult : WorkJsonResult
    {
        /// <summary>获取或设置大批量初始化时使用的高频调用凭据。</summary>
        public string mass_call_ticket { get; set; }
    }
}
