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
