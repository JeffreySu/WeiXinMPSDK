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
  
    文件名：SendCardRequestData.cs
    文件功能描述：建发放消费卡接口请求数据
    
    
    创建标识：Senparc - 20210915
    
----------------------------------------------------------------*/


using Newtonsoft.Json;
using Senparc.Weixin.TenPayV3.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 建发放消费卡接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_6_1.shtml </para>
    /// </summary>
    public class SendCardRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="card_id">消费卡ID  <para>path消费卡ID，即card_id。card_id获取方法请参见《接入前准备》配置应用中的创建消费卡。</para><para>示例值：pIJMr5MMiIkO_93VtPyIiEk2DZ4w</para></param>
        /// <param name="appid">消费卡归属appid  <para>body消费卡card_id归属appid，需与api调用方商户号有M-A绑定关系，需和创建消费卡信息中填入的归属appid一致。入参中的用户openid也需用此appid生成。</para><para>示例值：wxc0b84a53ed8e8d29</para></param>
        /// <param name="openid">用户openid  <para>body待发卡用户的openid，需为消费卡归属appid生成的openid。</para><para>示例值：obLatjhnqgy2syxrXVM3MJirbkdI</para></param>
        /// <param name="out_request_no">商户单据号  <para>body商户此次发放凭据号。推荐使用大小写字母和数字，不同添加请求发放凭据号不同，商户侧需保证同一发券请求的out_request_no和send_time的唯一性。</para><para>示例值：oTYhjfdsahnssddj_0136</para></param>
        /// <param name="send_time">请求发卡时间  <para>body单次请求发卡时间，消费卡在商户系统的实际发放时间，为东八区标准时间（UTC+8）。商户需保证同一次请求的out_request_no和send_time唯一。由于系统限制，暂不支持传入早于当前时间24小时以上的时间进行发券请求。</para><para>示例值：2019-12-31T13:29:35.120+08:00</para></param>
        public SendCardRequestData(string card_id, string appid, string openid, string out_request_no, TenpayDateTime send_time)
        {
            this.card_id = card_id;
            this.appid = appid;
            this.openid = openid;
            this.out_request_no = out_request_no;
            this.send_time = send_time.ToString();
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SendCardRequestData()
        {
        }

        /// <summary>
        /// 消费卡ID 
        /// <para>path 消费卡ID，即card_id。card_id获取方法请参见《接入前准备》配置应用中的创建消费卡。 </para>
        /// <para>示例值：pIJMr5MMiIkO_93VtPyIiEk2DZ4w </para>
        /// </summary>
        [JsonIgnore]
        public string card_id { get; set; }

        /// <summary>
        /// 消费卡归属appid 
        /// <para>body 消费卡card_id 归属appid，需与api调用方商户号有M-A绑定关系，需和创建消费卡信息中填入的归属appid一致。入参中的用户openid也需用此appid生成。 </para>
        /// <para>示例值：wxc0b84a53ed8e8d29 </para>
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 用户openid 
        /// <para>body 待发卡用户的openid，需为消费卡归属appid生成的openid。 </para>
        /// <para>示例值：obLatjhnqgy2syxrXVM3MJirbkdI </para>
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 商户单据号 
        /// <para>body 商户此次发放凭据号。推荐使用大小写字母和数字，不同添加请求发放凭据号不同，商户侧需保证同一发券请求的out_request_no和send_time的唯一性。 </para>
        /// <para>示例值：oTYhjfdsahnssddj_0136 </para>
        /// </summary>
        public string out_request_no { get; set; }

        /// <summary>
        /// 请求发卡时间 
        /// <para>body 单次请求发卡时间，消费卡在商户系统的实际发放时间，为东八区标准时间（UTC+8）。商户需保证同一次请求的out_request_no和send_time唯一。由于系统限制，暂不支持传入早于当前时间24小时以上的时间进行发券请求。 </para>
        /// <para>示例值：2019-12-31T13:29:35.120+08:00 </para>
        /// </summary>
        public string send_time { get; set; }

    }
}
