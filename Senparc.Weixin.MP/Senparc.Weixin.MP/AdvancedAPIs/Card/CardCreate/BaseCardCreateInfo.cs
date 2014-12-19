using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class BaseCardCreateInfo
    {
        /// <summary>
        /// 卡券信息部分
        /// </summary>
        public Card_Card card { get; set; }
    }
    
    public class Card_Card
    {
        public CardType card_type { get; set; }
    }

    /// <summary>
    /// 通用券
    /// </summary>
    public class Card_GeneralCoupon : Card_Card
    {
        public Card_GeneralCouponData general_coupon { get; set; }
    }

    /// <summary>
    /// 团购券
    /// </summary>
    public class Card_Groupon : Card_Card
    {
        public Card_GrouponData groupon { get; set; }
    }

    /// <summary>
    /// 礼品券
    /// </summary>
    public class Card_Gift : Card_Card
    {
        public Card_GiftData gift { get; set; }
    }

    /// <summary>
    /// 代金券
    /// </summary>
    public class Card_Cash : Card_Card
    {
        public Card_CashData cash { get; set; }
    }

    /// <summary>
    /// 折扣券
    /// </summary>
    public class Card_DisCount : Card_Card
    {
        public Card_DisCountData discount { get; set; }
    }

    /// <summary>
    /// 会员卡
    /// </summary>
    public class Card_MemberCard : Card_Card
    {
        public Card_MemberCardData member_card { get; set; }
    }

    /// <summary>
    /// 门票
    /// </summary>
    public class Card_ScenicTicket : Card_Card
    {
        public Card_ScenicTicketData scenic_ticket { get; set; }
    }

    /// <summary>
    /// 电影票
    /// </summary>
    public class Card_MovieTicket : Card_Card
    {
        public Card_MovieTicketData movie_ticket { get; set; }
    }

    /// <summary>
    /// 飞机票
    /// </summary>
    public class Card_BoardingPass : Card_Card
    {
        public Card_BoardingPassData boarding_pass { get; set; }
    }

    /// <summary>
    /// 红包
    /// </summary>
    public class Card_LuckyMoney : Card_Card
    {
        public Card_LuckyMoneyData lucky_money { get; set; }
    }

    public class BaseCardInfo
    {
        /// <summary>
        /// 基本的卡券数据
        /// </summary>
        public BaseInfo base_info { get; set; }
    }

    #region 基本的卡券数据，所有卡券通用(BaseInfo)
    /// <summary>
    /// 基本的卡券数据，所有卡券通用。
    /// </summary>
    public class BaseInfo
    {
        /// <summary>
        /// 卡券的商户logo，尺寸为300*300。
        /// 必填
        /// </summary>
        public string logo_url { get; set; }
        /// <summary>
        /// code 码展示类型
        /// 必填
        /// </summary>
        public Card_CodeType code_type { get; set; }
        /// <summary>
        /// 商户名字,字数上限为12 个汉字。（填写直接提供服务的商户名， 第三方商户名填写在source 字段）
        /// 必填
        /// </summary>
        public string brand_name { get; set; }
        /// <summary>
        /// 券名，字数上限为9 个汉字。(建议涵盖卡券属性、服务及金额)
        /// 必填
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 券名的副标题，字数上限为18个汉字。
        /// 非必填
        /// </summary>
        public string sub_title { get; set; }
        /// <summary>
        /// 券颜色。按色彩规范标注填写Color010-Color100
        /// 必填
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// 使用提醒，字数上限为9 个汉字。（一句话描述，展示在首页，示例：请出示二维码核销卡券）
        /// 必填
        /// </summary>
        public string notice { get; set; }
        /// <summary>
        /// 客服电话
        /// 非必填
        /// </summary>
        public string service_phone { get; set; }
        /// <summary>
        /// 第三方来源名，例如同程旅游、格瓦拉。
        /// 非必填
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 使用说明。长文本描述，可以分行，上限为1000 个汉字。
        /// 必填
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 每人使用次数限制
        /// 非必填
        /// </summary>
        public int use_limit { get; set; }
        /// <summary>
        /// 每人最大领取次数，不填写默认等于quantity。
        /// 非必填
        /// </summary>
        public int get_limit { get; set; }
        /// <summary>
        /// 是否自定义code 码。填写true或false，不填代表默认为false。
        /// 非必填
        /// </summary>
        public bool use_custom_code { get; set; }
        /// <summary>
        /// 是否指定用户领取，填写true或false。不填代表默认为否。
        /// 非必填
        /// </summary>
        public bool bind_openid { get; set; }
        /// <summary>
        /// 领取卡券原生页面是否可分享，填写true 或false，true 代表可分享。默认可分享。
        /// 非必填
        /// </summary>
        public bool can_share { get; set; }
        /// <summary>
        /// 卡券是否可转赠，填写true 或false,true 代表可转赠。默认可转赠。
        /// 非必填
        /// </summary>
        public bool can_give_friend { get; set; }
        /// <summary>
        /// 门店位置ID。商户需在mp 平台上录入门店信息或调用批量导入门店信息接口获取门店位置ID。
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
        public Card_UrlNameType url_name_type { get; set; }
        /// <summary>
        /// 商户自定义url 地址，支持卡券页内跳转,跳转页面内容需与自定义cell 名称保持一致。
        /// 非必填
        /// </summary>
        public string custom_url { get; set; }
    }
    /// <summary>
    /// 使用日期，有效期的信息
    /// </summary>
    public class CardCreate_DateInfo
    {
        /// <summary>
        /// 使用时间的类型 1：固定日期区间，2：固定时长（自领取后按天算）
        /// 必填
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 固定日期区间专用，表示起用时间。从1970 年1 月1 日00:00:00 至起用时间的秒数，最终需转换为字符串形态传入，下同。（单位为秒）
        /// 必填
        /// </summary>
        public long begin_timestamp { get; set; }
        /// <summary>
        /// 固定日期区间专用，表示结束时间。（单位为秒）
        /// 必填
        /// </summary>
        public long end_timestamp { get; set; }
        /// <summary>
        /// 固定时长专用，表示自领取后多少天内有效。（单位为天）
        /// 必填
        /// </summary>
        public int fixed_term { get; set; }
        /// <summary>
        /// 固定时长专用，表示自领取后多少天开始生效。（单位为天）
        /// 必填
        /// </summary>
        public int fixed_begin_term { get; set; }
    }
    /// <summary>
    /// 商品信息
    /// </summary>
    public class CardCreate_Sku
    {
        /// <summary>
        /// 上架的数量。（不支持填写0或无限大）
        /// 必填
        /// </summary>
        public int quantity { get; set; }
    }

    #endregion
}
