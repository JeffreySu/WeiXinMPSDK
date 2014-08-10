using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.WeixinPayLib
{
    /// <summary>
    /// 微信支付基础信息储存类
    /// </summary>
    public class WeixinPayInfo
    {
        /// <summary>
        /// ?
        /// </summary>
        public string Tenpay { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string PartnerId { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// appid
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// paysignkey(非appkey) 
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 支付完成后的回调处理页面,*替换成notify_url.asp所在路径
        /// </summary>
        public string TenpayNotify { get; set; } // = "http://localhost/payNotifyUrl.aspx";

        /// <param name="tenpay">商户号</param>
        /// <param name="partnerId"></param>
        /// <param name="key">密钥</param>
        /// <param name="appId">appid</param>
        /// <param name="appKey">paysignkey(非appkey) </param>
        /// <param name="tenpayNotify">支付完成后的回调处理页面,*替换成notify_url.asp所在路径</param>
        public WeixinPayInfo(string tenpay, string partnerId, string key, string appId, string appKey, string tenpayNotify)
        {
            Tenpay = tenpay;
            PartnerId = partnerId;
            Key = key;
            AppId = appId;
            AppKey = appKey;
            TenpayNotify = tenpayNotify;
        }
    }
}
