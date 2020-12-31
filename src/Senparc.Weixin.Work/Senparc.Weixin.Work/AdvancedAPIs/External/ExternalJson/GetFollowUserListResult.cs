using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 获取配置了客户联系功能的成员列表返回
    /// </summary>
    public class GetFollowUserListResult : WorkJsonResult
    {
        /// <summary>
        /// 配置了客户联系功能的成员userid列表
        /// </summary>
        public List<string> follow_user { get; set; }

    }
}