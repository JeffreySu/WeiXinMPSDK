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
    
    文件名：RequestMessageEvent_AddExpressPath.cs
    文件功能描述：运单轨迹更新事件
    
    
    创建标识：chinanhb - 20230529
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.Entities
{
    public class RequestMessageEvent_AddExpressPath:RequestMessageEventBase,IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.add_express_path; }
        }
        /// <summary>
        /// 快递公司ID
        /// </summary>
        public string DeliveryID { get; set; }
        /// <summary>
        /// 运单ID
        /// </summary>
        public string WayBillId { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 轨迹版本号（整型）
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 轨迹节点数（整型）
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 轨迹列表
        /// </summary>
        public List<ActionModel> Actions { get; set; }
    }
    public class ActionModel
    {
        public long ActionTime { get; set; }
        public int ActionType { get; set; }
        public string ActionMsg { get; set; }
    }
}
