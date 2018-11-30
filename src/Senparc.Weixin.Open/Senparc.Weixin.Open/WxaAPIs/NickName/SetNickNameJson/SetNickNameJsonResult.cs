using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.WxaAPIs.NickName.SetNickNameJson
{
    /// <summary>
    /// 小程序名称设置及改名结果
    /// </summary>
    [Serializable]
    public class SetNickNameJsonResult: WxJsonResult
    {
        /// <summary>
        /// 材料说明
        /// </summary>
        public string wording { get; set; }

        /// <summary>
        /// 审核单id
        /// </summary>
        public int audit_id { get; set; }
    }
}
