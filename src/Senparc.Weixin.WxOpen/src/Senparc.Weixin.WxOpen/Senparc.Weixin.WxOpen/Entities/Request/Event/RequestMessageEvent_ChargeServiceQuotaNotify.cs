#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
    
    文件名：RequestMessageEvent_ChargeServiceQuotaNotify.cs
    文件功能描述：付费管理订单用量告警事件
    
    
    创建标识：mc7246 - 20240831
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.Entities
{
    public class RequestMessageEvent_ChargeServiceQuotaNotify : RequestMessageEventBase,IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.charge_service_quota_notify; }
        }

        /// <summary>
        /// 固定值 3
        /// </summary>
        public int event_type { get; set; }

        /// <summary>
        /// 购买的商品的SPU_ID
        /// </summary>
        public long spu_id { get; set; }

        /// <summary>
        /// 购买的商品的SPU_NAME
        /// </summary>
        public string spu_name { get; set; }

        /// <summary>
        /// 所购 SPU 当前总的用量
        /// </summary>
        public int total_quota { get; set; }

        /// <summary>
        /// 所购 SPU 当前总已使用的用量
        /// </summary>
        public int total_used_quota { get; set; }        

    }
}
