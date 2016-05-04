using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.UserTag.UserTagJson
{
    public class UserTagListResult :WxJsonResult
    {
        public List<int> tagid_list { get; set; }
    }
}
