using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 上传媒体文件（如图片，凭证等）
    /// </summary>
    public class UploadVpFileRequestData
    {
        /// <summary>
        /// 经base64编码后的图片内容，使用这个字段最多只能传1m的图片，超过1m请使用img_url字段
        /// </summary>
        public string base64_img { get; set; }

        /// <summary>
        /// 图片url，需要能直接下载，不能是返回302等返回码的地址，最高允许传2m图片（优先使用img_url）
        /// </summary>
        public string img_url { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string file_name { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }
}
