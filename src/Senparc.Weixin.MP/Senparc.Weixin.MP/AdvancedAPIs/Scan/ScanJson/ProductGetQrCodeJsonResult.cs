using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.Scan
{
    /// <summary>
    /// 获取商品二维码返回结果
    /// </summary>
    public class ProductGetQrCodeJsonResult : WxJsonResult
    {
        /// <summary>
        /// 商品二维码的图片链接，可直接下载到本地。
        /// </summary>
        public string pic_url { get; set; }

        /// <summary>
        /// 商品二维码的内容，以http://p.url.cn/为前缀，加上pid和extinfo三部分组成。
        /// </summary>
        public string qrcode_url { get; set; }
    }
}
