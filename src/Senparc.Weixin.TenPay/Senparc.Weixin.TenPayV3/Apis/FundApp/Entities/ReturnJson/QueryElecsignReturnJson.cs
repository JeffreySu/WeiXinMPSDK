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
  
    文件名：QueryElecsignReturnJson.cs
    文件功能描述：商家转账 - 查询电子回单API 返回信息
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.FundApp
{
    /// <summary>
    /// 商家转账 - 查询电子回单API 返回信息
    /// <para>适用于商户单号查询和微信单号查询电子回单接口</para>
    /// </summary>
    public class QueryElecsignReturnJson : ReturnJsonBase
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

        /// <summary>
        /// 电子回单文件下载链接
        /// <para>电子回单文件的下载链接，该链接有效期为30分钟</para>
        /// <para>注意：仅当申请单状态为 FINISHED 时，此字段才会返回</para>
        /// <para>示例值：https://api.mch.weixin.qq.com/v3/mch-transfer/bills/1330000071100999991182020050700019480001/receipt</para>
        /// </summary>
        public string download_url { get; set; }

        /// <summary>
        /// 电子回单文件的Hash值
        /// <para>电子回单文件的Hash值，用于校验文件完整性</para>
        /// <para>注意：仅当申请单状态为 FINISHED 时，此字段才会返回</para>
        /// <para>示例值：b7f77428ec07ea5a8ef3a6b44b3bb00af7a83baebbbfb3fc19ba7c3b3a8d98e2</para>
        /// </summary>
        public string hash_value { get; set; }

        /// <summary>
        /// Hash算法类型
        /// <para>电子回单文件Hash值的算法类型</para>
        /// <para>注意：仅当申请单状态为 FINISHED 时，此字段才会返回</para>
        /// <para>示例值：SHA256</para>
        /// </summary>
        public string hash_type { get; set; }

        /// <summary>
        /// 失败原因
        /// <para>电子回单生成失败时的失败原因</para>
        /// <para>注意：仅当申请单状态为 FAILED 时，此字段才会返回</para>
        /// <para>示例值：转账单据不满足回单申请条件</para>
        /// </summary>
        public string fail_reason { get; set; }
    }
}

