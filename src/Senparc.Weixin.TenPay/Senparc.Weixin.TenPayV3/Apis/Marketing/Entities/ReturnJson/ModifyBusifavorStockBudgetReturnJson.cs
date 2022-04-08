﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2022 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2022 Senparc
  
    文件名：AssociateBusifavorReturnJson.cs
    文件功能描述：修改批次预算返回Json类
    
    
    创建标识：Senparc - 20210913
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 修改批次预算返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_11.shtml </para>
    /// </summary>
    public class ModifyBusifavorStockBudgetReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="max_coupons">批次当前最大发放个数 <para>批次最大发放个数</para><para>示例值：300</para></param>
        /// <param name="max_coupons_by_day">当前单天发放上限个数 <para>当前单天发放上限个数</para><para>示例值：100</para><para>可为null</para></param>
        public ModifyBusifavorStockBudgetReturnJson(int max_coupons, int max_coupons_by_day)
        {
            this.max_coupons = max_coupons;
            this.max_coupons_by_day = max_coupons_by_day;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ModifyBusifavorStockBudgetReturnJson()
        {
        }

        /// <summary>
        /// 批次当前最大发放个数
        /// <para>批次最大发放个数 </para>
        /// <para>示例值：300</para>
        /// </summary>
        public int max_coupons { get; set; }

        /// <summary>
        /// 当前单天发放上限个数
        /// <para>当前单天发放上限个数 </para>
        /// <para>示例值：100</para>
        /// <para>可为null</para>
        /// </summary>
        public int max_coupons_by_day { get; set; }

    }
}


