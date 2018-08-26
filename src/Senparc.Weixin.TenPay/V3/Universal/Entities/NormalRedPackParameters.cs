using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPay.V3.Entities
{
    //暂时未用起来

    public class NormalRedPackParameters
    {
        /// <summary>
        /// 随机字符串，不长于32位。实例值：5K8264ILTKCH16CQ2502SI8ZNMTM67VS，类型：String(32)
        /// </summary>
        public string nonce_str { get; set; }
    }
}
