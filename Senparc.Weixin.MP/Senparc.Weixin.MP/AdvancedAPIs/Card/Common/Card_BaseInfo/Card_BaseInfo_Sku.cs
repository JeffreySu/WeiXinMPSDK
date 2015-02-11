using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 商品信息
    /// </summary>
    public class Card_BaseInfo_Sku
    {
        /// <summary>
        /// 上架的数量。（不支持填写0或无限大）
        /// 必填
        /// </summary>
        public int quantity { get; set; }
    }
}
