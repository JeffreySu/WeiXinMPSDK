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
  
    文件名：SetNotifyUrlRequsetData.cs
    文件功能描述：设置消息通知地址请求数据
    
    
    创建标识：Senparc - 20210901
    
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
    /// 设置消息通知地址请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_1_12.shtml </para>
    /// </summary>
    public class SetNotifyUrlRequsetData
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="mchid">商户号</param>
        /// <param name="notify_url">通知url地址</param>
        /// <param name="switch">回调开关，可为null <para>如果商户不需要再接收营销事件通知，可通过该开关关闭</para> <para>枚举值: true：开启推送 false：停止推送</para></param>
        public SetNotifyUrlRequsetData(string mchid, string notify_url, bool? @switch)
        {
            this.mchid = mchid;
            this.notify_url = notify_url;
            this.@switch = @switch;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public SetNotifyUrlRequsetData()
        {
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 通知url地址
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 回调开关
        /// <para>如果商户不需要再接收营销事件通知，可通过该开关关闭</para>
        /// <para>枚举值: true：开启推送 false：停止推送</para>
        /// </summary>
        public bool? @switch { get; set; }
    }
}
