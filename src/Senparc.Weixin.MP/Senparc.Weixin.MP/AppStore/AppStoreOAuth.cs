/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
  
    文件名：AppStoreOAuth.cs
    文件功能描述：AppStoreOAuth
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP.CommonAPIs;

namespace Senparc.Weixin.MP.AppStore
{
    public static class AppStoreOAuth
    {
        private static string Domain
        {
            get
            {
                return Config.IsDebug ? "http://localhost:12222" : "http://www.weiweihi.com";//用于自动切换本地单元测试的请求地址
            }
        }

        /// <summary>
        /// 获取验证地址
        /// </summary>
        /// <param name="weixinId">正在使用此APP的weixinId</param>
        /// <param name="clientId">此App的唯一标识。可以在此APP的【开发接入】页面的【OAuth认证 设置】页面看到</param>
        /// <param name="redirectUrl"></param>
        /// <param name="state"></param>
        /// <param name="responseType"></param>
        /// <returns></returns>
        public static string GetAuthorizeUrl(int weixinId, string clientId, string redirectUrl, string state, string responseType = "code")
        {
            var url = string.Format(Domain + "/OAuth2/Authorize?weixinId={0}&clientId={1}&RedirectUri={2}&response_type=code&state={3}",
                        weixinId.ToString("d").AsUrlData(), clientId.AsUrlData(), redirectUrl.AsUrlData(), state.AsUrlData());

            /* 这一步发送之后，客户会得到授权页面，无论同意或拒绝，都会返回redirectUrl页面。
             * 如果用户同意授权，页面将跳转至 redirect_uri/?code=CODE&state=STATE。这里的code用于换取access_token（和通用接口的access_token不通用）
             * 若用户禁止授权，则重定向后不会带上code参数，仅会带上state参数redirect_uri?state=STATE
             */
            return url;
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="clientId">此App的唯一标识。可以在此APP的【开发接入】页面的【OAuth认证 设置】页面看到</param>
        /// <param name="clientSecret">对应此App的唯一标识的密码。可以在此APP的【开发接入】页面的【OAuth认证 设置】页面看到</param>
        /// <param name="code">code作为换取access_token的票据，每次用户授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期。</param>
        /// <param name="grantType"></param>
        /// <returns></returns>
        public static OAuthAccessTokenResult GetAccessToken(string clientId, string clientSecret, string code, string grantType = "authorization_code")
        {
            var url = string.Format(Domain + "/OAuth2/AccessToken?clientId={0}&clientSecret={1}&code={2}&grantType={3}",
                        clientId.AsUrlData(), clientSecret.AsUrlData(), code.AsUrlData(), grantType.AsUrlData());

            return CommonJsonSend.Send<OAuthAccessTokenResult>(null, url, null, CommonJsonSendType.GET);
        }
    }
}
