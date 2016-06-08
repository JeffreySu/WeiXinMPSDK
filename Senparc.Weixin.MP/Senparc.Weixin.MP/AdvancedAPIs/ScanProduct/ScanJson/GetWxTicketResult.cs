using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.ScanProduct
{
    /// <summary>
    /// 检查 wxticket 参数返回
    /// </summary>
    public class GetWxTicketResult : WxJsonResult
    {
        /// <summary>
        /// 商品编码标准
        /// </summary>
        public string keystandard { get; set; }

        /// <summary>
        /// 商品编码内容
        /// </summary>
        public string keystr { get; set; }

        /// <summary>
        /// 用户的openid
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 打开商品场景（scan扫描，other标识来自朋友圈等）
        /// </summary>
        public string scene { get; set; }

        /// <summary>
        /// 该条二维码是否被扫描过
        /// </summary>
        public string is_check { get; set; }

        /// <summary>
        /// 是否关注了公众号
        /// </summary>
        public string is_contact { get; set; }
    }
}
