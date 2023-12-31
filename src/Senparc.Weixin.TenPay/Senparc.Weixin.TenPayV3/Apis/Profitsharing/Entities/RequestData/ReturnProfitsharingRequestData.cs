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
  
    文件名：ReturnProfitsharingRequestData.cs
    文件功能描述：请求分账回退接口请求数据
    
    
    创建标识：Senparc - 20210915

----------------------------------------------------------------*/

using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.Profitsharing
{
    /// <summary>
    /// 请求分账回退接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_1_3.shtml </para>
    /// </summary>
    public class ReturnProfitsharingRequestData
    {

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ReturnProfitsharingRequestData()
        {
        }

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="order_id">微信分账单号 <para>body微信分账单号，微信系统返回的唯一标识。</para><para>示例值：3008450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。取值范围：[0-9a-zA-Z_*@-]</para><para>示例值：P20150806125346</para>TODO:多选一</param>
        /// <param name="out_return_no">商户回退单号 <para>body此回退单号是商户在自己后台生成的一个新的回退单号，在商户后台唯一</para><para>示例值：R20190516001</para></param>
        /// <param name="return_mchid">回退商户号 <para>body分账接口中的分账接收方商户号</para><para>示例值：86693852</para></param>
        /// <param name="amount">回退金额 <para>body需要从分账接收方回退的金额，单位为分，只能为整数，不能超过原始分账单分出给该接收方的金额</para><para>示例值：10</para></param>
        /// <param name="description">回退描述 <para>body分账回退的原因描述</para><para>示例值：用户退款</para></param>
        public ReturnProfitsharingRequestData(string order_id, string out_order_no, string out_return_no, string return_mchid, int amount, string description)
        {
            this.order_id = order_id;
            this.out_order_no = out_order_no;
            this.out_return_no = out_return_no;
            this.return_mchid = return_mchid;
            this.amount = amount;
            this.description = description;
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para></param>
        /// <param name="order_id">微信分账单号 <para>body微信分账单号，微信系统返回的唯一标识。</para><para>示例值：3008450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。取值范围：[0-9a-zA-Z_*@-]</para><para>示例值：P20150806125346</para>TODO:多选一</param>
        /// <param name="out_return_no">商户回退单号 <para>body此回退单号是商户在自己后台生成的一个新的回退单号，在商户后台唯一</para><para>示例值：R20190516001</para></param>
        /// <param name="return_mchid">回退商户号 <para>body分账接口中的分账接收方商户号</para><para>示例值：86693852</para></param>
        /// <param name="amount">回退金额 <para>body需要从分账接收方回退的金额，单位为分，只能为整数，不能超过原始分账单分出给该接收方的金额</para><para>示例值：10</para></param>
        /// <param name="description">回退描述 <para>body分账回退的原因描述</para><para>示例值：用户退款</para></param>
        public ReturnProfitsharingRequestData(string sub_mchid, string order_id, string out_order_no, string out_return_no, string return_mchid, int amount, string description)
        {
            this.sub_mchid = sub_mchid;
            this.order_id = order_id;
            this.out_order_no = out_order_no;
            this.out_return_no = out_return_no;
            this.return_mchid = return_mchid;
            this.amount = amount;
            this.description = description;
        }

        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="brand_mchid">品牌主商户号 <para>品牌主商户号，填写微信支付分配的商户号。</para></param>
        /// <param name="sub_mchid">子商户号 <para>微信支付分配的子商户号，即分账的出资商户号。</para></param>
        /// <param name="order_id">微信分账单号 <para>body微信分账单号，微信系统返回的唯一标识。</para><para>示例值：3008450740201411110007820472</para></param>
        /// <param name="out_order_no">商户分账单号 <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。取值范围：[0-9a-zA-Z_*@-]</para><para>示例值：P20150806125346</para>TODO:多选一</param>
        /// <param name="out_return_no">商户回退单号 <para>body此回退单号是商户在自己后台生成的一个新的回退单号，在商户后台唯一</para><para>示例值：R20190516001</para></param>
        /// <param name="return_mchid">回退商户号 <para>body分账接口中的分账接收方商户号</para><para>示例值：86693852</para></param>
        /// <param name="amount">回退金额 <para>body需要从分账接收方回退的金额，单位为分，只能为整数，不能超过原始分账单分出给该接收方的金额</para><para>示例值：10</para></param>
        /// <param name="description">回退描述 <para>body分账回退的原因描述</para><para>示例值：用户退款</para></param>
        public ReturnProfitsharingRequestData(string brand_mchid, string sub_mchid, string order_id, string out_order_no, string out_return_no, string return_mchid, int amount, string description)
        {
            this.brand_mchid = brand_mchid;
            this.sub_mchid = sub_mchid;
            this.order_id = order_id;
            this.out_order_no = out_order_no;
            this.out_return_no = out_return_no;
            this.return_mchid = return_mchid;
            this.amount = amount;
            this.description = description;
        }

        #region 品牌连锁
        /// <summary>
        /// 品牌主商户号 
        /// 连锁平台需要 仅用于标志是否是连锁品牌分账，实际参数中不需要
        /// <para>品牌主商户号，填写微信支付分配的商户号。</para>
        /// </summary>
        [JsonIgnore]
        public string brand_mchid { get; set; }
        #endregion

        #region 服务商
        /// <summary>
        /// 子商户号 
        /// 服务商模式需要
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion

        /// <summary>
        /// 微信分账单号
        /// <para>body微信分账单号，微信系统返回的唯一标识。</para>
        /// <para>示例值：3008450740201411110007820472</para>
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 商户分账单号
        /// <para>body商户系统内部的分账单号，在商户系统内部唯一，同一分账单号多次请求等同一次。 取值范围：[0-9a-zA-Z_*@-]</para>
        /// <para>示例值：P20150806125346</para>TODO: 多选一
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 商户回退单号
        /// <para>body此回退单号是商户在自己后台生成的一个新的回退单号，在商户后台唯一</para>
        /// <para>示例值：R20190516001</para>
        /// </summary>
        public string out_return_no { get; set; }

        /// <summary>
        /// 回退商户号
        /// <para>body分账接口中的分账接收方商户号</para>
        /// <para>示例值：86693852</para>
        /// </summary>
        public string return_mchid { get; set; }

        /// <summary>
        /// 回退金额
        /// <para>body需要从分账接收方回退的金额，单位为分，只能为整数，不能超过原始分账单分出给该接收方的金额</para>
        /// <para>示例值：10</para>
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 回退描述
        /// <para>body分账回退的原因描述</para>
        /// <para>示例值：用户退款</para>
        /// </summary>
        public string description { get; set; }

    }
}
