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
    
    文件名：UserHelper.cs
    文件功能描述：用户帮助类
    
    创建标识：Senparc - 20210930

----------------------------------------------------------------*/

using Senparc.CO2NET.Helpers;
using Senparc.Weixin.Entities;
using System;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// 用户帮助类
    /// </summary>
    public static class UserHelper
    {
        /// <summary>
        /// 正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像）
        /// </summary>
        public enum HeadImageSize
        {
            /// <summary>
            /// 640*640正方形头像
            /// </summary>
            x0 = 0,
            /// <summary>
            /// 46*46正方形头像
            /// </summary>
            x46 = 46,
            /// <summary>
            /// 64*64
            /// </summary>
            x64 = 64,
            /// <summary>
            /// 96*96正方形头像
            /// </summary>
            x96 = 96,
            /// <summary>
            /// 132*132正方形头像
            /// </summary>
            x132 = 132,
        }

        /// <summary>
        /// 获取指定大小的用户头像网址
        /// </summary>
        /// <param name="userInfo">IUserInfo，包括用户头像信息</param>
        /// <param name="headImageSize">代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像）</param>
        /// <returns></returns>
        public static string GetHeadImageUrl(this IUserInfo userInfo, HeadImageSize headImageSize = HeadImageSize.x0)
        {
            return GetHeadImageUrl(userInfo.headimgurl, headImageSize);
        }

        /// <summary>
        /// 获取指定大小的用户头像网址
        /// </summary>
        /// <param name="headImgUrl">用户头像</param>
        /// <param name="headImageSize">代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像）</param>
        /// <returns></returns>
        public static string GetHeadImageUrl(string headImgUrl, HeadImageSize headImageSize = HeadImageSize.x0)
        {
            if (headImgUrl == null)
                return null;

            var tail = "/" + ((int)headImageSize).ToString("d");
            if (headImgUrl.EndsWith(tail))
                return headImgUrl;

            var slashIndex = headImgUrl.LastIndexOf('/');
            if (slashIndex < 0)
                return headImgUrl;

            return headImgUrl.Substring(0, slashIndex) + tail;
        }


        /// <summary>
        /// 获取关注时间
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static DateTimeOffset GetSubscribeTime(this IUserSubscribe userInfo)
        {
            return DateTimeHelper.GetDateTimeFromXml(userInfo.subscribe_time);
        }
    }
}
