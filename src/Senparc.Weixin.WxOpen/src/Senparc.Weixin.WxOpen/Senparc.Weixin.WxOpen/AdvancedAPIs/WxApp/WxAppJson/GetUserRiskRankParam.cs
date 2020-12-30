using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    public class GetUserRiskRankParam
    {
        /// <summary>
        /// 小程序appid(必填)
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 用户的openid(必填)
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 场景值，0:注册，1:营销作弊(必填)
        /// </summary>
        public int scene { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string mobile_no { get; set; }
        /// <summary>
        /// 用户访问源ip(必填)
        /// </summary>
        public string client_ip { get; set; }
        /// <summary>
        /// 用户邮箱地址
        /// </summary>
        public string email_address { get; set; }
        /// <summary>
        /// 额外补充信息
        /// </summary>
        public string extended_info { get; set; }
    }
}
