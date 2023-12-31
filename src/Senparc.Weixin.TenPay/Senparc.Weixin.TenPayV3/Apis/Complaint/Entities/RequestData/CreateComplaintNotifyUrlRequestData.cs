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
  
    文件名：CreateComplaintNotifyUrlRequestData.cs
    文件功能描述：创建投诉通知回调地址接口请求数据
    
    
    创建标识：Senparc - 20210925

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.Complaint
{
    /// <summary>
    /// 商圈积分同步接口请求数据
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter10_2_2.shtml </para>
    /// </summary>
    public class CreateComplaintNotifyUrlRequestData
    {

        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="url">通知地址  <para>body通知地址，仅支持https。</para><para>示例值：https://www.xxx.com/notify</para></param>
        public CreateComplaintNotifyUrlRequestData(string url)
        {
            this.url = url;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public CreateComplaintNotifyUrlRequestData()
        {
        }

        /// <summary>
        /// 通知地址 
        /// <para>body 通知地址，仅支持https。 </para>
        /// <para>示例值：https://www.xxx.com/notify </para>
        /// </summary>
        public string url { get; set; }

    }
}
