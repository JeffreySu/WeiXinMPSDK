using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    public class OperateProductResult : WxJsonResult
    {
        /// <summary>
        /// 转译后的商品 id,将直接编入“获取商品二维码接口”返回的二维码内容。
        /// </summary>
        public string pid { get; set; }
    }
}
