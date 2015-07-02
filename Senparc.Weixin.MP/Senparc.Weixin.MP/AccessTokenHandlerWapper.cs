/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：AccessTokenHandlerWapper.cs
    文件功能描述：使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150702
    修改描述：添加TryCommonApi()方法
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP
{
    /// <summary>
    /// 针对AccessToken无效或过期的自动处理类
    /// </summary>
    public static class AccessTokenHandlerWapper
    {
        /// <summary>
        /// 使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="fun">第一个参数为accessToken</param>
        /// <param name="retryIfFaild"></param>
        /// <returns></returns>
        public static T Do<T>(Func<string, T> fun, string appId, string appSecret, bool retryIfFaild = true)
            where T : WxJsonResult
        {
            T result = null;
            try
            {
                var accessToken = AccessTokenContainer.TryGetToken(appId, appSecret, false);
                result = fun(accessToken);
            }
            catch (ErrorJsonResultException ex)
            {
                if (retryIfFaild && ex.JsonResult.errcode == ReturnCode.获取access_token时AppSecret错误或者access_token无效)
                {
                    //尝试重新验证
                    var accessToken = AccessTokenContainer.TryGetToken(appId, appSecret, true);
                    result = Do(fun, appId, appSecret, false);
                }
            }
            return result;
        }

        /// <summary>
        /// 使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试。
        /// 使用此方法之前必须使用AccessTokenContainer.Register(_appId, _appSecret);或JsApiTicketContainer.Register(_appId, _appSecret);方法对账号信息进行过注册，否则会出错。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <param name="appId">如果为null，则取已经注册的第一个appId、appSecret信息</param>
        /// <returns></returns>
        public static T TryCommonApi<T>(Func<string, T> fun, string appId) where T : WxJsonResult
        {
            if (appId != null && !AccessTokenContainer.CheckRegistered(appId))
            {
                throw new WeixinException("此appId尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！");
            }

            appId = appId ?? AccessTokenContainer.GetFirstOrDefaultAppId();
            if (appId == null)
            {
                throw new WeixinException("尚无已经注册的AppId，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！");
            }

            T result = null;

            try
            {
                var accessTokenResult = AccessTokenContainer.GetTokenResult(appId, false);
                var accessToken = accessTokenResult.access_token;
                result = fun(accessToken);
            }
            catch (ErrorJsonResultException ex)
            {
                if (ex.JsonResult.errcode == ReturnCode.获取access_token时AppSecret错误或者access_token无效)
                {
                    //尝试重新验证
                    var accessTokenResult = AccessTokenContainer.GetTokenResult(appId, true);
                    var accessToken = accessTokenResult.access_token;
                    result = TryCommonApi(fun, appId);
                }
            }
            return result;
        }
    }
}
