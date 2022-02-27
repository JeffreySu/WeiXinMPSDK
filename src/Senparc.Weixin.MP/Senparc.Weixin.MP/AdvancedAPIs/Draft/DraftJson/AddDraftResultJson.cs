using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Draft.DraftJson
{
    /// <summary>
    /// 创建草稿的返回
    /// </summary>
    public class AddDraftResultJson  : WxJsonResult
    {
        /// <summary>
        /// 上传后的获取标志，长度不固定，但不会超过 128 字符
        /// </summary>
        public string media_id { get; set; }
    }
}