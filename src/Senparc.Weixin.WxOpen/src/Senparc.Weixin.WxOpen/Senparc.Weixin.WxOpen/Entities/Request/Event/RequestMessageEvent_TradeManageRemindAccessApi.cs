#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
    
    文件名：RequestMessageEvent_TradeManageRemindAccessApi.cs
    文件功能描述：事件之小程序发货信息事件推送
    
    
    创建标识：mc7246 - 20211209
   https://developers.weixin.qq.com/miniprogram/dev/platform-capabilities/business-capabilities/order-shipping/order-shipping.html#八、相关消息推送

----------------------------------------------------------------*/

namespace Senparc.Weixin.WxOpen.Entities
{
    /// <summary>
    /// 提醒接入发货信息管理服务API
    /// 小程序完成账期授权时
    /// 小程序产生第一笔交易时
    /// 已产生交易但从未发货的小程序，每天一次
    /// </summary>
    public class RequestMessageEvent_TradeManageRemindAccessApi : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.trade_manage_remind_access_api; }
        }

        /// <summary>
        /// 消息文本内容
        /// </summary>
        public string msg { get; set; }

    }
}
