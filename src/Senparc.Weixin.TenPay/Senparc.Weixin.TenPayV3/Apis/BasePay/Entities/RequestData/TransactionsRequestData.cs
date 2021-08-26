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
  
    文件名：TransactionsRequestData.cs
    文件功能描述：下单请求数据实体
    
    
    创建标识：Senparc - 20210825

    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.TenPayV3.Apis.BasePay.Entities.RequestData.Entities;
using Senparc.Weixin.TenPayV3.Entities;

namespace Senparc.Weixin.TenPayV3.Apis.BasePay
{
    public class TransactionsRequestData
    {
        /// <summary>
        /// 含参构造函数
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
        public TransactionsRequestData(string appid, string mchid, string description,
            string out_trade_no, TenpayDateTime time_expire, string attach,
            string notify_url, string goods_tag, Amount amount, Payer payer = null,
            Detail detail = null, Settle_Info settle_info = null, Scene_Info scene_info = null)
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
        }

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
    }
}

