using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.UserTag.UserTagJson
{
    public class UserTagJsonResult:WxJsonResult 
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
