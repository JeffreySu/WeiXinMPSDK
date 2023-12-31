/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：Shipping.cs
    文件功能描述：物流
    
    
    创建标识：Yaofeng - 20231026

----------------------------------------------------------------*/

namespace Senparc.Weixin.Open.WxaAPIs.SecOrder
{
    /// <summary>
    /// 物流
    /// </summary>
    public class Shipping
    {
        /// <summary>
        /// 【必填】物流单号，物流快递发货时必填，示例值: 323244567777 字符字节限制: [1, 128]
        /// </summary>
        public string tracking_no { get; set; }

        /// <summary>
        /// 【非必填】物流公司编码，快递公司ID，参见「查询物流公司编码列表」，物流快递发货时必填， 示例值: DHL 字符字节限制: [1, 128]
        /// </summary>
        public string express_company { get; set; }

        /// <summary>
        /// 【必填】商品信息，例如：微信红包抱枕*1个，限120个字以内
        /// </summary>
        public string item_desc { get; set; }

        /// <summary>
        /// 【非必填】联系方式，当发货的物流公司为顺丰时，联系方式为必填，收件人或寄件人联系方式二选一
        /// </summary>
        public Contact contact { get; set; }
    }
}