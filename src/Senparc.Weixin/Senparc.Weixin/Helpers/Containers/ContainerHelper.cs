using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Helpers
{
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
