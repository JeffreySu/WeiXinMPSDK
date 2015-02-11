using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 卡券信息数据中的card字段
    /// </summary>
    public class CardCreateInfo_Card
    {
        public CardType card_type { get; set; }
    }


    #region 不同卡券类型对应的信息

    /// <summary>
    /// 通用券
    /// </summary>
    public class Card_GeneralCoupon : CardCreateInfo_Card
    {
        public Card_GeneralCouponData general_coupon { get; set; }
    }

    /// <summary>
    /// 团购券
    /// </summary>
    public class Card_Groupon : CardCreateInfo_Card
    {
        public Card_GrouponData groupon { get; set; }
    }

    /// <summary>
    /// 礼品券
    /// </summary>
    public class Card_Gift : CardCreateInfo_Card
    {
        public Card_GiftData gift { get; set; }
    }

    /// <summary>
    /// 代金券
    /// </summary>
    public class Card_Cash : CardCreateInfo_Card
    {
        public Card_CashData cash { get; set; }
    }

    /// <summary>
    /// 折扣券
    /// </summary>
    public class Card_DisCount : CardCreateInfo_Card
    {
        public Card_DisCountData discount { get; set; }
    }

    /// <summary>
    /// 会员卡
    /// </summary>
    public class Card_MemberCard : CardCreateInfo_Card
    {
        public Card_MemberCardData member_card { get; set; }
    }

    /// <summary>
    /// 门票
    /// </summary>
    public class Card_ScenicTicket : CardCreateInfo_Card
    {
        public Card_ScenicTicketData scenic_ticket { get; set; }
    }

    /// <summary>
    /// 电影票
    /// </summary>
    public class Card_MovieTicket : CardCreateInfo_Card
    {
        public Card_MovieTicketData movie_ticket { get; set; }
    }

    /// <summary>
    /// 飞机票
    /// </summary>
    public class Card_BoardingPass : CardCreateInfo_Card
    {
        public Card_BoardingPassData boarding_pass { get; set; }
    }

    /// <summary>
    /// 红包
    /// </summary>
    public class Card_LuckyMoney : CardCreateInfo_Card
    {
        public Card_LuckyMoneyData lucky_money { get; set; }
    }

    #endregion


    public abstract class BaseCardInfo
    {
        /// <summary>
        /// 基本的卡券数据
        /// </summary>
        public Card_BaseInfoBase base_info { get; set; }
        /// <summary>
        /// 卡类型（不在Json数据中）
        /// </summary>
        public CardType CardType { get; set; }

        public BaseCardInfo(CardType cardType)
        {
            CardType = cardType;
        }
    }
}
