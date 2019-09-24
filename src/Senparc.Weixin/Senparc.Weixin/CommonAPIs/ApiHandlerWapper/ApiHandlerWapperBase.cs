#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：ApiHandlerWapperBase.cs
    文件功能描述：提供ApiHandlerWapper的公共基础方法
    
    
    创建标识：Senparc - 20170702
    

    修改标识：Senparc - 201700704
    修改描述：优化TryCommonApiBaseAsync方法

    修改标识：Senparc - 20170730
    修改描述：v4.13.5 完善AppId未注册提示

    修改标识：Senparc - 20181027
    修改描述：v6.1.10 改进 TryCommonApiBase 方法

    修改标识：Senparc - 20190429
    修改描述：v6.4.0 重构异步 ApiHandlerWapper

    修改标识：Senparc - 20190606
    修改描述：v6.4.8 TryCommonApiBase<T> 中 T 参数添加 new() 约束

----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Utilities.WeixinUtility;

namespace Senparc.Weixin.CommonAPIs.ApiHandlerWapper
{
    /// <summary>
    /// 所有子模块ApiHandlerWapper方法调用的基础方法
    /// </summary>
    public class ApiHandlerWapperBase
    {
        /// <summary>
        /// 返回配置错误结果信息（不抛出异常）
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static T GetConfigErrorResult<T>(string errorMessage) where T : BaseJsonResult, new()
        {
            var result = new T();
            result.errmsg = errorMessage;
            if (result is WxJsonResult)
            {
                (result as WxJsonResult).errcode = ReturnCode.SenparcWeixinSDK配置错误;
            }
            return result;
        }

        /// <summary>
        /// 返回 JsonResult 错误结果信息（不抛出异常）
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        private static T GetJsonErrorResult<T>(WxJsonResult jsonResult) where T : BaseJsonResult, new()
        {
            var result = new T();
            result.errmsg = jsonResult.errmsg;
            if (result is WxJsonResult)
            {
                (result as WxJsonResult).errcode = jsonResult.errcode;
            }
            return result;
        }

        #region 同步方法

