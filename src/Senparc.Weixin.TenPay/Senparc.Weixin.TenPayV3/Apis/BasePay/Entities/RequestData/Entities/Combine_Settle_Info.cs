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
  
    文件名：Combine_Settle_Info.cs
    文件功能描述：下合单请求数据结算信息
    
    
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
    /// 结算信息
    /// </summary>
    public class Combine_Settle_Info : Settle_Info
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="profit_sharing">是否指定分账，可为null</param>
        /// /// <param name="subsidy_amount">补差金额	，SettleInfo.profit_sharing为true时，该金额才生效，可为null</param>
        public Combine_Settle_Info(bool profit_sharing, long subsidy_amount)
            : base(profit_sharing)
        {
            this.subsidy_amount = subsidy_amount;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Combine_Settle_Info()
        {
        }

        /// <summary>
        /// 补差金额
        /// SettleInfo.profit_sharing为true时，该金额才生效。
        /// 注意：单笔订单最高补差金额为5000元
        /// 示例值：10
        /// </summary>
        public long subsidy_amount { get; set; }
    }
}
