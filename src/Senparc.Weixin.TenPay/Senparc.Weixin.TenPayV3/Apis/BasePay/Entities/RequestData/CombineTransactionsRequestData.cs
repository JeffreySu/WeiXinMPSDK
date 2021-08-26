#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2021 Senparc
  
    文件名：CombineTransactionsRequestData.cs
    文件功能描述：合单支付请求数据
    
    
    创建标识：Senparc - 20210825
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Apis.BasePay.Entities.RequestData.Entities;
using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class CombineTransactionsRequestData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="combine_appid">合单商户appid</param>
        /// <param name="combine_mchid">合单商户号</param>
        /// <param name="combine_out_trade_no">合单商户订单号</param>
        /// <param name="scene_info">支付场景描述，可为null</param>
        /// <param name="sub_orders">子单信息 最多支持子单条数：10</param>
        /// <param name="combine_payer_info">支付者信息</param>
        /// <param name="time_start">订单生成时间 遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，可为null</param>
        /// <param name="time_expire">订单失效时间 遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，可为null</param>
        /// <param name="notify_url">通知URL 必须为直接可访问的URL，不允许携带查询串，要求必须为https地址。</param>
        public CombineTransactionsRequestData(string combine_appid, string combine_mchid, string combine_out_trade_no, Scene_Info scene_info, Sub_Orders[] sub_orders, Payer combine_payer_info, TenpayDateTime time_start, TenpayDateTime time_expire, string notify_url)
        {
            this.combine_appid = combine_appid;
            this.combine_mchid = combine_mchid;
            this.combine_out_trade_no = combine_out_trade_no;
            this.scene_info = scene_info;
            this.sub_orders = sub_orders;
            this.combine_payer_info = combine_payer_info;
            this.time_start = time_start.ToString();
            this.time_expire = time_expire.ToString();
            this.notify_url = notify_url;
        }

        /// <summary>
        /// 合单商户appid	
        /// 合单发起方的appid。
        /// 示例值：wxd678efh567hg6787
        /// </summary>
        public string combine_appid { get; set; }

        /// <summary>
        /// 合单商户号
        /// 合单发起方商户号。
        /// 示例值：1900000109
        /// </summary>
        public string combine_mchid { get; set; }

        /// <summary>
        /// 合单商户订单号	
        /// 合单支付总订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
        /// 示例值：P20150806125346
        /// </summary>
        public string combine_out_trade_no { get; set; }

        /// <summary>
        /// 场景信息 支付场景描述
        /// </summary>
        public Scene_Info scene_info { get; set; }

        /// <summary>
        /// 子单信息
        /// 最多支持子单条数：10
        /// </summary>
        public Sub_Orders[] sub_orders { get; set; }

        /// <summary>
        /// 支付者信息
        /// </summary>
        public Payer combine_payer_info { get; set; }

        /// <summary>
        /// 订单生成时间
        /// 遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE
        /// 示例值：2018-06-08T10:34:56+08:00
        /// </summary>
        public string time_start { get; set; }

        /// <summary>
        /// 订单失效时间
        /// 遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE
        /// 示例值：2018-06-08T10:34:56+08:00
        /// </summary>
        public string time_expire { get; set; }

        /// <summary>
        /// 通知地址
        /// 通知URL必须为直接可访问的URL，不允许携带查询串，要求必须为https地址。
        /// 示例值：https://www.weixin.qq.com/wxpay/pay.php
        /// </summary>
        public string notify_url { get; set; }
    }
}
