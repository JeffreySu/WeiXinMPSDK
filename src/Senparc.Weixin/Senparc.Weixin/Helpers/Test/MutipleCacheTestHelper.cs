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
    
    文件名：MutipleCacheTestHelper.cs
    文件功能描述：多种缓存测试帮助类
    
    
    创建标识：Senparc - 20170702

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Senparc.Weixin.Cache;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// 多种缓存测试帮助类
    /// </summary>
    public class MutipleCacheTestHelper
    {
        /// <summary>
        /// 测试多种缓存
        /// </summary>
        public static void RunMutipleCache(Action action)
        {
            RunMutipleCache(action, CacheType.Local);
        }

        /// <summary>
        /// 遍历使用多种缓存测试同一个过程（委托），确保不同的缓存策略行为一致
        /// </summary>
        public static void RunMutipleCache(Action action, params CacheType[] cacheTypes)
        {
            List<IObjectCacheStrategy> cacheStrategies = new List<IObjectCacheStrategy>();

            foreach (var cacheType in cacheTypes)
            {
                var assabmleName = cacheType == CacheType.Local
                    ? "Senparc.Weixin"
                    : "Senparc.Weixin.Cache." + cacheType.ToString();

                var nameSpace = cacheType == CacheType.Local
                                    ? "Senparc.Weixin.Cache"
                                    : "Senparc.Weixin.Cache." + cacheType.ToString();

                var className = cacheType.ToString() + "ObjectCacheStrategy";

                var cacheInstance = ReflectionHelper.CreateInstance<IObjectCacheStrategy>(assabmleName, nameSpace,
                    className);

                cacheStrategies.Add(cacheInstance);

                //switch (cacheType)
                //{
                //    case CacheType.Local:
                //        cacheStrategies.Add(LocalObjectCacheStrategy.Instance);
                //        break;
                //    case CacheType.Redis:
                //        cacheStrategies.Add(RedisObjectCacheStrategy.Instance);
                //        break;
                //    case CacheType.Memcached:
                //        cacheStrategies.Add(MemcachedObjectCacheStrategy.Instance);
                //        break;
                //}
            }

            foreach (var objectCacheStrategy in cacheStrategies)
            {
                //原始缓存策越
                var originalCache = CacheStrategyFactory.GetObjectCacheStrategyInstance();

                Console.WriteLine("== 使用缓存策略：" + objectCacheStrategy.GetType().Name + " 开始 == ");

                //使用当前缓存策略
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => objectCacheStrategy);

                try
                {
                    action();//执行
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Console.WriteLine("== 使用缓存策略：" + objectCacheStrategy.GetType().Name + " 结束 == \r\n");

                //还原缓存策略
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => originalCache);
            }
        }
    }
}
