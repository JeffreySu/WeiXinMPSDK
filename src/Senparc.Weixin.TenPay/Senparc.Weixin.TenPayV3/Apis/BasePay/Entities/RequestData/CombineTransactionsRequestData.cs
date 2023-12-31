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
  
    文件名：CombineTransactionsRequestData.cs
    文件功能描述：合单支付请求数据
    
    
    创建标识：Senparc - 20210825
    
----------------------------------------------------------------*/

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
        /// 无参构造函数
        /// </summary>
        public CombineTransactionsRequestData() { }

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
        public CombineTransactionsRequestData(string combine_appid, string combine_mchid, string combine_out_trade_no, Scene_Info scene_info, IEnumerable<Sub_Order> sub_orders, Combine_Payer_Info combine_payer_info, TenpayDateTime time_start, TenpayDateTime time_expire, string notify_url)
        {
            this.combine_appid = combine_appid;
            this.combine_mchid = combine_mchid;
            this.combine_out_trade_no = combine_out_trade_no;
            this.scene_info = scene_info;
            this.sub_orders = sub_orders;
            this.combine_payer_info = combine_payer_info;
            this.time_start = time_start?.ToString();
            this.time_expire = time_expire?.ToString();
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
        /// 合单发起方商户号，服务商和电商模式下，传服务商商户号。
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
        /// 子单信息数组
        /// 最多支持子单条数：10
        /// </summary>
        public IEnumerable<Sub_Order> sub_orders { get; set; }

        /// <summary>
        /// 支付者信息
        /// </summary>
        public Combine_Payer_Info combine_payer_info { get; set; }

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


        #region 请求数据类型

        /// <summary>
        /// 场景信息
        /// </summary>
        public class Scene_Info
        {
            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="payer_client_ip">用户终端IP</param>
            /// <param name="device_id">商户端设备号，可为null</param>
            /// <param name="h5_info">H5场景信息，H5下单必填，其它支付方式必须为null</param>
            public Scene_Info(string payer_client_ip, string device_id, H5_Info h5_info = null)
            {
                this.device_id = device_id;
                this.payer_client_ip = payer_client_ip;
                this.h5_info = h5_info;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Scene_Info()
            {
            }

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

            /// <summary>
            /// H5场景信息
            /// </summary>
            public H5_Info h5_info { get; set; }

            #region 请求数据类型

            /// <summary>
            /// H5场景信息
            /// </summary>
            public class H5_Info
            {
                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="type">场景类型</param>
                /// <param name="app_name">应用名称，可为null</param>
                /// <param name="app_url">应用URL，可为null</param>
                /// <param name="bundle_id">iOS平台BundleID，可为null</param>
                /// <param name="package_name">Android平台PackageName，可为null</param>
                public H5_Info(string type, string app_name, string app_url, string bundle_id, string package_name)
                {
                    this.type = type;
                    this.app_name = app_name;
                    this.app_url = app_url;
                    this.bundle_id = bundle_id;
                    this.package_name = package_name;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public H5_Info()
                {
                }

                /// <summary>
                /// 场景类型
                /// 示例值：iOS, Android, Wap
                /// </summary>
                public string type { get; set; }

                /// <summary>
                /// 应用名称
                /// 示例值：王者荣耀
                /// </summary>
                public string app_name { get; set; }

                /// <summary>
                /// 网站URL
                /// 示例值：https://pay.qq.com
                /// </summary>
                public string app_url { get; set; }

                /// <summary>
                /// iOS平台BundleID
                /// 示例值：com.tencent.wzryiOS
                /// </summary>
                public string bundle_id { get; set; }

                /// <summary>
                /// Android平台PackageName
                /// 示例值：com.tencent.tmgp.sgame
                /// </summary>
                public string package_name { get; set; }
            }
            #endregion
        }

        /// <summary>
        /// 子单信息
        /// </summary>
        public class Sub_Order
        {
            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="mchid">子单商户号，即合单参与方商户号，必须与发起方appid有绑定关系</param>
            /// <param name="attach">附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用</param>
            /// <param name="amount">订单金额</param>
            /// <param name="out_trade_no">商户系统内部订单号</param>
            /// <param name="goods_tag">订单优惠标记，可为null</param>
            /// <param name="description">商品描述</param>
            /// <param name="settle_info">结算信息，可为null</param>
            public Sub_Order(string mchid, string attach, Amount amount, string out_trade_no, string goods_tag, string description, Settle_Info settle_info)
            {
                this.mchid = mchid;
                this.attach = attach;
                this.amount = amount;
                this.out_trade_no = out_trade_no;
                this.goods_tag = goods_tag;
                this.description = description;
                this.settle_info = settle_info;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Sub_Order()
            {
            }

            /// <summary>
            /// 子单商户号
            /// 子单发起方商户号即合单参与方商户号，必须与发起方appid有绑定关系。服务商和电商模式下，传服务商商户号。
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
            /// 订单优惠标记
            /// 示例值：WXG
            /// </summary>
            public string goods_tag { get; set; }

            /// <summary>
            /// 商品描述
            /// 示例值：Image形象店-深圳腾大-QQ公仔
            /// </summary>
            public string description { get; set; }

            /// <summary>
            /// 结算信息
            /// </summary>
            public Settle_Info settle_info { get; set; }

            #region 请求数据类型

            /// <summary>
            /// 订单金额
            /// </summary>
            public class Amount
            {
                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="total_amount">订单总金额，单位为分</param>
                /// <param name="currency">货币类型 CNY：人民币，境内商户号仅支持人民币</param>
                public Amount(int total_amount, string currency)
                {
                    this.total_amount = total_amount;
                    this.currency = currency;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Amount()
                {
                }

                /// <summary>
                /// 总金额
                /// 订单总金额，单位为分。
                /// 示例值：100 (1元)
                /// </summary>
                public int total_amount { get; set; }

                /// <summary>
                /// 货币类型
                /// CNY：人民币，境内商户号仅支持人民币。
                /// 示例值：CNY
                /// </summary>
                public string currency { get; set; }
            }

            /// <summary>
            /// 结算信息
            /// </summary>
            public class Settle_Info
            {
                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="profit_sharing">是否指定分账，可为null</param>
                /// <param name="subsidy_amount">补差金额	，SettleInfo.profit_sharing为true时，该金额才生效，可为null</param>
                public Settle_Info(bool profit_sharing, long subsidy_amount)
                {
                    this.profit_sharing = profit_sharing;
                    this.subsidy_amount = subsidy_amount;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Settle_Info()
                {
                }

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

            #endregion
        }

        /// <summary>
        /// 支付者信息
        /// </summary>
        public class Combine_Payer_Info
        {
            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="openid">用户在直连商户appid下的唯一标识</param>
            public Combine_Payer_Info(string openid)
            {
                this.openid = openid;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Combine_Payer_Info()
            {
            }

            /// <summary>
            /// 用户标识	
            /// 用户在直连商户appid下的唯一标识
            /// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
            /// </summary>
            public string openid { get; set; }
        }

        #endregion
    }
}
