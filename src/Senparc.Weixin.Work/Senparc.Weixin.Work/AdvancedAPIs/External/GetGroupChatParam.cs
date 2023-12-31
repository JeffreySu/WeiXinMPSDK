/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetGroupChatParam.cs
    文件功能描述：获取「群聊数据统计」数据 按群主聚合的方式 接口请求参数
    
    
    创建标识：WangDrama - 20210630

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    /// 获取「群聊数据统计」数据 按群主聚合的方式 接口请求参数
    /// </summary>
    public class GetGroupChatParam
    {
        public long day_begin_time { get; set; }

        public long? day_end_time { get; set; }
        /// <summary>
        /// 如果不填，表示获取应用可见范围内全部群主的数据（但是不建议这么用，如果可见范围人数超过1000人，为了防止数据包过大，会报错 81017）是   群主ID列表。最多100个
        /// </summary>
        public GroupChatOwnerFilter owner_filter { get; set; }

        /// <summary>
        /// 否	排序方式。1 - 新增群的数量2 - 群总数3 - 新增群人数4 - 群总人数 默认为1
        /// </summary>
        public int order_by { get; set; }
        /// <summary>
        /// 否	是否升序。0-否；1-是。默认降序
        /// </summary>
        public int order_asc { get; set; }

        /// <summary>
        /// 否	分页，偏移量, 默认为0
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// 否	分页，预期请求的数据量，默认为500，取值范围 1 ~ 1000
        /// </summary>
        public int limit { get; set; }
    }
    public class GroupChatOwnerFilter
    {
        /// <summary>
        /// 用户ID列表。最多100个
        /// </summary>
        public string[] userid_list { get; set; }
    }

    public class GetGroupChatListResult : WorkJsonResult
    {
        /// <summary>
        /// 命中过滤条件的记录总个数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 当前分页的下一个offset。当next_offset和total相等时，说明已经取完所有
        /// </summary>
        public int next_offset { get; set; }
        public List<GetGroupChatItem> items { get; set; }

    }
    public class GetGroupChatItem
    {
        /// <summary>
        /// 群主ID
        /// </summary>
        public string owner { get; set; }
        public GetGroupChatItemData data { get; set; }
    }
    public class GetGroupChatItemData
    {
        /// <summary>
        /// 新增客户群数量
        /// </summary>
        public int new_chat_cnt { get; set; }
        /// <summary>
        /// 截至当天客户群总数量
        /// </summary>
        public int chat_total { get; set; }
        /// <summary>
        /// 截至当天有发过消息的客户群数量
        /// </summary>
        public int chat_has_msg { get; set; }
        /// <summary>
        /// 客户群新增群人数。
        /// </summary>
        public int new_member_cnt { get; set; }
        /// <summary>
        /// 截至当天客户群总人数
        /// </summary>
        public int member_total { get; set; }
        /// <summary>
        /// 截至当天有发过消息的群成员数
        /// </summary>
        public int member_has_msg { get; set; }
        /// <summary>
        /// 截至当天客户群消息总数
        /// </summary>
        public int msg_total { get; set; }
    }



    public class GetGroupChatGroupByDayParam
    {
        public long day_begin_time { get; set; }

        public long? day_end_time { get; set; }
        /// <summary>
        /// 如果不填，表示获取应用可见范围内全部群主的数据（但是不建议这么用，如果可见范围人数超过1000人，为了防止数据包过大，会报错 81017）是   群主ID列表。最多100个
        /// </summary>

        public GroupChatOwnerFilter owner_filter { get; set; }
    }

    public class GetGroupChatGroupByDayItem
    {
        /// <summary>
        /// 群主ID
        /// </summary>
        public long stat_time { get; set; }
        public GetGroupChatItemData data { get; set; }
    }

    public class GetGroupChatGroupByDayListResult : WorkJsonResult
    {
        public List<GetGroupChatItem> items { get; set; }

    }

}
