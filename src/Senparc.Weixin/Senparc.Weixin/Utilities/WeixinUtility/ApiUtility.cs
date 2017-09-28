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
    
    修改标识：Senparc - 20170730
    修改描述：v4.13.4 修改企业微信APPId判断标准错误（使用新规则）

    修改标识：Senparc - 20170926
    修改描述：v4.16.4 在2017年9月26日企业号长度变化，对应修改IsAppId()方法逻辑

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
        /// 判断accessTokenOrAppId参数是否是AppId（或对应企业微信的AppKey）
        /// </summary>
        /// <param name="accessTokenOrAppId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        public static bool IsAppId(string accessTokenOrAppId, PlatformType platFormType)
        {
            if (platFormType == PlatformType.QY || platFormType == PlatformType.Work)
            {
                /*
                 * 企业号（企业微信）AppKey（Length=84）：wx7618c00000000222@044ZI5s6-ACxpAuOcm4md410pZ460pQUmxO9hIoMd09kRaJ1iSqhPfmg3-aBFF7q
                 * 企业号（企业微信）AccessToken（length=300）：MGelzm_P0N-41qH3PwHsNxp70rdVuB0SMEN7dE4E8eKpb0OpNQSp8jPUfgwIL_P9jcz-qGIOLbLEy3d8XQEJFfZtOLgTJqyg0rJbj6WyQJxdRVjbLnHr0-pg7oN9dD1NFI7-T7GLuJER3Pun-5cSiSmZgAegTDhXKZC8XfgjQAPPYLjZl7StBnO7dVcZStdyivZ92zq4PrDdNif9fa2p9lPSLqkur2PpDB9P7MsR8PDJWsKghEcmjB41OXohHGnqPWd5lUZaV1Y8p35BVz6PqjF-90UgAjI9IohVKVRClks
                 */

                //return accessTokenOrAppId != null && accessTokenOrAppId.Length < 256;

                /*
                 * 2017年9月26日开始，AccessToken长度有变化（长度有300、215、191等）
                 * AccessToken（Length=215）：_0evr6HbAnWCUfn1tRpbVY2uV63fDOfT-fUnpQcq6egl8bYFp3Xq45ebImXn5Aj1_nz_mFCUz9sDnoEkfy-jyXqJEc4Hty0BAo2VQTB8ogx7qkL2w1p0H2E1fKWwJrQ1285V0XhEQ0pcHMLwy9RbHuD4sHdAJ5ZkXGchNQ1eHsmseoBxucKvyAnEq9psJVLMjkU4G3ZRa0NoTBSy0g6ujg
                 */

                return accessTokenOrAppId != null && accessTokenOrAppId.Contains("@");
            }
            else
            {
                /*
                 * 公众号AppId：wxe273c3a02e09ff8c
                 * 公众号AccessToken：ga0wJ5ZmdB1Ef1gMMxmps6Uz1a9TXoutQtRqgYTbIqHfTm4Ssfoj0DjMLp1_KkG7FkaqS7m7f9rrYbqBQMBizRBQjHFG5ZIov8Wb0FBnHDq5fGpCu0S2H2j2aM8c6KDqGGEiAIAJJH
                 */
                return accessTokenOrAppId != null && accessTokenOrAppId.Length <= 32 /*wxc3c90837b0e76080*/
                ;
            }

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
