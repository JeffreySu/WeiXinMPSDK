using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WeDrive.WeDriveJson
{
    /// <summary>
    /// 微盘内新建空间返回参数
    /// </summary>
    public class SpaceCreateJsonResult : WorkJsonResult
    {
        /// <summary>
        /// 空间id
        /// </summary>
        public string spaceid { get; set; }
    }
}
