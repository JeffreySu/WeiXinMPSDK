using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 获取空间邀请分享链接返回参数
    /// </summary>
    public class SpaceShareJsonResult: WorkJsonResult
    {
        /// <summary>
        /// 邀请链接
        /// </summary>
        public string space_share_url { get; set; }
    }
}
