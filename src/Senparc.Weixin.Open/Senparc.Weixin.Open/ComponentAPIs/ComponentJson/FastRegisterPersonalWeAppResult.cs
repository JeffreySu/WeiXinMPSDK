using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 创建个人主体小程序接口返回结果
    /// </summary>
    [Serializable]
    public class FastRegisterPersonalWeAppResult : WxJsonResult
    {
        /// <summary>
        /// 任务id，后面query查询需要用到
        /// </summary>
        public string taskid { get; set; }

        /// <summary>
        /// 给用户扫码认证的验证url
        /// </summary>
        public string authorize_url { get; set; }

        /// <summary>
        /// 任务的状态 0成功
        /// </summary>
        public int status { get; set; }
    }
}
