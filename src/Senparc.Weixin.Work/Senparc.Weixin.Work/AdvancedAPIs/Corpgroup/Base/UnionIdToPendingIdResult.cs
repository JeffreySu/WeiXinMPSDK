using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Base
{
    /// <summary>
    /// unionid查询pending_id 响应参数
    /// </summary>
    public class UnionIdToPendingIdResult : WorkJsonResult
    {
        /// <summary>
        /// 	unionid和openid对应的pending_id
        /// </summary>
        public string pending_id { get; set; }
    }
}
