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
  
    文件名：CloseCombineOrderRequestData.cs
    文件功能描述：合单关闭订单请求数据
    
    
    创建标识：Senparc - 20210819

    修改标识：Senparc - 20210819
    修改描述：完善注释; 增加构造函数
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class CloseCombineOrderRequestData
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CloseCombineOrderRequestData() { }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="combine_appid">合单发起方的appid</param>
        /// <param name="sub_orders">子单信息 最多支持子单条数：10</param>
        public CloseCombineOrderRequestData(string combine_appid, Sub_Order[] sub_orders)
        {
            this.combine_appid = combine_appid;
            this.sub_orders = sub_orders;
        }

        /// <summary>
        /// 合单商户appid	
        /// 合单发起方的appid。
        /// 示例值：wxd678efh567hg6787
        /// </summary>
        public string combine_appid { get; set; }

        /// <summary>
        /// 子单信息数组
        /// 最多支持子单条数：10
        /// </summary>
        public Sub_Order[] sub_orders { get; set; }

        #region 请求数据类型

        /// <summary>
        /// 子单信息
        /// </summary>
        public class Sub_Order
        {
            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="mchid">子单商户号</param>
            /// <param name="out_trade_no">子单商户订单号</param>
            public Sub_Order(string mchid, string out_trade_no)
            {
                this.mchid = mchid;
                this.out_trade_no = out_trade_no;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Sub_Order()
            {
            }

            /// <summary>
            /// 子单商户号
            /// 子单发起方商户号即合单参与方商户号，必须与发起方appid有绑定关系。
            /// 示例值：1900000109
            /// </summary>
            public string mchid { get; set; }
         
            /// <summary>
            /// 子单商户订单号	
            /// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
            /// 示例值：20150806125346
            /// </summary>
            public string out_trade_no { get; set; }
        }

        #endregion
    }
}
