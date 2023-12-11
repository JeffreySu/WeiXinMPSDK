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
    public class QueryPublishGoodsJsonResult : WxJsonResult
    {
        /// <summary>
        /// 发布的道具列表
        /// </summary>
        public List<QueryPublishGoodsItem> publish_item { get; set; }

        /// <summary>
        /// 0-无任务在运行 1-任务运行中 2-上传失败或部分失败（上传任务已经完成） 3-上传成功
        /// </summary>
        public int status { get; set; }
    }

    /// <summary>
    /// 发布的道具列表
    /// </summary>
    public class QueryPublishGoodsItem
    {
        /// <summary>
        /// 道具id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 0-上传中 1-id已经存在 2-发布成功 3-发布失败
        /// </summary>
        public int publish_status { get; set; }

        /// <summary>
        /// 上传失败的原因
        /// </summary>
        public string errmsg { get; set; }
    }
}
