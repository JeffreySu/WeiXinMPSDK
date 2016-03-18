/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：AuthorizerContainer.cs
    文件功能描述：通用接口JsApiTicket容器，用于OPEN第三方JSSDK自动管理JsApiTicket，如果过期会重新获取


    创建标识：Senparc - 20150211

    修改标识：renny - 20150921
    修改描述：整理接口

    修改标识：senparc - 20151004
    修改描述：文件名从JsApiTicketContainer.cs变为AuthorizerContainer.cs，用于集成所有授权方信息

    修改标识：Senparc - 20160206
    修改描述：将public object Lock更改为internal object Lock

    修改标识：Senparc - 20160318
    修改描述：v1.6.4 使用FlushCache.CreateInstance使注册过程立即生效

----------------------------------------------------------------*/

using System;
using Senparc.Weixin.CacheUtility;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Open.Entities;
using Senparc.Weixin.Open.Exceptions;

namespace Senparc.Weixin.Open.ComponentAPIs
{
    /// <summary>
    /// 之前的JsApiTicketBag
    /// </summary>
    [Serializable]
    public class AuthorizerBag : BaseContainerBag
    {
        /// <summary>
        /// 授权方AppId，缓存中实际的Key
        /// </summary>
        public string AuthorizerAppId
        {
            get { return _authorizerAppId; }
            set { base.SetContainerProperty(ref _authorizerAppId, value, "AuthorizerAppId"); }
        }

        /// <summary>
        /// 第三方平台AppId
        /// </summary>
        public string ComponentAppId
        {
            get { return _componentAppId; }
            set { base.SetContainerProperty(ref _componentAppId, value, "ComponentAppId"); }
        }

        ///// <summary>
        ///// 从ComponentContainer取过来的对应ComponentAppId的ComponentBag
        ///// </summary>
        //public ComponentBag ComponentBag { get; set; }

        /// <summary>
        /// 授权信息
        /// </summary>
        public GetAuthorizerInfoResult FullAuthorizerInfoResult
        {
            get
            {
                var result = new GetAuthorizerInfoResult()
                {
                    authorizer_info = AuthorizerInfo,
                    authorization_info = AuthorizationInfo
                };
                return result;
            }
        }


        public JsApiTicketResult JsApiTicketResult
        {
            get { return _jsApiTicketResult; }
            set { base.SetContainerProperty(ref _jsApiTicketResult, value, "JsApiTicketResult"); }
        }

        public DateTime JsApiTicketExpireTime
        {
            get { return _jsApiTicketExpireTime; }
            set { base.SetContainerProperty(ref _jsApiTicketExpireTime, value, "JsApiTicketExpireTime"); }
        }

        /// <summary>
        /// 授权信息（请使用TryUpdateAuthorizationInfo()方法进行更新）
        /// </summary>
        public AuthorizationInfo AuthorizationInfo
        {
            get { return _authorizationInfo; }
            set
            {
                base.SetContainerProperty(ref _authorizationInfo, value, "AuthorizationInfo");
                //base.SetContainerProperty(ref _authorizationInfo, value, nameof(FullAuthorizerInfoResult));
            }
        }

        public DateTime AuthorizationInfoExpireTime
        {
            get { return _authorizationInfoExpireTime; }
            set { base.SetContainerProperty(ref _authorizationInfoExpireTime, value, "AuthorizationInfoExpireTime"); }
        }

        /// <summary>
        /// 授权方资料信息
        /// </summary>
        public AuthorizerInfo AuthorizerInfo
        {
            get { return _authorizerInfo; }
            set { base.SetContainerProperty(ref _authorizerInfo, value, "AuthorizerInfo"); }
        }

        //public DateTime AuthorizerInfoExpireTime { get; set; }


        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        internal object Lock = new object();

        private string _authorizerAppId;
        private string _componentAppId;
        private JsApiTicketResult _jsApiTicketResult;
        private DateTime _jsApiTicketExpireTime;
        private AuthorizationInfo _authorizationInfo;
        private DateTime _authorizationInfoExpireTime;
        private AuthorizerInfo _authorizerInfo;
    }

