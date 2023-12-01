using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 启动批量发布道具任务
    /// </summary>
    public class StartPublishGoodsRequestData
    {
        /// <summary>
        /// 发布的商品列表
        /// </summary>
        public List<StartPublishGoodsItem> publish_item { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }

    /// <summary>
    /// 发布的商品列表
    /// </summary>
    public class StartPublishGoodsItem
    {
        /// <summary>
        /// 道具id，添加到开发环境时传的道具id
        /// </summary>
        public string id { get; set; }
    }
}
