/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：MediaUrlUploadJson.cs
    文件功能描述：企业微信通过 URL 异步上传临时素材强类型模型


    创建标识：Senparc - 20260725

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加 URL 上传任务请求与结果模型

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.Media
{
    /// <summary>
    /// 通过 URL 异步上传临时素材请求。
    /// <para><see href="https://developer.work.weixin.qq.com/document/path/96219">企业微信官方文档</see></para>
    /// </summary>
    public class MediaUploadByUrlRequest
    {
        /// <summary>
        /// 上传场景值。
        /// </summary>
        public int scene { get; set; }

        /// <summary>
        /// 媒体文件类型。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 可由企业微信服务器访问的文件 URL。
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 包含扩展名的文件名。
        /// </summary>
        public string filename { get; set; }

        /// <summary>
        /// 可选的文件 MD5 值。
        /// </summary>
        public string md5 { get; set; }
    }

    /// <summary>
    /// URL 上传任务提交结果。
    /// </summary>
    public class MediaUploadByUrlResult : WorkJsonResult
    {
        /// <summary>
        /// 异步上传任务 ID。
        /// </summary>
        public string jobid { get; set; }
    }

    /// <summary>
    /// URL 上传任务结果查询请求。
    /// </summary>
    public class MediaGetUploadByUrlResultRequest
    {
        /// <summary>
        /// 待查询的异步上传任务 ID。
        /// </summary>
        public string jobid { get; set; }
    }

    /// <summary>
    /// URL 上传任务执行明细。
    /// </summary>
    public class MediaUploadByUrlTaskDetail
    {
        /// <summary>
        /// 任务执行返回码。
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 任务执行错误描述。
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 上传成功后返回的媒体文件 ID。
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 媒体文件上传时间戳。
        /// </summary>
        public long? created_at { get; set; }
    }

    /// <summary>
    /// URL 上传任务结果。
    /// </summary>
    public class MediaUploadByUrlTaskResult : WorkJsonResult
    {
        /// <summary>
        /// 异步上传任务状态。
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 任务执行明细。
        /// </summary>
        public MediaUploadByUrlTaskDetail detail { get; set; }
    }
}
