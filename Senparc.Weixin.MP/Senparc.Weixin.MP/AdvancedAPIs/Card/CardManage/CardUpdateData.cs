/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：CardUpdateData.cs
    文件功能描述：卡券更新需要的数据
    
    
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
    /// <summary>
    /// 通用券数据
    /// </summary>
    public class Card_GeneralCouponUpdateData : BaseUpdateInfo
    {

    }

    /// <summary>
    /// 团购券数据
    /// </summary>
    public class Card_GrouponUpdateData : BaseUpdateInfo
    {

    }

    /// <summary>
    /// 礼品券数据
    /// </summary>
    public class Card_GiftUpdateData : BaseUpdateInfo
    {
        public Update_AdvancedCardInfo advanced_info { get; set; }

        public Card_GiftUpdateData() 
        {
            advanced_info = new Update_AdvancedCardInfo();
        }
  
    }

    /// <summary>
    /// 代金券数据
    /// </summary>
    public class Card_CashUpdateData : BaseUpdateInfo
    {
        public Update_AdvancedCardInfo advanced_info { get; set; }

        public Card_CashUpdateData() 
        {
            advanced_info = new Update_AdvancedCardInfo();
        }
    }

    /// <summary>
    /// 折扣券数据
    /// </summary>
    public class Card_DisCountUpdateData : BaseUpdateInfo
    {

    }


    /// <summary>
    /// 会员卡数据
    /// </summary>
    public class Card_MemberCardUpdateData : BaseUpdateInfo
    {
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
        /// 非必填
        /// </summary>
        public string prerogative { get; set; }

        /// <summary>
        /// 自定义会员信息类目，会员卡激活后显示。
        /// 非必填
        /// </summary>
        public CustomField custom_field1 { get; set; }

        /// <summary>
        /// 自定义会员信息类目，会员卡激活后显示。
        /// 非必填
        /// </summary>
        public CustomField custom_field2 { get; set; }
        /// <summary>
        /// 自定义会员信息类目，会员卡激活后显示。
        /// 非必填
        /// </summary>
        public CustomField custom_field3 { get; set; }
        /// <summary>
        /// 自定义会员信息类目，会员卡激活后显示
        /// 非必填
        /// </summary>
        public CustomCell custom_cell1 { get; set; }

        /// <summary>
        /// 积分余额变动消息类型
        /// </summary>
        public Modify_Msg_Operation modify_msg_operation { get; set; }
    }

    /// <summary>
    /// 门票数据
    /// </summary>
    public class Card_ScenicTicketUpdateData : BaseUpdateInfo
    {
        /// <summary>
        /// 导览图url
        /// 非必填
        /// </summary>
        public string guide_url { get; set; }
    }

    /// <summary>
    /// 电影票数据
    /// </summary>
    public class Card_MovieTicketUpdateData : BaseUpdateInfo
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
    public class Card_BoardingPassUpdateData : BaseUpdateInfo
    {
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
        /// 登机口。如发生登机口变更，建议商家实时调用该接口变更
        /// </summary>
        public string gate { get; set; }
        /// <summary>
        /// 登机时间，只显示“时分”不显示日期，按时间戳格式填写。如发生登机时间变更，建议商家实时调用该接口变更
        /// </summary>
        public string boarding_time { get; set; }
    }
}
