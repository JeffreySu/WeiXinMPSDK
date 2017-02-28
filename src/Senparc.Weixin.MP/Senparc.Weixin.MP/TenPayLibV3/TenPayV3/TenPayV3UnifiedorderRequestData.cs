/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
  
    文件名：TenPayV3UnifiedorderRequestData.cs
    文件功能描述：微信支付提交的XML Data数据[统一下单]
    
    创建标识：Senparc - 20161227

    修改标识：Senparc - 20170108
    修改描述：v14.3.117 修复微信支付“商品详情”显示bug

    修改标识：Senparc - 20170128
    修改描述：v14.3.122 调整sign_type设置顺序

    修改标识：Senparc - 20170211
    修改描述：v14.3.125 重新调整sign_type设置顺序，v14.3.122版本中不应该做调整

    修改标识：Senparc - 20170213
    修改描述：v14.3.126 修复微信支付 "TotalFee" 类型错误[decimal→int]

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
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
        /// 自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"，String(32)如：013467007045764
        /// </summary>
        public string DeviceInfo { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }
        /// <summary>
        /// 签名类型，默认为MD5，支持HMAC-SHA256和MD5。（使用默认）
        /// </summary>
        public string SignType { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 商品详细列表，使用Json格式，传输签名前请务必使用CDATA标签将JSON文本串保护起来。
        ///cost_price Int 可选 32 订单原价，商户侧一张小票订单可能被分多次支付，订单原价用于记录整张小票的支付金额。当订单原价与支付金额不相等则被判定为拆单，无法享/受/优/惠。
        /// receipt_id String 可选 32 商家小票ID
        ///goods_detail 服务商必填[]：
        ///└ goods_id String 必填 32 商品的编号
        ///└ wxpay_goods_id String 可选 32 微信支付定义的统一商品编号
        ///└ goods_name String 可选 256 商品名称 
        ///└ quantity Int 必填  32 商品数量
        ///└ price Int 必填 32 商品单价，如果商户有优惠，需传输商户优惠后的单价
        ///注意：单品总金额应&lt;=订单总金额total_fee，否则会无法享受优惠。
        /// String(6000)
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。String(127)，如：深圳分店
        /// </summary>
        public string Attach { get; set; }
        /// <summary>
        /// 符合ISO 4217标准的三位字母代码，默认人民币：CNY，详细列表请参见货币类型。String(16)，如：CNY
        /// </summary>
        public string FeeType { get; set; }
        /// <summary>
        /// 商家订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 商品金额,以分为单位(money * 100).ToString()
        /// </summary>
        public int TotalFee { get; set; }
        /// <summary>
        /// 用户的公网ip，不是商户服务器IP
        /// </summary>
        public string SpbillCreateIP { get; set; }
        /// <summary>
        /// 订单生成时间，最终生成格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。其他详见时间规则。
        /// 如果为空，则默认为当前服务器时间
        /// </summary>
        public string TimeStart { get; set; }
        /// <summary>
        /// 订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。其他详见时间规则
        /// 注意：最短失效时间间隔必须大于5分钟。
        /// 留空则不设置失效时间
        /// </summary>
        public string TimeExpire { get; set; }
        /// <summary>
        /// 商品标记，使用代金券或立减优惠功能时需要的参数，说明详见代金券或立减优惠。String(32)，如：WXG
        /// </summary>
        public string GoodsTag { get; set; }
        /// <summary>
        /// 接收财付通通知的URL
        /// </summary>
        public string NotifyUrl { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public TenPayV3Type TradeType { get; set; }
        /// <summary>
        /// trade_type=NATIVE时（即扫码支付），此参数必传。此参数为二维码中包含的商品ID，商户自行定义。
        /// String(32)，如：12235413214070356458058
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 上传此参数no_credit--可限制用户不能使用信用卡支付
        /// </summary>
        public string LimitPay { get; set; }
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
        /// <param name="deviceInfo">自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"，String(32)如：013467007045764</param>
        /// <param name="timeStart">订单生成时间，如果为空，则默认为当前服务器时间</param>
        /// <param name="timeExpire">订单失效时间，留空则不设置失效时间</param>
        /// <param name="detail">商品详细列表</param>
        /// <param name="attach">附加数据</param>
        /// <param name="feeType">符合ISO 4217标准的三位字母代码，默认人民币：CNY</param>
        /// <param name="goodsTag">商品标记，使用代金券或立减优惠功能时需要的参数，说明详见代金券或立减优惠。String(32)，如：WXG</param>
        /// <param name="productId">trade_type=NATIVE时（即扫码支付），此参数必传。此参数为二维码中包含的商品ID，商户自行定义。String(32)，如：12235413214070356458058</param>
        /// <param name="limitPay">是否限制用户不能使用信用卡支付</param>
        public TenPayV3UnifiedorderRequestData(string appId, string mchId, string body, string outTradeNo, int totalFee, string spbillCreateIp,
            string notifyUrl, TenPayV3Type tradeType, string openid, string key, string nonceStr,
            string deviceInfo = null, DateTime? timeStart = null, DateTime? timeExpire = null,
           string detail = null, string attach = null, string feeType = "CNY", string goodsTag = null, string productId = null, bool limitPay = false)
        {
            AppId = appId;
            MchId = mchId;
            DeviceInfo = deviceInfo;
            NonceStr = nonceStr;
            SignType = "MD5";
            Body = body ?? "Senparc TenpayV3";
            Detail = detail;
            Attach = attach;
            OutTradeNo = outTradeNo;
            FeeType = feeType;
            TotalFee = totalFee;
            SpbillCreateIP = spbillCreateIp;
            TimeStart = (timeStart ?? DateTime.Now).ToString("yyyyMMddHHmmss");
            TimeExpire = timeExpire.HasValue ? timeExpire.Value.ToString("yyyyMMddHHmmss") : null;
            GoodsTag = goodsTag;
            NotifyUrl = notifyUrl;
            TradeType = tradeType;
            ProductId = productId;
            LimitPay = limitPay ? "no_credit" : null;
            OpenId = openid;
            Key = key;

            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();

            //设置package订单参数
            //以下设置顺序按照官方文档排序，方便维护：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
            PackageRequestHandler.SetParameter("appid", this.AppId);                       //公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId);                      //商户号
            PackageRequestHandler.SetParameterWhenNotNull("device_info", this.DeviceInfo); //自定义参数
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr);                //随机字符串
            PackageRequestHandler.SetParameterWhenNotNull("sign_type", this.SignType);     //签名类型，默认为MD5
            PackageRequestHandler.SetParameter("body", this.Body);                         //商品信息
            PackageRequestHandler.SetParameterWhenNotNull("detail", this.Detail);          //商品详细列表
            PackageRequestHandler.SetParameterWhenNotNull("attach", this.Attach);          //附加数据
            PackageRequestHandler.SetParameter("out_trade_no", this.OutTradeNo);           //商家订单号
            PackageRequestHandler.SetParameterWhenNotNull("fee_type", this.FeeType);       //符合ISO 4217标准的三位字母代码，默认人民币：CNY
            PackageRequestHandler.SetParameter("total_fee", this.TotalFee.ToString());  //商品金额,以分为单位(money * 100).ToString()
            PackageRequestHandler.SetParameter("spbill_create_ip", this.SpbillCreateIP);   //用户的公网ip，不是商户服务器IP
            PackageRequestHandler.SetParameterWhenNotNull("time_start", this.TimeStart);   //订单生成时间
            PackageRequestHandler.SetParameterWhenNotNull("time_expire", this.TimeExpire);  //订单失效时间
            PackageRequestHandler.SetParameterWhenNotNull("goods_tag", this.GoodsTag);     //商品标记
            PackageRequestHandler.SetParameter("notify_url", this.NotifyUrl);              //接收财付通通知的URL
            PackageRequestHandler.SetParameter("trade_type", this.TradeType.ToString());   //交易类型
            PackageRequestHandler.SetParameterWhenNotNull("product_id", this.ProductId);   //trade_type=NATIVE时（即扫码支付），此参数必传。
            PackageRequestHandler.SetParameterWhenNotNull("limit_pay", this.LimitPay);     //上传此参数no_credit--可限制用户不能使用信用卡支付
            PackageRequestHandler.SetParameter("openid", this.OpenId);          //用户的openId，trade_type=JSAPI时（即公众号支付），此参数必传
            Sign = PackageRequestHandler.CreateMd5Sign("key", this.Key);
            PackageRequestHandler.SetParameter("sign", Sign);                              //签名
            #endregion
        }
    }
}
