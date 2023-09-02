using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    public class MngProInfoJsonResult:WorkJsonResult
    {
        /// <summary>
        /// true为专业版，false为不是专业版
        /// </summary>
        public bool is_pro { get; set; }
        /// <summary>
        /// 总的vip账号数量
        /// </summary>
        public int total_vip_acct_num { get; set; }
        /// <summary>
        /// 已的vip账号数量
        /// </summary>
        public int use_vip_acct_num { get; set; }
        /// <summary>
        /// 专业版到期时间，时间戳，精确到秒
        /// </summary>
        public int pro_expire_time { get; set; }
    }
}
