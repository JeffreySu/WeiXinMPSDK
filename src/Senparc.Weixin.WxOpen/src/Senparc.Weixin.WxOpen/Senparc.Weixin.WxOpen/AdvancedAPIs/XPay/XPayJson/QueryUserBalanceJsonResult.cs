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
 
    文件名：QueryUserBalanceJsonResult.cs
    文件功能描述：查询用户代币余额 返回结果
    
    创建标识：Senparc - 20231201

    修改标识：Senparc - 20241020
    修改描述：v3.21.2 修正 first_save_flag 类型错误，应为 int; 同时增加 FirstSaveFlag 属性, 用于 bool 类型判断是否首次充值

----------------------------------------------------------------*/


using Senparc.Weixin.Entities;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.XPay
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryUserBalanceJsonResult : WxJsonResult
    {
        /// <summary>
        /// 代币总余额，包括有价和赠送部分
        /// </summary>
        public int balance { get; set; }

        /// <summary>
        /// 赠送账户的代币余额
        /// </summary>
        public int present_balance { get; set; }

        /// <summary>
        /// 累计有价货币充值数量
        /// </summary>
        public int sum_save { get; set; }

        /// <summary>
        /// 累计赠送无价货币数量
        /// </summary>
        public int sum_present { get; set; }

        /// <summary>
        /// 历史总增加的代币金额
        /// </summary>
        public int sum_balance { get; set; }

        /// <summary>
        /// 历史总消耗代币金额
        /// </summary>
        public int sum_cost { get; set; }

        /// <summary>
        /// 代币总余额，包括有价和赠送部分
        /// </summary>
        public int first_save_flag { get; set; }

        /// <summary>
        /// 代币总余额，包括有价和赠送部分
        /// </summary>
        public bool FirstSaveFlag => first_save_flag == 1;
    }
}
