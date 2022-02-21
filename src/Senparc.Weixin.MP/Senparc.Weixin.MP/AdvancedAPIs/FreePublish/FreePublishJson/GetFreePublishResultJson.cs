using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.FreePublish.FreePublishJson
{
    public class GetFreePublishResultJson : WxJsonResult
    {
        /// <summary>
        /// 发布任务id
        /// </summary>
        public string publish_id { get; set; }
        /// <summary>
        /// 发布状态，0:成功, 1:发布中，2:原创失败, 3: 常规失败, 4:平台审核不通过, 5:成功后用户删除所有文章, 6: 成功后系统封禁所有文章
        /// </summary>
        public int publish_status { get; set; }
        /// <summary>
        /// 当发布状态为0时（即成功）时，返回图文的 article_id，可用于“客服消息”场景
        /// </summary>
        public string article_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FreePublish_Article_detail article_detail { get; set; }
        /// <summary>
        /// 当发布状态为2或4时，返回不通过的文章编号，第一篇为 1；其他发布状态则为空
        /// </summary>
        public List<int> fail_idx { get; set; }
    }

    public class FreePublish_Item
    {
        /// <summary>
        /// 当发布状态为0时（即成功）时，返回文章对应的编号
        /// </summary>
        public int idx { get; set; }
        /// <summary>
        /// 当发布状态为0时（即成功）时，返回图文的永久链接
        /// </summary>
        public string article_url { get; set; }
    }

    public class FreePublish_Article_detail
    {
        /// <summary>
        /// 当发布状态为0时（即成功）时，返回文章数量
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<FreePublish_Item> item { get; set; }
    }

}