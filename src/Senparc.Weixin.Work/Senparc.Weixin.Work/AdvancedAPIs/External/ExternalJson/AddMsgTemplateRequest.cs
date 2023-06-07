using System.Collections.Generic;
using Senparc.Weixin.Work.Entities.Models;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 添加企业群发消息任务参数实体
    /// </summary>
    public class AddMsgTemplateRequest
    {
        /// <summary>
        /// 群发任务的类型，默认为single，表示发送给客户，group表示发送给客户群
        /// </summary>
        public string chat_type { get; set; } = "single";
        /// <summary>
        /// 客户的外部联系人id列表，仅在chat_type为single时有效，不可与sender同时为空，最多可传入1万个客户
        /// </summary>
        public List<string> external_userid { get; set; }
        /// <summary>
        /// 发送企业群发消息的成员userid，当类型为发送给客户群时必填
        /// </summary>
        public string sender { get; set; }
        /// <summary>
        /// 文本消息
        /// </summary>
        public MessageText text { get; set; }
        /// <summary>
        /// 图片消息
        /// </summary>
        public MessageImage image { get; set; }
        /// <summary>
        /// 图文消息
        /// </summary>
        public MessageLink link { get; set; }
        /// <summary>
        /// 小程序消息
        /// </summary>
        public MessageMiniprogram miniprogram { get; set; }
    }
}