using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.Corpgroup.Corp
{
    /// <summary>
    /// 获取下级/下游企业的access_token 响应参数
    /// </summary>
    public class GetTokenResult : WorkJsonResult
    {
        /// <summary>
        /// 凭证的有效时间（秒）
        /// </summary>
        public int expires_in { get; set; }

        /// <summary>
        /// 获取到的下级/下游企业调用凭证，最长为512字节
        /// </summary>
        public string access_token { get; set; }
    }
}
