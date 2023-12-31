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
  
    文件名：PayServiceOrderRequestData.cs
    文件功能描述：微信支付V3商户发起催收扣款接口请求数据
    
    
    创建标识：Senparc - 20210924
    
----------------------------------------------------------------*/

using Newtonsoft.Json;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3商户发起催收扣款接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_19.shtml </para>
    /// </summary>
    public class PayServiceOrderRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="out_order_no">商户服务订单号  <para>path商户系统内部服务订单号（不是交易单号），与创建订单时一致。</para><para>示例值：2304203423948239423</para></param>
        /// <param name="appid">应用ID  <para>body微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系，此参数需在本系统先进行配置。</para><para>示例值：wxd678efh567hg6787</para></param>
        /// <param name="service_id">服务ID  <para>body该服务ID有本接口对应产品的权限。</para><para>示例值：500001</para></param>
        public PayServiceOrderRequestData(string out_order_no, string appid, string service_id)
        {
            this.out_order_no = out_order_no;
            this.appid = appid;
            this.service_id = service_id;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public PayServiceOrderRequestData()
        {
        }

        /// <summary>
        /// 商户服务订单号 
        /// <para>path商户系统内部服务订单号（不是交易单号），与创建订单时一致。 </para>
        /// <para>示例值：2304203423948239423 </para>
        /// </summary>
        [JsonIgnore]
        public string out_order_no { get; set; }

        /// <summary>
        /// 应用ID 
        /// <para>body 微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系，此参数需在本系统先进行配置。 </para>
        /// <para>示例值：wxd678efh567hg6787 </para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 服务ID 
        /// <para>body 该服务ID有本接口对应产品的权限。</para>
        /// <para>示例值：500001 </para>
        /// </summary>
        public string service_id { get; set; }

    }

}
