using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.AdvancedAPIs.WiFi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Sec
{
    public class GetOrderJsonResult : WxJsonResult
    {
        public OrderModel order { get; set; }
    }

    public class OrderModel
    {
        public string transaction_id { get; set; }

        public string merchant_id { get; set; }

        public string sub_merchant_id { get; set; }

        public string merchant_trade_no { get; set; }

        public string description { get; set; }

        public long paid_amount { get; set; }

        public string openid { get; set; }

        public long trade_create_time { get; set; }

        public long pay_time { get; set; }

        public int order_state { get; set; }

        public bool in_complaint { get; set; }

        public ShippingModel shipping { get; set; }

    }

    public class ShippingModel
    {
        public int delivery_mode { get; set; }

        public int logistics_type { get; set; }

        public bool finish_shipping { get; set; }

        public string goods_desc { get; set; }

        public int finish_shipping_count { get; set; }

        public List<Shipping_ShippingListModel> shipping_list { get; set; }
    }

    public class Shipping_ShippingListModel : ShippingListModel
    {
        public string goods_desc { get; set; }

        public long upload_time { get; set; }
    }
}
