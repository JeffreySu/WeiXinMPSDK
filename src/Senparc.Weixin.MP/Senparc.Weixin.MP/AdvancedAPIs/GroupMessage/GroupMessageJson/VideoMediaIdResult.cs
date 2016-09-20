/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：VideoMediaIdResult.cs
    文件功能描述：群发视频文件调用接口获取视频群发用的MediaId
    
    
    创建标识：Senparc - 20150623
----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    public class VideoMediaIdResult : WxJsonResult
    {
        /// <summary>
        /// mediaId
        /// </summary>
        public string media_id { get; set; }
        /// <summary>
        /// 类型（通常为video）
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 创建时间戳
        /// </summary>
        public long created_at { get; set; }
    }
}
