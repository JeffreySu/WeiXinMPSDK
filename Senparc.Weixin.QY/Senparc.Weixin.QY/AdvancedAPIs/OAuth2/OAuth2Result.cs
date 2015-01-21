using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    /// <summary>
    /// 获取成员信息返回结果
    /// </summary>
    public class GetUserIdResult : WxJsonResult
    {
        public string UserId { get; set; }//员工UserID
    }
}