        /// <summary>
        /// TryCommonApi 方法的基类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="platformType">平台类型，PlatformType枚举</param>
        /// <param name="accessTokenContainer_GetFirstOrDefaultAppIdFunc">AccessTokenContainer中的GetFirstOrDefaultAppId()方法</param>
        /// <param name="accessTokenContainer_CheckRegisteredFunc">AccessTokenContainer中的bool CheckRegistered(appId,getNew)方法</param>
        /// <param name="accessTokenContainer_GetAccessTokenResultFunc">AccessTokenContainer中的AccessTokenResult GetAccessTokenResult(appId)方法</param>
        /// <param name="invalidCredentialValue">"ReturnCode.获取access_token时AppSecret错误或者access_token无效"枚举的值</param>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="retryIfFaild"></param>
        /// <returns></returns>
        public static T TryCommonApiBase<T>(
            PlatformType platformType,
            Func<string> accessTokenContainer_GetFirstOrDefaultAppIdFunc,
            Func<string, bool> accessTokenContainer_CheckRegisteredFunc,
            Func<string, bool, IAccessTokenResult> accessTokenContainer_GetAccessTokenResultFunc,
            int invalidCredentialValue,
            Func<string, T> fun, string accessTokenOrAppId = null, bool retryIfFaild = true) where T : BaseJsonResult, new()
        {

            //ApiHandlerWapperFactory.ApiHandlerWapperFactoryCollection["s"] = ()=> new Senparc.Weixin.MP.AdvancedAPIs.User.UserInfoJson();

            //var platform = ApiHandlerWapperFactory.CurrentPlatform;//当前平台

            /*
             * 对于企业微信来说，AppId = key = CorpId+CorpSecret
             */

            string appId = null;
            string accessToken = null;

            if (accessTokenOrAppId == null)
            {
                appId = accessTokenContainer_GetFirstOrDefaultAppIdFunc != null ? accessTokenContainer_GetFirstOrDefaultAppIdFunc() : null;// AccessTokenContainer.GetFirstOrDefaultAppId();
                if (appId == null)
                {
                    var unregisterAppIdEx = new UnRegisterAppIdException(null, $"尚无已经注册的AppId，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！模块：{platformType}");
                    if (Config.ThrownWhenJsonResultFaild)
                    {
                        throw unregisterAppIdEx;//抛出异常
                    }
                    else
                    {
                        return GetConfigErrorResult<T>(unregisterAppIdEx.Message);//返回 Json 错误结果
                    }
                }
            }
            else if (ApiUtility.IsAppId(accessTokenOrAppId, platformType))
            {
                //if (!AccessTokenContainer.CheckRegistered(accessTokenOrAppId))
                if (!accessTokenContainer_CheckRegisteredFunc(accessTokenOrAppId))
                {
                    var unregisterAppIdEx = new UnRegisterAppIdException(null, $"此appId（{accessTokenOrAppId}）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！模块：{platformType}");
                    if (Config.ThrownWhenJsonResultFaild)
                    {
                        throw unregisterAppIdEx;//抛出异常
                    }
                    else
                    {
                        return GetConfigErrorResult<T>(unregisterAppIdEx.Message);//返回 Json 错误结果
                    }
                }

                appId = accessTokenOrAppId;
            }
            else
            {
                accessToken = accessTokenOrAppId;//accessToken
            }

            T result = null;

            try
            {
                if (accessToken == null)
                {
                    var accessTokenResult = accessTokenContainer_GetAccessTokenResultFunc(appId, false); //AccessTokenContainer.GetAccessTokenResult(appId, false);
                    accessToken = accessTokenResult.access_token;
                }
                result = fun(accessToken);

                //当系统不抛出异常，且当前返回结果不成功，且允许重试的时候，在内部抛出一个异常，以便进行 Retry
                if (!Config.ThrownWhenJsonResultFaild
                    && result is WxJsonResult
                    && (result as WxJsonResult).errcode != ReturnCode.请求成功
                    && retryIfFaild
                    )
                {
                    var errorResult = result as WxJsonResult;
                    throw new ErrorJsonResultException(
                          string.Format("微信请求发生错误！错误代码：{0}，说明：{1}",
                                          (int)errorResult.errcode, errorResult.errmsg), null, errorResult);
                }
            }
            catch (ErrorJsonResultException ex)
            {
                if (retryIfFaild
                    && appId != null    //如果 appId 为 null，已经没有重试的意义（直接提供的 AccessToken 是错误的）
                                        //&& ex.JsonResult.errcode == ReturnCode.获取access_token时AppSecret错误或者access_token无效)
                    && (int)ex.JsonResult.errcode == invalidCredentialValue)
                {
                    //尝试重新验证
                    var accessTokenResult = accessTokenContainer_GetAccessTokenResultFunc(appId, true);//AccessTokenContainer.GetAccessTokenResult(appId, true);
                                                                                                       //强制获取并刷新最新的AccessToken
                    accessToken = accessTokenResult.access_token;
                    result = TryCommonApiBase(platformType,
                                accessTokenContainer_GetFirstOrDefaultAppIdFunc,
                                accessTokenContainer_CheckRegisteredFunc,
                                accessTokenContainer_GetAccessTokenResultFunc,
                                invalidCredentialValue,
                                fun, accessToken, false);
                }
                else
                {
                    ex.AccessTokenOrAppId = accessTokenOrAppId;

                    //如果要求抛出异常，并且传入的是 AccessToken（AppId 为 null），那么已经没有必要重试，直接抛出异常
                    if (Config.ThrownWhenJsonResultFaild && appId == null)
                    {
                        throw;//抛出异常
                    }
                    else
                    {
                        return GetJsonErrorResult<T>(ex.JsonResult);//返回 Json 错误结果
                    }
                }
            }
            catch (WeixinException ex)
            {
                ex.AccessTokenOrAppId = accessTokenOrAppId;

                //判断如果传进来的是 AccessToken，并且不抛出异常，那么这里不throw
                if (Config.ThrownWhenJsonResultFaild && ApiUtility.IsAppId(accessTokenOrAppId, platformType))
                {
                    throw;//抛出异常
                }
                else
                {
                    return GetConfigErrorResult<T>(ex.Message);//返回 Json 错误结果
                }
            }

            return result;
        }


        #endregion


        #region 异步方法


