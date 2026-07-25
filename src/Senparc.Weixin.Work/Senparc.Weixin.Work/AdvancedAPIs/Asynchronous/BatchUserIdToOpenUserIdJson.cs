/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：BatchUserIdToOpenUserIdJson.cs
    文件功能描述：企业微信批量成员 ID 转 OpenUserId 强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加批量成员 ID 转 OpenUserId 请求与结果模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Asynchronous
{
    /// <summary>
    /// 批量成员 ID 转 OpenUserId 请求。
    /// <para><see href="https://developer.work.weixin.qq.com/document/path/95435">企业微信官方文档</see></para>
    /// </summary>
    public class BatchUserIdToOpenUserIdRequest
    {
        /// <summary>
        /// 待转换的企业内部成员 ID 列表。
        /// </summary>
        public string[] userid_list { get; set; }
    }

    /// <summary>
    /// 单个成员 ID 与 OpenUserId 的对应关系。
    /// </summary>
    public class BatchUserIdToOpenUserIdItem
    {
        /// <summary>
        /// 企业内部成员 ID。
        /// </summary>
        public string userid { get; set; }

        /// <summary>
        /// 成员在服务商范围内的 OpenUserId。
        /// </summary>
        public string open_userid { get; set; }
    }

    /// <summary>
    /// 批量成员 ID 转 OpenUserId 结果。
    /// </summary>
    public class BatchUserIdToOpenUserIdResult : WorkJsonResult
    {
        /// <summary>
        /// 成功转换的成员对应关系。
        /// </summary>
        public BatchUserIdToOpenUserIdItem[] open_userid_list { get; set; }

        /// <summary>
        /// 无法转换的企业内部成员 ID 列表。
        /// </summary>
        public string[] invalid_userid_list { get; set; }
    }
}
