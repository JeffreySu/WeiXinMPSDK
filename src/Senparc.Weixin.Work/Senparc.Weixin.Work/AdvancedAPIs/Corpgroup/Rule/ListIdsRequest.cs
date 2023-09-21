using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Rule
{
    /// <summary>
    /// 获取对接规则id列表 请求参数
    /// </summary>
    public class ListIdsRequest
    {
        /// <summary>
        /// 上下游id
        /// 必填
        /// </summary>
        public string chain_id { get; set; }

    }
}
