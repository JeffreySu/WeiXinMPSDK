using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    public class GetExternalContactInfoBatchResult : WorkJsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ExternalContactList> external_contact_list { get; set; }

        /// <summary>
        /// 分页游标，再下次请求时填写以获取之后分页的记录，如果已经没有更多的数据则返回空
        /// </summary>
        public string next_cursor { get; set; }
    }

    public class ExternalContactList
    {
        public ExternalContact external_contact { get; set; }
        public FollowUser follow_info { get; set; }
    }


    public class ExternalContact
    {
        public string external_userid { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public string avatar { get; set; }
        public int gender { get; set; }
        public string unionid { get; set; }
    }

    public class FollowUser
    {
        public string remark { get; set; }
        public string description { get; set; }
        public long creattime { get; set; }
        public string[] tag_id { get; set; }
        public string[] remark_mobiles { get; set; }
        public int add_way { get; set; }
    }
}
