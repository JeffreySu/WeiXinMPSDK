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
  
    文件名：QueryBusifavorSetNotifyUrlReturnJson.cs
    文件功能描述：查询商家券事件通知地址返回Json类
    
    
    创建标识：Senparc - 20210912
    
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Marketing
{
    /// <summary>
    /// 查询商家券事件通知地址返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_8.shtml </para>
    /// </summary>
    public class QueryBusifavorNotifyUrlReturnJson : ReturnJsonBase
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="notify_url">通知URL地址  <para>商户提供的用于接收商家券事件通知的url地址，必须支持https。</para><para>示例值：https://pay.weixin.qq.com</para></param>
        /// <param name="mchid">商户号  <para>微信支付商户的商户号，由微信支付生成并下发。</para><para>示例值：10000098</para></param>
        public QueryBusifavorNotifyUrlReturnJson(string notify_url, string mchid)
        {
            this.notify_url = notify_url;
            this.mchid = mchid;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public QueryBusifavorNotifyUrlReturnJson()
        {
        }

        /// <summary>
        /// 通知URL地址 
        /// <para>商户提供的用于接收商家券事件通知的url地址，必须支持https。 </para>
        /// <para>示例值：https://pay.weixin.qq.com </para>
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 商户号 
        /// <para>微信支付商户的商户号，由微信支付生成并下发。 </para>
        /// <para>示例值：10000098 </para>
        /// </summary>
        public string mchid { get; set; }

    }


}
