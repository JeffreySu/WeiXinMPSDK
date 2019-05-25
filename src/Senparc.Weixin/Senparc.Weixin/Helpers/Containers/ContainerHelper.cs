#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// 容器帮助类
    /// </summary>
    public class ContainerHelper
    {
        /// <summary>
        /// 获取缓存Key（命名空间，形如：Container:Senparc.Weixin.MP.Containers.AccessTokenBag）
        /// </summary>
        /// <returns></returns>
        public static string GetCacheKeyNamespace(Type bagType)
        {
            return string.Format("Container:{0}", bagType);
        }

        /// <summary>
        /// 获取ContainerBag缓存Key，包含命名空间（形如：Container:Senparc.Weixin.MP.Containers.AccessTokenBag:wx669ef95216eef885）
        /// </summary>
        /// <param name="shortKey">最简短的Key，比如AppId，不需要考虑容器前缀</param>
        /// <returns></returns>
        public static string GetItemCacheKey(Type bagType, string shortKey)
        {
            return string.Format("{0}:{1}", GetCacheKeyNamespace(bagType), shortKey);
        }

        /// <summary>
        /// 获取ContainerBag缓存Key，包含命名空间（形如：Container:Senparc.Weixin.MP.Containers.AccessTokenBag:wx669ef95216eef885）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bag"></param>
        /// <returns></returns>
        public static string GetItemCacheKey(IBaseContainerBag bag)
        {
            return GetItemCacheKey(bag, bag.Key);
        }

        /// <summary>
        /// 获取ContainerBag缓存Key，包含命名空间（形如：Container:Senparc.Weixin.MP.Containers.AccessTokenBag:wx669ef95216eef885）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bag"></param>
        /// <param name="shortKey"></param>
        /// <returns></returns>
        public static string GetItemCacheKey(IBaseContainerBag bag, string shortKey)// where T : IBaseContainerBag
        {
            return GetItemCacheKey(bag.GetType(), shortKey);
        }
    }
}
