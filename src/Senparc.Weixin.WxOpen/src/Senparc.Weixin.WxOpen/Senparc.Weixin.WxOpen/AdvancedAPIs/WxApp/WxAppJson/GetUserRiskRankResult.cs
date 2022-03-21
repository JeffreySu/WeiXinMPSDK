using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.WxApp.WxAppJson
{
    public class GetUserRiskRankResult : WxJsonResult
    {
        /// <summary>
        /// 用户风险等级
        /// </summary>
        public int risk_rank { get; set; }
    }
}
