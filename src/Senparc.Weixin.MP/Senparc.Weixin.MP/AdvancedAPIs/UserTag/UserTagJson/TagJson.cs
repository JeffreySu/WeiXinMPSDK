using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.AdvancedAPIs.UserTag
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