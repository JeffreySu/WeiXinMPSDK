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
  
    文件名：QueryRequestData.cs
    文件功能描述：申请交易账单请求数据
    
    
    创建标识：Senparc - 20230421

    修改标识：Senparc - 20230520
    修改描述：v0.7.1 修复 TradeBillQueryRequestData 默认参数问题

----------------------------------------------------------------*/

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class TradeBillQueryRequestData
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TradeBillQueryRequestData() { }

        /// <summary>
        /// 含参构造函数(商家模式)
        /// </summary>
        /// <param name="bill_date">账单日期 格式yyyy-MM-DD 仅支持三个月内的账单下载申请。</param>
        /// <param name="bill_type">账单类型 不填则默认是ALL</param>
        /// <param name="tar_type">压缩类型  不填则默认是数据流</param>
        public TradeBillQueryRequestData(string bill_date, string bill_type = "ALL", string tar_type = null)
        {
            this.bill_date = bill_date;
            this.bill_type = bill_type;
            this.tar_type = tar_type;
        }


        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">子商户的商户号，由微信支付生成并下发。</param>
        /// <param name="bill_date">账单日期 格式yyyy-MM-DD 仅支持三个月内的账单下载申请。</param>
        /// <param name="bill_type">账单类型 不填则默认是ALL</param>
        /// <param name="tar_type">压缩类型  不填则默认是数据流</param>
        public TradeBillQueryRequestData(string sub_mchid, string bill_date, string bill_type = "ALL", string tar_type = "GZIP")
        {
            this.sub_mchid = sub_mchid;
            this.bill_date = bill_date;
            this.bill_type = bill_type;
            this.tar_type = tar_type;
        }

        #region 商户
        #endregion

        #region 服务商
        /// <summary>
        /// 子商户号
        /// 子商户的商户号，由微信支付生成并下发。
        /// 示例值：1900000109
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 账单日期
        /// 格式yyyy-MM-DD
        /// 仅支持三个月内的账单下载申请。
        /// 示例值：2019-06-11
        /// </summary>
        public string bill_date { get; set; }

        /// <summary>
        /// 账单类型
        /// 不填则默认是ALL
        /// 枚举值：
        /// ALL：返回当日所有订单信息（不含充值退款订单）
        /// SUCCESS：返回当日成功支付的订单（不含充值退款订单）
        /// REFUND：返回当日退款订单（不含充值退款订单）
        /// 示例值：ALL
        /// </summary>
        public string bill_type { get; set; }

        /// <summary>
        /// 压缩类型
        /// 不填则默认是数据流
        /// 枚举值：
        /// GZIP：返回格式为.gzip的压缩包账单
        /// 示例值：GZIP
        /// </summary>
        public string tar_type { get; set; }
    }
}

