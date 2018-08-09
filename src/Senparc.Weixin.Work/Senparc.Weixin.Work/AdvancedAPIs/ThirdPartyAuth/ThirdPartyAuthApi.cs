/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：ThirdPartyAuthApi.cs
    文件功能描述：第三方应用授权接口
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
 
    修改标识：Senparc - 20150313
    修改描述：开放代理请求超时时间

    修改标识：杨杨得意 - 20160618
    修改描述：更新到20160615最新接口

    修改标识：Senparc - 20160720
    修改描述：增加其接口的异步方法

    -----------------------------------
    
    修改标识：Senparc - 20170617
    修改描述：从QY移植，同步Work接口

    修改标识：Senparc - 20170707
    修改描述：v14.5.1 完善异步方法async/await

    修改标识：Senparc - 20170712
    修改描述：v14.5.1 AccessToken HandlerWaper改造
----------------------------------------------------------------*/

/*
    官方文档：http://work.weixin.qq.com/api/doc#10975
 */

using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.Work.AdvancedAPIs.ThirdPartyAuth;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Helpers;
using Senparc.CO2NET.Helpers.Serializers;
using Senparc.CO2NET.Extensions;

namespace Senparc.Weixin.Work.AdvancedAPIs
{
    public static class ThirdPartyAuthApi
    {
        #region 同步方法

        /// <summary>
        /// 获取应用套件令牌【QY移植修改】
        /// </summary>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="suiteSecret">应用套件secret</param>
        /// <param name="suiteTicket">微信后台推送的ticket</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetSuiteTokenResult GetSuiteToken(string suiteId, string suiteSecret, string suiteTicket, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/service/get_suite_token";

                var data = new
                {
                    suite_id = suiteId,
                    suite_secret = suiteSecret,
                    suite_ticket = suiteTicket
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetSuiteTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteId);


        }

        ///// <summary>
        ///// 获取预授权码
        ///// </summary>
        ///// <param name="suiteAccessToken"></param>
        ///// <param name="suiteId">应用套件id</param>
        ///// <param name="appId">应用id，本参数选填，表示用户能对本套件内的哪些应用授权，不填时默认用户有全部授权权限</param>
        ///// <param name="timeOut">代理请求超时时间（毫秒）</param>
        ///// <returns></returns>
        //public static GetPreAuthCodeResult GetPreAuthCode(string suiteAccessToken, string suiteId, int[] appId, int timeOut = Config.TIME_OUT)
        //{
        //    var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_pre_auth_code?suite_access_token={0}", suiteAccessToken.AsUrlData());

        //    var data = new
        //        {
        //            suite_id = suiteId,
        //            appid = appId
        //        };

        //    return CommonJsonSend.Send<GetPreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        //}

        /// <summary>
        /// 获取预授权码【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetPreAuthCodeResult GetPreAuthCode(string suiteAccessToken, string suiteId, int timeOut = 10000)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_pre_auth_code?suite_access_token={0}", suiteAccessToken.AsUrlData());
                var data = new
                {
                    suite_id = suiteId,
                };
                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetPreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 设置授权配置【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="authCode">预授权码</param>
        /// <param name="appid">允许进行授权的应用id，如1、2、3， 不填或者填空数组都表示允许授权套件内所有应用 </param>
        /// <param name="auth_type">授权类型：0 正式授权， 1 测试授权， 默认值为0 </param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static WorkJsonResult SetAuthConfig(string suiteAccessToken, string authCode, int[] appid = null, int? auth_type = null, int timeOut = 10000)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/set_session_info?suite_access_token={0}", suiteAccessToken.AsUrlData());
                var data = new
                {
                    pre_auth_code = authCode,
                    session_info = new
                    {
                        appid = appid,
                        auth_type = auth_type
                    }
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 获取企业号的永久授权码【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCode">临时授权码会在授权成功时附加在redirect_uri中跳转回应用提供商网站。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetPermanentCodeResult GetPermanentCode(string suiteAccessToken, string suiteId, string authCode, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_permanent_code?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_code = authCode
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetPermanentCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 获取企业授权信息【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，通过get_permanent_code获取</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetAuthInfoResult GetAuthInfo(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_auth_info?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetAuthInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 获取企业号应用【Work中未定义】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，从get_permanent_code接口中获取</param>
        /// <param name="agentId">授权方应用id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetAgentResult GetAgent(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, string agentId, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_agent?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                    agentid = agentId
                };

                return CommonJsonSend.Send<GetAgentResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 设置企业号应用【Work中未定义】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，从get_permanent_code接口中获取</param>
        /// <param name="agent">要设置的企业应用的信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static WorkJsonResult SetAgent(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, ThirdParty_AgentData agent, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/set_agent?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                    agent = agent
                };

                return CommonJsonSend.Send<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 获取企业access_token【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，通过get_permanent_code获取</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static GetCorpTokenResult GetCorpToken(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, int timeOut = Config.TIME_OUT)
        {
            return ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_corp_token?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                };

