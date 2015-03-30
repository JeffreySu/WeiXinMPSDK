/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：AccessTokenContainer.cs
    文件功能描述：通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.CommonAPIs
{
    class OAuthAccessTokenBag
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public DateTime ExpireTime { get; set; }
        public OAuthAccessTokenResult OAuthAccessTokenResult { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// 只针对这个AppId的锁
        /// </summary>
        public object Lock = new object();
    }

    /// <summary>
    /// 通用接口AccessToken容器，用于自动管理AccessToken，如果过期会重新获取
    /// </summary>
    public class OAuthAccessTokenContainer
    {
        static Dictionary<string, OAuthAccessTokenBag> OAuthAccessTokenCollection =
           new Dictionary<string, OAuthAccessTokenBag>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="code"></param>
        public static void Register(string appId, string appSecret, string code)
        {
            OAuthAccessTokenCollection[appId] = new OAuthAccessTokenBag()
            {
                AppId = appId,
                AppSecret = appSecret,
                ExpireTime = DateTime.MinValue,
                Code = code,
                OAuthAccessTokenResult = new OAuthAccessTokenResult()
            };
        }

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="appSecret"></param>
        /// <param name="code"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static OAuthAccessTokenResult TryGetOAuthToken(string appId, string appSecret, string code, bool getNewToken = false)
        {
            if (!CheckRegistered(appId) || getNewToken)
            {
                Register(appId, appSecret, code);
            }
            return GetOAuthToken(appId, code);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static OAuthAccessTokenResult GetOAuthToken(string appId,string code, bool getNewToken = false)
        {
            return GetOAuthTokenResult(appId, code, getNewToken);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="code"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static OAuthAccessTokenResult GetOAuthTokenResult(string appId, string code, bool getNewToken = false)
        {
            if (!OAuthAccessTokenCollection.ContainsKey(appId))
            {
                throw new WeixinException("此appId尚未注册，请先使用AccessTokenContainer.Register完成注册（全局执行一次即可）！");
            }

            var oAuthAccessTokenBag = OAuthAccessTokenCollection[appId];
            lock (oAuthAccessTokenBag.Lock)
            {
                if (getNewToken || oAuthAccessTokenBag.ExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    oAuthAccessTokenBag.OAuthAccessTokenResult = OAuthApi.GetAccessToken(oAuthAccessTokenBag.AppId, oAuthAccessTokenBag.AppSecret,code);
                    oAuthAccessTokenBag.ExpireTime = DateTime.Now.AddSeconds(oAuthAccessTokenBag.OAuthAccessTokenResult.expires_in);
                }
            }
            return oAuthAccessTokenBag.OAuthAccessTokenResult;
        }

        /// <summary>
        /// 检查是否已经注册
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public static bool CheckRegistered(string appId)
        {
            return OAuthAccessTokenCollection.ContainsKey(appId);
        }
    }
}
