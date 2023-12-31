/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：GetUserLivingRequest.cs
    文件功能描述：获取指定成员的所有直播ID 接口请求参数
    
    
    创建标识：WangDrama - 20210630

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Work.AdvancedAPIs.Living.LivingJson
{
    /// <summary>
    /// 获取指定成员的所有直播ID 接口请求参数
    /// </summary>
    public class GetUserLivingRequest
    {
        /// <summary>
        /// 企业成员的userid。必填：是	
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 上一次调用时返回的next_cursor，第一次拉取可以不填。必填：否
        /// </summary>
        public string cursor { get; set; }
        /// <summary>
        /// 每次拉取的数据量，默认值和最大值都为100，必填：否
        /// </summary>
        public int limit { get; set; } = 100;

    }

    public class GetUserLivingResponse : WorkJsonResult
    {
        /// <summary>
        /// 当前数据最后一个key值，如果下次调用带上该值则从该key值往后拉，用于实现分页拉取，返回空字符串代表已经是最后一页
        /// </summary>
        public string next_cursor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string[] livingid_list { get; set; }

    }

    public class GetUserLivingInfoResponse : WorkJsonResult
    {
        public GetUserLivingInfoResult living_info { get; set; }
    }

    public class GetUserLivingInfoResult
    {

        /// <summary>
        /// 直播主题
        /// </summary>
        public string theme { get; set; }
        /// <summary>
        /// 直播开始时间戳
        /// </summary>
        public long living_start { get; set; }
        /// <summary>
        /// 直播时长，单位为秒
        /// </summary>
        public int living_duration { get; set; }
        /// <summary>
        /// 直播的状态，0：预约中，1：直播中，2：已结束，3：已过期，4：已取消
        /// </summary>
        public short status { get; set; }
        /// <summary>
        /// 直播预约的开始时间戳
        /// </summary>
        public long reserve_start { get; set; }
        /// <summary>
        /// 直播预约时长，单位为秒
        /// </summary>
        public int reserve_living_duration { get; set; }
        /// <summary>
        /// 直播的描述，最多支持100个汉字
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 主播的userid
        /// </summary>
        public string anchor_userid { get; set; }
        /// <summary>
        /// 主播所在主部门id
        /// </summary>
        public int main_department { get; set; }
        /// <summary>
        /// 观看直播总人数
        /// </summary>
        public int viewer_num { get; set; }
        /// <summary>
        /// 评论数
        /// </summary>
        public int comment_num { get; set; }
        /// <summary>
        /// 连麦发言人数
        /// </summary>
        public int mic_num { get; set; }
        /// <summary>
        /// 是否开启回放，1表示开启，0表示关闭
        /// </summary>
        public short open_replay { get; set; }
        /// <summary>
        /// open_replay为1时才返回该字段。0表示生成成功，1表示生成中，2表示回放已删除，3表示生成失败
        /// </summary>
        public short replay_status { get; set; }
        /// <summary>
        /// 直播的类型，0：通用直播，1：小班课，2：大班课，3：企业培训，4：活动直播
        /// </summary>
        public short type { get; set; }
        /// <summary>
        /// 推流地址，仅直播类型为活动直播并且直播状态是待开播返回该字段
        /// </summary>
        public string push_stream_url { get; set; }
        /// <summary>
        /// 当前在线观看人数
        /// </summary>
        public int online_count { get; set; }

        /// <summary>
        /// 直播预约人数
        /// </summary>
        public int subscribe_count { get; set; }

    }



    public class GetUserLivingWatchStateResponse : WorkJsonResult
    {

        /// <summary>
        /// 是否结束。0：表示还有更多数据，需要继续拉取，1：表示已经拉取完所有数据。注意只能根据该字段判断是否已经拉完数据
        /// </summary>
        public short ending { get; set; }

        /// <summary>
        /// 当前数据最后一个key值，如果下次调用带上该值则从该key值往后拉，用于实现分页拉取
        /// </summary>
        public string next_key { get; set; }

        public GetLivingStateInfo stat_info { get; set; }
    }


    public class GetLivingStateInfo
    {
        public List<GetLivingStateUserInfo> users { get; set; }
        public List<GetLivingStateExternalUserInfo> external_users { get; set; }

    }
    public class GetLivingStateUserInfo
    {
        /// <summary>
        /// 企业成员的userid
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 观看时长，单位为秒
        /// </summary>
        public int watch_time { get; set; }

        /// <summary>
        /// 是否评论。0-否；1-是
        /// </summary>
        public short is_comment { get; set; }
        /// <summary>
        /// 是否连麦发言。0-否；1-是
        /// </summary>
        public short is_mic { get; set; }
    }
    public class GetLivingStateExternalUserInfo
    {
        /// <summary>
        /// 外部成员的userid
        /// </summary>
        public string external_userid { get; set; }

        /// <summary>
        /// 外部成员类型，1表示该外部成员是微信用户，2表示该外部成员是企业微信用户
        /// </summary>
        public short type { get; set; }

        /// <summary>
        /// 外部成员的名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 观看时长，单位为秒
        /// </summary>
        public int watch_time { get; set; }

        /// <summary>
        /// 是否评论。0-否；1-是
        /// </summary>
        public short is_comment { get; set; }
        /// <summary>
        /// 是否连麦发言。0-否；1-是
        /// </summary>
        public short is_mic { get; set; }
    }
}
