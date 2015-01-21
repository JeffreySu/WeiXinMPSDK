using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 通用券数据
    /// </summary>
    public class Card_GeneralCouponData : BaseCardInfo
    {
        /// <summary>
        /// 描述文本
        /// 必填
        /// </summary>
        public string default_detail { get; set; }
    }
    /// <summary>
    /// 团购券数据
    /// </summary>
    public class Card_GrouponData : BaseCardInfo
    {
        /// <summary>
        /// 团购券专用，团购详情
        /// 必填
        /// </summary>
        public string deal_detail { get; set; }
    }
    /// <summary>
    /// 礼品券数据
    /// </summary>
    public class Card_GiftData : BaseCardInfo
    {
        /// <summary>
        /// 礼品券专用，表示礼品名字
        /// 必填
        /// </summary>
        public string gift { get; set; }
    }
    /// <summary>
    /// 代金券数据
    /// </summary>
    public class Card_CashData : BaseCardInfo
    {
        /// <summary>
        /// 代金券专用，表示起用金额（单位为分）
        /// 非必填
        /// </summary>
        public decimal least_cost { get; set; }
        /// <summary>
        /// 代金券专用，表示减免金额（单位为分）
        /// 必填
        /// </summary>
        public decimal reduce_cost { get; set; }
    }
    /// <summary>
    /// 折扣券数据
    /// </summary>
    public class Card_DisCountData : BaseCardInfo
    {
        /// <summary>
        ///折扣券专用，表示打折额度（百分比）。填30 就是七折。
        /// 必填
        /// </summary>
        public float discount { get; set; }
    }
    /// <summary>
    /// 会员卡数据
    /// </summary>
    public class Card_MemberCardData : BaseCardInfo
    {
        /// <summary>
        /// 是否支持积分，填写true 或false，如填写true，积分相关字段均为必填。填写false，积分字段无需填写。储值字段处理方式相同。
        /// 必填
        /// </summary>
        public bool supply_bonus { get; set; }
        /// <summary>
        /// 是否支持储值，填写true 或false。
        /// 必填
        /// </summary>
        public bool supply_balance { get; set; }
        /// <summary>
        /// 积分清零规则
        /// 非必填
        /// </summary>
        public string bonus_cleared { get; set; }
        /// <summary>
        /// 积分规则
        /// 非必填
        /// </summary>
        public string bonus_rules { get; set; }
        /// <summary>
        /// 储值说明
        /// 非必填
        /// </summary>
        public string balance_rules { get; set; }
        /// <summary>
        /// 特权说明
        /// 必填
        /// </summary>
        public string prerogative { get; set; }
        /// <summary>
        /// 绑定旧卡的url，与“activate_url”字段二选一必填。
        /// </summary>
        public string bind_old_card_url { get; set; }
        /// <summary>
        /// 激活会员卡的url，与“bind_old_card_url”字段二选一必填。
        /// </summary>
        public string activate_url { get; set; }
    }
    /// <summary>
    /// 门票数据
    /// </summary>
    public class Card_ScenicTicketData : BaseCardInfo
    {
        /// <summary>
        /// 票类型，例如平日全票，套票等
        /// 非必填
        /// </summary>
        public string ticket_class { get; set; }
        /// <summary>
        /// 导览图url
        /// 非必填
        /// </summary>
        public string guide_url { get; set; }
    }
    /// <summary>
    /// 电影票数据
    /// </summary>
    public class Card_MovieTicketData : BaseCardInfo
    {
        /// <summary>
        /// 电影票详请
        /// 非必填
        /// </summary>
        public string detail { get; set; }
    }
    /// <summary>
    /// 飞机票数据
    /// </summary>
    public class Card_BoardingPassData : BaseCardInfo
    {
        /// <summary>
        /// 起点，上限为18 个汉字
        /// 必填
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// 终点，上限为18 个汉字
        /// 必填
        /// </summary>
        public string to { get; set; }
        /// <summary>
        /// 航班
        /// 必填
        /// </summary>
        public string flight { get; set; }
        /// <summary>
        /// 起飞时间，上限为17 个汉字
        /// 非必填
        /// </summary>
        public string departure_time { get; set; }
        /// <summary>
        /// 降落时间，上限为17 个汉字
        /// 非必填
        /// </summary>
        public string landing_time { get; set; }
        /// <summary>
        /// 在线值机的链接
        /// 非必填
        /// </summary>
        public string check_in_url { get; set; }
        /// <summary>
        /// 登机口。如发生登机口变更，建议商家实时调用该接口变更
        /// </summary>
        public string gate { get; set; }
        /// <summary>
        /// 登机时间，只显示“时分”不显示日期，按时间戳格式填写。如发生登机时间变更，建议商家实时调用该接口变更
        /// </summary>
        public string boarding_time { get; set; }
        /// <summary>
        /// 机型，上限为8 个汉字
        /// 非必填
        /// </summary>
        public string air_model { get; set; }
    }
    /// <summary>
    /// 红包数据
    /// </summary>
    public class Card_LuckyMoneyData : BaseCardInfo
    {
    }
}
