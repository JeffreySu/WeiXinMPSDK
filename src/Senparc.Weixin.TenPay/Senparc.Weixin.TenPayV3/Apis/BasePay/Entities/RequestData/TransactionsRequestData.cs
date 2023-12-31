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
  
    文件名：TransactionsRequestData.cs
    文件功能描述：下单请求数据实体
    
    
    创建标识：Senparc - 20210825

    修改标识：Senparc - 20230821
    修改描述：v0.7.10.3 基础支付参数缺失处理 RP#2901
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class TransactionsRequestData
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TransactionsRequestData() { }

        /// <summary>
        /// 含参构造函数(商家模式)
        /// </summary>
        /// <param name="appid">由微信生成的应用ID，全局唯一</param>
        /// <param name="mchid">直连商户的商户号，由微信支付生成并下发</param>
        /// <param name="description">商品描述 示例值：Image形象店-深圳腾大-QQ公仔</param>
        /// <param name="out_trade_no">商户系统内部订单号</param>
        /// <param name="time_expire">订单失效时间 遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，可为null</param>
        /// <param name="attach">附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用，可为null</param>
        /// <param name="notify_url">通知URL 必须为直接可访问的URL，不允许携带查询串，要求必须为https地址</param>
        /// <param name="goods_tag">订单优惠标记 示例值：WXG，可为null</param>
        /// <param name="amount">订单金额</param>
        /// <param name="payer">支付者，JSAPI下单必填，其它下单方式必须为null</param>
        /// <param name="detail">优惠功能，可为null</param>
        /// <param name="settle_info">结算信息，可为null</param>
        /// <param name="scene_info">支付场景描述，H5下单必填，其它支付方式可为null</param>
        /// <param name="support_fapiao">电子发票入口开放标识</param>
        public TransactionsRequestData(string appid, string mchid, string description,
            string out_trade_no, TenpayDateTime time_expire, string attach,
            string notify_url, string goods_tag, Amount amount, Payer payer = null,
            Detail detail = null, Settle_Info settle_info = null, Scene_Info scene_info = null,
            bool support_fapiao = false)
        {
            this.appid = appid;
            this.mchid = mchid;
            this.description = description;
            this.out_trade_no = out_trade_no;
            this.time_expire = time_expire.ToString();
            this.attach = attach;
            this.notify_url = notify_url;
            this.goods_tag = goods_tag;
            this.amount = amount;
            this.payer = payer;
            this.detail = detail;
            this.settle_info = settle_info;
            this.scene_info = scene_info;
            this.support_fapiao = support_fapiao;
        }


        /// <summary>
        /// 含参构造函数(服务商模式)
        /// </summary>
        /// <param name="sp_appid">由微信生成的应用ID，全局唯一。请求基础下单接口时请注意APPID的应用属性，例如公众号场景下，需使用应用属性为公众号的服务号APPID</param>
        /// <param name="sp_mchid">服务商户号，由微信支付生成并下发</param>
        /// <param name="sub_appid">子商户申请的应用ID，全局唯一。请求基础下单接口时请注意APPID的应用属性，例如公众号场景下，需使用应用属性为公众号的APPID 若sub_openid有传的情况下，sub_appid必填，且sub_appid需与sub_openid对应</param>
        /// <param name="sub_mchid">子商户的商户号，由微信支付生成并下发。</param>
        /// <param name="description">商品描述 示例值：Image形象店-深圳腾大-QQ公仔</param>
        /// <param name="out_trade_no">商户系统内部订单号</param>
        /// <param name="time_expire">订单失效时间 遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE，可为null</param>
        /// <param name="attach">附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用，可为null</param>
        /// <param name="notify_url">通知URL 必须为直接可访问的URL，不允许携带查询串，要求必须为https地址</param>
        /// <param name="goods_tag">订单优惠标记 示例值：WXG，可为null</param>
        /// <param name="amount">订单金额</param>
        /// <param name="payer">支付者，JSAPI下单必填，其它下单方式必须为null</param>
        /// <param name="detail">优惠功能，可为null</param>
        /// <param name="settle_info">结算信息，可为null</param>
        /// <param name="scene_info">支付场景描述，H5下单必填，其它支付方式可为null</param>
        /// <param name="support_fapiao">电子发票入口开放标识</param>
        public TransactionsRequestData(string sp_appid, string sp_mchid, string sub_appid, string sub_mchid,
            string description, string out_trade_no, TenpayDateTime time_expire, string attach,
            string notify_url, string goods_tag, Amount amount, Payer payer = null,
            Detail detail = null, Settle_Info settle_info = null, Scene_Info scene_info = null, 
            bool support_fapiao = false)
        {
            this.sp_appid = sp_appid;
            this.sp_mchid = sp_mchid;
            this.sub_appid = sub_appid;
            this.sub_mchid = sub_mchid;
            this.description = description;
            this.out_trade_no = out_trade_no;
            this.time_expire = time_expire.ToString();
            this.attach = attach;
            this.notify_url = notify_url;
            this.goods_tag = goods_tag;
            this.amount = amount;
            this.payer = payer;
            this.detail = detail;
            this.settle_info = settle_info;
            this.scene_info = scene_info;
            this.support_fapiao = support_fapiao;
        }

        #region 商户
        /// <summary>
        /// 应用ID
        /// 由微信生成的应用ID，全局唯一。请求基础下单接口时请注意APPID的应用属性，例如公众号场景下，需使用应用属性为公众号的APPID
        /// 示例值：wxd678efh567hg6787
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 直连商户号
        /// 直连商户的商户号，由微信支付生成并下发。
        /// 示例值：1230000109
        /// </summary>
        public string mchid { get; set; }
        #endregion

        #region 服务商
        /// <summary>
        /// 服务商应用ID
        /// 由微信生成的应用ID，全局唯一。请求基础下单接口时请注意APPID的应用属性，例如公众号场景下，需使用应用属性为公众号的服务号APPID
        /// 示例值：wx8888888888888888
        /// </summary>
        public string sp_appid { get; set; }

        /// <summary>
        /// 服务商户号
        /// 服务商户号，由微信支付生成并下发
        /// 示例值：1230000109
        /// </summary>
        public string sp_mchid { get; set; }

        /// <summary>
        /// 子商户应用ID
        /// 子商户申请的应用ID，全局唯一。请求基础下单接口时请注意APPID的应用属性，例如公众号场景下，需使用应用属性为公众号的APPID
        /// 若sub_openid有传的情况下，sub_appid必填，且sub_appid需与sub_openid对应
        /// 示例值：wxd678efh567hg6999
        /// </summary>
        public string sub_appid { get; set; }

        /// <summary>
        /// 子商户号
        /// 子商户的商户号，由微信支付生成并下发。
        /// 示例值：1900000109
        /// </summary>
        public string sub_mchid { get; set; }
        #endregion


        /// <summary>
        /// 商品描述
        /// 示例值：Image形象店-深圳腾大-QQ公仔
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 商户订单号
        /// 商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一
        /// 建议：最短失效时间间隔大于1分钟
        /// 示例值：1217752501201407033233368018
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 订单失效时间
        /// 遵循rfc3339标准格式，格式为YYYY-MM-DDTHH:mm:ss+TIMEZONE
        /// 示例值：2018-06-08T10:34:56+08:00
        /// </summary>
        public string time_expire { get; set; }

        /// <summary>
        /// 附加数据
        /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用
        /// 示例值：自定义数据
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 通知地址
        /// 通知URL必须为直接可访问的URL，不允许携带查询串，要求必须为https地址。
        /// 示例值：https://www.weixin.qq.com/wxpay/pay.php
        /// </summary>
        public string notify_url { get; set; }

        /// <summary>
        /// 订单优惠标记
        /// 示例值：WXG
        /// </summary>
        public string goods_tag { get; set; }

        /// <summary>
        /// 电子发票入口开放标识
        /// 传入true时，支付成功消息和支付详情页将出现开票入口。需要在微信支付商户平台或微信公众平台开通电子发票功能，传此字段才可生效。
        /// true：是
        /// false：否
        /// </summary>
        public bool support_fapiao { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public Amount amount { get; set; }

        /// <summary>
        /// 支付者信息
        /// </summary>
        public Payer payer;

        /// <summary>
        /// 优惠功能
        /// </summary>
        public Detail detail { get; set; }

        /// <summary>
        /// 结算信息
        /// </summary>
        public Settle_Info settle_info;

        /// <summary>
        /// 场景信息 支付场景描述
        /// </summary>
        public Scene_Info scene_info { get; set; }

        #region 请求数据类型

        /// <summary>
        /// 订单金额
        /// </summary>
        public class Amount
        {
            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="total">订单总金额，单位为分</param>
            /// <param name="currency">货币类型 CNY：人民币，境内商户号仅支持人民币，可为null</param>
            public Amount(int total, string currency)
            {
                this.total = total;
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
            public int total { get; set; }

            /// <summary>
            /// 货币类型
            /// CNY：人民币，境内商户号仅支持人民币。
            /// 示例值：CNY
            /// </summary>
            public string currency { get; set; }
        }

        /// <summary>
        /// 支付者信息
        /// </summary>
        public class Payer
        {
            /// <summary>
            /// 含参构造函数
            /// 商户专用
            /// </summary>
            /// <param name="openid">用户在直连商户appid下的唯一标识</param>
            public Payer(string openid)
            {
                this.openid = openid;
            }

            /// <summary>
            /// 含参构造函数
            /// 服务商专用
            /// </summary>
            /// <param name="sp_openid"></param>
            /// <param name="sub_openid"></param>
            public Payer(string sp_openid, string sub_openid)
            {
                this.sp_openid = sp_openid;
                this.sub_openid = sub_openid;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Payer()
            {
            }

            /// <summary>
            /// 用户标识	
            /// 用户在直连商户appid下的唯一标识
            /// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
            /// </summary>
            public string openid { get; set; }

            #region 服务商专用
            /// <summary>
            /// 用户在服务商appid下的唯一标识。 下单前需获取到用户的Openid，Openid获取详见。
            /// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
            /// </summary>
            public string sp_openid { get; set; }

            /// <summary>
            /// 用户在子商户appid下的唯一标识。若传sub_openid，那sub_appid必填。下单前需获取到用户的Openid，Openid获取详见。
            /// 示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o
            /// </summary>
            public string sub_openid { get; set; }
            #endregion
        }

        /// <summary>
        /// 优惠功能
        /// </summary>
        public class Detail
        {
            /// <summary>
            /// 含参构造函数
            /// </summary>
            /// <param name="cost_price">订单原价，可为null</param>
            /// <param name="invoice_id">商家小票ID，可为null</param>
            /// <param name="goods_detail">单品列表 条目个数限制：[1，6000]，可为null</param>
            public Detail(int cost_price, string invoice_id, Goods_Detail[] goods_detail)
            {
                this.invoice_id = invoice_id;
                this.goods_detail = goods_detail;
                this.cost_price = cost_price;
            }

            /// <summary>
            /// 无参构造函数
            /// </summary>
            public Detail()
            {
            }

            /// <summary>
            /// 商家小票ID
            /// 示例值：微信123
            /// </summary>
            public string invoice_id { get; set; }

            /// <summary>
            /// 单品列表
            /// 条目个数限制：[1，6000]
            /// </summary>
            public Goods_Detail[] goods_detail { get; set; }

            /// <summary>
            /// 订单原价
            /// 1、商户侧一张小票订单可能被分多次支付，订单原价用于记录整张小票的交易金额。
            /// 2、当订单原价与支付金额不相等，则不享受优惠。
            /// 3、该字段主要用于防止同一张小票分多次支付，以享受多次优惠的情况，正常支付订单不必上传此参数。
            /// 示例值：608800
            /// </summary>
            public int cost_price { get; set; }


            #region  请求数据类型

            /// <summary>
            /// 单品信息
            /// </summary>
            public class Goods_Detail
            {
                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="merchant_goods_id">商户侧商品编码</param>
                /// <param name="wechatpay_goods_id">微信侧商品编码，可为null</param>
                /// <param name="goods_name">商品名称，可为null</param>
                /// <param name="quantity">用户购买的数量</param>
                /// <param name="unit_price">商品单价，单位为分</param>
                public Goods_Detail(string merchant_goods_id, string wechatpay_goods_id, string goods_name, int quantity, int unit_price)
                {
                    this.goods_name = goods_name;
                    this.wechatpay_goods_id = wechatpay_goods_id;
                    this.quantity = quantity;
                    this.merchant_goods_id = merchant_goods_id;
                    this.unit_price = unit_price;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Goods_Detail()
                {
                }

                /// <summary>
                /// 商品名称
                /// 商品的实际名称
                /// 示例值：iPhoneX 256G
                /// </summary>
                public string goods_name { get; set; }

                /// <summary>
                /// 微信侧商品编码
                /// 微信支付定义的统一商品编号（没有可不传）
                /// 示例值：1001
                /// </summary>
                public string wechatpay_goods_id { get; set; }

                /// <summary>
                /// 商品数量
                /// 用户购买的数量
                /// 示例值：1
                /// </summary>
                public int quantity { get; set; }

                /// <summary>
                /// 商户侧商品编码
                /// 由半角的大小写字母、数字、中划线、下划线中的一种或几种组成。
                /// 示例值：1246464644
                /// </summary>
                public string merchant_goods_id { get; set; }

                /// <summary>
                /// 商品单价
                /// 商品单价，单位为分
                /// 示例值：828800 (8288元)
                /// </summary>
                public int unit_price { get; set; }
            }
            #endregion
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
            public Settle_Info(bool profit_sharing)
            {
                this.profit_sharing = profit_sharing;
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
        }

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
            /// <param name="store_info">商户门店信息，可为null</param>
            /// <param name="h5_info">H5场景信息，H5下单必填，其它支付方式必须为null</param>
            public Scene_Info(string payer_client_ip, string device_id, Store_Info store_info, H5_Info h5_info = null)
            {
                this.store_info = store_info;
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
            /// 商户门店信息
            /// </summary>
            public Store_Info store_info { get; set; }

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
            /// 商户门店信息
            /// </summary>
            public class Store_Info
            {
                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="id">商户侧门店编号</param>
                /// <param name="name">商户侧门店名称，可为null</param>
                /// <param name="area_code">地区编码，详细请见省市区编号对照表，可为null</param>
                /// <param name="address">详细地址，可为null</param>
                public Store_Info(string id, string name, string area_code, string address)
                {
                    this.address = address;
                    this.area_code = area_code;
                    this.name = name;
                    this.id = id;
                }

                /// <summary>
                /// 无参构造函数
                /// </summary>
                public Store_Info()
                {
                }

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
            /// H5场景信息
            /// </summary>
            public class H5_Info
            {
                /// <summary>
                /// 含参构造函数
                /// </summary>
                /// <param name="type">场景类型 枚举值: iOS, Android, Wap</param>
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

        #endregion
    }
}

