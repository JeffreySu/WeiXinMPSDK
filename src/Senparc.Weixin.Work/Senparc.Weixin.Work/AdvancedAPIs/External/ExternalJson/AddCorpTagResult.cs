using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 添加企业客户标签 返回结果
    /// </summary>
    public class AddCorpTagResult : WorkJsonResult
    {
        /// <summary>
        /// 标签组
        /// </summary>
        public AddCorpTagResult_Tag_Group tag_group { get; set; }
    }

    public class AddCorpTagResult_Tag_Group
    {
        /// <summary>
        /// 标签组id
        /// </summary>
        public string group_id { get; set; }
        /// <summary>
        /// 标签组名称
        /// </summary>
        public string group_name { get; set; }
        /// <summary>
        /// 标签组创建时间
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 标签组次序值。order值大的排序靠前。有效的值范围是[0, 2^32)
        /// </summary>
        public long order { get; set; }
        /// <summary>
        /// 标签组内的标签列表
        /// </summary>
        public AddCorpTagResult_Tag_Group_Tag[] tag { get; set; }
    }

    public class AddCorpTagResult_Tag_Group_Tag
    {
        /// <summary>
        /// 新建标签id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 新建标签名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 标签创建时间
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 标签次序值。order值大的排序靠前。有效的值范围是[0, 2^32)
        /// </summary>
        public long order { get; set; }
    }

}
