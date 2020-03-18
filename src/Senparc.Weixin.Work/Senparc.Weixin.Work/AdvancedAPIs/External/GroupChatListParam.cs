using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    /// 群状态
    /// </summary>
    public enum StatusFilter
    {
        普通列表 = 0,
        离职待继承 = 1,
        离职继承中 = 2,
        离职继承完成 = 3
    }
    /// <summary>
    /// 客户群列表查询参数
    /// </summary>
    public class GroupChatListParam
    {
        /// <summary>
        /// 群状态过滤。
        /// </summary>
        public StatusFilter status_filter { get; set; } = StatusFilter.普通列表;
        /// <summary>
        /// 群主过滤。如果不填，表示获取全部群主的数据
        /// </summary>
        public Owner_Filter owner_filter { get; set; }
        /// <summary>
        /// 分页，偏移量
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// 分页，预期请求的数据量，取值范围 1 ~ 1000s
        /// </summary>
        public int limit { get; set; }
    }

    public class Owner_Filter
    {
        /// <summary>
        /// 用户ID列表。最多100个
        /// </summary>
        public string[] userid_list { get; set; }
        /// <summary>
        /// 部门ID列表。最多100个
        /// </summary>
        public int[] partyid_list { get; set; }
    }

}
