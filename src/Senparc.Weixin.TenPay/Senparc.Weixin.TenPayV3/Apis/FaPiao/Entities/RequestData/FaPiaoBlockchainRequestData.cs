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
  
    文件名：FapiaoBlockchainRequestData.cs
    文件功能描述：电子发票 - 区块链电子发票API 请求数据
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.Fapiao
{
    /// <summary>
    /// 电子发票 - 获取商户开票基础信息API 请求数据
    /// </summary>
    public class GetMerchantInfoRequestData
    {
        /// <summary>
        /// 子商户号
        /// <para>微信支付分配的子商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string sub_mchid { get; set; }
    }

    /// <summary>
    /// 电子发票 - 开具电子发票API 请求数据
    /// </summary>
    public class CreateFapiaoRequestData
    {
        /// <summary>
        /// 商户号
        /// <para>微信支付分配的商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 子商户号
        /// <para>微信支付分配的子商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string sub_mchid { get; set; }

        /// <summary>
        /// 微信支付订单号
        /// <para>微信支付系统生成的订单号</para>
        /// <para>示例值：1217752501201407033233368018</para>
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// <para>商户系统内部订单号</para>
        /// <para>示例值：20150806125346</para>
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 发票申请时间
        /// <para>商户侧开票时间，格式为yyyy-MM-dd</para>
        /// <para>示例值：2021-06-10</para>
        /// </summary>
        public string fapiao_apply_time { get; set; }

        /// <summary>
        /// 发票信息
        /// <para>发票的详细信息</para>
        /// </summary>
        public FapiaoInfo fapiao_info { get; set; }

        /// <summary>
        /// 买家信息
        /// <para>开票的买家信息</para>
        /// </summary>
        public BuyerInfo buyer_info { get; set; }
    }

    /// <summary>
    /// 发票信息
    /// </summary>
    public class FapiaoInfo
    {
        /// <summary>
        /// 发票类型
        /// <para>发票的类型</para>
        /// <para>示例值：NORMAL_INVOICE</para>
        /// </summary>
        public string invoice_type { get; set; }

        /// <summary>
        /// 发票内容
        /// <para>发票的内容描述</para>
        /// <para>示例值：商品销售</para>
        /// </summary>
        public string invoice_content { get; set; }

        /// <summary>
        /// 发票金额
        /// <para>发票金额，单位为分</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int invoice_amount { get; set; }

        /// <summary>
        /// 商品详细列表
        /// <para>发票商品的详细列表</para>
        /// </summary>
        public InvoiceItem[] items { get; set; }
    }

    /// <summary>
    /// 发票商品明细
    /// </summary>
    public class InvoiceItem
    {
        /// <summary>
        /// 商品名称
        /// <para>商品的名称</para>
        /// <para>示例值：苹果</para>
        /// </summary>
        public string item_name { get; set; }

        /// <summary>
        /// 商品数量
        /// <para>商品的数量</para>
        /// <para>示例值：2</para>
        /// </summary>
        public int item_quantity { get; set; }

        /// <summary>
        /// 商品单价
        /// <para>商品的单价，单位为分</para>
        /// <para>示例值：50</para>
        /// </summary>
        public int item_price { get; set; }

        /// <summary>
        /// 商品总金额
        /// <para>商品的总金额，单位为分</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int item_total_amount { get; set; }
    }

    /// <summary>
    /// 买家信息
    /// </summary>
    public class BuyerInfo
    {
        /// <summary>
        /// 买家类型
        /// <para>买家的类型</para>
        /// <para>示例值：PERSONAL</para>
        /// </summary>
        public string buyer_type { get; set; }

        /// <summary>
        /// 买家名称
        /// <para>买家的名称</para>
        /// <para>示例值：张三</para>
        /// </summary>
        public string buyer_name { get; set; }

        /// <summary>
        /// 买家税号
        /// <para>买家的税号</para>
        /// <para>示例值：110108199909090909</para>
        /// </summary>
        public string buyer_taxpayer_num { get; set; }

        /// <summary>
        /// 买家地址
        /// <para>买家的地址</para>
        /// <para>示例值：北京市海淀区xxx</para>
        /// </summary>
        public string buyer_address { get; set; }

        /// <summary>
        /// 买家电话
        /// <para>买家的电话</para>
        /// <para>示例值：13800138000</para>
        /// </summary>
        public string buyer_phone { get; set; }
    }

    /// <summary>
    /// 电子发票 - 冲红电子发票API 请求数据
    /// </summary>
    public class ReverseFapiaoRequestData
    {
        /// <summary>
        /// 发票申请单号
        /// <para>微信支付系统生成的发票申请单号</para>
        /// <para>示例值：2020112611140011234567890</para>
        /// </summary>
        public string fapiao_apply_id { get; set; }

        /// <summary>
        /// 冲红原因
        /// <para>冲红发票的原因</para>
        /// <para>示例值：开票有误</para>
        /// </summary>
        public string reverse_reason { get; set; }
    }

    /// <summary>
    /// 电子发票 - 获取发票下载信息API 请求数据
    /// </summary>
    public class GetFapiaoFileRequestData
    {
        /// <summary>
        /// 发票申请单号
        /// <para>微信支付系统生成的发票申请单号</para>
        /// <para>示例值：2020112611140011234567890</para>
        /// </summary>
        public string fapiao_apply_id { get; set; }
    }
}
