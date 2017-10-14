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
    
    文件名：ApiHandlerWapper.cs（v12之前原AccessTokenHandlerWapper.cs）
    文件功能描述：使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150703
    修改描述：添加TryCommonApi()方法
 
    修改标识：Senparc - 20170102
    修改描述：v14.3.116 TryCommonApi抛出ErrorJsonResultException、WeixinException异常时加入了accessTokenOrAppId参数

    修改标识：Senparc - 20170123
    修改描述：v14.3.121 TryCommonApiAsync方法返回代码改为return await result;避免死锁。
----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.CommonAPIs;
using Senparc.Weixin.QY.Containers;
using Senparc.Weixin.Utilities.WeixinUtility;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.CommonAPIs.ApiHandlerWapper;

namespace Senparc.Weixin.QY
{
    /// <summary>
    /// 针对AccessToken无效或过期的自动处理类
    /// </summary>
    public static class ApiHandlerWapper
    {
        #region 同步方法

        /// <summary>
        /// 使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试。
        /// 使用此方法之前必须使用AccessTokenContainer.Register(_appId, _appSecret);或JsApiTicketContainer.Register(_appId, _appSecret);方法对账号信息进行过注册，否则会出错。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppKey">AccessToken或AppKey。如果为null，则自动取已经注册的第一个corpId/corpSecret来信息获取AccessToken。</param>
        /// <param name="retryIfFaild">请保留默认值true，不用输入。</param>
        /// <returns></returns>
        public static T TryCommonApi<T>(Func<string, T> fun, string accessTokenOrAppKey, bool retryIfFaild = true) where T : QyJsonResult
        {
            Func<string> accessTokenContainer_GetFirstOrDefaultAppIdFunc =
                () => AccessTokenContainer.GetFirstOrDefaultAppId();

            Func<string, bool> accessTokenContainer_CheckRegisteredFunc =
                appKey =>
                {
                    /*
                     * 对于企业微信来说，AppId = key = CorpId+'@'+CorpSecret
                     */
                    return AccessTokenContainer.CheckRegistered(appKey);
                };

            Func<string, bool, IAccessTokenResult> accessTokenContainer_GetAccessTokenResultFunc =
                (appKey, getNewToken) =>
                {
                    /*
                     * 对于企业微信来说，AppId = key = CorpId+'@'+CorpSecret
                     */
                    return AccessTokenContainer.GetTokenResult(appKey, getNewToken);
                };

            int invalidCredentialValue = (int)ReturnCode_Work.获取access_token时Secret错误_或者access_token无效;

            var result = ApiHandlerWapperBase.
                TryCommonApiBase(
                    PlatformType.Work,
                    accessTokenContainer_GetFirstOrDefaultAppIdFunc,
                    accessTokenContainer_CheckRegisteredFunc,
                    accessTokenContainer_GetAccessTokenResultFunc,
                    invalidCredentialValue,
                    fun, accessTokenOrAppKey, retryIfFaild);
            return result;
        }


        #endregion

#if !NET35 && !NET40
        #region 异步方法
        /// <summary>
        /// 【异步方法】使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试。
        /// 使用此方法之前必须使用AccessTokenContainer.Register(_appId, _appSecret);或JsApiTicketContainer.Register(_appId, _appSecret);方法对账号信息进行过注册，否则会出错。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppKey">AccessToken或AppKey。如果为null，则自动取已经注册的第一个corpId/corpSecret来信息获取AccessToken。</param>
        /// <param name="retryIfFaild">请保留默认值true，不用输入。</param>
        /// <returns></returns>
        public static async Task<T> TryCommonApiAsync<T>(Func<string, Task<T>> fun, string accessTokenOrAppKey, bool retryIfFaild = true) where T : QyJsonResult
        {
            Func<string> accessTokenContainer_GetFirstOrDefaultAppIdFunc =
                () => AccessTokenContainer.GetFirstOrDefaultAppId();

            Func<string, bool> accessTokenContainer_CheckRegisteredFunc =
                appKey =>
                {
                    /*
                     * 对于企业微信来说，AppId = key = CorpId+'@'+CorpSecret
                     */
                    return AccessTokenContainer.CheckRegistered(appKey);
                };

            Func<string, bool, Task<IAccessTokenResult>> accessTokenContainer_GetAccessTokenResultAsyncFunc =
                (appKey, getNewToken) =>
                {
                    /*
                     * 对于企业微信来说，AppId = key = CorpId+'@'+CorpSecret
                     */
                    return AccessTokenContainer.GetTokenResultAsync(appKey, getNewToken);
                };

            int invalidCredentialValue = (int)ReturnCode_Work.获取access_token时Secret错误_或者access_token无效;

            var result = ApiHandlerWapperBase.
                TryCommonApiBaseAsync(
                    PlatformType.Work,
                    accessTokenContainer_GetFirstOrDefaultAppIdFunc,
                    accessTokenContainer_CheckRegisteredFunc,
                    accessTokenContainer_GetAccessTokenResultAsyncFunc,
                    invalidCredentialValue,
                    fun, accessTokenOrAppKey, retryIfFaild);
            return await result;
        }
        #endregion
#endif
    }
}
