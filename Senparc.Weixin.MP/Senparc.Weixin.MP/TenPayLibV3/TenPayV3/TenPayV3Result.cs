/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
 
    文件名：TenPayV3Result.cs
    文件功能描述：微信支付V3返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 基础返回结果
    /// </summary>
    public class TenPayV3Result
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
    }

    /// <summary>
    /// 统一支付接口在 return_code为 SUCCESS的时候有返回
    /// </summary>
    public class Result : TenPayV3Result
    {
        /// <summary>
        /// 微信分配的公众账号ID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 微信支付分配的商户号
        /// </summary>
        public string mch_id { get; set; }

        /// <summary>
        /// 微信支付分配的终端设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 随机字符串，不长于32 位
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }

        /// <summary>
        /// SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }

        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }
    }

    /// <summary>
    /// 统一支付接口在return_code 和result_code 都为SUCCESS 的时候有返回
    /// </summary>
    public class UnifiedorderResult : Result
    {
        /// <summary>
        /// 交易类型:JSAPI、NATIVE、APP
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 微信生成的预支付ID，用于后续接口调用中使用
        /// </summary>
        public string prepay_id { get; set; }

        /// <summary>
        /// trade_type为NATIVE时有返回，此参数可直接生成二维码展示出来进行扫码支付
        /// </summary>
        public string code_url { get; set; }
    }

    /// <summary>
    /// 支付结果在return_code 都为SUCCESS 的时候有返回
    /// </summary>
    public class PaymentResult : Result
    {
        /// <summary>
        /// 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public string total_fee { get; set; }

        /// <summary>
        /// 货币种类
        /// </summary>
        public string fee_type { get; set; }

        /// <summary>
        /// 现金支付金额
        /// </summary>
        public string cash_fee { get; set; }
    }

    /// <summary>
    /// 支付通知结果
    /// </summary>
    public class PaymentNotifyResult : PaymentResult
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// 是否关注公众账号
        /// </summary>
        public string is_subscribe { get; set; }

        /// <summary>
        /// 现金支付货币类型
        /// </summary>
        public string cash_fee_type { get; set; }

        /// <summary>
        /// 代金券或立减优惠金额
        /// </summary>
        public string coupon_fee { get; set; }

        /// <summary>
        /// 代金券或立减优惠使用数量
        /// </summary>
        public string coupon_count { get; set; }

        //public string coupon_id_ { get; set; }

        /// <summary>
        /// 商家数据包
        /// </summary>
        public string attach { get; set; }

        /// <summary>
        /// 支付完成时间
        /// </summary>
        public string time_end { get; set; }
    }

    /// <summary>
    /// 退款结果在return_code 都为SUCCESS 的时候有返回
    /// </summary>
    public class RefundResult : PaymentResult
    {
        /// <summary>
        /// 现金退款金额
        /// </summary>
        public string cash_refund_fee { get; set; }

        /// <summary>
        /// 代金券或立减优惠退款金额
        /// </summary>
        public string coupon_refund_fee { get; set; }

        /// <summary>
        /// 代金券或立减优惠使用数量
        /// </summary>
        public string coupon_refund_count { get; set; }

        /// <summary>
        /// 代金券或立减优惠ID
        /// </summary>
        public string coupon_refund_id { get; set; }
    }

    /// <summary>
    ///  付款通知结果 在return_code 和result_code都为SUCCESS的时候有返回
    /// </summary>
    public class TransfersResult : PaymentResult
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string partner_trade_no { get; set; }

        /// <summary>
        /// 微信订单号
        /// </summary>
        public string payment_no { get; set; }

        /// <summary>
        /// 微信支付成功时间
        /// </summary>
        public string payment_time { get; set; }
    }
}
