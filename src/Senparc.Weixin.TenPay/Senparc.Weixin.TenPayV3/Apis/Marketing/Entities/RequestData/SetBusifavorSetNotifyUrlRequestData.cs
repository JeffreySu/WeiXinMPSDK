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
  
    文件名：SetBusifavorSetNotifyUrlRequestData.cs
    文件功能描述：设置商家券事件通知地址请求数据
    
    
    创建标识：Senparc - 20210912
    
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
    /// 设置商家券事件通知地址请求数据设置消息通知地址请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_2_7.shtml </para>
    /// </summary>
    public class SetBusifavorSetNotifyUrlRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="mchid">商户号  <para>body微信支付商户的商户号，由微信支付生成并下发，不填默认查询调用方商户的通知URL。</para><para>示例值：10000098</para><para>可为null</para></param>
        /// <param name="notify_url">通知URL地址  <para>body商户提供的用于接收商家券事件通知的url地址，必须支持https。</para><para>示例值：https://pay.weixin.qq.com</para></param>
        public SetBusifavorSetNotifyUrlRequestData(string mchid, string notify_url)
        {
            this.mchid = mchid;
            this.notify_url = notify_url;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SetBusifavorSetNotifyUrlRequestData()
        {
        }

        /// <summary>
        /// 商户号 
        /// <para>body 微信支付商户的商户号，由微信支付生成并下发，不填默认查询调用方商户的通知URL。 </para>
        /// <para>示例值：10000098 </para>
        /// <para>可为null</para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 通知URL地址 
        /// <para>body 商户提供的用于接收商家券事件通知的url地址，必须支持https。 </para>
        /// <para>示例值：https://pay.weixin.qq.com </para>
        /// </summary>
        public string notify_url { get; set; }

    }


}
