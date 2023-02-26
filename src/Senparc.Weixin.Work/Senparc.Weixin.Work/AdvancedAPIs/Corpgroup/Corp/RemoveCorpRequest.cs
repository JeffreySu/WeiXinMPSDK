using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 移除企业 请求参数
    /// </summary>
    public class RemoveCorpRequest
    {
        /// <summary>
        /// 上下游id
        /// 必填
        /// </summary>
        public string chain_id { get; set; }

        /// <summary>
        /// 需要移除的下游企业corpid
        /// 必填
        /// </summary>
        public string corpid { get; set; }

        /// <summary>
        /// 需要移除的未加入下游企业corpid，corpid和pending_corpid至少填一个，都填corpid生效
        /// 非必填
        /// </summary>
        public string pending_corpid { get; set; }

    }
}
