using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Sec
{
    public class UploadCombinedShippingInfoModel
    {

        /// <summary>
        /// 订单，需要上传物流信息的订单
        /// </summary>
        public OrderKeyModel order_key { get; set; }

        /// <summary>
        /// 子单物流详情
        /// </summary>
        public List<SubOrdersModel> sub_orders { get; set; }

        /// <summary>
        /// 上传时间，用于标识请求的先后顺序 示例值: `2022-12-15T13:29:35.120+08:00`
        /// </summary>
        public string upload_time { get; set; }

        /// <summary>
        /// 支付者，支付者信息
        /// </summary>
        public PayerModel payer { get; set; }
    }

    /// <summary>
    /// 子单物流详情
    /// </summary>
    public class SubOrdersModel
    {
        /// <summary>
        /// 需要上传物流详情的子单订单，订单类型与合单订单保持一致
        /// </summary>
        public OrderKeyModel order_key { get; set; }
        /// <summary>
        /// 物流模式，发货方式枚举值：1、实体物流配送采用快递公司进行实体物流配送形式 2、同城配送 3、虚拟商品，虚拟商品，例如话费充值，点卡等，无实体配送形式 4、用户自提
        /// </summary>
        public int logistics_type { get; set; }

        /// <summary>
        /// 发货模式，发货模式枚举值：1、UNIFIED_DELIVERY（统一发货）2、SPLIT_DELIVERY（分拆发货） 示例值: UNIFIED_DELIVERY
        /// </summary>
        public int delivery_mode { get; set; }

        /// <summary>
        /// 分拆发货模式时必填，用于标识分拆发货模式下是否已全部发货完成，只有全部发货完成的情况下才会向用户推送发货完成通知。示例值: true/false
        /// </summary>
        public bool is_all_delivered { get; set; }

        /// <summary>
        /// 物流信息列表，发货物流单列表，支持统一发货（单个物流单）和分拆发货（多个物流单）两种模式，多重性: [1, 10]
        /// </summary>
        public List<ShippingListModel> shipping_list { get; set; }
    }
}
