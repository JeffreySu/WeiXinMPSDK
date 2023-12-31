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
  
    文件名：DisassociateBusifavorReturnJson.cs
    文件功能描述：取消关联商家券订单信息接口请求数据
    
    
    创建标识：Senparc - 20210913
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 取消关联商家券订单信息接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_10.shtml </para>
    /// </summary>
    public class DisassociateBusifavorRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="stock_id">批次号 <para>body微信为每个商家券批次分配的唯一ID，对于商户自定义code的批次，关联请求必须填写批次号</para><para>示例值：100088</para></param>
        /// <param name="coupon_code">券code <para>body券的唯一标识</para><para>示例值：sxxe34343434</para></param>
        /// <param name="out_trade_no">关联的商户订单号 <para>body微信支付下单时的商户订单号，欲与该商家券关联的微信支付</para><para>示例值：MCH_102233445</para></param>
        /// <param name="out_request_no">商户请求单号 <para>body商户创建批次凭据号（格式：商户id+日期+流水号），商户侧需保持唯一性，可包含英文字母，数字，｜，_，*，-等内容，不允许出现其他不合法符号。</para><para>示例值：1002600620019090123143254435</para></param>
        public DisassociateBusifavorRequestData(string stock_id, string coupon_code, string out_trade_no, string out_request_no)
        {
            this.stock_id = stock_id;
            this.coupon_code = coupon_code;
            this.out_trade_no = out_trade_no;
            this.out_request_no = out_request_no;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DisassociateBusifavorRequestData()
        {
        }

        /// <summary>
        /// 批次号
        /// <para>body微信为每个商家券批次分配的唯一ID， 对于商户自定义code的批次，关联请求必须填写批次号</para>
        /// <para>示例值：100088 </para>
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 券code
        /// <para>body 券的唯一标识 </para>
        /// <para>示例值：sxxe34343434 </para>
        /// </summary>
        public string coupon_code { get; set; }

        /// <summary>
        /// 关联的商户订单号
        /// <para>body 微信支付下单时的商户订单号，欲与该商家券关联的微信支付</para>
        /// <para>示例值：MCH_102233445 </para>
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商户请求单号
        /// <para>body 商户创建批次凭据号（格式：商户id+日期+流水号），商户侧需保持唯一性，可包含英文字母，数字，｜，_，*，-等内容，不允许出现其他不合法符号。 </para>
        /// <para>示例值：1002600620019090123143254435 </para>
        /// </summary>
        public string out_request_no { get; set; }

    }



}
