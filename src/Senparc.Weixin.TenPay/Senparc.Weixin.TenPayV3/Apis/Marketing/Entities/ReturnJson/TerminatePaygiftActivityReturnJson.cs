﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2025 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2025 Senparc
  
    文件名：TerminatePaygiftActivityReturnJson.cs
    文件功能描述：终止支付有礼活动返回Json类
    
    
    创建标识：Senparc - 20210915
    
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
    /// 终止支付有礼活动返回Json类
    /// <para>详细请参考微信支付官方文档 https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter9_7_7.shtml </para>
    /// </summary>
    public class TerminatePaygiftActivityReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 含参构造函数
        /// </summary>
        /// <param name="terminate_time">生效时间<para>生效时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。</para><para>示例值：2015-05-20T13:29:35+08:00</para></param>
        /// <param name="activity_id">活动id<para>活动id</para><para>示例值：126002309</para></param>
        public TerminatePaygiftActivityReturnJson(string terminate_time, string activity_id)
        {
            this.terminate_time = terminate_time;
            this.activity_id = activity_id;
        }

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TerminatePaygiftActivityReturnJson()
        {
        }

        /// <summary>
        /// 生效时间 
        /// <para>生效时间，遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，YYYY-MM-DD表示年月日，T出现在字符串中，表示time元素的开头，HH:mm:ss表示时分秒，TIMEZONE表示时区（+08:00表示东八区时间，领先UTC 8小时，即北京时间）。例如：2015-05-20T13:29:35+08:00表示，北京时间2015年5月20日13点29分35秒。 </para>
        /// <para>示例值：2015-05-20T13:29:35+08:00 </para>
        /// </summary>
        public string terminate_time { get; set; }

        /// <summary>
        /// 活动id 
        /// <para>活动id </para>
        /// <para>示例值：126002309 </para>
        /// </summary>
        public string activity_id { get; set; }

    }
}


