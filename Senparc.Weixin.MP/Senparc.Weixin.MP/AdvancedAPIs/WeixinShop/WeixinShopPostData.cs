using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 修改商品信息
    /// </summary>
    public class ReviseProductData : ProductData
    {
        public string product_id { get; set; }
    }
}
