using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.CustomService
{
    /// <summary>
    /// 客服基本信息的列表获取结果
    /// </summary>
    public class GetKfListResultJson : WxJsonResult
    {
        /// <summary>
        /// 客服列表
        /// </summary>
        public List<KfInfo> kf_list { get; set; }
    }

    public class KfInfo
    {
        /// <summary>
        /// 客服昵称
        /// </summary>
        public string kf_nick { get; set; }

        /// <summary>
        /// 客服编号
        /// </summary>
        public string kf_id { get; set; }

        /// <summary>
        /// 客服头像
        /// </summary>
        public string kf_headimgurl { get; set; }

        /// <summary>
        /// 客服微信号
        /// </summary>
        public string kf_wx { get; set; }

        /// <summary>
        /// 客服openid
        /// </summary>
        public string kf_openid { get; set; }
    }
}
