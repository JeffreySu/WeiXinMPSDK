#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
    
    文件名：ApiHandlerWapper.cs（v12之前原AccessTokenHandlerWapper.cs）
    文件功能描述：使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150703
    修改描述：添加TryCommonApi()方法
 
    修改标识：Senparc - 20170102
    修改描述：MP v14.3.116 TryCommonApi抛出ErrorJsonResultException、WeixinException异常时加入了accessTokenOrAppId参数

    修改标识：Senparc - 20170123
    修改描述：MP v14.3.121 TryCommonApiAsync方法返回代码改为return await result;避免死锁。

    修改标识：Senparc - 20180917
    修改描述：BaseContainer.GetFirstOrDefaultAppId() 方法添加 PlatformType 属性

    修改标识：Senparc - 20190429
    修改描述：v3.5.1 重构异步 ApiHandlerWapper

    修改标识：Senparc - 20190606
    修改描述：TryCommonApiBase<T> 中 T 参数添加 new() 约束

    修改标识：Senparc - 20200229
    修改描述：v3.7.301.1 改进 ApiHandlerWapper 对无效 AccessToken 的判断条件

----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Utilities.WeixinUtility;
using Senparc.Weixin.CommonAPIs;
using Senparc.Weixin.CommonAPIs.ApiHandlerWapper;
using System.Collections.Generic;

namespace Senparc.Weixin.Work
{
    /// <summary>
    /// 针对AccessToken无效或过期的自动处理类
    /// </summary>
    public static class ApiHandlerWapper
    {
        internal static IEnumerable<int> InvalidCredentialValues = new[] { (int)ReturnCode.不合法的access_token };//ReturnCode_Work.获取access_token时Secret错误_或者access_token无效;

        #region 同步方法

        internal static Func<string> AccessTokenContainer_GetFirstOrDefaultAppIdFunc =
                     () => AccessTokenContainer.GetFirstOrDefaultAppId(PlatformType.Work);

        internal static Func<string, bool> AccessTokenContainer_CheckRegisteredFunc =
            appKey =>
            {
                /*
                 * 对于企业微信来说，AppId = key = CorpId+'@'+CorpSecret
                 */
                return AccessTokenContainer.CheckRegistered(appKey);
            };

        internal static Func<string, bool, IAccessTokenResult> AccessTokenContainer_GetAccessTokenResultFunc =
            (appKey, getNewToken) =>
            {
                /*
                 * 对于企业微信来说，AppId = key = CorpId+'@'+CorpSecret
                 */
                return AccessTokenContainer.GetTokenResult(appKey, getNewToken);
            };


        /// <summary>
        /// 使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试。
        /// 使用此方法之前必须使用AccessTokenContainer.Register(_appId, _appSecret);或JsApiTicketContainer.Register(_appId, _appSecret);方法对账号信息进行过注册，否则会出错。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppKey">AccessToken或AppKey。如果为null，则自动取已经注册的第一个corpId/corpSecret来信息获取AccessToken。</param>
        /// <param name="retryIfFaild">请保留默认值true，不用输入。</param>
        /// <returns></returns>
        public static T TryCommonApi<T>(Func<string, T> fun, string accessTokenOrAppKey, bool retryIfFaild = true) where T : WorkJsonResult, new()
        {
            var result = ApiHandlerWapperBase.
                TryCommonApiBase(
                    PlatformType.Work,
                    AccessTokenContainer_GetFirstOrDefaultAppIdFunc,
                    AccessTokenContainer_CheckRegisteredFunc,
                    AccessTokenContainer_GetAccessTokenResultFunc,
                    InvalidCredentialValues,
                    fun, accessTokenOrAppKey, retryIfFaild);
            return result;
        }


        #endregion


        #region 异步方法

        internal static Func<Task<string>> AccessTokenContainer_GetFirstOrDefaultAppIdAsyncFunc =
              async () => await AccessTokenContainer.GetFirstOrDefaultAppIdAsync(PlatformType.Work).ConfigureAwait(false);

        internal static Func<string, Task<bool>> AccessTokenContainer_CheckRegisteredAsyncFunc =
          async appKey =>
          {
              /*
               * 对于企业微信来说，AppId = key = CorpId+'@'+CorpSecret
               */
              return await AccessTokenContainer.CheckRegisteredAsync(appKey).ConfigureAwait(false);
          };

        internal static Func<string, bool, Task<IAccessTokenResult>> AccessTokenContainer_GetAccessTokenResultAsyncFunc =
            (appKey, getNewToken) =>
            {
                /*
                 * 对于企业微信来说，AppId = key = CorpId+'@'+CorpSecret
                 */
                return AccessTokenContainer.GetTokenResultAsync(appKey, getNewToken);
            };

        /// <summary>
        /// 【异步方法】使用AccessToken进行操作时，如果遇到AccessToken错误的情况，重新获取AccessToken一次，并重试。
        /// 使用此方法之前必须使用AccessTokenContainer.Register(_appId, _appSecret);或JsApiTicketContainer.Register(_appId, _appSecret);方法对账号信息进行过注册，否则会出错。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fun"></param>
        /// <param name="accessTokenOrAppKey">AccessToken或AppKey。如果为null，则自动取已经注册的第一个corpId/corpSecret来信息获取AccessToken。</param>
        /// <param name="retryIfFaild">请保留默认值true，不用输入。</param>
        /// <returns></returns>
        public static async Task<T> TryCommonApiAsync<T>(Func<string, Task<T>> fun, string accessTokenOrAppKey, bool retryIfFaild = true) where T : WorkJsonResult, new()
        {
            var result = ApiHandlerWapperBase.
                TryCommonApiBaseAsync(
                    PlatformType.Work,
                    AccessTokenContainer_GetFirstOrDefaultAppIdAsyncFunc,
                    AccessTokenContainer_CheckRegisteredAsyncFunc,
                    AccessTokenContainer_GetAccessTokenResultAsyncFunc,
                    InvalidCredentialValues,
                    fun, accessTokenOrAppKey, retryIfFaild);
            return await result.ConfigureAwait(false);
        }
        #endregion
    }
}
