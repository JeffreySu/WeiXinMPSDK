/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：StreamUtility.cs
    文件功能描述：微信对象公共类
    
    
    创建标识：Senparc - 20150703
    
----------------------------------------------------------------*/

using System;

namespace Senparc.Weixin.Utilities.WeixinUtility
{
    public static class ApiUtility
    {
        /// <summary>
        /// 判断accessTokenOrAppId参数是否是AppId
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <returns></returns>
        public static bool IsAppId(string accessTokenOrAppId)
        {
            return accessTokenOrAppId != null && accessTokenOrAppId.Length <= 18/*wxc3c90837b0e76080*/;
        }

        /// <summary>
        /// 获取过期时间
        /// </summary>
        /// <param name="expireInSeconds">有效时间（秒）</param>
        /// <returns></returns>
        public static DateTime GetExpireTime(int expireInSeconds)
        {
            if (expireInSeconds > 3600)
            {
                expireInSeconds -= 600;//提前10分钟过期
            }
            else if (expireInSeconds > 1800)
            {
                expireInSeconds -= 300;//提前5分钟过期
            }
            else if (expireInSeconds > 300)
            {
                expireInSeconds -= 30;//提前1分钟过期
            }

            return DateTime.Now.AddSeconds(expireInSeconds);//提前2分钟重新获取
        }
    }
}
