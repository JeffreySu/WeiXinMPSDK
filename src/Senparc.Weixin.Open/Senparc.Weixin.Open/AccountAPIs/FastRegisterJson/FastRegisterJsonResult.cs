using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.AccountAPIs.FastRegisterJson
{
    /// <summary>
    /// 快速注册小程序返回结果
    /// </summary>
    [Serializable]
    public class FastRegisterJsonResult : WxJsonResult
    {
        /// <summary>
        /// 新创建小程序的appid
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 新创建小程序的授权码
        /// </summary>
        public string authorization_code { get; set; }

        /// <summary>
        /// 复用公众号微信认证小程序是否成功
        /// </summary>
        public bool is_wx_verify_succ { get; set; }

        /// <summary>
        /// 小程序是否和公众号关联成功
        /// </summary>
        public bool is_link_succ { get; set; }
    }
}
