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
  
    文件名：NativeCombineRequestData.cs
    文件功能描述：Native合单支付请求数据
    
    
    创建标识：Senparc - 20210814
    
----------------------------------------------------------------*/

using Senparc.Weixin.TenPayV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class NativeCombineRequestData
    {

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
        public Combine_Payer_Info combine_payer_info { get; set; }

        /// <summary>
        /// 订单生成时间
        /// 遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE
        /// 示例值：2018-06-08T10:34:56+08:00
        /// </summary>
        public TenpayDateTime time_start { get; set; }

        /// <summary>
        /// 订单失效时间
        /// 遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE
        /// 示例值：2018-06-08T10:34:56+08:00
        /// </summary>
        public TenpayDateTime time_expire { get; set; }

        /// <summary>
        /// 通知地址
        /// 通知URL必须为直接可访问的URL，不允许携带查询串，要求必须为https地址。
        /// 示例值：https://www.weixin.qq.com/wxpay/pay.php
        /// </summary>
        public string notify_url { get; set; }

        public class Amount
        {
            /// <summary>
            /// 总金额
            /// 订单总金额，单位为分。
            /// 示例值：100 (1元)
            /// </summary>
            public int total { get; set; }

            /// <summary>
            /// 货币类型
            /// CNY：人民币，境内商户号仅支持人民币。
            /// 示例值：CNY
            /// </summary>
            public string currency { get; set; }
        }

        /// <summary>
        /// 场景信息
        /// </summary>
        public class Scene_Info
        {
            /// <summary>
            /// 商户端设备号
            /// 商户端设备号（门店号或收银设备ID）。
            /// 示例值：013467007045764
            /// </summary>
            public string device_id { get; set; }

            /// <summary>
            /// 用户终端IP
            /// 用户的客户端IP，支持IPv4和IPv6两种格式的IP地址。
            /// 示例值：14.23.150.211
            /// </summary>
            public string payer_client_ip { get; set; }
        }

        /// <summary>
        /// 商户门店信息
        /// </summary>
        public class Store_Info
        {
            /// <summary>
            /// 详细地址
            /// 详细的商户门店地址
            /// 示例值：广东省深圳市南山区科技中一道10000号
            /// </summary>
            public string address { get; set; }

            /// <summary>
            /// 地区编码	
            /// 地区编码，详细请见省市区编号对照表。
            /// 示例值：440305
            /// </summary>
            public string area_code { get; set; }

            /// <summary>
            /// 门店名称
            /// 商户侧门店名称
            /// 示例值：腾讯大厦分店
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 门店编号
            /// 商户侧门店编号
            /// 示例值：0001
            /// </summary>
            public string id { get; set; }
        }

        /// <summary>
        /// 结算信息
        /// </summary>
        public class Settle_Info
        {
            /// <summary>
            /// 是否指定分账
            /// </summary>
            public bool profit_sharing { get; set; }

            /// <summary>
            /// 补差金额
            /// SettleInfo.profit_sharing为true时，该金额才生效。
            /// 注意：单笔订单最高补差金额为5000元
            /// 示例值：10
            /// </summary>
            public long subsidy_amount { get; set; }
        }

        /// <summary>
        /// 子单信息
        /// </summary>
        public class Sub_Orders
        {
            /// <summary>
            /// 子单商户号
            /// 子单发起方商户号即合单参与方商户号，必须与发起方appid有绑定关系。
            /// 示例值：1900000109
            /// </summary>
            public string mchid { get; set; }

            /// <summary>
            /// 附加数据
            /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用
            /// 示例值：自定义数据
            /// </summary>
            public string attach { get; set; }

            /// <summary>
            /// 订单金额
            /// </summary>
            public Amount amount { get; set; }

            /// <summary>
            /// 子单商户订单号	
            /// 商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|*@ ，且在同一个商户号下唯一。
            /// 示例值：20150806125346
            /// </summary>
            public string out_trade_no { get; set; }

            /// <summary>
            /// 商品描述
            /// 示例值：Image形象店-深圳腾大-QQ公仔
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 结算信息
            /// </summary>
            public Settle_Info settle_info { get; set; }
        }

        /// <summary>
        /// 支付者信息
        /// </summary>
        public class Combine_Payer_Info
        {
            /// <summary>
            /// 用户标识	
            /// 用户在直连商户appid下的唯一标识。 下单前需获取到用户的Openid，Openid获取详见
            /// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
            /// </summary>
            public string openid { get; set; }
        }

    }

}
