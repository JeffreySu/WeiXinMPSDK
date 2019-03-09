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
    
    文件名：Card_BaseInfo_Sku.cs
    文件功能描述：基本的卡券使用日期，有效期的信息，所有卡券通用。
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/


namespace Senparc.Weixin.MP.AdvancedAPIs.Card
{
    /// <summary>
    /// 使用日期，有效期的信息
    /// </summary>
    public class Card_BaseInfo_DateInfo
    {
        /// <summary>
        /// 使用时间的类型 1：固定日期区间，2：固定时长（自领取后按天算）
        /// 必填
        /// </summary>
        public string type { get; set; }
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
    ///会员卡支持微信支付刷卡 
    /// </summary>
    public class Card_BaseInfo_member_card_PayInfo
    {
        public Card_BaseInfo_member_card_SwipeCard swipe_card { get; set; }
    }

    public class Card_BaseInfo_member_card_SwipeCard
    {
        public bool is_swipe_card { get; set;}
    }

}
