/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：ApiHandlerWapper.cs（v12之前原AccessTokenHandlerWapper.cs）
    文件功能描述：使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150703
    修改描述：添加TryCommonApi()方法
----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.Utilities.WeixinUtility;

namespace Senparc.Weixin.MP
{
    /// <summary>
    /// 针对AccessToken无效或过期的自动处理类
    /// </summary>
    public static class ApiHandlerWapper
    {
        /// <summary>
        /// 使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试。
        /// 使用此方法之前必须使用AccessTokenContainer.Register(_appId, _appSecret);或JsApiTicketContainer.Register(_appId, _appSecret);方法对账号信息进行过注册，否则会出错。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppId">AccessToken或AppId。如果为null，则自动取已经注册的第一个appId/appSecret来信息获取AccessToken。</param>
        /// <param name="retryIfFaild">请保留默认值true，不用输入。</param>
        /// <returns></returns>
        public static T TryCommonApi<T>(Func<string, T> fun, string accessTokenOrAppId = null, bool retryIfFaild = true) where T : WxJsonResult
        {
            string appId = null;
            string accessToken = null;

            if (accessTokenOrAppId == null)
            {
                appId = AccessTokenContainer.GetFirstOrDefaultAppId();
                if (appId == null)
                {
                    throw new UnRegisterAppIdException(null,
                        "尚无已经注册的AppId，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！");
                }
            }
            else if (ApiUtility.IsAppId(accessTokenOrAppId))
            {
                if (!AccessTokenContainer.CheckRegistered(accessTokenOrAppId))
                {
                    throw new UnRegisterAppIdException(accessTokenOrAppId, string.Format("此appId（{0}）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！", accessTokenOrAppId));
                }

                appId = accessTokenOrAppId;
            }
            else
            {
                //accessToken
                accessToken = accessTokenOrAppId;
            }


            T result = null;

            try
            {
                if (accessToken == null)
                {
                    var accessTokenResult = AccessTokenContainer.GetAccessTokenResult(appId, false);
                    accessToken = accessTokenResult.access_token;
                }
                result = fun(accessToken);
            }
            catch (ErrorJsonResultException ex)
            {
                if (retryIfFaild
                    && appId != null
                    && ex.JsonResult.errcode == ReturnCode.获取access_token时AppSecret错误或者access_token无效)
                {
                    //尝试重新验证
                    var accessTokenResult = AccessTokenContainer.GetAccessTokenResult(appId, true);//强制获取并刷新最新的AccessToken
                    accessToken = accessTokenResult.access_token;
                    result = TryCommonApi(fun, appId, false);
                }
                else
                {
                    throw;
                }
            }

            return result;
        }

        /// <summary>
        /// 使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="fun">第一个参数为accessToken</param>
        /// <param name="retryIfFaild"></param>
        /// <returns></returns>
        [Obsolete("请使用TryCommonApi()方法")]
        public static T Do<T>(Func<string, T> fun, string appId, string appSecret, bool retryIfFaild = true)
            where T : WxJsonResult
        {
            T result = null;
            try
            {
                var accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret, false);
                result = fun(accessToken);
            }
            catch (ErrorJsonResultException ex)
            {
                if (retryIfFaild && ex.JsonResult.errcode == ReturnCode.获取access_token时AppSecret错误或者access_token无效)
                {
                    //尝试重新验证
                    var accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret, true);
                    result = Do(fun, appId, appSecret, false);
                }
            }
            return result;
        }

    }
}
