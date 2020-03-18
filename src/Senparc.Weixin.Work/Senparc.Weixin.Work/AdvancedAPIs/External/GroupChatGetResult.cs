using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    /// 获取客户群详情 返回结果
    /// </summary>
    public class GroupChatGetResult : WorkJsonResult
    {
        /// <summary>
        /// 客户群详情
        /// </summary>
        public Group_Chat group_chat { get; set; }
    }
    /// <summary>
    /// 客户群详情
    /// </summary>
    public class Group_Chat
    {
        /// <summary>
        /// 客户群ID
        /// </summary>
        public string chat_id { get; set; }
        /// <summary>
        /// 群名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 群主ID
        /// </summary>
        public string owner { get; set; }
        /// <summary>
        /// 群的创建时间
        /// </summary>
        public int create_time { get; set; }
        /// <summary>
        /// 群公告
        /// </summary>
        public string notice { get; set; }
        /// <summary>
        /// 群成员列表
        /// </summary>
        public Member_List[] member_list { get; set; }
    }
    /// <summary>
    /// 群成员列表
    /// </summary>
    public class Member_List
    {
        /// <summary>
        /// 群成员id
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 成员类型。
        /// 1 - 企业成员
        /// 2 - 外部联系人
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 入群时间
        /// </summary>
        public int join_time { get; set; }
        /// <summary>
        /// 入群方式。
        /// 1 - 由成员邀请入群（直接邀请入群）
        /// 2 - 由成员邀请入群（通过邀请链接入群）
        /// 3 - 通过扫描群二维码入群
        /// </summary>
        public int join_scene { get; set; }
    }

}