    /// <summary>
    /// 授权方信息（用户的微信公众号）
    /// 包括通用接口JsApiTicket容器，用于自动管理JsApiTicket，如果过期会重新获取
    /// </summary>
    public class AuthorizerContainer : BaseContainer<AuthorizerBag>
    {
        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Ticket，并将清空之前的Ticket，
        /// </summary>
        private static void Register(string componentAppId, string authorizerAppId)
        {
            var componentBag = ComponentContainer.TryGetItem(componentAppId);
            if (componentBag == null)
            {
                throw new WeixinOpenException(string.Format("注册AuthorizerContainer之前，必须先注册对应的ComponentContainer！ComponentAppId：{0},AuthorizerAppId:{1}", componentAppId, authorizerAppId));
            }

            using (FlushCache.CreateInstance())
            {
                Update(authorizerAppId, new AuthorizerBag()
                {
                    AuthorizerAppId = authorizerAppId,
                    ComponentAppId = componentAppId,

                    AuthorizationInfo = new AuthorizationInfo(),
                    AuthorizationInfoExpireTime = DateTime.MinValue,

                    AuthorizerInfo = new AuthorizerInfo(),
                    //AuthorizerInfoExpireTime = DateTime.MinValue,

                    JsApiTicketResult = new JsApiTicketResult(),
                    JsApiTicketExpireTime = DateTime.MinValue,
                });
            }

            //TODO：这里也可以考虑尝试进行授权（会影响速度）
        }

        /// <summary>
        /// 尝试注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <returns></returns>
        private static void TryRegister(string componentAppId, string authorizerAppid)
        {
            if (!CheckRegistered(authorizerAppid))
            {
                Register(componentAppId, authorizerAppid);
            }
        }

        #region 授权信息

        /// <summary>
        /// 获取或更新AuthorizationInfo。
        /// 如果读取refreshToken失败，则返回null。
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static AuthorizationInfo GetAuthorizationInfo(string componentAppId, string authorizerAppid,
            bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizerBag = (AuthorizerBag)ItemCollection[authorizerAppid];
            lock (authorizerBag.Lock)
            {
                //更新Authorization
                if (getNewTicket || authorizerBag.AuthorizationInfoExpireTime <= DateTime.Now)
                {
                    var componentVerifyTicket = ComponentContainer.TryGetComponentVerifyTicket(componentAppId);
                    var componentAccessToken = ComponentContainer.GetComponentAccessToken(componentAppId, componentVerifyTicket);

                    //获取新的AuthorizerAccessToken
                    var refreshToken = ComponentContainer.GetAuthorizerRefreshTokenFunc(authorizerAppid);

                    if (refreshToken == null)
                    {
                        return null;
                    }

                    var refreshResult = RefreshAuthorizerToken(componentAccessToken, componentAppId, authorizerAppid,
                        refreshToken);

                    //更新数据
                    TryUpdateAuthorizationInfo(componentAppId, authorizerAppid,
                        refreshResult.authorizer_access_token, refreshResult.authorizer_refresh_token, refreshResult.expires_in);
                }
            }
            return authorizerBag.AuthorizationInfo;
        }


        /// <summary>
        /// 获取可用AuthorizerAccessToken
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetAuthorizerAccessToken(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            return GetAuthorizerInfoResult(componentAppId, authorizerAppid).authorization_info.authorizer_access_token;
        }

        /// <summary>
        /// 获取可用的GetAuthorizerInfoResult
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        ///// <exception cref="WeixinOpenException">此公众号没有高级权限</exception>
        public static GetAuthorizerInfoResult GetAuthorizerInfoResult(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var authorizerBag = (AuthorizerBag)ItemCollection[authorizerAppid];
            lock (authorizerBag.Lock)
            {

                //更新AuthorizerInfo
                if (getNewTicket || authorizerBag.AuthorizerInfo.user_name == null)
                {
                    var componentVerifyTicket = ComponentContainer.TryGetComponentVerifyTicket(componentAppId);
                    var componentAccessToken = ComponentContainer.GetComponentAccessToken(componentAppId, componentVerifyTicket);

                    //已过期，重新获取
                    var getAuthorizerInfoResult = ComponentApi.GetAuthorizerInfo(componentAccessToken, componentAppId, authorizerAppid);//TODO:如果是过期，可以通过刷新的方式重新获取

                    //AuthorizerInfo
                    authorizerBag.AuthorizerInfo = getAuthorizerInfoResult.authorizer_info;

                    //var componentBag = ComponentContainer.TryGetItem(componentAppId);
                    //if (string.IsNullOrEmpty(authorizerBag.AuthorizerInfoResult.authorization_info.authorizer_access_token))
                    //{
                    //    //账号没有此权限
                    //    throw new WeixinOpenException("此公众号没有高级权限", componentBag);
                    //}
                }
            }
            return authorizerBag.FullAuthorizerInfoResult;
        }

