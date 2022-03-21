using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Express
{
    /// <summary>
    /// 
    /// </summary>
    public class ReOrderModel
    {
        /// <summary>
        /// 预下单接口返回的参数，配送公司可保证在一段时间内运费不变
        /// 非必填
        /// </summary>
        public string delivery_token { get; set; }
        /// <summary>
        /// 商家id， 由配送公司分配的appkey
        /// </summary>
        public string shopid { get; set; }
        /// <summary>
        /// 唯一标识订单的 ID，由商户生成, 不超过128字节
        /// </summary>
        public string shop_order_id { get; set; }
        /// <summary>
        /// 商家门店编号，在配送公司登记，美团、闪送必填
        /// </summary>
        public string shop_no { get; set; }
        /// <summary>
        /// 用配送公司提供的appSecret加密的校验串说明
        /// </summary>
        public string delivery_sign { get; set; }
        /// <summary>
        /// 配送公司ID
        /// </summary>
        public string delivery_id { get; set; }
        /// <summary>
        /// 下单用户的openid
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 发件人信息，闪送、顺丰同城急送必须填写，美团配送、达达，若传了shop_no的值可不填该字段
        /// </summary>
        public PreAddOrderSender sender { get; set; }
        /// <summary>
        /// 收件人信息
        /// </summary>
        public PreAddOrderReceiver receiver { get; set; }
        /// <summary>
        /// 货物信息
        /// </summary>
        public PreAddOrderCargo cargo { get; set; }
        /// <summary>
        /// 订单信息
        /// </summary>
        public PreAddOrderOrderInfo order_info { get; set; }
        /// <summary>
        /// 商品信息，会展示到物流通知消息中
        /// </summary>
        public PreAddOrderShop shop { get; set; }
        /// <summary>
        /// 子商户id，区分小程序内部多个子商户
        /// </summary>
        public string sub_biz_id { get; set; }
    }
}
