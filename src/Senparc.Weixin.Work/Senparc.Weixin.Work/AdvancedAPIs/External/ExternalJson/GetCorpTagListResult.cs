using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    /// 
    /// </summary>
    public class GetCorpTagListResult : WorkJsonResult
    {
        /// <summary>
        /// 标签组列表
        /// </summary>
        public Tag_Group[] tag_group { get; set; }
    }

    public class Tag_Group
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
        /// 标签组排序的次序值，order值大的排序靠前。有效的值范围是[0, 2^32)
        /// </summary>
        public long order { get; set; }
        /// <summary>
        /// 标签组是否已经被删除，只在指定tag_id进行查询时返回
        /// </summary>
        public bool? deleted { get; set; }
        /// <summary>
        /// 标签组内的标签列表
        /// </summary>
        public Tag[] tag { get; set; }
    }

    public class Tag
    {
        /// <summary>
        /// 标签id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 标签创建时间
        /// </summary>
        public long create_time { get; set; }
        /// <summary>
        /// 标签排序的次序值，order值大的排序靠前。有效的值范围是[0, 2^32)
        /// </summary>
        public long order { get; set; }
        /// <summary>
        /// 标签是否已经被删除，只在指定tag_id/group_id进行查询时返回
        /// </summary>
        public bool? deleted { get; set; }
    }
}    
