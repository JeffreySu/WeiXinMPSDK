using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class WeixinShopResult
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }

    /// <summary>
    /// 增加商品
    /// </summary>
    public class AddProductResult : WeixinShopResult
    {
        public string product_id { get; set; }
    }
}
