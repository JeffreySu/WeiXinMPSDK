#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
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
            return Config.DefaultAppIdCheckFunc(accessTokenOrAppId, platFormType);
        }

        /// <summary>
        /// 获取过期时间
        /// </summary>
        /// <param name="expireInSeconds">有效时间（秒）</param>
        /// <returns></returns>
        public static DateTime GetExpireTime(int expireInSeconds)
        {
            return DateTime.Now.Add(GetExpiryTimeSpan(expireInSeconds));//提前x分钟重新获取
        }

        /// <summary>
        /// 获取过期 TimeSpan
        /// </summary>
        /// <param name="expireInSeconds">有效时间（秒）</param>
        /// <returns></returns>
        public static TimeSpan GetExpiryTimeSpan(int expireInSeconds)
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
            return TimeSpan.FromSeconds(expireInSeconds);
        }
    }
}
