using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryUploadGoodsJsonResult : WxJsonResult
    {
        /// <summary>
        /// 上传的道具列表
        /// </summary>
        public List<QueryUploadGoodsItem> upload_item { get; set; }

        /// <summary>
        /// 0-无任务在运行 1-任务运行中 2-上传失败或部分失败（上传任务已经完成） 3-上传成功
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 上传的商品列表
    /// </summary>
    public class QueryUploadGoodsItem
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

        /// <summary>
        /// 0-上传中 1-id已经存在 2-上传成功 3-上传失败
        /// </summary>
        public int upload_status { get; set; }

        /// <summary>
        /// 上传失败的原因
        /// </summary>
        public string errmsg { get; set; }
    }
}
