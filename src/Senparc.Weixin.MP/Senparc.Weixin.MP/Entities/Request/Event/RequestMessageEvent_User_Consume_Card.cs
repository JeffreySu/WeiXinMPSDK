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
    
    文件名：RequestMessageEvent_User_Consume_Card.cs
    文件功能描述：事件之卡券核销
    
    
    创建标识：Senparc - 20150722
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_User_Consume_Card : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 卡券核销
        /// </summary>
        public override Event Event
        {
            get { return Event.user_consume_card; }
        }

        /// <summary>
        /// 卡券ID
        /// </summary>
        public string CardId { get; set; }
        /// <summary>
        /// 卡券Code码
        /// </summary>
        public string UserCardCode { get; set; }
        /// <summary>
        /// 核销来源。支持开发者统计API核销（FROM_API）、公众平台核销（FROM_MP）、卡券商户助手核销（FROM_MOBILE_HELPER）（核销员微信号）
        /// </summary>
        public string ConsumeSource { get; set; }

        /// <summary>
        /// 门店名称，当前卡券核销的门店名称（只有通过自助核销和买单核销时才会出现该字段）
        /// </summary>
        public string LocationName { get; set; }
        /// <summary>
        ///核销该卡券核销员的openid（只有通过卡券商户助手核销时才会出现） 
        /// </summary>
        public string StaffOpenId { get; set; }
        /// <summary>
        /// 自助核销时，用户输入的验证码
        /// </summary>
        public string VerifyCode { get; set; }
        /// <summary>
        /// 自助核销时，用户输入的备注金额
        /// </summary>
        public string RemarkAmount { get; set; }
        /// <summary>
        /// 开发者发起核销时传入的自定义参数，用于进行核销渠道统计
        /// </summary>
        public string OuterStr { get; set; }
    }
}
