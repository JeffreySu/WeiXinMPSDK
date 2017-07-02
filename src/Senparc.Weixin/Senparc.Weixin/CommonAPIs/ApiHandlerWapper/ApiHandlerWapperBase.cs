#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
    
    文件名：ApiHandlerWapperBase.cs
    文件功能描述：提供ApiHandlerWapper的公共基础方法
    
    
    创建标识：Senparc - 20170702
    
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
    public class ApiHandlerWapperBase
    {
        /// <summary>
        /// TryCommonApi 方法的基类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="accessTokenContainer_GetFirstOrDefaultAppIdFunc">AccessTokenContainer中的GetFirstOrDefaultAppId()方法</param>
        /// <param name="accessTokenContainer_CheckRegisteredFunc">AccessTokenContainer中的bool CheckRegistered(appId,getNew)方法</param>
        /// <param name="accessTokenContainer_GetAccessTokenResultFunc">AccessTokenContainer中的AccessTokenResult GetAccessTokenResult(appId)方法</param>
        /// <param name="invalidCredentialValue">"ReturnCode.获取access_token时AppSecret错误或者access_token无效"枚举的值</param>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="retryIfFaild"></param>
        /// <returns></returns>
        public static T TryCommonApiBase<T>(
            Func<string> accessTokenContainer_GetFirstOrDefaultAppIdFunc,
            Func<string, bool> accessTokenContainer_CheckRegisteredFunc,
            Func<string, bool, AccessTokenResultBase> accessTokenContainer_GetAccessTokenResultFunc,
            int invalidCredentialValue,
            Func<string, T> fun, string accessTokenOrAppId = null, bool retryIfFaild = true) where T : WxJsonResult
        {

            //ApiHandlerWapperFactory.ApiHandlerWapperFactoryCollection["s"] = ()=> new Senparc.Weixin.MP.AdvancedAPIs.User.UserInfoJson();

            var platform = ApiHandlerWapperFactory.CurrentPlatform;//当前平台

            string appId = null;
            string accessToken = null;

            if (accessTokenOrAppId == null)
            {
                appId = accessTokenContainer_GetFirstOrDefaultAppIdFunc != null ? accessTokenContainer_GetFirstOrDefaultAppIdFunc() : null;// AccessTokenContainer.GetFirstOrDefaultAppId();
                if (appId == null)
                {
                    throw new UnRegisterAppIdException(null,
                        "尚无已经注册的AppId，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！");
                }
            }
            else if (ApiUtility.IsAppId(accessTokenOrAppId))
            {
                //if (!AccessTokenContainer.CheckRegistered(accessTokenOrAppId))
                if (!accessTokenContainer_CheckRegisteredFunc(accessTokenOrAppId))
                {
                    throw new UnRegisterAppIdException(accessTokenOrAppId, string.Format("此appId（{0}）尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！", accessTokenOrAppId));
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
            }
            catch (ErrorJsonResultException ex)
            {
                if (retryIfFaild
                    && appId != null
                    //&& ex.JsonResult.errcode == ReturnCode.获取access_token时AppSecret错误或者access_token无效)
                    && (int)ex.JsonResult.errcode == invalidCredentialValue)
                {
                    //尝试重新验证
                    var accessTokenResult = accessTokenContainer_GetAccessTokenResultFunc(appId, false);//AccessTokenContainer.GetAccessTokenResult(appId, true);
                    //强制获取并刷新最新的AccessToken
                    accessToken = accessTokenResult.access_token;
                    result = TryCommonApiBase(accessTokenContainer_GetFirstOrDefaultAppIdFunc,
                                              accessTokenContainer_CheckRegisteredFunc,
                                              accessTokenContainer_GetAccessTokenResultFunc,
                                              invalidCredentialValue,
                                              fun, appId, false);
                }
                else
                {
                    ex.AccessTokenOrAppId = accessTokenOrAppId;
                    throw;
                }
            }
            catch (WeixinException ex)
            {
                ex.AccessTokenOrAppId = accessTokenOrAppId;
                throw;
            }

            return result;

        }
    }
}
