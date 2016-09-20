using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Url;

namespace Senparc.Weixin.MP.AdvancedAPI
{
    /// <summary>
    /// 长短链接接口
    /// </summary>
    public class UrlApi
    {
        /*
        接口地址：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1443433600&token=&lang=zh_CN
                    将一条长链接转成短链接。
        主要使用场景： 开发者用于生成二维码的原链接（商品、支付二维码等）太长导致扫码速度和成功率下降，将原长链接通过此接口转成短链接再生成二维码将大大提升扫码速度和成功率。
        */

        ///  <summary>
        /// 将一条长链接转成短链接。
        ///  </summary>
        ///  <param name="accessTokenOrAppId"></param>
        ///  <param name="action">此处填long2short，代表长链接转短链接</param>
        ///  <param name="longUrl">需要转换的长链接，支持http://、https://、weixin://wxpay 格式的url</param>
        /// <param name="timeOut">请求超时时间</param>
        public static ShortUrlResult ShortUrl(string accessTokenOrAppId, string action, string longUrl, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                const string urlFormat = "https://api.weixin.qq.com/cgi-bin/shorturl?access_token={0}";
                var data = new
                {
                    action = action,
                    long_url = longUrl
                };
                return CommonJsonSend.Send<ShortUrlResult>(accessToken, urlFormat, data, timeOut: timeOut);

            }, accessTokenOrAppId);
        }

    }
}
