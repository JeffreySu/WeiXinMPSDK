using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    /// <summary>
    /// 微信支付维权接口，官方API：https://mp.weixin.qq.com/htmledition/res/bussiness-course2/wxm-payment-kf-api.pdf
    /// </summary>
    public static class TenPayRights
    {
        
        public static string NativePay(string appId, string timesTamp, string nonceStr, string productId, string sign)
        {
            var urlFormat = "weixin://wxpay/bizpayurl?sign={0}&appid={1}&productid={2}&timestamp={3}&noncestr={4}";
            var url = string.Format(urlFormat, appId, timesTamp, nonceStr, productId, sign);

            return url;
        }
    }
}
