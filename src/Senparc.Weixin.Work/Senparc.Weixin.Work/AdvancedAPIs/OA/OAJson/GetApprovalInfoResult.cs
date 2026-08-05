

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.OA.OAJson
{
    /// <summary>
    /// 
    /// </summary>
    public class GetApprovalInfoResult : WorkJsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> sp_no_list { get; set; }

        /// <summary>
        /// 分页查询游标，后续请求可传入该值继续拉取
        /// </summary>
        public int next_cursor { get; set; }
    }
}
