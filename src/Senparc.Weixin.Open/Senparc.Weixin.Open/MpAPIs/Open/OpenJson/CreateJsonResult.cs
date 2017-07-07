using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.MpAPIs.Open
{
    /// <summary>
    /// 创建开放平台帐号并绑定公众号/小程序接口返回结果
    /// </summary>
    [Serializable]
    public class CreateJsonResult : WxJsonResult
    {
        /// <summary>
        /// 所创建的开放平台帐号的appid
        /// </summary>
        public string open_appid { get; set; }
    }
}
