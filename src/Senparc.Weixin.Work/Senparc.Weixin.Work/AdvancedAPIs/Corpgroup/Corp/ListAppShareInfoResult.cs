using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 获取应用共享信息 响应参数
    /// </summary>
    public class ListAppShareInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 1表示拉取完毕，0表示数据没有拉取完
        /// </summary>
        public int ending { get; set; }

        /// <summary>
        /// 分页游标，再下次请求时填写以获取之后分页的记录，如果已经没有更多的数据则返回空
        /// </summary>
        public string next_cursor { get; set; }

        /// <summary>
        /// 应用共享信息
        /// </summary>
        public List<ListAppShareInfoResult_CorpList> corp_list { get; set; }
    }

    public class ListAppShareInfoResult_CorpList
    {
        /// <summary>
        /// 下级/下游企业corpid
        /// </summary>
        public string corpid { get; set; }

        /// <summary>
        /// 下级/下游企业名称
        /// </summary>
        public string corp_name { get; set; }

        /// <summary>
        /// 下级/下游企业应用id
        /// </summary>
        public int agentid { get; set; }
    }
}
