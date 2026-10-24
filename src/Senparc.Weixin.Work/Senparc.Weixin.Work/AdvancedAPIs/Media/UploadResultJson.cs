/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc
    
    文件名：UploadResultJson.cs
    文件功能描述：上传媒体文件返回结果
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口

    修改标识：Senparc - 20261024
    修改描述：兼容 System.Text.Json 媒体类型字符串响应，保持枚举数字序列化
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers.Serializers;
using System.Text.Json.Serialization;

namespace Senparc.Weixin.Work.AdvancedAPIs.Media
{
    /// <summary>
    /// 上传临时媒体文件返回结果
    /// </summary>
    public class UploadTemporaryResultJson : WorkJsonResult
    {
        [JsonConverter(typeof(JsonStringOrNumberEnumConverterFactory))]
        public UploadMediaFileType type { get; set; }
        public string media_id { get; set; }
        public long created_at { get; set; }
    }

    /// <summary>
    /// 上传永久素材返回结果
    /// </summary>
    public class UploadForeverResultJson : WorkJsonResult
    {
        public string media_id { get; set; }
    }
}
