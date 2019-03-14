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
    
    文件名：RequestMessageEvent_User_Pay_From_Pay_Cell.cs
    文件功能描述：事件之微信买单完成
    
    
    创建标识：

----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_User_Pay_From_Pay_Cell : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 删除卡券
        /// </summary>
        public override Event Event
        {
            get { return Event.user_pay_from_pay_cell; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// code 序列号。自定义code 及非自定义code的卡券被领取后都支持事件推送。
        /// </summary>
        public string UserCardCode { get; set; }
        /// <summary>
        /// 微信支付交易订单号（只有使用买单功能核销的卡券才会出现）
        /// </summary>
        public string TransId { get; set; }
        /// <summary>
        ///门店ID，当前卡券核销的门店ID（只有通过卡券商户助手和买单核销时才会出现）
        /// </summary>
        public string LocationId { get; set; }
        /// <summary>
        ///实付金额，单位为分 
        /// </summary>
        public int Fee { get; set; }
        /// <summary>
        /// 应付金额，单位为分
        /// </summary>
        public int OriginalFee { get; set; }
    }
}
