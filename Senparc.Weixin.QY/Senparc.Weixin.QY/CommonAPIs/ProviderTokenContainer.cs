/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：ProviderTokenContainer.cs
    文件功能描述：通用接口ProviderToken容器，用于自动管理ProviderToken，如果过期会重新获取
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.QY.Entities;

namespace Senparc.Weixin.QY.CommonAPIs
{
    class ProviderTokenBag
    {
        public string CorpId { get; set; }
        public string CorpSecret { get; set; }
        public DateTime ExpireTime { get; set; }
        public ProviderTokenResult ProviderTokenResult { get; set; }
        /// <summary>
        /// 只针对这个CorpId的锁
        /// </summary>
        public object Lock = new object();
    }

    /// <summary>
    /// 通用接口ProviderToken容器，用于自动管理ProviderToken，如果过期会重新获取
    /// </summary>
    public class ProviderTokenContainer
    {
        static Dictionary<string, ProviderTokenBag> ProviderTokenCollection =
           new Dictionary<string, ProviderTokenBag>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 注册应用凭证信息，此操作只是注册，不会马上获取Token，并将清空之前的Token，
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        public static void Register(string corpId, string corpSecret)
        {
            ProviderTokenCollection[corpId] = new ProviderTokenBag()
            {
                CorpId = corpId,
                CorpSecret = corpSecret,
                ExpireTime = DateTime.MinValue,
                ProviderTokenResult = new ProviderTokenResult()
            };
        }

        /// <summary>
        /// 使用完整的应用凭证获取Token，如果不存在将自动注册
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="corpSecret"></param>
        /// <param name="getNewToken"></param>
        /// <returns></returns>
        public static string TryGetToken(string corpId, string corpSecret, bool getNewToken = false)
        {
            if (!CheckRegistered(corpId) || getNewToken)
            {
                Register(corpId, corpSecret);
            }
            return GetToken(corpId);
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static string GetToken(string corpId, bool getNewToken = false)
        {
            return GetTokenResult(corpId, getNewToken).provider_access_token;
        }

        /// <summary>
        /// 获取可用Token
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="getNewToken">是否强制重新获取新的Token</param>
        /// <returns></returns>
        public static ProviderTokenResult GetTokenResult(string corpId, bool getNewToken = false)
        {
            if (!ProviderTokenCollection.ContainsKey(corpId))
            {
                throw new WeixinException("此CorpId尚未注册，请先使用ProviderTokenContainer.Register完成注册（全局执行一次即可）！");
            }

            var accessTokenBag = ProviderTokenCollection[corpId];
            lock (accessTokenBag.Lock)
            {
                if (getNewToken || accessTokenBag.ExpireTime <= DateTime.Now)
                {
                    //已过期，重新获取
                    accessTokenBag.ProviderTokenResult = CommonApi.GetProviderToken(accessTokenBag.CorpId,
                        accessTokenBag.CorpSecret);
                    accessTokenBag.ExpireTime = DateTime.Now.AddSeconds(7200);
                }
            }
            return accessTokenBag.ProviderTokenResult;
        }

        /// <summary>
        /// 检查是否已经注册
        /// </summary>
        /// <param name="corpId"></param>
        /// <returns></returns>
        public static bool CheckRegistered(string corpId)
        {
            return ProviderTokenCollection.ContainsKey(corpId);
        }
    }
}
