using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Media
{
    /// <summary>
    /// 上传提审素材
    /// </summary>
    public class UploadMediaResultJson : WxJsonResult
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 媒体id
        /// </summary>
        public string mediaid { get; set; }
    }
}
