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
    
    文件名：StreamUtility.cs
    文件功能描述：微信对象公共类
    
    
    创建标识：Senparc - 20150703
    
----------------------------------------------------------------*/



using System;

namespace Senparc.Weixin.Utilities.WeixinUtility
{
    /// <summary>
    /// 微信 API 工具类
    /// </summary>
    public static class ApiUtility
    {
        /// <summary>
        /// 判断accessTokenOrAppId参数是否是AppId
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static bool IsAppId(string accessTokenOrAppId)
        {
            /* 
             * 公众号AccessToken，如：ga0wJ5ZmdB1Ef1gMMxmps6Uz1a9TXoutQtRqgYTbIqHfTm4Ssfoj0DjMLp1_KkG7FkaqS7m7f9rrYbqBQMBizRBQjHFG5ZIov8Wb0FBnHDq5fGpCu0S2H2j2aM8c6KDqGGEiAIAJJH
             * 企业号CorpId(wx7618c0a5d9358622) + Secret=Key,如：wx7618c0a5d9358622044ZI5s6-QB0UiOscm4md410pZ460pQUmxO9hRRMd09kRaJ1iSqhPfmg3-aBFF7q
             */
            return accessTokenOrAppId != null && accessTokenOrAppId.Length <= 100/*wxc3c90837b0e76080*/
            ;
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
