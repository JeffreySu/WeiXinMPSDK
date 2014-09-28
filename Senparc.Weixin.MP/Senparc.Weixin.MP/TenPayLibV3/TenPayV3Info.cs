using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付基础信息储存类
    /// </summary>
    public class TenPayV3Info
    {
        /// <summary>
        /// appid
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 商户ID
        /// </summary>
        public string Mchid { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// JSAPI 接口中获取openid，审核后在公众平台开启开发模式后可查看 
        /// </summary>
        public string Appsecret { get; set; }
        /// <summary>
        /// 支付完成后的回调处理页面,*替换成notify_url.asp所在路径
        /// </summary>
        public string TenPayNotify { get; set; } // = "http://localhost/payNotifyUrl.aspx";

        public TenPayV3Info(string appId, string mchid, string key, string appsecret, string tenPayNotify)
        {
            AppId = appId;
            Mchid = mchid;
            Key = key;
            Appsecret = appsecret;
            TenPayNotify = tenPayNotify;
        }
    }
}
