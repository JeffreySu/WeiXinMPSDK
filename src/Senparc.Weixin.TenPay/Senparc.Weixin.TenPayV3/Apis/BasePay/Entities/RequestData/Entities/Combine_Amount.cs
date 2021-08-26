#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2021 Senparc
  
    文件名：Combine_Amount.cs
    文件功能描述：下合单请求订单金额信息
    
    
    创建标识：Senparc - 20210825
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay.Entities.RequestData.Entities
{
    /// <summary>
    /// 订单金额
    /// </summary>
    public class Combine_Amount
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="total_amount">订单总金额，单位为分</param>
        /// <param name="currency">货币类型 CNY：人民币，境内商户号仅支持人民币</param>
        public Combine_Amount(int total_amount, string currency)
        {
            this.total_amount = total_amount;
            this.currency = currency;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Combine_Amount()
        {
        }

        /// <summary>
        /// 总金额
        /// 订单总金额，单位为分。
        /// 示例值：100 (1元)
        /// </summary>
        public int total_amount { get; set; }

        /// <summary>
        /// 货币类型
        /// CNY：人民币，境内商户号仅支持人民币。
        /// 示例值：CNY
        /// </summary>
        public string currency { get; set; }
    }
}
