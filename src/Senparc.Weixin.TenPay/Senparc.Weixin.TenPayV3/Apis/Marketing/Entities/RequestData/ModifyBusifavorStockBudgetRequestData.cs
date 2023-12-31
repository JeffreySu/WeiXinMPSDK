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
  
    文件名：PatchBusifavorBudgetRequestData.cs
    文件功能描述：修改批次预算接口请求数据
    
    
    创建标识：Senparc - 20210914
    
    修改标识：Senparc - 20230107
    修改描述：v0.6.8.3 更新 ModifyBusifavorStockBudgetRequestData 参数，删除 stock_id

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 修改批次预算接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_11.shtml </para>
    /// </summary>
    public class ModifyBusifavorStockBudgetRequestData
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="target_max_coupons">目标批次最大发放个数 <para>body批次最大发放个数</para><para>注：目标批次即修改后的批次</para><para>示例值：3000</para><para>target_max_coupons和target_max_coupons_by_day二选一</para></param>
        /// <param name="target_max_coupons_by_day">目标单天发放上限个数 <para>body单天发放上限个数</para><para>注：目标批次即修改后的批次</para><para>示例值：500</para><para>target_max_coupons和target_max_coupons_by_day二选一</para></param>
        /// <param name="current_max_coupons">当前批次最大发放个数 <para>body当前批次最大发放个数，当传入target_max_coupons大于0时，current_max_coupons必传</para><para>注：当前批次即未修改的批次</para><para>示例值：500</para><para>可为null</para></param>
        /// <param name="current_max_coupons_by_day">当前单天发放上限个数 <para>body当前单天发放上限个数，当传入target_max_coupons_by_day大于0时，current_max_coupons_by_day必填</para><para>注：当前批次即未修改的批次</para><para>示例值：300</para><para>可为null</para></param>
        /// <param name="modify_budget_request_no">修改预算请求单据号 <para>body修改预算请求单据号</para><para>示例值：1002600620019090123143254436</para></param>
        public ModifyBusifavorStockBudgetRequestData( int? target_max_coupons, int? target_max_coupons_by_day, int? current_max_coupons, int? current_max_coupons_by_day, string modify_budget_request_no)
        {
            this.target_max_coupons = target_max_coupons;
            this.target_max_coupons_by_day = target_max_coupons_by_day;
            this.current_max_coupons = current_max_coupons;
            this.current_max_coupons_by_day = current_max_coupons_by_day;
            this.modify_budget_request_no = modify_budget_request_no;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ModifyBusifavorStockBudgetRequestData()
        {
        }

        /// <summary>
        /// 目标批次最大发放个数
        /// <para>body批次最大发放个数 </para>
        /// <para>注：目标批次即修改后的批次</para>
        /// <para>示例值：3000</para>
        /// </summary>
        public int? target_max_coupons { get; set; }

        /// <summary>
        /// 目标单天发放上限个数
        /// <para>body单天发放上限个数 </para>
        /// <para>注：目标批次即修改后的批次</para>
        /// <para>示例值：500</para>TODO: 多选一
        /// </summary>
        public int? target_max_coupons_by_day { get; set; }

        /// <summary>
        /// 当前批次最大发放个数
        /// <para>body当前批次最大发放个数，当传入target_max_coupons大于0时，current_max_coupons必传</para>
        /// <para>注：当前批次即未修改的批次</para>
        /// <para>示例值：500</para>
        /// <para>可为null</para>
        /// </summary>
        public int? current_max_coupons { get; set; }

        /// <summary>
        /// 当前单天发放上限个数
        /// <para>body当前单天发放上限个数 ，当传入target_max_coupons_by_day大于0时，current_max_coupons_by_day必填</para>
        /// <para>注：当前批次即未修改的批次</para>
        /// <para>示例值：300</para>
        /// <para>可为null</para>
        /// </summary>
        public int? current_max_coupons_by_day { get; set; }

        /// <summary>
        /// 修改预算请求单据号
        /// <para>body修改预算请求单据号 </para>
        /// <para>示例值：1002600620019090123143254436</para>
        /// </summary>
        public string modify_budget_request_no { get; set; }
    }
}
