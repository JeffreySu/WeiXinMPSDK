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
        /// 构造函数
        /// </summary>
        /// <param name="combine_appid">合单发起方的appid </param>
        /// <param name="sub_orders">子单信息 最多支持子单条数：10</param>
        public CloseCombineOrderRequestData(string combine_appid, Sub_Orders[] sub_orders)
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
        /// 子单信息
        /// 最多支持子单条数：10
        /// </summary>
        public Sub_Orders[] sub_orders { get; set; }

        public class Amount
        {
            /// <summary>
            /// 总金额
            /// 订单总金额，单位为分。
            /// 示例值：100 (1元)
            /// </summary>
            public int total { get; set; }

            /// <summary>
            /// 货币类型
            /// CNY：人民币，境内商户号仅支持人民币。
            /// 示例值：CNY
            /// </summary>
            public string currency { get; set; }
        }

        /// <summary>
        /// 结算信息
        /// </summary>
        public class Settle_Info
        {
            /// <summary>
            /// 是否指定分账
            /// </summary>
            public bool profit_sharing { get; set; }

            /// <summary>
            /// 补差金额
            /// SettleInfo.profit_sharing为true时，该金额才生效。
            /// 注意：单笔订单最高补差金额为5000元
            /// 示例值：10
            /// </summary>
            public long subsidy_amount { get; set; }
        }

        /// <summary>
        /// 子单信息
        /// </summary>
        public class Sub_Orders
        {
            /// <summary>
            /// 子单商户号
            /// 子单发起方商户号即合单参与方商户号，必须与发起方appid有绑定关系。
            /// 示例值：1900000109
            /// </summary>
            public string mchid { get; set; }

            /// <summary>
            /// 附加数据
            /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用
            /// 示例值：自定义数据
            /// </summary>
            public string attach { get; set; }

            /// <summary>
            /// 订单金额
            /// </summary>
            public Amount amount { get; set; }

            /// <summary>
            /// 子单商户订单号	
            /// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
            /// 示例值：20150806125346
            /// </summary>
            public string out_trade_no { get; set; }

            /// <summary>
            /// 商品描述
            /// 示例值：Image形象店-深圳腾大-QQ公仔
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 结算信息
            /// </summary>
            public Settle_Info settle_info { get; set; }
        }
    }

}
