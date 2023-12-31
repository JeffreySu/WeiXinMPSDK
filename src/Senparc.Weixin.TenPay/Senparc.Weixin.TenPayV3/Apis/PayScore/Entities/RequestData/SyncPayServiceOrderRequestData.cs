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
  
    文件名：SyncPayServiceOrderRequestData.cs
    文件功能描述：微信支付V3同步服务订单信息接口请求数据
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/

using Newtonsoft.Json;
using Senparc.Weixin.TenPayV3.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3同步服务订单信息接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter6_1_20.shtml </para>
    /// </summary>
    public class SyncPayServiceOrderRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="out_order_no">商户服务订单号  <para>path商户系统内部订单号（不是交易单号），要求此参数只能由数字、大小写字母_-|*组成，且在同一个商户号下唯一，详见「商户订单号」，需要和创建订单的商户服务订单号一致。</para><para>示例值：1234323JKHDFE1243252</para></param>
        /// <param name="appid">应用ID  <para>body微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系。此参数需在本系统先进行配置，并与创建订单时的appid保持一致。</para><para>示例值：wxd678efh567hg6787</para></param>
        /// <param name="service_id">服务ID  <para>body该服务ID有本接口对应产品的权限，需要与创建订单时保持一致。</para><para>示例值：500001</para></param>
        /// <param name="type">场景类型  <para>body场景类型为“Order_Paid”，字符串表示“订单收款成功”。</para><para>示例值：Order_Paid</para></param>
        /// <param name="detail">内容信息详情 <para>body场景类型为Order_Paid时，为必填项。</para><para>可为null</para></param>
        public SyncPayServiceOrderRequestData(string out_order_no, string appid, string service_id, string type, Detail detail)
        {
            this.out_order_no = out_order_no;
            this.appid = appid;
            this.service_id = service_id;
            this.type = type;
            this.detail = detail;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SyncPayServiceOrderRequestData()
        {
        }

        /// <summary>
        /// 商户服务订单号 
        /// <para>path 商户系统内部订单号（不是交易单号），要求此参数只能由数字、大小写字母_-|*组成，且在同一个商户号下唯一，详见「商户订单号」，需要和创建订单的商户服务订单号一致。</para>
        /// <para>示例值：1234323JKHDFE1243252 </para>
        /// </summary>
        [JsonIgnore]
        public string out_order_no { get; set; }

        /// <summary>
        /// 应用ID 
        /// <para>body 微信公众平台分配的与传入的商户号建立了支付绑定关系的appid，可在公众平台查看绑定关系。 此参数需在本系统先进行配置，并与创建订单时的appid保持一致。 </para>
        /// <para>示例值：wxd678efh567hg6787 </para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 服务ID 
        /// <para>body 该服务ID有本接口对应产品的权限，需要与创建订单时保持一致。 </para>
        /// <para>示例值：500001 </para>
        /// </summary>
        public string service_id { get; set; }

        /// <summary>
        /// 场景类型 
        /// <para>body 场景类型为“Order_Paid”，字符串表示“订单收款成功” 。</para>
        /// <para>示例值：Order_Paid </para>
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 内容信息详情
        /// <para>body 场景类型为Order_Paid时，为必填项。 </para>
        /// <para>可为null</para>
        /// </summary>
        public Detail detail { get; set; }

        #region 子数据类型
        public class Detail
        {

            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="paid_time">收款成功时间  <para>支付成功时间，支持两种格式：yyyyMMddHHmmss和yyyyMMdd</para><para></para><para>●传入20091225091010表示2009年12月25日9点10分10秒。</para><para>●传入20091225默认时间为2009年12月25日0点0分0秒。</para><para></para><para>用户通过其他方式付款成功的实际时间需满足条件：服务开始时间＜调用商户完结订单接口的时间＜用户通过其他方式付款成功的实际时间≤商户调用支付分订单同步接口的时间。【服务开始时间】1、当完结订单有填写【实际服务开始时间】时，【服务开始时间】=完结订单【实际服务开始时间】。2、当完结订单未填写【实际服务开始时间】时，【服务开始时间】=创建订单【服务开始时间】场景类型为Order_Paid时，必填。支持两种格式：yyyyMMddHHmmss和yyyyMMdd●传入20091225091010表示2009年12月25日9点10分10秒。●传入20091225表示时间为2009年12月25日23点59分59秒。</para><para>注意：微信支付分会根据此时间更新用户侧的守约记录、负面记录信息；因此请务必如实填写用户实际付款成功时间，以免造成不必要的客诉。</para><para>示例值：20091225091210</para></param>
            public Detail(TenpayDateTime paid_time)
            {
                this.paid_time = paid_time.ToString();
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Detail()
            {
            }

            /// <summary>
            /// 收款成功时间 
            /// <para>支付成功时间，支持两种格式：yyyyMMddHHmmss和yyyyMMdd</para>
            /// <para></para>
            /// <para>● 传入20091225091010表示2009年12月25日9点10分10秒。 </para>
            /// <para>● 传入20091225默认时间为2009年12月25日0点0分0秒。 </para>
            /// <para></para>
            /// <para>用户通过其他方式付款成功的实际时间需满足条件：服务开始时间＜调用商户完结订单接口的时间＜用户通过其他方式付款成功的实际时间≤商户调用支付分订单同步接口的时间。  【服务开始时间】 1、当完结订单有填写【实际服务开始时间】时，【服务开始时间】=完结订单【实际服务开始时间】。 2、当完结订单未填写【实际服务开始时间】时，【服务开始时间】=创建订单【服务开始时间】  场景类型为Order_Paid时，必填。 支持两种格式：yyyyMMddHHmmss和yyyyMMdd ● 传入20091225091010表示2009年12月25日9点10分10秒。 ● 传入20091225表示时间为2009年12月25日23点59分59秒。 </para>
            /// <para>注意：微信支付分会根据此时间更新用户侧的守约记录、负面记录信息；因此请务必如实填写用户实际付款成功时间，以免造成不必要的客诉。 </para>
            /// <para>示例值：20091225091210 </para>
            /// </summary>
            public string paid_time { get; set; }

        }


        #endregion
    }




}
