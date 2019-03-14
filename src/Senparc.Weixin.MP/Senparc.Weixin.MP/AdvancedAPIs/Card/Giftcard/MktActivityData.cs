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
    
    文件名：MktActivityData.cs
    文件功能描述：社交立减金活动数据
    
    
    创建标识：Senparc - 20181008
    
    

----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers.Serializers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;

namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    ///  创建支付后领取立减金活动接口数据
    /// </summary>
    public class CreateActivityData
    {
        public Info info { get; set; }
    }

    public class Info
    {
        public Basic_Info basic_info { get; set; }
        public Card_Info_List[] card_info_list { get; set; }
        public Custom_Info custom_info { get; set; }
    }

    public class Basic_Info
    {
        public string activity_bg_color { get; set; }
        public string activity_tinyappid { get; set; }
        public int begin_time { get; set; }
        public int end_time { get; set; }
        public int gift_num { get; set; }
        public int max_partic_times_act { get; set; }
        public int max_partic_times_one_day { get; set; }
        public string mch_code { get; set; }
    }

    public class Custom_Info
    {
        public string type { get; set; }
    }

    public class Card_Info_List
    {
        public string card_id { get; set; }
        public int min_amt { get; set; }
        public string membership_appid { get; set; }
    }


    public class ApiConfirmAuthorizationData
    {
        public string component_appid { get; set; }
        public string authorizer_appid { get; set; }
        public int funcscope_category_id { get; set; }
        public int confirm_value { get; set; }
    }
    
}
