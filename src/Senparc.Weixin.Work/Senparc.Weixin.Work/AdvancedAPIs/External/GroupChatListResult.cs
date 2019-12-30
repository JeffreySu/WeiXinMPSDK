using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    /// 获取客户群列表 返回结果
    /// </summary>
    public class GroupChatListResult : WorkJsonResult
    {
        /// <summary>
        /// 客户群列表
        /// </summary>
        public Group_Chat_List[] group_chat_list { get; set; }
    }

    public class Group_Chat_List
    {
        /// <summary>
        /// 客户群IDs
        /// </summary>
        public string chat_id { get; set; }
        /// <summary>
        /// 客户群状态。
        /// 0 - 正常
        /// 1 - 跟进人离职
        /// 2 - 离职继承中
        /// 3 - 离职继承完成
        /// </summary>
        public int status { get; set; }
    }
}
