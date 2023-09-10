using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    public class GetExternalContactInfoResult : WorkJsonResult
    {
        public ExternalContactInfo external_contact { get; set; }
        public FollowUserInfo follow_user { get; set; }
    }

    public class ExternalContactInfo
    {
        public string external_userid { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public string avatar { get; set; }
        public int gender { get; set; }
        public string unionid { get; set; }
    }

    public class FollowUserInfo
    {
        public string remark { get; set; }
        public string description { get; set; }
        public long creattime { get; set; }
        public string[] tag_id { get; set; }
        public string[] remark_mobiles { get; set; }
        public int add_way { get; set; }
        public string remark_corp_name { get; set; }
        public string oper_userid { get; set; }
        public string state { get; set; }
    }
}
