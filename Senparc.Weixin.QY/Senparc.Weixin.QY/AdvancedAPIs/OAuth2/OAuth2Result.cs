using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.QYPIs
{
    /// <summary>
    /// 获取成员信息返回结果
    /// </summary>
    public class GetUserIdResult : WxJsonResult
    {
        public string UserId { get; set; }//员工UserID
    }
}
