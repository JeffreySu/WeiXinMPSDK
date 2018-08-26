/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：SsoApi.cs
    文件功能描述：单点登录接口（Work中重新整理）
    
    
    创建标识：Senparc - 20170617

    修改标识：Senparc - 201807128
    修改描述：修改命名空间为 Senparc.Weixin.Work.AdvancedAPIs

    ----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.SSO;
using Senparc.Weixin.Work.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs
{

    /// <summary>
    /// 单点登录接口
    /// </summary>
    public class SsoApi
    {
        /*此接口不提供异步方法*/
        /// <summary>
        /// 服务商引导用户进入登录授权页
        /// 1、用户进入服务商网站 用户进入服务商网站，如www.ABC.com。
        /// 2、服务商引导用户进入登录授权页 服务可以在自己的网站首页中放置“微信企业号登录”的入口，引导用户（指企业号系统管理员者）进入登录授权页。网址为: https://qy.weixin.qq.com/cgi-bin/loginpage?corp_id=xxxx&redirect_uri=xxxxx&state=xxxx&usertype=member 服务商需要提供corp_id，跳转uri和state参数，其中uri需要经过一次urlencode作为参数，state用于服务商自行校验session，防止跨域攻击。
        /// 3、用户确认并同意授权 用户进入登录授权页后，需要确认并同意将自己的企业号和登录账号信息授权给服务商，完成授权流程。
        /// 4、授权后回调URI，得到授权码和过期时间 授权流程完成后，会进入回调URI，并在URL参数中返回授权码和过期时间(redirect_url?auth_code=xxx&expires_in=600)
        /// 5、利用授权码调用企业号的相关API 在得到授权码后，第三方可以使用授权码换取登录授权信息。
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="redirectUrl">跳转url</param>
        /// <param name="state">用于服务商自行校验session</param>
        /// <param name="usertype">redirect_uri支持登录的类型，有member(成员登录)、admin(管理员登录)、all(成员或管理员皆可登录)，默认值为admin</param>
        /// <returns></returns>
        public static string GetLoginAuthUrl(string corpId, string redirectUrl, string state = "", Login_User_Type usertype = Login_User_Type.admin)
        {
            return string.Format("https://qy.weixin.qq.com/cgi-bin/loginpage?corp_id={0}&redirect_uri={1}&state={2}&usertype={3}",
                              corpId.AsUrlData(), redirectUrl.AsUrlData(), state.AsUrlData(), usertype.ToString());
        }

        /// <summary>
        /// 获取登录用户信息【QY移植修改】
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="providerSecret"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ProviderTokenResult GetProviderToken(string corpId, string providerSecret, int timeOut = Config.TIME_OUT)
        {
                var url = Config.ApiWorkHost + "/cgi-bin/service/get_provider_token";

                var data = new
                {
                    corpid = corpId,
                    provider_secret = providerSecret
                };

                return CommonJsonSend.Send<ProviderTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }


        #region 同步方法

        /// <summary>
        /// 获取登录用户信息【QY移植修改】
        /// </summary>
        /// <param name="providerAccessToken">服务提供商的accesstoken</param>
        /// <param name="authCode">oauth2.0授权企业号管理员登录产生的code</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetLoginInfoResult GetLoginInfo(string providerAccessToken, string authCode, int timeOut = Config.TIME_OUT)
        {
                string url = Config.ApiWorkHost + "/cgi-bin/service/get_login_info?provider_access_token={0}";

                var data = new
                {
                    auth_code = authCode
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetLoginInfoResult>(providerAccessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        /// 【异步方法】获取登录用户信息【QY移植修改】
        /// </summary>
        /// <param name="providerAccessToken">服务提供商的accesstoken</param>
        /// <param name="authCode">oauth2.0授权企业号管理员登录产生的code</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetLoginInfoResult> GetLoginInfoAsync(string providerAccessToken, string authCode, int timeOut = Config.TIME_OUT)
        {
                string url = Config.ApiWorkHost + "/cgi-bin/service/get_login_info?provider_access_token={0}";

                var data = new
                {
                    auth_code = authCode
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetLoginInfoResult>(providerAccessToken, url, data, CommonJsonSendType.POST, timeOut);
        }

        /// <summary>
        /// 【异步方法】获取登录用户信息【QY移植修改】
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="providerSecret"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<ProviderTokenResult> GetProviderTokenAsync(string corpId, string providerSecret, int timeOut = Config.TIME_OUT)
        {
                var url = Config.ApiWorkHost + "/cgi-bin/service/get_provider_token";

                var data = new
                {
                    corpid = corpId,
                    provider_secret = providerSecret
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<ProviderTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        }


        #endregion
#endif
    }
}
