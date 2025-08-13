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
  
    文件名：EcommerceCombineRequestData.cs
    文件功能描述：电商收付通 - 合单支付 请求数据
    
    
    创建标识：Senparc - 20250813

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.TenPayV3.Apis.Ecommerce
{
    /// <summary>
    /// 电商收付通 - 合单下单API 请求数据
    /// </summary>
    public class CombineTransactionsRequestData
    {
        /// <summary>
        /// 合单商户appid
        /// <para>合单发起方的appid</para>
        /// <para>示例值：wxd678efh567hg6787</para>
        /// </summary>
        public string combine_appid { get; set; }

        /// <summary>
        /// 合单商户号
        /// <para>合单发起方商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string combine_mchid { get; set; }

        /// <summary>
        /// 合单商户订单号
        /// <para>合单发起方商户订单号</para>
        /// <para>示例值：P20150806125346</para>
        /// </summary>
        public string combine_out_trade_no { get; set; }

        /// <summary>
        /// 场景信息
        /// <para>支付场景描述</para>
        /// </summary>
        public CombineSceneInfo scene_info { get; set; }

        /// <summary>
        /// 子单信息
        /// <para>子单信息，最多支持50单</para>
        /// </summary>
        public CombineSubOrder[] sub_orders { get; set; }

        /// <summary>
        /// 支付者信息
        /// <para>支付者信息</para>
        /// </summary>
        public CombinePayer combine_payer_info { get; set; }

        /// <summary>
        /// 交易结束时间
        /// <para>订单失效时间，格式为yyyy-MM-ddTHH:mm:ss+TIMEZONE</para>
        /// <para>示例值：2018-06-08T10:34:56+08:00</para>
        /// </summary>
        public string time_expire { get; set; }

        /// <summary>
        /// 通知地址
        /// <para>接收微信支付异步通知回调地址</para>
        /// <para>示例值：https://yourapp.com/notify</para>
        /// </summary>
        public string notify_url { get; set; }
    }

    /// <summary>
    /// 合单场景信息
    /// </summary>
    public class CombineSceneInfo
    {
        /// <summary>
        /// 用户终端IP
        /// <para>用户的客户端IP</para>
        /// <para>示例值：14.23.150.211</para>
        /// </summary>
        public string payer_client_ip { get; set; }

        /// <summary>
        /// 商户端设备号
        /// <para>商户端设备号</para>
        /// <para>示例值：013467007045764</para>
        /// </summary>
        public string device_id { get; set; }

        /// <summary>
        /// H5场景信息
        /// <para>H5场景信息</para>
        /// </summary>
        public CombineH5Info h5_info { get; set; }
    }

    /// <summary>
    /// 合单H5场景信息
    /// </summary>
    public class CombineH5Info
    {
        /// <summary>
        /// 场景类型
        /// <para>场景类型</para>
        /// <para>示例值：iOS, Android, Wap</para>
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 应用名称
        /// <para>应用名称</para>
        /// <para>示例值：王者荣耀</para>
        /// </summary>
        public string app_name { get; set; }

        /// <summary>
        /// 网站URL
        /// <para>网站URL</para>
        /// <para>示例值：https://pay.qq.com</para>
        /// </summary>
        public string app_url { get; set; }

        /// <summary>
        /// iOS平台BundleID
        /// <para>iOS平台BundleID</para>
        /// <para>示例值：com.tencent.wzryiOS</para>
        /// </summary>
        public string bundle_id { get; set; }

        /// <summary>
        /// Android平台PackageName
        /// <para>Android平台PackageName</para>
        /// <para>示例值：com.tencent.tmgp.sgame</para>
        /// </summary>
        public string package_name { get; set; }
    }

    /// <summary>
    /// 合单子订单信息
    /// </summary>
    public class CombineSubOrder
    {
        /// <summary>
        /// 子商户号
        /// <para>子商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 附加数据
        /// <para>附加数据，在查询API和支付通知中原样返回</para>
        /// <para>示例值：自定义数据</para>
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 订单金额
        /// <para>订单总金额，单位为分</para>
        /// <para>示例值：100</para>
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 子商户订单号
        /// <para>子商户订单号</para>
        /// <para>示例值：20150806125346</para>
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商品描述
        /// <para>商品简单描述</para>
        /// <para>示例值：腾讯充值中心-QQ会员充值</para>
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 结算信息
        /// <para>结算信息</para>
        /// </summary>
        public CombineSettleInfo settle_info { get; set; }
    }

    /// <summary>
    /// 合单结算信息
    /// </summary>
    public class CombineSettleInfo
    {
        /// <summary>
        /// 是否指定分账
        /// <para>是否指定分账</para>
        /// <para>示例值：true</para>
        /// </summary>
        public bool profit_sharing { get; set; }

        /// <summary>
        /// 补贴金额
        /// <para>SettleInfo.profit_sharing为true时，该金额才生效</para>
        /// <para>示例值：10</para>
        /// </summary>
        public int subsidy_amount { get; set; }
    }

    /// <summary>
    /// 合单支付者信息
    /// </summary>
    public class CombinePayer
    {
        /// <summary>
        /// 用户标识
        /// <para>用户在直连商户appid下的唯一标识</para>
        /// <para>示例值：oUpF8uMuAJO_M2pxb1Q9zNjWeS6o</para>
        /// </summary>
        public string openid { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 合单查询订单API 请求数据
    /// </summary>
    public class QueryCombineTransactionsRequestData
    {
        /// <summary>
        /// 合单商户订单号
        /// <para>合单发起方商户订单号</para>
        /// <para>示例值：P20150806125346</para>
        /// </summary>
        public string combine_out_trade_no { get; set; }
    }

    /// <summary>
    /// 电商收付通 - 合单关闭订单API 请求数据
    /// </summary>
    public class CloseCombineTransactionsRequestData
    {
        /// <summary>
        /// 合单商户订单号
        /// <para>合单发起方商户订单号</para>
        /// <para>示例值：P20150806125346</para>
        /// </summary>
        public string combine_out_trade_no { get; set; }

        /// <summary>
        /// 子单信息
        /// <para>子单信息</para>
        /// </summary>
        public CombineSubOrderInfo[] sub_orders { get; set; }
    }

    /// <summary>
    /// 合单关闭子订单信息
    /// </summary>
    public class CombineSubOrderInfo
    {
        /// <summary>
        /// 子商户号
        /// <para>子商户号</para>
        /// <para>示例值：1900000109</para>
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /// 子商户订单号
        /// <para>子商户订单号</para>
        /// <para>示例值：20150806125346</para>
        /// </summary>
        public string out_trade_no { get; set; }
    }
}
