using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.UserTag
{
    public class UserTagListResult : WxJsonResult
    {
        public List<int> tagid_list { get; set; }
    }
}