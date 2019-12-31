using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.TenPay.V3
{
    /// <summary>
    /// 服务商特约商户
    /// https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=25_3&index=4
    /// 境内普通商户添加分账接收方
    /// https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=27_3&index=4
    /// 2019-12-30
    /// </summary>
    public class TenpayV3ProfitShareingAddReceiverRequestData
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 微信支付分配的商户号 
        /// </summary>
        public string MchId { get; set; }


        #region 服务商

        /// <summary>
        /// 子商户公众账号ID sub_appid
        /// </summary>
        public string SubAppId { get; set; }

        /// <summary>
        /// 子商户号 sub_mch_id  是 String(32)  1900000109	微信支付分配的子商户号
        /// </summary>
        public string SubMchId { get; set; }

        #endregion

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; }
        /// <summary>
        /// 签名类型， HMAC-SHA256;
        /// 签名类型，目前只支持HMAC-SHA256
        /// </summary>
        public readonly string SignType = "HMAC-SHA256";

        /// <summary>
        /// 分账接收方对象，json格式
        /// </summary>
        public TenpayV3ProfitShareingAddReceiverRequestData_ReceiverInfo Receiver { get; set; }


        /// <summary>
        /// 此不带参数的构造函数是为了反序列化的实例初始化，提交数据时请使用其他构造函数
        /// </summary>
        public TenpayV3ProfitShareingAddReceiverRequestData() 
        {
        }

        /// <summary>
        /// 服务商
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="mchId"></param>
        /// <param name="subappid">子商户公众账号ID</param>
        /// <param name="submchid">子商户号</param>
        /// <param name="body"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="totalFee">单位：分</param>
        /// <param name="spbillCreateIp"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="tradeType"></param>
        /// <param name="openid">trade_type=NATIVE时，OpenId应该为null</param>
        /// <param name="subOpenid">用户子标识，不需要则填写null</param>
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
        /// <param name="sceneInfo">场景信息。该字段用于上报场景信息，目前支持上报实际门店信息。</param>
        /// <param name="profitSharing">        
        /// 是否需要分账
        /// https://pay.weixin.qq.com/wiki/doc/api/allocation_sl.php?chapter=24_3&index=3
        /// https://pay.weixin.qq.com/wiki/doc/api/allocation.php?chapter=26_3
        /// "Y" -- 需要分账, null 或者 "N"-不需要分账,
        /// 服务商需要在 产品中心--特约商户授权产品 申请特约商户授权,
        /// 并且特约商户需要在 产品中心-我授权的商品中给服务商授权才可以使用分账功能;
        /// 普通商户需要 产品中心-我的产品 中开通分账功能；
        /// </param>
        public TenpayV3ProfitShareingAddReceiverRequestData(
            string appId, string mchId, string subappid, string submchid, string key, string nonceStr,
            TenpayV3ProfitShareingAddReceiverRequestData_ReceiverInfo receiver
        )
        {
            AppId = appId;
            MchId = mchId;
            NonceStr = nonceStr;
            Key = key;
            SubAppId = subappid;
            SubMchId = submchid;
            Receiver = receiver;


            #region 设置RequestHandler

            //创建支付应答对象
            PackageRequestHandler = new RequestHandler(null);
            //初始化
            PackageRequestHandler.Init();

            //设置package订单参数
            //以下设置顺序按照官方文档排序，方便维护：https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
            PackageRequestHandler.SetParameter("appid", this.AppId);                       //公众账号ID
            PackageRequestHandler.SetParameter("mch_id", this.MchId);                      //商户号
            PackageRequestHandler.SetParameterWhenNotNull("sub_appid", this.SubAppId);     //子商户公众账号ID
            PackageRequestHandler.SetParameterWhenNotNull("sub_mch_id", this.SubMchId);    //子商户号
            PackageRequestHandler.SetParameter("nonce_str", this.NonceStr);                //随机字符串
            PackageRequestHandler.SetParameterWhenNotNull("sign_type", this.SignType);     //签名类型，默认为MD5
            if (Receiver != null)
            {
                JsonSerializerSettings setting = new JsonSerializerSettings();
                setting.NullValueHandling = NullValueHandling.Ignore;
                PackageRequestHandler.SetParameter("receiver", Receiver.ToJson(false, setting));   //场景信息
            }

            Sign = PackageRequestHandler.CreateSha256Sign("key", this.Key);

            PackageRequestHandler.SetParameter("sign", Sign);                              //签名
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }

        public readonly RequestHandler PackageRequestHandler;
        public readonly string Sign;
    }
}