        /// <summary>
        /// TryCommonApi 方法的基类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="platformType">平台类型，PlatformType枚举</param>
        /// <param name="accessTokenContainer_GetFirstOrDefaultAppIdAsyncFunc">AccessTokenContainer中的GetFirstOrDefaultAppId()方法</param>
        /// <param name="accessTokenContainer_CheckRegisteredAsyncFunc">AccessTokenContainer中的bool CheckRegistered(appId,getNew)方法</param>
        /// <param name="accessTokenContainer_GetAccessTokenResultAsyncFunc">AccessTokenContainer中的AccessTokenResult GetAccessTokenResultAsync(appId)方法（异步方法）</param>
        /// <param name="invalidCredentialValue">"ReturnCode.获取access_token时AppSecret错误或者access_token无效"枚举的值</param>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="retryIfFaild"></param>
        /// <returns></returns>
        public static async Task<T> TryCommonApiBaseAsync<T>(
            PlatformType platformType,
            Func<Task<string>> accessTokenContainer_GetFirstOrDefaultAppIdAsyncFunc,
            Func<string, Task<bool>> accessTokenContainer_CheckRegisteredAsyncFunc,
            Func<string, bool, Task<IAccessTokenResult>> accessTokenContainer_GetAccessTokenResultAsyncFunc,
            int invalidCredentialValue,
            Func<string, Task<T>> fun, string accessTokenOrAppId = null, bool retryIfFaild = true) where T : BaseJsonResult, new()
        {

            //ApiHandlerWapperFactory.ApiHandlerWapperFactoryCollection["s"] = ()=> new Senparc.Weixin.MP.AdvancedAPIs.User.UserInfoJson();

            //var platform = ApiHandlerWapperFactory.CurrentPlatform;//当前平台

            string appId = null;
            string accessToken = null;

            if (accessTokenOrAppId == null)
            {
                appId = await accessTokenContainer_GetFirstOrDefaultAppIdAsyncFunc().ConfigureAwait(false);// AccessTokenContainer.GetFirstOrDefaultAppId();
                if (appId == null)
                {
                    var unregisterAppIdEx = new UnRegisterAppIdException(null, $"尚无已经注册的AppId，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！模块：{platformType}");
                    if (Config.ThrownWhenJsonResultFaild)
                    {
                        throw unregisterAppIdEx;//抛出异常
                    }
                    else
                    {
                        return GetConfigErrorResult<T>(unregisterAppIdEx.Message);//返回 Json 错误结果
                    }
                }
            }
            else if (ApiUtility.IsAppId(accessTokenOrAppId, platformType))
            {
                //if (!AccessTokenContainer.CheckRegistered(accessTokenOrAppId))
                if (!await accessTokenContainer_CheckRegisteredAsyncFunc(accessTokenOrAppId))
                {
                    var unregisterAppIdEx = new UnRegisterAppIdException(null, $"此appId（{accessTokenOrAppId}）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！模块：{platformType}");
                    if (Config.ThrownWhenJsonResultFaild)
                    {
                        throw unregisterAppIdEx;//抛出异常
                    }
                    else
                    {
                        return GetConfigErrorResult<T>(unregisterAppIdEx.Message);//返回 Json 错误结果
                    }
                }

                appId = accessTokenOrAppId;
            }
            else
            {
                accessToken = accessTokenOrAppId; //accessToken
            }


            T result = null;

            try
            {
                if (accessToken == null)
                {
                    var accessTokenResult = await accessTokenContainer_GetAccessTokenResultAsyncFunc(appId, false);//AccessTokenContainer.GetAccessTokenResultAsync(appId, false);
                    accessToken = accessTokenResult.access_token;
                }
                result = await fun(accessToken).ConfigureAwait(false);

                //当系统不抛出异常，且当前返回结果不成功，且允许重试的时候，在内部抛出一个异常，以便进行 Retry
                if (!Config.ThrownWhenJsonResultFaild
                    && result is WxJsonResult
                    && (result as WxJsonResult).errcode != ReturnCode.请求成功
                    && retryIfFaild
                    )
                {
                    var errorResult = result as WxJsonResult;
                    throw new ErrorJsonResultException(
                          string.Format("微信请求发生错误！错误代码：{0}，说明：{1}",
                                          (int)errorResult.errcode, errorResult.errmsg), null, errorResult);
                }
            }
            catch (ErrorJsonResultException ex)
            {
                if (retryIfFaild
                    && appId != null    //如果 appId 为 null，已经没有重试的意义（直接提供的 AccessToken 是错误的）
                                        //&& ex.JsonResult.errcode == ReturnCode.获取access_token时AppSecret错误或者access_token无效)
                    && (int)ex.JsonResult.errcode == invalidCredentialValue)
                {
                    //尝试重新验证（如果是低版本VS，此处不能使用await关键字，可以直接使用xx.Result输出。VS2013不支持：无法在 catch 字句体中等待）
                    var accessTokenResult = await accessTokenContainer_GetAccessTokenResultAsyncFunc(appId, true).ConfigureAwait(false);//AccessTokenContainer.GetAccessTokenResultAsync(appId, true);
                                                                                                                                        //强制获取并刷新最新的AccessToken
                    accessToken = accessTokenResult.access_token;

                    result = await TryCommonApiBaseAsync(platformType,
                                accessTokenContainer_GetFirstOrDefaultAppIdAsyncFunc,
                                accessTokenContainer_CheckRegisteredAsyncFunc,
                                accessTokenContainer_GetAccessTokenResultAsyncFunc,
                                invalidCredentialValue,
                                fun, appId, false).ConfigureAwait(false);
                    //result = TryCommonApiAsync(fun, appId, false);
                }
                else
                {
                    //如果要求抛出异常，并且传入的是 AccessToken（AppId 为 null），那么已经没有必要重试，直接抛出异常
                    if (Config.ThrownWhenJsonResultFaild && appId == null)
                    {
                        throw;//抛出异常
                    }
                    else
                    {
                        return GetJsonErrorResult<T>(ex.JsonResult);//返回 Json 错误结果
                    }
                }
            }
            catch (WeixinException ex)
            {
                ex.AccessTokenOrAppId = accessTokenOrAppId;

                //判断如果传进来的是 AccessToken，并且不抛出异常，那么这里不throw
                if (Config.ThrownWhenJsonResultFaild && ApiUtility.IsAppId(accessTokenOrAppId, platformType))
                {
                    throw;//抛出异常
                }
                else
                {
                    return GetConfigErrorResult<T>(ex.Message);//返回 Json 错误结果
                }
            }

            return result;
        }


        #endregion

    }
}
