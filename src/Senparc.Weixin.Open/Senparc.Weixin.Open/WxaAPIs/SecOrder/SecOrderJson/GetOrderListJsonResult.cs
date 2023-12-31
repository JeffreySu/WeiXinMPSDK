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
    
    文件名：UploadShippingInfo.cs
    文件功能描述：发货信息录入接口
    
    
    创建标识：Yaofeng - 20231026

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.Open.WxaAPIs.SecOrder
{
    /// <summary>
    /// 查询订单列表
    /// </summary>
    public class GetOrderListJsonResult : WxJsonResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string last_index { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool has_more { get; set; }

        /// <summary>
        /// 支付单信息
        /// </summary>
        public List<Order> order_list { get; set; }
    }
}