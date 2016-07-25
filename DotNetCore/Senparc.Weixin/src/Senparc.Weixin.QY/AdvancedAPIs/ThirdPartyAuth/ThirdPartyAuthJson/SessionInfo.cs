using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.QY.AdvancedAPIs.ThirdPartyAuth
{
    public class SessionInfo
    {
        /// <summary>
        /// 允许进行授权的应用id，如1、2、3
        /// </summary>
        public List<string> appid { get; set; }
    }
}
