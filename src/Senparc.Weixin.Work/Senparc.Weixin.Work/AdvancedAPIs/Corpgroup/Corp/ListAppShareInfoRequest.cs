using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 获取应用共享信息 请求参数
    /// </summary>
    public class ListAppShareInfoRequest
    {
        /// <summary>
        /// 上级/上游企业应用agentid
        /// 必填
        /// </summary>
        public int agentid { get; set; }

        /// <summary>
        /// 填0则为企业互联/局校互联，填1则表示上下游企业
        /// 非必填
        /// </summary>
        public int business_type { get; set; }

        /// <summary>
        /// 下级/下游企业corpid，若指定该参数则表示拉取该下级/下游企业的应用共享信息
        /// 非必填
        /// </summary>
        public string corpid { get; set; }

        /// <summary>
        /// 返回的最大记录数，整型，最大值100，默认情况或者值为0表示下拉取全量数据，建议分页拉取或者通过指定corpid参数拉取。
        /// 非必填
        /// </summary>
        public int limit { get; set; }

        /// <summary>
        /// 用于分页查询的游标，字符串类型，由上一次调用返回，首次调用可不填
        /// 非必填
        /// </summary>
        public string cursor { get; set; }
    }
}
