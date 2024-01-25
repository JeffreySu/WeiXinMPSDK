using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadVpFileJsonResult : WxJsonResult
    {
        /// <summary>
        /// 返回文件id
        /// </summary>
        public string file_id { get; set; }
    }
}
