/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：GetApiDomainIpResult.cs
    文件功能描述：企业微信接口域名 IP 段结果


    创建标识：Senparc - 20260726

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 新增企业微信接口域名 IP 段结果模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.Entities
{
    /// <summary>获取企业微信接口域名 IP 段结果。</summary>
    public class GetApiDomainIpResult : WorkJsonResult
    {
        /// <summary>获取或设置企业微信 API 服务器 IP 段列表。</summary>
        public string[] ip_list { get; set; }
    }
}
