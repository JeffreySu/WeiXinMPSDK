/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：UploadMediaFileResult.cs
    文件功能描述：上传媒体文件返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150320
    修改描述：修改结果类型（有临时和永久之分）

    修改标识：Senparc - 20261024
    修改描述：统一兼容 System.Text.Json 媒体类型字符串响应，保持旧数字序列化
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers.Serializers;
using System.Text.Json.Serialization;

namespace Senparc.Weixin.MP.AdvancedAPIs.Media
{
    /// <summary>
    /// 上传临时媒体文件返回结果
    /// </summary>
    public class UploadTemporaryMediaResult : WxJsonResult
    {
        [JsonConverter(typeof(JsonStringOrNumberEnumConverter<UploadMediaFileType>))]
        public UploadMediaFileType type { get; set; }
        public string media_id { get; set; }
        /// <summary>
        /// 上传缩略图返回的meidia_id参数.
        /// </summary>
        public string thumb_media_id { get; set; }
        public long created_at { get; set; }
    }

    /// <summary>
    /// 上传永久媒体文件返回结果
    /// </summary>
    public class UploadForeverMediaResult : WxJsonResult
    {
        /// <summary>
        /// 新增的永久素材的media_id
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 新增的图片素材的图片URL（仅新增图片素材时会返回该字段）
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 上传图文消息内的图片获取URL返回结果
    /// </summary>
    public class UploadImgResult : WxJsonResult
    {
        public string url { get; set; }
    }
}
