﻿/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
  
    文件名：TenPayV3UnifiedorderRequestData.cs
    文件功能描述：微信支付提交的XML Data数据[统一下单]
    
    创建标识：Senparc - 20161227

    修改标识：Senparc - 20170108
    修改描述：v14.3.117 修复微信支付“商品详情”显示bug

----------------------------------------------------------------*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付提交的XML Data数据[统一下单]
    /// </summary>
    public class TenPayV3UnifiedorderRequestData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 商家订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 商品金额,以分为单位(money * 100).ToString()
        /// </summary>
        public decimal TotalFee { get; set; }
        /// <summary>
        /// 用户的公网ip，不是商户服务器IP
        /// </summary>
        public string SpbillCreateIP { get; set; }
        /// <summary>
        /// 接收财付通通知的URL
        /// </summary>
        public string NotifyUrl { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public TenPayV3Type TradeType { get; set; }
        /// <summary>
        /// 用户的openId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public readonly string Sign;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="body"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="totalFee">单位：分</param>
        /// <param name="spbillCreateIp"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="tradeType"></param>
        /// <param name="openid"></param>
        /// <param name="key"></param>
        /// <param name="nonceStr"></param>
        public TenPayV3UnifiedorderRequestData(string appId, string mchId, string body, string outTradeNo, decimal totalFee, string spbillCreateIp, string notifyUrl, TenPayV3Type tradeType, string openid, string key, string nonceStr, TenPayV3UnifiedorderRequestOptionalData optionalParams = null)
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            Body = body ?? "Senparc TenpayV3";
            OutTradeNo = outTradeNo;
            TotalFee = totalFee;
            SpbillCreateIP = spbillCreateIp;
            NotifyUrl = notifyUrl;
            TradeType = tradeType;
            OpenId = openid;
            Key = key;

            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();

            //添加可选参数
            if (optionalParams != null)
            {
                PropertyInfo[] ps = typeof(TenPayV3UnifiedorderRequestOptionalData).GetProperties();
                foreach (PropertyInfo param in ps)
                {
                    var v = (string)param.GetValue(optionalParams, null);
                    if (!string.IsNullOrEmpty(v))
                        PackageRequestHandler.SetParameter(param.Name, v);
                }
            }

            //设置package订单参数
            PackageRequestHandler.SetParameter("appid", this.AppId);          //公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId);         //商户号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr);                    //随机字符串
            PackageRequestHandler.SetParameter("body", this.Body);    //商品信息
            PackageRequestHandler.SetParameter("out_trade_no", this.OutTradeNo);      //商家订单号
            PackageRequestHandler.SetParameter("total_fee", this.TotalFee.ToString("0"));                    //商品金额,以分为单位(money * 100).ToString()
            PackageRequestHandler.SetParameter("spbill_create_ip", this.SpbillCreateIP);   //用户的公网ip，不是商户服务器IP
            PackageRequestHandler.SetParameter("notify_url", this.NotifyUrl);          //接收财付通通知的URL
            PackageRequestHandler.SetParameter("trade_type", this.TradeType.ToString());                        //交易类型
            PackageRequestHandler.SetParameter("openid", this.OpenId);                      //用户的openId
            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign);                       //签名
            #endregion
        }
    }

    public class TenPayV3UnifiedorderRequestOptionalData
    {
        public string device_info { get; set; }
        /// <summary>
        /// 默认值"MD5"
        /// </summary>
        public string sign_type { get; set; } = "MD5";
        public string detail { get; set; }
        public string attach { get; set; }
        /// <summary>
        /// 默认值"CNY"
        /// </summary>
        public string fee_type { get; set; } = "CNY";
        public string time_start { get; set; }
        public string time_expire { get; set; }
        public string goods_tag { get; set; }
        /// <summary>
        /// APP支付中不存在
        /// </summary>
        public string product_id { get; set; }
        public string limit_pay { get; set; }
    }
}
