/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：BaseCardUpdateInfo.cs
    文件功能描述：更新卡券的信息部分
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    public class BaseCardUpdateInfo
    {
        /// <summary>
        /// 卡券信息部分
        /// </summary>
        public string card_id { get; set; }
    }

    /// <summary>
    /// 通用券
    /// </summary>
    public class CardUpdate_GeneralCoupon : BaseCardUpdateInfo
    {
        public Card_GeneralCouponUpdateData general_coupon { get; set; }
    }

    /// <summary>
    /// 团购券
    /// </summary>
    public class CardUpdate_Groupon : BaseCardUpdateInfo
    {
        public Card_GrouponUpdateData groupon { get; set; }
    }

    /// <summary>
    /// 礼品券
    /// </summary>
    public class CardUpdate_Gift : BaseCardUpdateInfo
    {
        public Card_GiftUpdateData gift { get; set; }
    }

    /// <summary>
    /// 代金券
    /// </summary>
    public class CardUpdate_Cash : BaseCardUpdateInfo
    {
        public Card_CashUpdateData cash { get; set; }
    }

    /// <summary>
    /// 折扣券
    /// </summary>
    public class CardUpdate_DisCount : BaseCardUpdateInfo
    {
        public Card_DisCountUpdateData discount { get; set; }
    }


    /// <summary>
    /// 会员卡
    /// </summary>
    public class CardUpdate_MemberCard : BaseCardUpdateInfo
    {
        public Card_MemberCardUpdateData member_card { get; set; }
    }

    /// <summary>
    /// 门票
    /// </summary>
    public class CardUpdate_ScenicTicket : BaseCardUpdateInfo
    {
        public Card_ScenicTicketData scenic_ticket { get; set; }
    }

    /// <summary>
    /// 电影票
    /// </summary>
    public class CardUpdate_MovieTicket : BaseCardUpdateInfo
    {
        public Card_MovieTicketData movie_ticket { get; set; }
    }

    /// <summary>
    /// 飞机票
    /// </summary>
    public class CardUpdate_BoardingPass : BaseCardUpdateInfo
    {
        public Card_BoardingPassData boarding_pass { get; set; }
    }

    public class BaseUpdateInfo
    {
        public BaseUpdateInfo() {
            base_info = new Update_BaseCardInfo();
        }
        /// <summary>
        /// 基本的卡券数据
        /// </summary>
        public Update_BaseCardInfo base_info { get; set; }
    }

    #region 基本的卡券数据，所有卡券通用(相当于BaseInfo)

    /// <summary>
    /// 基本的卡券数据，所有卡券通用。
    /// </summary>
    public class Update_BaseCardInfo
    {
        public Update_BaseCardInfo() {
            date_info = new Card_UpdateDateInfo();
        }

        /// <summary>
        /// 卡券的商户logo，尺寸为300*300。
        /// 必填
        /// </summary>
        public string logo_url { get; set; }

        /// <summary>
        /// 券颜色。按色彩规范标注填写Color010-Color100
        /// 必填
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// 使用提醒，字数上限为9 个汉字。（一句话描述，展示在首页，示例：请出示二维码核销卡券）
        /// 非必填
        /// </summary>
        public string notice { get; set; }

        /// <summary>
        /// 客服电话
        /// 非必填
        /// </summary>
        public string service_phone { get; set; }

        /// <summary>
        /// 使用说明。长文本描述，可以分行，上限为1000 个汉字。
        /// 必填
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// code 码展示类型
        /// 必填
        /// </summary>
        public Card_CodeType code_type { get; set; }

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
        public Card_UpdateDateInfo date_info { get; set; }
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
        /// <summary>
        /// 显示在入口右侧的提示语。
        /// </summary>
        public string custom_url_name { get; set; }

        /// <summary>
        /// 自定义跳转外链的入口名字。详情见活用自定义入口
        /// </summary>
        public string custom_url_sub_title { get; set; }

        /// <summary>
        /// 入口跳转外链的地址链接
        /// </summary>
        public string promotion_url { get; set; }

        /// <summary>
        /// 营销场景的自定义入口名称
        /// </summary>
        public string promotion_url_name { get; set; }

        /// <summary>
        /// 显示在营销入口右侧的提示语。
        /// </summary>
        public string promotion_url_sub_title { get; set; }
    }
    /// <summary>
    /// 使用日期，有效期的信息
    /// </summary>
    public class Card_UpdateDateInfo
    {
        /// <summary>
        /// 有效期类型，仅支持更改type为DATE_TYPE_FIX_TIME_RANGE 的时间戳，不支持填入DATE_TYPE_FIX_TERM。
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 固定日期区间专用，表示起用时间。从1970 年1 月1 日00:00:00 至起用时间的秒数，最终需转换为字符串形态传入，下同。（单位为秒）
        /// 非必填
        /// </summary>
        public string begin_timestamp { get; set; }
        /// <summary>
        /// 固定日期区间专用，表示结束时间。（单位为秒）
        /// 非必填
        /// </summary>
        public string end_timestamp { get; set; }
    }

    #endregion
}
