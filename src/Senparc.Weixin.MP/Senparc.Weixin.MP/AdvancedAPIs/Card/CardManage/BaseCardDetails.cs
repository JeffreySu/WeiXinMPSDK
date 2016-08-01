/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：BaseCardDetails.cs
    文件功能描述：卡券详情
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150323
    修改描述：添加新卡券类型：会议门票
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 卡券详情
    /// </summary>
    public class BaseCardDetails
    {
        /// <summary>
        /// 通用券
        /// </summary>
        public Card_GeneralCouponResult general_coupon { get; set; }
        /// <summary>
        /// 团购券
        /// </summary>
        public Card_GrouponResult groupon { get; set; }
        /// <summary>
        /// 礼品券
        /// </summary>
        public Card_GiftResult gift { get; set; }
        /// <summary>
        /// 代金券
        /// </summary>
        public Card_CashResult cash { get; set; }
        /// <summary>
        /// 折扣券
        /// </summary>
        public Card_DisCountResult discount { get; set; }
        /// <summary>
        /// 会员卡
        /// </summary>
        public Card_MemberCardResult member_card { get; set; }
        /// <summary>
        /// 门票
        /// </summary>
        public Card_ScenicTicketResult scenic_ticket { get; set; }
        /// <summary>
        /// 电影票
        /// </summary>
        public Card_MovieTicketResult movie_ticket { get; set; }
        /// <summary>
        /// 飞机票
        /// </summary>
        public Card_BoardingPassResult boarding_pass { get; set; }
        /// <summary>
        /// 红包
        /// </summary>
        public Card_LuckyMoneyResult lucky_money { get; set; }
    }

    /// <summary>
    /// 通用券数据
    /// </summary>
    public class Card_GeneralCouponResult : BaseCardInfoResult
    {
        /// <summary>
        /// 描述文本
        /// </summary>
        public string default_detail { get; set; }
    }
    /// <summary>
    /// 团购券数据
    /// </summary>
    public class Card_GrouponResult : BaseCardInfoResult
    {
        /// <summary>
        /// 团购券专用，团购详情
        /// </summary>
        public string deal_detail { get; set; }
    }
    /// <summary>
    /// 礼品券数据
    /// </summary>
    public class Card_GiftResult : BaseCardInfoResult
    {
        /// <summary>
        /// 礼品券专用，表示礼品名字
        /// </summary>
        public string gift { get; set; }
    }
    /// <summary>
    /// 代金券数据
    /// </summary>
    public class Card_CashResult : BaseCardInfoResult
    {
        /// <summary>
        /// 代金券专用，表示起用金额（单位为分）
        /// </summary>
        public decimal least_cost { get; set; }
        /// <summary>
        /// 代金券专用，表示减免金额（单位为分）
        /// </summary>
        public decimal reduce_cost { get; set; }
    }
    /// <summary>
    /// 折扣券数据
    /// </summary>
    public class Card_DisCountResult : BaseCardInfoResult
    {
        /// <summary>
        ///折扣券专用，表示打折额度（百分比）。填30 就是七折。
        /// </summary>
        public float discount { get; set; }
    }
    /// <summary>
    /// 会员卡数据
    /// </summary>
    public class Card_MemberCardResult : BaseCardInfoResult
    {
        /// <summary>
        /// 是否支持积分，填写true 或false，如填写true，积分相关字段均为必填。填写false，积分字段无需填写。储值字段处理方式相同。
        /// </summary>
        public bool supply_bonus { get; set; }
        /// <summary>
        /// 是否支持储值，填写true 或false。
        /// </summary>
        public bool supply_balance { get; set; }
        /// <summary>
        /// 积分清零规则
        /// </summary>
        public string bonus_cleared { get; set; }
        /// <summary>
        /// 积分规则
        /// </summary>
        public string bonus_rules { get; set; }
        /// <summary>
        /// 储值说明
        /// </summary>
        public string balance_rules { get; set; }
        /// <summary>
        /// 特权说明
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
    public class Card_ScenicTicketResult : BaseCardInfoResult
    {
        /// <summary>
        /// 票类型，例如平日全票，套票等
        /// </summary>
        public string ticket_class { get; set; }
        /// <summary>
        /// 导览图url
        /// </summary>
        public string guide_url { get; set; }
    }
    /// <summary>
    /// 电影票数据
    /// </summary>
    public class Card_MovieTicketResult : BaseCardInfoResult
    {
        /// <summary>
        /// 电影票详请
        /// </summary>
        public string detail { get; set; }
    }
    /// <summary>
    /// 飞机票数据
    /// </summary>
    public class Card_BoardingPassResult : BaseCardInfoResult
    {
        /// <summary>
        /// 起点，上限为18 个汉字
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// 终点，上限为18 个汉字
        /// </summary>
        public string to { get; set; }
        /// <summary>
        /// 航班
        /// </summary>
        public string flight { get; set; }
        /// <summary>
        /// 起飞时间，上限为17 个汉字
        /// </summary>
        public string departure_time { get; set; }
        /// <summary>
        /// 降落时间，上限为17 个汉字
        /// </summary>
        public string landing_time { get; set; }
        /// <summary>
        /// 在线值机的链接
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
        /// </summary>
        public string air_model { get; set; }
    }
    /// <summary>
    /// 红包数据
    /// </summary>
    public class Card_LuckyMoneyResult : BaseCardInfoResult
    {
    }

    public class BaseCardInfoResult
    {
        /// <summary>
        /// 基本的卡券数据
        /// </summary>
        public BaseInfoResult base_info { get; set; }
    }

    #region 基本的卡券数据，所有卡券通用(BaseInfoResult)
    /// <summary>
    /// 基本的卡券数据，所有卡券通用。
    /// </summary>
    public class BaseInfoResult : Card_BaseInfoBase
    {
        /// <summary>
        /// 卡券Id
        /// </summary>
        public string id { get; set; }

        public string status { get; set; }
    }

    #endregion
}
