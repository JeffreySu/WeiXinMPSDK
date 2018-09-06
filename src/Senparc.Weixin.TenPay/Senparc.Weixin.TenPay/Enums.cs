using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.TenPay
{
    /// <summary>
    /// 支付类型
    /// </summary>
    public enum TenPayV3Type
    {
        /// <summary>
        /// 公众号JS-API支付和小程序支付
        /// </summary>
        JSAPI,
        NATIVE,
        APP,
        MWEB
    }


    /// <summary>
    /// 红包的场景id（scene_id），最中输出为字符串
    /// </summary>
    public enum RedPack_Scene
    {
        /// <summary>
        /// 商品促销
        /// </summary>
        PRODUCT_1,
        /// <summary>
        /// 抽奖
        /// </summary>
        PRODUCT_2,
        /// <summary>
        /// 虚拟物品兑奖
        /// </summary>
        PRODUCT_3,
        /// <summary>
        /// 企业内部福利
        /// </summary>
        PRODUCT_4,
        /// <summary>
        /// 渠道分润
        /// </summary>
        PRODUCT_5,
        /// <summary>
        /// 保险回馈
        /// </summary>
        PRODUCT_6,
        /// <summary>
        /// 彩票派奖
        /// </summary>
        PRODUCT_7,
        /// <summary>
        /// 税务刮奖
        /// </summary>
        PRODUCT_8
    }
}
