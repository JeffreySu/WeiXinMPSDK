using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 获取企业群发消息发送结果返回参数
    /// </summary>
    public class GroupMsgResult : WorkJsonResult
    {
        /// <summary>
        /// 返回结果明细
        /// </summary>
        public List<GroupMsgResultDetail> detail_list { get; set; }
    }

    public class GroupMsgResultDetail
    {
        /// <summary>
        /// 外部联系人userid
        /// </summary>
        public string external_userid { get; set; }
        /// <summary>
        /// 外部客户群id
        /// </summary>
        public string chat_id { get; set; }
        /// <summary>
        /// 企业服务人员的userid
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 发送状态 0-未发送 1-已发送 2-因客户不是好友导致发送失败 3-因客户已经收到其他群发消息导致发送失败
        /// </summary>
        public GroupTaskSentStatus status { get; set; }
        /// <summary>
        /// 发送时间，发送状态为1时返回
        /// </summary>
        public long send_time { get; set; }
    }
}