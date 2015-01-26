using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class BaseUpStreamMsgResult
    {
        /// <summary>
        /// 数据的日期，需在begin_date和end_date之间
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 消息类型，代表含义如下：1代表文字 2代表图片 3代表语音 4代表视频 6代表第三方应用消息（链接消息）        
        /// </summary>
        public int msg_type { get; set; }
        /// <summary>
        /// 上行发送了（向公众号发送了）消息的用户数
        /// </summary>
        public int msg_user { get; set; }
        /// <summary>
        /// 上行发送了消息的消息总数
        /// </summary>
        public int msg_count { get; set; }
    }

    /// <summary>
    /// 获取消息发送概况数据返回结果
    /// </summary>
    public class UpStreamMsgResultJson : WxJsonResult
    {
        public List<BaseUpStreamMsgResult> list { get; set; }
    }

    /// <summary>
    /// 获取消息分送分时数据返回结果
    /// </summary>
    public class UpStreamMsgHourResultJson : WxJsonResult
    {
        public List<UpStreamMsgHour> list { get; set; }
    }

    public class UpStreamMsgHour : BaseUpStreamMsgResult
    {
        /// <summary>
        /// 数据的小时，包括从000到2300，分别代表的是[000,100)到[2300,2400)，即每日的第1小时和最后1小时
        /// </summary>
        public int ref_hour { get; set; }
    }

    /// <summary>
    /// 获取消息发送周数据返回结果
    /// </summary>
    public class UpStreamMsgWeekResultJson : WxJsonResult
    {
        public List<BaseUpStreamMsgResult> list { get; set; }
    }

    /// <summary>
    /// 获取消息发送月数据返回结果
    /// </summary>
    public class UpStreamMsgMonthResultJson : WxJsonResult
    {
        public List<BaseUpStreamMsgResult> list { get; set; }
    }

    public class BaseUpStreamMsgDist
    {
        /// <summary>
        /// 数据的日期，需在begin_date和end_date之间
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 当日发送消息量分布的区间，0代表 “0”，1代表“1-5”，2代表“6-10”，3代表“10次以上”
        /// </summary>
        public int count_interval { get; set; }
        /// <summary>
        /// 上行发送了（向公众号发送了）消息的用户数
        /// </summary>
        public int msg_user { get; set; }
    }

    /// <summary>
    /// 获取消息发送分布数据返回结果
    /// </summary>
    public class UpStreamMsgDistResultJson : WxJsonResult
    {
        public List<BaseUpStreamMsgDist> list { get; set; }
    }

    /// <summary>
    /// 获取消息发送分布周数据返回结果
    /// </summary>
    public class UpStreamMsgDistWeekResultJson : WxJsonResult
    {
        public List<BaseUpStreamMsgDist> list { get; set; }
    }

    /// <summary>
    /// 获取消息发送分布月数据返回结果
    /// </summary>
    public class UpStreamMsgDistMonthResultJson : WxJsonResult
    {
        public List<BaseUpStreamMsgDist> list { get; set; }
    }
}
