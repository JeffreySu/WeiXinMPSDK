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
  
    文件名：NotifyReturnData.cs
    文件功能描述：支付回调通知返回给微信的数据
    
    
    创建标识：Senparc - 20210820
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.Entities
{
    /// <summary>
    /// 支付回调通知返回给微信的数据
    /// </summary>
    public class NotifyReturnData
    {
        public NotifyReturnData() : this(null, null)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">返回状态码 SUCCESS为清算机构接收成功，其他状态码为失败</param>
        /// <param name="message">返回信息，如非空，为错误原因。</param>
        public NotifyReturnData(string code, string message)
        {
            this.code = code;
            this.message = message;
        }

        /// <summary>
        /// 返回状态码
        /// 错误码，SUCCESS为清算机构接收成功，其他错误码为失败。
        /// 示例值：SUCCESS
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 返回信息，如非空，为错误原因。
        /// 示例值：系统错误
        /// </summary>
        public string message { get; set; }
    }
}
