using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 启动批量上传道具任务
    /// </summary>
    public class StartUploadGoodsRequestData
    {
        /// <summary>
        /// 上传的商品列表
        /// </summary>
        public List<StartUploadGoodsItem> upload_item { get; set; }

        /// <summary>
        /// 0-正式环境 1-沙箱环境
        /// </summary>
        public int env { get; set; }
    }

    /// <summary>
    /// 上传的商品列表
    /// </summary>
    public class StartUploadGoodsItem
    {
        /// <summary>
        /// 道具id，长度(0,64]，字符只允许使用字母、数字、'_'、'-'
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 道具名称，长度(0,1024]
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 道具单价，单位分，需要大于0
        /// </summary>
        public int price { get; set; }

        /// <summary>
        /// 道具备注，长度(0,1024]
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 道具图片的url地址，当前仅支持jpg,png等格式
        /// </summary>
        public string item_url { get; set; }
    }
}
