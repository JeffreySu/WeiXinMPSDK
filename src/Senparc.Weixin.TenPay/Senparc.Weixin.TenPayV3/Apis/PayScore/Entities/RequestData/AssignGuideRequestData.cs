#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2023 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2023 Senparc
  
    文件名：AssignGuideRequestData.cs
    文件功能描述：微信支付V3服务人员分配接口请求数据
    
    
    创建标识：Senparc - 20210925
    
----------------------------------------------------------------*/


using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.PayScore
{
    /// <summary>
    /// 微信支付V3服务人员分配接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter8_4_2.shtml </para>
    /// </summary>
    public class AssignGuideRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="guide_id">服务人员ID <para>path服务人员在服务人员系统中的唯一标识</para><para>示例值：LLA3WJ6DSZUfiaZDS79FH5Wm5m4X69TBic</para></param>
        /// <param name="out_trade_no">商户订单号 <para>body商户系统内部订单号，要求32个字符内，仅支持使用字母、数字、中划线-、下划线_、竖线|、星号*这些英文半角字符的组合，请勿使用汉字或全角等特殊字符，且在同一个商户号下唯一。</para><para>示例值：20150806125346</para></param>
        public AssignGuideRequestData(string guide_id, string out_trade_no)
        {
            this.guide_id = guide_id;
            this.out_trade_no = out_trade_no;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public AssignGuideRequestData()
        {
        }

        /// <summary>
        /// 服务人员ID
        /// <para>path 服务人员在服务人员系统中的唯一标识 </para>
        /// <para>示例值：LLA3WJ6DSZUfiaZDS79FH5Wm5m4X69TBic</para>
        /// </summary>
        [JsonIgnore]
        public string guide_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// <para>body 商户系统内部订单号，要求32个字符内，仅支持使用字母、数字、中划线-、下划线_、竖线|、星号*这些英文半角字符的组合，请勿使用汉字或全角等特殊字符，且在同一个商户号下唯一。</para>
        /// <para>示例值：20150806125346</para>
        /// </summary>
        public string out_trade_no { get; set; }

    }
}
