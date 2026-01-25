#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2026 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2026 Senparc
  
    文件名：ApplyElecsignReturnJson.cs
    文件功能描述：商家转账 - 申请电子回单API 返回信息
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.FundApp
{
    /// <summary>
    /// 商家转账 - 申请电子回单API 返回信息
    /// <para>适用于商户单号申请和微信单号申请电子回单接口</para>
    /// </summary>
    public class ApplyElecsignReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 申请单状态
        /// <para>GENERATING：生成中，FINISHED: 已完成，FAILED: 已失败</para>
        /// <para>可选取值：</para>
        /// <para>GENERATING: 表示当前电子回单已受理成功并在处理中</para>
        /// <para>FINISHED: 表示当前电子回单已处理完成</para>
        /// <para>FAILED: 表示当前电子回单生成失败，失败原因字段会返回具体的失败原因</para>
        /// <para>示例值：FINISHED</para>
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 申请单创建时间
        /// <para>申请单的创建时间，按照使用rfc3339所定义的格式，格式为yyyy-MM-DDThh:mm:ss+TIMEZONE</para>
        /// <para>示例值：2015-05-20T13:29:35.120+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }
    }
}

