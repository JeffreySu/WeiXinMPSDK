#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：GiftCardData.cs
    文件功能描述：礼品卡数据
    
    
    创建标识：Senparc - 20181008
    
    

----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 设置支付后投放卡券接口数据
    /// </summary>
    public class AddCardAfterPayData
    {
        public Rule_Info rule_info { get; set; }
    }

    /// <summary>
    /// 支付后营销规则结构体
    /// </summary>
    public class Rule_Info
    {
        /// <summary>
        /// 营销规则类型
        /// </summary>
        public string type { get; set; }
        public Base_Info base_info { get; set; }
        public Member_Rule member_rule { get; set; }
    }

    /// <summary>
    /// 营销规则结构体
    /// </summary>
    public class Base_Info
    {
        public string[] mchid_list { get; set; }
        public int begin_time { get; set; }
        public int end_time { get; set; }
        public string status { get; set; }
        public string create_time { get; set; }
        public string update_time { get; set; }
    }

    /// <summary>
    /// 支付即会员结构体
    /// </summary>
    public class Member_Rule
    {
        public string card_id { get; set; }
        public int least_cost { get; set; }
        public int max_cost { get; set; }
        public string jump_url { get; set; }
        public string app_brand_id { get; set; }
        public string app_brand_pass { get; set; }
    }

    /// <summary>
    /// 批量查询支付后投放卡券规则接口数据
    /// </summary>
    public class AfterPay_BatchGetData
    {
        public string type { get; set; }
        public bool effective { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
    }

    /// <summary>
    /// 增加支付即会员规则接口数据
    /// </summary>
    public class AddPayMemberRuleData
    {
        public string card_id { get; set; }
        public string jump_url { get; set; }
        public string[] mchid_list { get; set; }
        public int begin_time { get; set; }
        public int end_time { get; set; }
        public int min_cost { get; set; }
        public int max_cost { get; set; }
        public bool is_locked { get; set; }
    }

    /// <summary>
    /// 删除支付即会员规则接口数据
    /// </summary>
    public class DeletePayMemberRuleData
    {
        public string card_id { get; set; }
        public int[] mchid_list { get; set; }
    }

}
