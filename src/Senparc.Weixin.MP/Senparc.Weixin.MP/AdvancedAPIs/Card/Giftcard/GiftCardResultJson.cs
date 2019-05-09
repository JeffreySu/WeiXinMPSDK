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
    
    文件名：GiftCardResultJson.cs
    文件功能描述：礼品卡返回结果


    创建标识：Senparc - 20181008
    

----------------------------------------------------------------*/



using System.CodeDom;
using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 设置支付后投放卡券接口返回结果
    /// </summary>
    public class AddCardAfterPayResultJson : WxJsonResult
    {
        public string rule_id { get; set; }
        public List<Fail_mchid_Item> fail_mchid_list { get; set; }
        public List<string> succ_mchid_list { get; set; }
    }

    /// <summary>
    /// 设置失败的mchid列表
    /// </summary>
    public class Fail_mchid_Item
    {
        public string mchid { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public int occupy_rule_id { get; set; }
        public string occupy_appid { get; set; }
    }

    /// <summary>
    /// 查询支付后投放卡券规则详情接口返回结果
    /// </summary>
    public class AfterPay_GetByIdResultJson : WxJsonResult
    {
        public Rule_Info rule_info { get; set; }
    }

    /// <summary>
    /// 批量查询支付后投放卡券规则详情接口返回结果
    /// </summary>
    public class AfterPay_BatchGetResultJson : WxJsonResult
    {
        public int total_count { get; set; }
        public List<Rule_Info> rule_list { get; set; }
    }

    /// <summary>
    /// 增加支付即会员规则接口返回结果
    /// </summary>
    public class AddPayMemberRuleResultJson : WxJsonResult
    {
        public List<Fail_mchid_Item> fail_mchid_list { get; set; }
        public string[] succ_mchid_list { get; set; }
    }

    /// <summary>
    /// 删除支付即会员规则接口返回结果
    /// </summary>
    public class DeletePayMemberRuleResultJson : WxJsonResult
    {
        public List<Fail_mchid_Item> fail_mchid_list { get; set; }
        public List<Fail_mchid_Item> succ_mchid_list { get; set; }
    }

    /// <summary>
    /// 查询商户号支付即会员规则接口返回结果
    /// </summary>
    public class GetPayMemberRuleResultJson : WxJsonResult
    {
        public string card_id { get; set; }
        public string occupy_appid { get; set; }
        public bool is_locked { get; set; }
    }

}
