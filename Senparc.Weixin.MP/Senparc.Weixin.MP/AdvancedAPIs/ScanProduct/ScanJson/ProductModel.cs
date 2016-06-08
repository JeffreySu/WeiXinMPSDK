using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class ProductModel 
    {
        /// <summary>
        /// 商品编码标准,只支持 ean13 和 qrcode 两种标准。
        /// </summary>
        public string keystandard { get; set; }

        /// <summary>
        /// 商品编码内容。
        /// 标准是 ean13,则直接填写商品条码,如“6901939621608”。
        /// 标准是 qrcode,二维码的内容可由商户自定义,建议使用商品条码,≤20 个字符,由大小字母、数字、下划线和连字符组成。
        /// </summary>
        /// <remarks>
        /// 注意:编码标准是 ean13 时,编码内容必须在商户的号段之下,否则会报错。
        /// </remarks>
        public string keystr { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public Product_Brand_Info brand_info { get; set; }
    }
}
