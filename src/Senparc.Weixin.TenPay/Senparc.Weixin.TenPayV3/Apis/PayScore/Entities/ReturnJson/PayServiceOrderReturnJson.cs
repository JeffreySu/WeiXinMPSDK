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
  
    文件名：PayServiceOrderReturnJson.cs
    文件功能描述：商户发起催收扣款返回Json类
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 商户发起催收扣款返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_19.shtml </para>
    /// </summary>
    public class PayServiceOrderReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="appid">应用ID  <para>微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系，此参数需在本系统先进行配置。</para><para>示例值：wxd678efh567hg6787</para></param>
        /// <param name="mchid">商户号  <para>调用接口提交的商户号。</para><para>示例值：1230000109</para></param>
        /// <param name="out_order_no">商户订单号  <para>调用接口提交的商户服务订单号。</para><para>示例值：1230000109</para></param>
        /// <param name="service_id">服务ID  <para>调用该接口提交的服务ID。</para><para>示例值：500001</para></param>
        /// <param name="order_id">微信支付服务订单号  <para>微信支付服务订单号，每个微信支付服务订单号与商户号下对应的商户服务订单号一一对应。</para><para>示例值：15646546545165651651</para></param>
        public PayServiceOrderReturnJson(string appid, string mchid, string out_order_no, string service_id, string order_id)
        {
            this.appid = appid;
            this.mchid = mchid;
            this.out_order_no = out_order_no;
            this.service_id = service_id;
            this.order_id = order_id;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public PayServiceOrderReturnJson()
        {
        }

        /// <summary>
        /// 应用ID 
        /// <para>微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系，此参数需在本系统先进行配置。 </para>
        /// <para>示例值：wxd678efh567hg6787 </para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户号 
        /// <para>调用接口提交的商户号。 </para>
        /// <para>示例值：1230000109 </para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 商户订单号 
        /// <para>调用接口提交的商户服务订单号。 </para>
        /// <para>示例值：1230000109 </para>
        /// </summary>
        public string out_order_no { get; set; }

        /// <summary>
        /// 服务ID 
        /// <para>调用该接口提交的服务ID。 </para>
        /// <para>示例值：500001 </para>
        /// </summary>
        public string service_id { get; set; }

        /// <summary>
        /// 微信支付服务订单号 
        /// <para>微信支付服务订单号，每个微信支付服务订单号与商户号下对应的商户服务订单号一一对应。 </para>
        /// <para>示例值：15646546545165651651 </para>
        /// </summary>
        public string order_id { get; set; }

    }




}
