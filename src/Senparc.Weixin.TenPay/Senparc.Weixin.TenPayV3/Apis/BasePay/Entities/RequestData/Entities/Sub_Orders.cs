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
  
    文件名：Settle_Info.cs
    文件功能描述：下合单请求数据子单信息
    
    
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
    /// 子单信息
    /// </summary>
    public class Sub_Orders
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="mchid">子单商户号，即合单参与方商户号，必须与发起方appid有绑定关系</param>
        /// <param name="attach">附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用</param>
        /// <param name="amount">订单金额</param>
        /// <param name="out_trade_no">商户系统内部订单号</param>
        /// <param name="goods_tag">订单优惠标记，可为null</param>
        /// <param name="description">商品描述</param>
        /// <param name="settle_info">结算信息，可为null</param>
        public Sub_Orders(string mchid, string attach, Combine_Amount amount, string out_trade_no, string goods_tag, string description, Combine_Settle_Info settle_info)
        {
            this.mchid = mchid;
            this.attach = attach;
            this.amount = amount;
            this.out_trade_no = out_trade_no;
            this.goods_tag = goods_tag;
            this.description = description;
            this.settle_info = settle_info;
        }

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
        public Combine_Amount amount { get; set; }

        /// <summary>
        /// 子单商户订单号	
        /// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
        /// 示例值：20150806125346
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 订单优惠标记
        /// 示例值：WXG
        /// </summary>
        public string goods_tag { get; set; }

        /// <summary>
        /// 商品描述
        /// 示例值：Image形象店-深圳腾大-QQ公仔
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 结算信息
        /// </summary>
        public Combine_Settle_Info settle_info { get; set; }
    }
}
