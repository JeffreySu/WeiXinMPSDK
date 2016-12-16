using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.UserTag
{
    public class UserTagJsonResult : WxJsonResult
    {
        public int count { get; set; }
        public UserTagList data { get; set; }
        public string next_openid { get; set; }
    }

    public class UserTagList
    {
        public List<string> openid { get; set; }
    }
}