using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.GroupMessage
{
    public class BaseGroupMessageDataByFilter
    {
        public string msgtype { get; set; }

        /// <summary>
        /// 群发接口新增 send_ignore_reprint 参数，开发者可以对群发接口的 send_ignore_reprint 参数进行设置，指定待群发的文章被判定为转载时，是否继续群发。
        /// 当 send_ignore_reprint 参数设置为1时，文章被判定为转载时，且原创文允许转载时，将继续进行群发操作。
        /// 当 send_ignore_reprint 参数设置为0时，文章被判定为转载时，将停止群发操作。
        /// send_ignore_reprint 默认为0。
        /// </summary>
        public int send_ignore_reprint { get; set; }
    }
}
