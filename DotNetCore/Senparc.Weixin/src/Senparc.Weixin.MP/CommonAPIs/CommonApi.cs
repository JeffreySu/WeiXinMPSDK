/*
    API：http://mp.weixin.qq.com/wiki/index.php?title=%E6%8E%A5%E5%8F%A3%E6%96%87%E6%A1%A3&oldid=103
    
 */

using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.CommonAPIs
{
    /// <summary>
    /// 通用接口
    /// 通用接口用于和微信服务器通讯，一般不涉及自有网站服务器的通讯
    /// </summary>
    public partial class CommonApi
    {
        /// <summary>
        /// 获取凭证接口
        /// </summary>
        /// <param name="grant_type">获取access_token填写client_credential</param>
        /// <param name="appid">第三方用户唯一凭证</param>
        /// <param name="secret">第三方用户唯一凭证密钥，既appsecret</param>
        /// <returns></returns>
        public static AccessTokenResult GetToken(string appid, string secret, string grant_type = "client_credential")
        {
            //注意：此方法不能再使用ApiHandlerWapper.TryCommonApi()，否则会循环
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
                                    grant_type.AsUrlData(), appid.AsUrlData(), secret.AsUrlData());

            AccessTokenResult result = Get.GetJson<AccessTokenResult>(url);
            return result;
        }

        /// <summary>
        /// 用户信息接口
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static WeixinUserInfoResult GetUserInfo(string accessTokenOrAppId, string openId)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format("http://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}",
                                        accessToken.AsUrlData(), openId.AsUrlData());
                WeixinUserInfoResult result = Get.GetJson<WeixinUserInfoResult>(url);
                return result;

            }, accessTokenOrAppId);
        }


        /// <summary>
        /// 获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="secret"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicket(string appId, string secret, string type = "jsapi")
        {
            var accessToken = AccessTokenContainer.TryGetAccessToken(appId, secret);
            return GetTicketByAccessToken(accessToken,type);
        }

        /// <summary>
        /// 获取调用微信JS接口的临时票据
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static JsApiTicketResult GetTicketByAccessToken(string accessTokenOrAppId, string type = "jsapi")
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type={1}",
                                    accessToken.AsUrlData(), type.AsUrlData());

            JsApiTicketResult result = Get.GetJson<JsApiTicketResult>(url);
            return result;

            }, accessTokenOrAppId);
        }
    }
}