        /// <summary>
        /// 尝试更新AuthorizationInfo（如果没有AccessToken则不更新）
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="authorizationInfo"></param>
        public static void TryUpdateAuthorizationInfo(string componentAppId, string authorizerAppid, AuthorizationInfo authorizationInfo)
        {
            TryRegister(componentAppId, authorizerAppid);

            if (authorizationInfo.expires_in > 0 && authorizationInfo.authorizer_access_token != null)
            {
                var authorizerBag = (AuthorizerBag)ItemCollection[authorizerAppid];

                var refreshTokenChanged = authorizerBag.AuthorizationInfo.authorizer_access_token !=
                                         authorizationInfo.authorizer_access_token
                                           || authorizerBag.AuthorizationInfo.authorizer_refresh_token !=
                                              authorizationInfo.authorizer_refresh_token;

                authorizerBag.AuthorizationInfo = authorizationInfo;
                authorizerBag.AuthorizationInfoExpireTime = DateTime.Now.AddSeconds(authorizationInfo.expires_in);

                //通知变更
                if (refreshTokenChanged)
                {
                    ComponentContainer.AuthorizerTokenRefreshedFunc(authorizerAppid,
                        new RefreshAuthorizerTokenResult(authorizationInfo.authorizer_access_token,
                            authorizationInfo.authorizer_refresh_token, authorizationInfo.expires_in));
                }
            }
        }

        /// <summary>
        /// 尝试更新AuthorizationInfo（如果没有AccessToken则不更新）
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="authorizerAccessToken"></param>
        /// <param name="authorizerRefreshToken"></param>
        /// <param name="expiresIn"></param>
        public static void TryUpdateAuthorizationInfo(string componentAppId, string authorizerAppid, string authorizerAccessToken, string authorizerRefreshToken, int expiresIn)
        {
            TryRegister(componentAppId, authorizerAppid);

            if (expiresIn > 0 && authorizerAccessToken != null)
            {
                var authorizerBag = (AuthorizerBag)ItemCollection[authorizerAppid];

                var refreshTokenChanged = authorizerBag.AuthorizationInfo.authorizer_access_token !=
                                          authorizerAccessToken
                                            || authorizerBag.AuthorizationInfo.authorizer_refresh_token !=
                                               authorizerRefreshToken;

                authorizerBag.AuthorizationInfo.authorizer_access_token = authorizerAccessToken;
                authorizerBag.AuthorizationInfo.authorizer_refresh_token = authorizerRefreshToken;
                authorizerBag.AuthorizationInfo.expires_in = expiresIn;
                authorizerBag.AuthorizationInfoExpireTime = DateTime.Now.AddSeconds(expiresIn);

                //通知变更
                if (refreshTokenChanged)
                {
                    ComponentContainer.AuthorizerTokenRefreshedFunc(authorizerAppid,
                        new RefreshAuthorizerTokenResult(authorizerAccessToken, authorizerRefreshToken, expiresIn));
                }
            }
        }

        /// <summary>
        /// 刷新AuthorizerToken
        ///
        /// </summary>
        /// <param name="componentAccessToken"></param>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static RefreshAuthorizerTokenResult RefreshAuthorizerToken(string componentAccessToken, string componentAppId, string authorizerAppid,
                      string refreshToken)
        {
            var refreshResult = ComponentApi.ApiAuthorizerToken(componentAccessToken, componentAppId, authorizerAppid,
                         refreshToken);
            //更新到存储
            ComponentContainer.AuthorizerTokenRefreshedFunc(authorizerAppid, refreshResult);
            return refreshResult;
        }

        #endregion

        #region JSTicket


        /// <summary>
        /// 使用完整的应用凭证获取Ticket，如果不存在将自动注册
        /// </summary>
        /// <param name="componentAppId"></param>
        /// /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket"></param>
        /// <returns></returns>
        public static string TryGetJsApiTicket(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            return GetJsApiTicket(componentAppId, authorizerAppid);
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static string GetJsApiTicket(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            return GetJsApiTicketResult(componentAppId, authorizerAppid, getNewTicket).ticket;
        }

        /// <summary>
        /// 获取可用Ticket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="authorizerAppid"></param>
        /// <param name="getNewTicket">是否强制重新获取新的Ticket</param>
        /// <returns></returns>
        public static JsApiTicketResult GetJsApiTicketResult(string componentAppId, string authorizerAppid, bool getNewTicket = false)
        {
            TryRegister(componentAppId, authorizerAppid);

            var accessTicketBag = (AuthorizerBag)ItemCollection[authorizerAppid];
            lock (accessTicketBag.Lock)
            {
                if (getNewTicket || accessTicketBag.JsApiTicketExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    var authorizerAccessToken = TryGetAuthorizerAccessToken(componentAppId, authorizerAppid);

                    accessTicketBag.JsApiTicketResult = ComponentApi.GetJsApiTicket(authorizerAccessToken);

                    accessTicketBag.JsApiTicketExpireTime = DateTime.Now.AddSeconds(accessTicketBag.JsApiTicketResult.expires_in);
                }
            }
            return accessTicketBag.JsApiTicketResult;
        }

        #endregion
    }
}
