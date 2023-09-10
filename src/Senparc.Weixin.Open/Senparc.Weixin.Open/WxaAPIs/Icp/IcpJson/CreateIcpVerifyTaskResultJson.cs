using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs.Icp
{
    /// <summary>
    /// 发起小程序管理员人脸核身结果
    /// </summary>
    public class CreateIcpVerifyTaskResultJson : WxJsonResult
    {
        /// <summary>
        /// 人脸核验任务id
        /// </summary>
        public string task_id { get; set; }
    }
}