                return Senparc.Weixin.CommonAPIs.CommonJsonSend.Send<GetCorpTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }
        #endregion

#if !NET35 && !NET40
        #region 异步方法

        /// <summary>
        ///【异步方法】 获取应用套件令牌【QY移植修改】
        /// </summary>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="suiteSecret">应用套件secret</param>
        /// <param name="suiteTicket">微信后台推送的ticket</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetSuiteTokenResult> GetSuiteTokenAsync(string suiteId, string suiteSecret, string suiteTicket, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = Config.ApiWorkHost + "/cgi-bin/service/get_suite_token";

                var data = new
                {
                    suite_id = suiteId,
                    suite_secret = suiteSecret,
                    suite_ticket = suiteTicket
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetSuiteTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteId);


        }

        ///// <summary>
        ///// 【异步方法】获取预授权码
        ///// </summary>
        ///// <param name="suiteAccessToken"></param>
        ///// <param name="suiteId">应用套件id</param>
        ///// <param name="appId">应用id，本参数选填，表示用户能对本套件内的哪些应用授权，不填时默认用户有全部授权权限</param>
        ///// <param name="timeOut">代理请求超时时间（毫秒）</param>
        ///// <returns></returns>
        //public static GetPreAuthCodeResult GetPreAuthCode(string suiteAccessToken, string suiteId, int[] appId, int timeOut = Config.TIME_OUT)
        //{
        //    var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_pre_auth_code?suite_access_token={0}", suiteAccessToken.AsUrlData());

        //    var data = new
        //        {
        //            suite_id = suiteId,
        //            appid = appId
        //        };

        //    return CommonJsonSend.Send<GetPreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
        //}

        /// <summary>
        /// 【异步方法】获取预授权码【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetPreAuthCodeResult> GetPreAuthCodeAsync(string suiteAccessToken, string suiteId, int timeOut = 10000)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_pre_auth_code?suite_access_token={0}", suiteAccessToken.AsUrlData());
                var data = new
                {
                    suite_id = suiteId,
                };
                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetPreAuthCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】设置授权配置【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="authCode">预授权码</param>
        /// <param name="appid">允许进行授权的应用id，如1、2、3， 不填或者填空数组都表示允许授权套件内所有应用 </param>
        /// <param name="auth_type">授权类型：0 正式授权， 1 测试授权， 默认值为0 </param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SetAuthConfigAsync(string suiteAccessToken, string authCode, int[] appid = null, int? auth_type = null, int timeOut = 10000)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/set_session_info?suite_access_token={0}", suiteAccessToken.AsUrlData());
                var data = new
                {
                    pre_auth_code = authCode,
                    session_info = new
                    {
                        appid = appid,
                        auth_type = auth_type
                    }
                };

                JsonSetting jsonSetting = new JsonSetting(true);

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut, jsonSetting: jsonSetting);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】获取企业号的永久授权码【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCode">临时授权码会在授权成功时附加在redirect_uri中跳转回应用提供商网站。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetPermanentCodeResult> GetPermanentCodeAsync(string suiteAccessToken, string suiteId, string authCode, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_permanent_code?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_code = authCode
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetPermanentCodeResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】获取企业授权信息【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，通过get_permanent_code获取</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetAuthInfoResult> GetAuthInfoAsync(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_auth_info?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetAuthInfoResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】获取企业号应用【Work中未定义】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，从get_permanent_code接口中获取</param>
        /// <param name="agentId">授权方应用id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetAgentResult> GetAgentAsync(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, string agentId, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_agent?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                    agentid = agentId
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetAgentResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】设置企业号应用【Work中未定义】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，从get_permanent_code接口中获取</param>
        /// <param name="agent">要设置的企业应用的信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<WorkJsonResult> SetAgentAsync(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, ThirdParty_AgentData agent, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/set_agent?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                    agent = agent
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<WorkJsonResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }

        /// <summary>
        /// 【异步方法】获取企业access_token【QY移植修改】
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，通过get_permanent_code获取</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public static async Task<GetCorpTokenResult> GetCorpTokenAsync(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, int timeOut = Config.TIME_OUT)
        {
            return await ApiHandlerWapper.TryCommonApiAsync(async accessToken =>
            {
                var url = string.Format(Config.ApiWorkHost + "/cgi-bin/service/get_corp_token?suite_access_token={0}", suiteAccessToken.AsUrlData());

                var data = new
                {
                    suite_id = suiteId,
                    auth_corpid = authCorpId,
                    permanent_code = permanentCode,
                };

                return await Senparc.Weixin.CommonAPIs.CommonJsonSend.SendAsync<GetCorpTokenResult>(null, url, data, CommonJsonSendType.POST, timeOut);
            }, suiteAccessToken);


        }
        #endregion
#endif
    }
}
