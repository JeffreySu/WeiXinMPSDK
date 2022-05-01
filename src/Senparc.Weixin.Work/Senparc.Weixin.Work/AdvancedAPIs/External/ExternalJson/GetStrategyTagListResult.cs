using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
    /// <summary>
    ///获取指定规则组下的企业客户标签 返回结果
    /// </summary>
    public class GetStrategyTagListResult : WorkJsonResult
    {
        public GetStrategyTagListResult_Tag_Group[] tag_group { get; set; }
    }

    public class GetStrategyTagListResult_Tag_Group
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
        /// 标签组所属的规则组id
        /// </summary>
        public int strategy_id { get; set; }
        /// <summary>
        /// 标签组内的标签列表
        /// </summary>
        public GetStrategyTagListResult_Tag_Group_Tag[] tag { get; set; }
    }

    public class GetStrategyTagListResult_Tag_Group_Tag
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
    }

}
