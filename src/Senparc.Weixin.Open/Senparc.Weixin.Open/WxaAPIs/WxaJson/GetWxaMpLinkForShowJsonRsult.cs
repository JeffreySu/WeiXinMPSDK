using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    public class GetWxaMpLinkForShowJsonRsult : WxJsonResult
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public int total_num { get; set; }

        /// <summary>
        /// 公众号信息列表
        /// </summary>
        public List<BizInfo> biz_info_list { get; set; }
    }

    public class BizInfo
    {
        /// <summary>
        /// 公众号昵称
        /// </summary>
        public string nickname { get; set; }

        /// <summary>
        /// 公众号 appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 公众号头像
        /// </summary>
        public string headimg { get; set; }
    }
}
