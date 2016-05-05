using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.AdvancedAPIs.UserTag.UserTagJson
{
    public class TagJson : WxJsonResult
    {
        public List<TagJson_Tag> tags { get; set; }
    }
    public class TagJson_Tag
    {
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
    }
}
