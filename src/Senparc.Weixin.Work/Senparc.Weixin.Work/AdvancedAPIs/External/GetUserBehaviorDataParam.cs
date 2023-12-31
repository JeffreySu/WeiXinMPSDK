/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetUserBehaviorDataParam.cs
    文件功能描述：获取「联系客户统计」数据 接口请求参数
    
    
    创建标识：WangDrama - 20210630

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{

    public class GetUserBehaviorDataParam
    {
        /*
         {
    "userid": [
        "zhangsan",
        "lisi"
    ],
    "partyid":
    [
        1001,
        1002
    ],
    "start_time":1536508800,
    "end_time":1536595200
}
        
        userid	否	成员ID列表，最多100个
partyid	否	部门ID列表，最多100个
start_time	是	数据起始时间
end_time	是	数据结束时间
userid和partyid不可同时为空;
此接口提供的数据以天为维度，查询的时间范围为[start_time,end_time]，即前后均为闭区间，支持的最大查询跨度为30天；
用户最多可获取最近180天内的数据；
当传入的时间不为0点时间戳时，会向下取整，如传入1554296400(Wed Apr 3 21:00:00 CST 2019)会被自动转换为1554220800（Wed Apr 3 00:00:00 CST 2019）;
如传入多个userid，则表示获取这些成员总体的联系客户数据。
         */
        /// <summary>
        /// 否	成员ID列表，最多100个 如传入多个userid，则表示获取这些成员总体的联系客户数据。
        /// </summary>
        public string[] userid { get; set; }
        /// <summary>
        /// 部门ID列表，最多100个
        /// </summary>
        public string[] partyid { get; set; }
        /// <summary>
        /// 是	数据起始时间
        /// </summary>
        public long start_time { get; set; }
        /// <summary>
        /// 是	数据结束时间
        /// </summary>
        public long end_time { get; set; }
    }

    public class GetUserBehaviorDataListResult : WorkJsonResult
    {
        public List<BehaviorData> behavior_data { get; set; }
        /*
         {
    "errcode": 0,
    "errmsg": "ok",
    "behavior_data":
    [
        {
        "stat_time":1536508800,
        "chat_cnt":100,
        "message_cnt":80,
        "reply_percentage":60.25,
        "avg_reply_time":1,
        "negative_feedback_cnt":0,
        "new_apply_cnt":6,
        "new_contact_cnt":5
        },
        {
        "stat_time":1536595200,
        "chat_cnt":20,
        "message_cnt":40,
        "reply_percentage":100,
        "avg_reply_time":1,
        "negative_feedback_cnt":0,
        "new_apply_cnt":6,
        "new_contact_cnt":5
        }
    ]
}
        behavior_data.stat_time	数据日期，为当日0点的时间戳
behavior_data.new_apply_cnt	发起申请数，成员通过「搜索手机号」、「扫一扫」、「从微信好友中添加」、「从群聊中添加」、「添加共享、分配给我的客户」、「添加单向、双向删除好友关系的好友」、「从新的联系人推荐中添加」等渠道主动向客户发起的好友申请数量。
behavior_data.new_contact_cnt	新增客户数，成员新添加的客户数量。
behavior_data.chat_cnt	聊天总数， 成员有主动发送过消息的单聊总数。
behavior_data.message_cnt	发送消息数，成员在单聊中发送的消息总数。
behavior_data.reply_percentage	已回复聊天占比，浮点型，客户主动发起聊天后，成员在一个自然日内有回复过消息的聊天数/客户主动发起的聊天数比例，不包括群聊，仅在确有聊天时返回。
behavior_data.avg_reply_time	平均首次回复时长，单位为分钟，即客户主动发起聊天后，成员在一个自然日内首次回复的时长间隔为首次回复时长，所有聊天的首次回复总时长/已回复的聊天总数即为平均首次回复时长，不包括群聊，仅在确有聊天时返回。
behavior_data.negative_feedback_cnt	删除/拉黑成员的客户数，即将成员删除或加入黑名单的客户数。
         */

    }
    public class BehaviorData
    {
        /*
         {
        "stat_time":1536595200,
        "chat_cnt":20,
        "message_cnt":40,
        "reply_percentage":100,
        "avg_reply_time":1,
        "negative_feedback_cnt":0,
        "new_apply_cnt":6,
        "new_contact_cnt":5
        }
        behavior_data.stat_time	数据日期，为当日0点的时间戳
behavior_data.new_apply_cnt	发起申请数，成员通过「搜索手机号」、「扫一扫」、「从微信好友中添加」、「从群聊中添加」、「添加共享、分配给我的客户」、「添加单向、双向删除好友关系的好友」、「从新的联系人推荐中添加」等渠道主动向客户发起的好友申请数量。
behavior_data.new_contact_cnt	新增客户数，成员新添加的客户数量。
behavior_data.chat_cnt	聊天总数， 成员有主动发送过消息的单聊总数。
behavior_data.message_cnt	发送消息数，成员在单聊中发送的消息总数。
behavior_data.reply_percentage	已回复聊天占比，浮点型，客户主动发起聊天后，成员在一个自然日内有回复过消息的聊天数/客户主动发起的聊天数比例，不包括群聊，仅在确有聊天时返回。
behavior_data.avg_reply_time	平均首次回复时长，单位为分钟，即客户主动发起聊天后，成员在一个自然日内首次回复的时长间隔为首次回复时长，所有聊天的首次回复总时长/已回复的聊天总数即为平均首次回复时长，不包括群聊，仅在确有聊天时返回。
behavior_data.negative_feedback_cnt	删除/拉黑成员的客户数，即将成员删除或加入黑名单的客户数。
         */
        /// <summary>
        /// 数据日期，为当日0点的时间戳
        /// </summary>
        public long stat_time { get; set; }

        /// <summary>
        /// 聊天总数， 成员有主动发送过消息的单聊总数。
        /// </summary>
        public int chat_cnt { get; set; }
        /// <summary>
        /// 发送消息数，成员在单聊中发送的消息总数。
        /// </summary>
        public int message_cnt { get; set; }
        /// <summary>
        /// 已回复聊天占比，浮点型，客户主动发起聊天后，成员在一个自然日内有回复过消息的聊天数/客户主动发起的聊天数比例，不包括群聊，仅在确有聊天时返回。
        /// </summary>
        public decimal reply_percentage { get; set; }
        /// <summary>
        /// 平均首次回复时长，单位为分钟，即客户主动发起聊天后，成员在一个自然日内首次回复的时长间隔为首次回复时长，所有聊天的首次回复总时长/已回复的聊天总数即为平均首次回复时长，不包括群聊，仅在确有聊天时返回。
        /// </summary>
        public int avg_reply_time { get; set; }
        /// <summary>
        /// 删除/拉黑成员的客户数，即将成员删除或加入黑名单的客户数。
        /// </summary>
        public int negative_feedback_cnt { get; set; }
        /// <summary>
        /// 发起申请数，成员通过「搜索手机号」、「扫一扫」、「从微信好友中添加」、「从群聊中添加」、「添加共享、分配给我的客户」、「添加单向、双向删除好友关系的好友」、「从新的联系人推荐中添加」等渠道主动向客户发起的好友申请数量。
        /// </summary>
        public int new_apply_cnt { get; set; }
        /// <summary>
        /// 新增客户数，成员新添加的客户数量。
        /// </summary>
        public int new_contact_cnt { get; set; }
    }
}
