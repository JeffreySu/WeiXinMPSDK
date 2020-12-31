using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.CustomerTag
{
    public class GetCorpTagListResult : WorkJsonResult
    {
        public List<CorpTagGroup> tag_group { get; set; }
    }

    public class AddCorpCustomerTagResult : WorkJsonResult
    {
        public CorpTagGroup tag_group { get; set; }
    }

    public class EditCorpCustomerTagRequest
    {
        /// <summary>
        /// 标签或标签组的id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 新的标签或标签组名称，最长为30个字符
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 标签/标签组的次序值
        /// order值大的排序靠前。有效的值范围是[0, 2^32)
        /// </summary>
        public int order { get; set; }
    }

    public class DeleteCorpCustomerTagRequest
    {
        public List<string> tag_id { get; set; }
        public List<string> group_id { get; set; }
    }

    public class ExternalContactMarkTagRequest
    {
        public string userid { get; set; }
        public string external_userid { get; set; }
        public List<string> add_tag { get; set; }
        public List<string> remove_tag { get; set; }
    }

    public class CorpTagGroup
    {
        public string group_id { get; set; }
        public string group_name { get; set; }
        public long create_time { get; set; }
        public long order { get; set; }
        public bool deleted { get; set; }
        public List<CorpTag> tag { get; set; }
    }

    public class CorpTag
    {
        public string id { get; set; }
        public string name { get; set; }
        public long create_time { get; set; }
        public long order { get; set; }
        public bool deleted { get; set; }
    }
}