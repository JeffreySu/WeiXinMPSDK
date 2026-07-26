/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WeChatCustomerServiceQualificationJson.cs
    文件功能描述：企业微信客服企业资质查询强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加企业资质查询结果模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeChatCustomerService
{
    /// <summary>
    /// 企业微信客服资质查询结果。
    /// <para><see href="https://developer.work.weixin.qq.com/document/path/95153">企业微信官方文档</see></para>
    /// </summary>
    public class KfCorpQualificationResult : WorkJsonResult
    {
        /// <summary>
        /// 企业是否已绑定视频号。
        /// </summary>
        public bool wechat_channels_binding { get; set; }
    }
}
