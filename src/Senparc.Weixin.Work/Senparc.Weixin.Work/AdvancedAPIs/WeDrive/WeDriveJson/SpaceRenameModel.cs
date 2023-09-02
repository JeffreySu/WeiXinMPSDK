using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 重命名已有空间入口参数
    /// </summary>
    public class SpaceRenameModel
    {
        /// <summary>
        /// 空间spaceid
        /// </summary>
        public string spaceid { get; set; }
        /// <summary>
        /// 重命名后的空间名
        /// </summary>
        public string space_name { get; set; }
    }
}
