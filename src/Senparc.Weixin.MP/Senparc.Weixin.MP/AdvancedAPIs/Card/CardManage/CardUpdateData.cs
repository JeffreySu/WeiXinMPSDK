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
    
    文件名：CardUpdateData.cs
    文件功能描述：卡券更新需要的数据
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20160808
    修改描述：修改Card_MemberCardUpdateData
----------------------------------------------------------------*/



using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.Helpers;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 会员卡数据
    /// </summary>
    public class Card_MemberCardUpdateData : BaseUpdateInfo
    {
        /// <summary>
        /// 是否支持积分。要求设置为true后不能设置为false。
        /// </summary>
        public bool supply_bonus { get; set;}
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
        /// 设置为true时用户领取会员卡后系统自动将其激活，无需调用激活接口。
        /// 非必填
        /// </summary>
        [JsonSetting.IgnoreValueAttribute(false)]
        public bool auto_activate { get; set; }
        /// <summary>
        /// 设置为true时会员卡支持一键激活，不允许同时传入activate_url字段，否则设置wx_activate失效。
        /// 非必填
        /// </summary>
        [JsonSetting.IgnoreValueAttribute(false)]
        public bool wx_activate { get; set; }

        /// <summary>
        /// 激活会员卡的url，与“bind_old_card_url”字段二选一必填。
        /// </summary>
        public string activate_url { get; set; }
        /// <summary>
        /// 设置跳转外链查看积分详情。仅适用于积分无法通过激活接口同步的情况下使用该字段。
        /// 非必填
        /// </summary>
        public string bonus_url { get; set; }
        /// <summary>
        /// 积分信息类目对应的小程序 user_name，格式为原始id+@app 小程序原始id可以在小程序的设置页面底部查看到
        /// </summary>
        public string bonus_app_brand_user_name { get; set; }
        /// <summary>
        /// 积分入口小程序的页面路径 
        /// </summary>
        public string bonus_app_brand_pass { get; set; }
        /// <summary>
        /// 初始设置积分 int型数据
        /// 非必填,null时显示查看
        /// </summary>
        //[JsonSetting.IgnoreValueAttribute("0")]
        //public string init_increase_bonus { get; set; }

        /// <summary>
        /// 设置跳转外链查看余额详情。仅适用于余额无法通过激活接口同步的情况下使用该字段。
        /// 非必填
        /// </summary>
        public string balance_url { get; set; }
        /// <summary>
        /// 余额信息类目对应的小程序 user_name，格式为原始id+@app 小程序原始id可以在小程序的设置页面底部查看到
        /// </summary>
        public string balance_app_brand_user_name { get; set; }
        /// <summary>
        /// 余额入口小程序的页面路径
        /// </summary>
        public string balance_app_brand_pass { get; set; }
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
        /// 卡背景图，非必填
        /// </summary>
        public string background_pic_url { get; set; }

        /// <summary>
        /// 积分规则结构体
        /// </summary>
        public BonusRule bonus_rule { get; set; }
        /// <summary>
        /// 折扣
        /// </summary>
        [JsonSetting.IgnoreValueAttribute(0)]
        public int discount { get; set; }
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