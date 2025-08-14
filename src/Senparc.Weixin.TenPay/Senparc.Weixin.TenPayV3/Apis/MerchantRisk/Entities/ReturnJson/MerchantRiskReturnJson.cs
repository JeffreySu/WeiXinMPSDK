#region Apache License Version 2.0
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
  
    文件名：MerchantRiskReturnJson.cs
    文件功能描述：商户违规通知回调 - API 返回信息
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.Entities;
using System;

namespace Senparc.Weixin.TenPayV3.Apis.MerchantRisk
{
    /// <summary>
    /// 商户违规通知回调 - 创建回调地址API 返回信息
    /// </summary>
    public class CreateMerchantRiskNotifyUrlReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 通知地址
        /// <para>接收商户违规通知的回调地址</para>
        /// <para>示例值：https://www.weixin.qq.com/wxpay/pay.php</para>
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 创建时间
        /// <para>回调地址创建时间</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }
    }

    /// <summary>
    /// 商户违规通知回调 - 查询回调地址API 返回信息
    /// </summary>
    public class QueryMerchantRiskNotifyUrlReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 通知地址
        /// <para>接收商户违规通知的回调地址</para>
        /// <para>示例值：https://www.weixin.qq.com/wxpay/pay.php</para>
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 创建时间
        /// <para>回调地址创建时间</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public DateTime create_time { get; set; }

        /// <summary>
        /// 更新时间
        /// <para>回调地址最后更新时间</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public DateTime update_time { get; set; }
    }

    /// <summary>
    /// 商户违规通知回调 - 修改回调地址API 返回信息
    /// </summary>
    public class UpdateMerchantRiskNotifyUrlReturnJson : ReturnJsonBase
    {
        /// <summary>
        /// 通知地址
        /// <para>接收商户违规通知的回调地址</para>
        /// <para>示例值：https://www.weixin.qq.com/wxpay/pay.php</para>
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 更新时间
        /// <para>回调地址更新时间</para>
        /// <para>示例值：2015-05-20T13:29:35+08:00</para>
        /// </summary>
        public DateTime update_time { get; set; }
    }

    /// <summary>
    /// 商户违规通知回调 - 删除回调地址API 返回信息
    /// </summary>
    public class DeleteMerchantRiskNotifyUrlReturnJson : ReturnJsonBase
    {
        // 删除成功时，微信支付无特定返回内容，HTTP状态码为204
    }
}


