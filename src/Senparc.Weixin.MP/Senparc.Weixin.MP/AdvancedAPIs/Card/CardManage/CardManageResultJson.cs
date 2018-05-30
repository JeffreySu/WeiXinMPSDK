#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：CardManageResultJson.cs
    文件功能描述：管理卡券返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20171127
    修改描述：v14.8.7 完善CardGetResultJson字段

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
    /// 查询code返回结果,check_consume=false 的结果。
    /// </summary>
    public class CardGetResultJson : WxJsonResult
    {
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
        //can_consume
        public bool can_consume { get; set; }
        public UserCardStatus user_card_status { get; set; }
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
    public class MemberCardDealResultJson : WxJsonResult
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
    public class GetCardListResultJson : WxJsonResult
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
    public class UpdateUserResultJson : WxJsonResult
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
    public class GetHtmlResultJson : WxJsonResult
    {
        /// <summary>
        /// 返回一段html代码，可以直接嵌入到图文消息的正文里。即可以把这段代码嵌入到上传图文消息素材接口中的content字段里。
        /// </summary>
        public string content { get; set; }
    }
    /// <summary>
    /// 拉取卡券返回结果
    /// </summary>
    public class GetCardBizuinInfoResultJson : WxJsonResult
    {
        public List<GetCardBizuinInfo_List> list { get; set; }
    }

    public class GetCardBizuinInfo_List
    {
        /// <summary>
        /// 日期信息
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int view_cnt { get; set; }
        /// <summary>
        /// 浏览人数
        /// </summary>
        public int view_user { get; set; }
        /// <summary>
        /// 领取次数
        /// </summary>
        public int receive_cnt { get; set; }
        /// <summary>
        /// 领取人数
        /// </summary>
        public int receive_user { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public int verify_cnt { get; set; }
        /// <summary>
        /// 使用人数
        /// </summary>
        public int verify_user { get; set; }
        /// <summary>
        /// 转赠次数
        /// </summary>
        public int given_cnt { get; set; }
        /// <summary>
        /// 转赠人数
        /// </summary>
        public int given_user { get; set; }
        /// <summary>
        /// 过期次数
        /// </summary>
        public int expire_cnt { get; set; }
        /// <summary>
        /// 过期人数
        /// </summary>
        public int expire_user { get; set; }
    }

    public class GetCardInfoResultJson : WxJsonResult
    {
        public List<GetCardInfoItem> GetCardInfo { get; set; }
    }

    public class GetCardInfoItem
    {
        /// <summary>
        /// 日期信息
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }
        /// <summary>
        /// cardtype:0：折扣券，1：代金券，2：礼品券，3：优惠券，4：团购券（暂不支持拉取特殊票券类型数据，电影票、飞机票、会议门票、景区门票）
        /// </summary>
        public int card_type { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int view_cnt { get; set; }
        /// <summary>
        /// 浏览人数
        /// </summary>
        public int view_user { get; set; }
        /// <summary>
        /// 领取次数
        /// </summary>
        public int receive_cnt { get; set; }
        /// <summary>
        /// 领取人数
        /// </summary>
        public int receive_user { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public int verify_cnt { get; set; }
        /// <summary>
        /// 使用人数
        /// </summary>
        public int verify_user { get; set; }
        /// <summary>
        /// 转赠次数
        /// </summary>
        public int given_cnt { get; set; }
        /// <summary>
        /// 转赠人数
        /// </summary>
        public int given_user { get; set; }
        /// <summary>
        /// 过期次数
        /// </summary>
        public int expire_cnt { get; set; }
        /// <summary>
        /// 过期人数
        /// </summary>
        public int expire_user { get; set; }

    }

    public class GetCardMemberCardInfoResultJson : WxJsonResult
    {
        public List<GetCardMemberCardInfoItem> GetCardMemberCardInfo { get; set; }
     }

    public class GetCardMemberCardInfoItem
    {
        /// <summary>
        /// 日期信息
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 浏览次数
        /// </summary>
        public int view_cnt { get; set; }
        /// <summary>
        /// 浏览人数
        /// </summary>
        public int view_user { get; set; }
        /// <summary>
        /// 领取次数
        /// </summary>
        public int receive_cnt { get; set; }
        /// <summary>
        /// 领取人数
        /// </summary>
        public int receive_user { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public int active_user { get; set; }
        /// <summary>
        /// 使用人数
        /// </summary>
        public int verify_cnt { get; set; }
        /// <summary>
        /// 激活人数
        /// </summary>
        public int verify_user { get; set; }
        /// <summary>
        /// 有效会员总人数
        /// </summary>
        public int total_user { get; set; }
        /// <summary>
        /// 历史领取会员卡总人数
        /// </summary>
        public int total_receive_user { get; set; }
    }
}
