﻿/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：LoginAuthApi.cs
    文件功能描述：企业号登录授权接口


    创建标识：Senparc - 20150325

    修改标识：zeje - 20150507
    修改描述：v3.3.5 更新登陆、授权接口方法

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

----------------------------------------------------------------*/

/*
    接口文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%99%BB%E5%BD%95%E6%8E%88%E6%9D%83%E6%B5%81%E7%A8%8B%E8%AF%B4%E6%98%8E
 */

using System.Threading.Tasks;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.QY.AdvancedAPIs.LoginAuth;
using Senparc.Weixin.QY.CommonAPIs;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    public static class LoginAuthApi
    {
        #region 同步请求
        
       
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
        /// 获取企业号管理员登录信息
        /// </summary>
        /// <param name="providerAccessToken">服务提供商的accesstoken</param>
        /// <param name="authCode">oauth2.0授权企业号管理员登录产生的code</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetLoginInfoResult GetLoginInfo(string providerAccessToken, string authCode, int timeOut = Config.TIME_OUT)
        {
                string url = "https://qyapi.weixin.qq.com/cgi-bin/service/get_login_info?provider_access_token={0}";

                var data = new
                {
                    auth_code = authCode
                };

                return CommonJsonSend.Send<GetLoginInfoResult>(providerAccessToken, url, data, CommonJsonSendType.POST,
                                                               timeOut);


        }
        /// <summary>
        /// 获取企业号管理员登录信息
        /// </summary>
        /// <param name="providerAccessToken">服务提供商的accesstoken</param>
        /// <param name="loginTicket">通过get_login_info得到的login_ticket, 24小时有效</param>
        /// <param name="target">登录跳转到企业号后台的目标页面，目前有：agent_setting、send_msg、contact</param>
        /// <param name="agentid">授权方应用id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetLoginUrlResult GetLoginUrl(string providerAccessToken, string loginTicket,string target,int agentid,int timeOut = Config.TIME_OUT)
        {
                string url = "https://qyapi.weixin.qq.com/cgi-bin/service/get_login_url?provider_access_token={0}";

                var data = new
                {
                    login_ticket = loginTicket,
                    target = target,
                    agentid = agentid
                };

                return CommonJsonSend.Send<GetLoginUrlResult>(providerAccessToken, url, data, CommonJsonSendType.POST,
                                                               timeOut);


        }
        #endregion

#if !NET35 && !NET40
        #region 异步请求
         /// <summary>
        /// 【异步方法】获取企业号管理员登录信息
        /// </summary>
        /// <param name="providerAccessToken">服务提供商的accesstoken</param>
        /// <param name="authCode">oauth2.0授权企业号管理员登录产生的code</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetLoginInfoResult> GetLoginInfoAsync(string providerAccessToken, string authCode, int timeOut = Config.TIME_OUT)
        {
                string url = "https://qyapi.weixin.qq.com/cgi-bin/service/get_login_info?provider_access_token={0}";

                var data = new
                {
                    auth_code = authCode
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetLoginInfoResult>(providerAccessToken, url, data, CommonJsonSendType.POST,
                                                               timeOut);


        }
        /// <summary>
        /// 【异步方法】获取企业号管理员登录信息
        /// </summary>
        /// <param name="providerAccessToken">服务提供商的accesstoken</param>
        /// <param name="loginTicket">通过get_login_info得到的login_ticket, 24小时有效</param>
        /// <param name="target">登录跳转到企业号后台的目标页面，目前有：agent_setting、send_msg、contact</param>
        /// <param name="agentid">授权方应用id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetLoginUrlResult> GetLoginUrlAsync(string providerAccessToken, string loginTicket,string target,int agentid,int timeOut = Config.TIME_OUT)
        {
                string url = "https://qyapi.weixin.qq.com/cgi-bin/service/get_login_url?provider_access_token={0}";

                var data = new
                {
                    login_ticket = loginTicket,
                    target = target,
                    agentid = agentid
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetLoginUrlResult>(providerAccessToken, url, data, CommonJsonSendType.POST,
                                                               timeOut);


        }
        #endregion
#endif

    }
}
