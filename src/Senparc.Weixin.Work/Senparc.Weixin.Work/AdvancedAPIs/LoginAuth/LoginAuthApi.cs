/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：LoginAuthApi.cs
    文件功能描述：企业号登录授权接口


    创建标识：Senparc - 20150325

    修改标识：zeje - 20150507
    修改描述：v3.3.5 更新登陆、授权接口方法

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    修改标识：Senparc - 20190129
    修改描述：统一 CommonJsonSend.Send<T>() 方法请求接口


----------------------------------------------------------------*/

/*
    接口文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%99%BB%E5%BD%95%E6%8E%88%E6%9D%83%E6%B5%81%E7%A8%8B%E8%AF%B4%E6%98%8E
 */

using System.Threading.Tasks;
using Senparc.NeuChar;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work.AdvancedAPIs.LoginAuth;
using Senparc.Weixin.Work.CommonAPIs;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    public static class LoginAuthApi
    {


        #region 同步方法


        /// <summary>
        /// 获取企业号管理员登录信息【Work中未定义】
        /// </summary>
        /// <param name="providerAccessToken">服务提供商的accesstoken</param>
        /// <param name="loginTicket">通过get_login_info得到的login_ticket, 24小时有效</param>
        /// <param name="target">登录跳转到企业号后台的目标页面，目前有：agent_setting、send_msg、contact</param>
        /// <param name="agentid">授权方应用id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "LoginAuthApi.GetLoginUrl", true)]
        public static GetLoginUrlResult GetLoginUrl(string providerAccessToken, string loginTicket, string target, int agentid, int timeOut = Config.TIME_OUT)
        {
                string url = Config.ApiWorkHost + "/cgi-bin/service/get_login_url?provider_access_token={0}";

                var data = new
                {
                    login_ticket = loginTicket,
                    target = target,
                    agentid = agentid
                };

                return CommonJsonSend.Send<GetLoginUrlResult>(providerAccessToken, url, data, CommonJsonSendType.POST, timeOut);
        }
        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】获取企业号管理员登录信息【Work中未定义】
        /// </summary>
        /// <param name="providerAccessToken">服务提供商的accesstoken</param>
        /// <param name="loginTicket">通过get_login_info得到的login_ticket, 24小时有效</param>
        /// <param name="target">登录跳转到企业号后台的目标页面，目前有：agent_setting、send_msg、contact</param>
        /// <param name="agentid">授权方应用id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        [ApiBind(NeuChar.PlatformType.WeChat_Work, "LoginAuthApi.GetLoginUrlAsync", true)]
        public static async Task<GetLoginUrlResult> GetLoginUrlAsync(string providerAccessToken, string loginTicket, string target, int agentid, int timeOut = Config.TIME_OUT)
        {
                string url = Config.ApiWorkHost + "/cgi-bin/service/get_login_url?provider_access_token={0}";

                var data = new
                {
                    login_ticket = loginTicket,
                    target = target,
                    agentid = agentid
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetLoginUrlResult>(providerAccessToken, url, data, CommonJsonSendType.POST, timeOut).ConfigureAwait(false);


        }
        #endregion

    }
}
