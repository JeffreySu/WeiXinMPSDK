using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
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
    public class Card_GeneralCouponResult : BaseInfoResult
    {
        /// <summary>
        /// 描述文本
        /// </summary>
        public string default_detail { get; set; }
    }
    /// <summary>
    /// 团购券数据
    /// </summary>
    public class Card_GrouponResult : BaseInfoResult
    {
        /// <summary>
        /// 团购券专用，团购详情
        /// </summary>
        public string deal_detail { get; set; }
    }
    /// <summary>
    /// 礼品券数据
    /// </summary>
    public class Card_GiftResult : BaseInfoResult
    {
        /// <summary>
        /// 礼品券专用，表示礼品名字
        /// </summary>
        public string gift { get; set; }
    }
    /// <summary>
    /// 代金券数据
    /// </summary>
    public class Card_CashResult : BaseInfoResult
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
    public class Card_DisCountResult : BaseInfoResult
    {
        /// <summary>
        ///折扣券专用，表示打折额度（百分比）。填30 就是七折。
        /// </summary>
        public float discount { get; set; }
    }
    /// <summary>
    /// 会员卡数据
    /// </summary>
    public class Card_MemberCardResult : BaseInfoResult
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
    public class Card_ScenicTicketResult : BaseInfoResult
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
    public class Card_MovieTicketResult : BaseInfoResult
    {
        /// <summary>
        /// 电影票详请
        /// </summary>
        public string detail { get; set; }
    }
    /// <summary>
    /// 飞机票数据
    /// </summary>
    public class Card_BoardingPassResult : BaseInfoResult
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
    public class Card_LuckyMoneyResult : BaseInfoResult
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
    public class BaseInfoResult
    {
        /// <summary>
        /// 卡券Id
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 卡券的商户logo
        /// </summary>
        public string logo_url { get; set; }
        /// <summary>
        /// code 码展示类型
        /// </summary>
        public string code_type { get; set; }
        /// <summary>
        /// 商户名字
        /// </summary>
        public string brand_name { get; set; }
        /// <summary>
        /// 券名
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 券名的副标题
        /// </summary>
        public string sub_title { get; set; }
        /// <summary>
        /// 券颜色。色彩规范标注值对应的色值。如#3373bb
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// 使用提醒（一句话描述，展示在首页，示例：请出示二维码核销卡券）
        /// </summary>
        public string notice { get; set; }
        /// <summary>
        /// 客服电话
        /// </summary>
        public string service_phone { get; set; }
        /// <summary>
        /// 使用说明。长文本描述，可以分行，上限为1000 个汉字。
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 每人使用次数限制
        /// </summary>
        public int use_limit { get; set; }
        /// <summary>
        /// 每人最大领取次数，不填写默认等于quantity。
        /// </summary>
        public int get_limit { get; set; }
        /// <summary>
        /// 是否自定义code 码
        /// </summary>
        public bool use_custom_code { get; set; }
        /// <summary>
        /// 是否指定用户领取
        /// </summary>
        public bool bind_openid { get; set; }
        /// <summary>
        /// 领取卡券原生页面是否可分享
        /// </summary>
        public bool can_share { get; set; }
        /// <summary>
        /// 卡券是否可转赠
        /// 非必填
        /// </summary>
        public bool can_give_friend { get; set; }
        /// <summary>
        /// 门店位置ID
        /// 非必填
        /// </summary>
        public string location_id_list { get; set; }
        /// <summary>
        /// 使用日期，有效期的信息
        /// 必填
        /// </summary>
        public CardCreate_DateInfo date_info { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public CardCreate_Sku sku { get; set; }
        /// <summary>
        /// 商户自定义cell 名称
        /// 非必填
        /// </summary>
        public string url_name_type { get; set; }
        /// <summary>
        /// 商户自定义url 地址，支持卡券页内跳转,跳转页面内容需与自定义cell 名称保持一致。
        /// 非必填
        /// </summary>
        public string custom_url { get; set; }
        /// <summary>
        /// 第三方来源名，例如同程旅游、格瓦拉
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 1：待审核，2：审核失败，3：通过审核， 4：已删除（飞机票的status 字段为1：正常2：已删除）
        /// </summary>
        public string status { get; set; }
    }

    #endregion
}
