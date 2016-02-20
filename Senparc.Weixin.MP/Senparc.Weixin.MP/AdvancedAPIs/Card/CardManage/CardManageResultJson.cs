/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：CardManageResultJson.cs
    文件功能描述：管理卡券返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 删除卡券返回结果
    /// </summary>
    public class CardDeleteResultJson : WxJsonResult
    {
    }

    /// <summary>
    /// 查询code返回结果
    /// </summary>
    public class CardGetResultJson : WxJsonResult
    {
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
        public Get_Card card { get; set; }
    }

    public class Get_Card
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// 起始使用时间
        /// </summary>
        public string begin_time { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string end_time { get; set; }
    }

    /// <summary>
    /// 批量查询卡列表返回结果
    /// </summary>
    public class CardBatchGetResultJson : WxJsonResult
    {
        public List<string> card_id_list { get; set; }
        public int total_num { get; set; }
    }

    /// <summary>
    /// 查询卡券详情返回结果
    /// </summary>
    public class CardDetailGetResultJson : WxJsonResult
    {
        public CardDetail card { get; set; }
    }

    public class CardDetail : BaseCardDetails
    {
        /// <summary>
        /// 卡券类型
        /// </summary>
        public string card_type { get; set; }
    }

    /// <summary>
    /// 会员卡交易返回结果
    /// </summary>
    public class MemberCardDeal : WxJsonResult
    {
        /// <summary>
        /// 当前用户积分总额
        /// </summary>
        public decimal result_bonus { get; set; }
        /// <summary>
        /// 当前用户预存总金额
        /// </summary>
        public decimal result_balance { get; set; }
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
    }

    /// <summary>
    /// 获取用户已领取卡券返回结果
    /// </summary>
    public class GetCardListResult : WxJsonResult
    {
        /// <summary>
        /// 卡券列表
        /// </summary>
        public List<CardListItem> card_list { get; set; }
    }

    public class CardListItem
    {
        public string code { get; set; }
        public string card_id { get; set; }
    }

    /// <summary>
    /// 更新会员信息返回结果
    /// </summary>
    public class UpdateUserResult : WxJsonResult
    {
        /// <summary>
        /// 当前用户积分总额。
        /// </summary>
        public int result_bonus { get; set; }
        /// <summary>
        /// 当前用户预存总金额。
        /// </summary>
        public int result_balance { get; set; }
        /// <summary>
        /// 用户openid。
        /// </summary>
        public string openid { get; set; }
    }

    /// <summary>
    /// 图文消息群发卡券返回结果
    /// </summary>
    public class GetHtmlResult : WxJsonResult
    {
        /// <summary>
        /// 返回一段html代码，可以直接嵌入到图文消息的正文里。即可以把这段代码嵌入到上传图文消息素材接口中的content字段里。
        /// </summary>
        public string content { get; set; }
    }
}
