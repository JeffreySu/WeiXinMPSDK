using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.CustomService
{
    /// <summary>
    /// 在线客户列表获取结果
    /// </summary>
    public class GetOnlineKfListResultJson : WxJsonResult
    {
        public List<OnlineKfInfo> kf_online_list { get; set; }
    }

    public class OnlineKfInfo
    {
        public string kf_account { get; set; }

        public int status { get; set; }

        public string kf_id { get; set; }

        public string kf_openid { get; set; }
    }
}
