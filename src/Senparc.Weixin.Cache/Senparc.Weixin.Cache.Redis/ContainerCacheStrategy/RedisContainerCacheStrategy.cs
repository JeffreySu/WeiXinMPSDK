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

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：RedisContainerCacheStrategy.cs
    文件功能描述：Redis 容器缓存策略。


    创建标识：Senparc - 20160308

    修改标识：Senparc - 20160808
    修改描述：v0.2.0 删除 ItemCollection 属性，直接使用ContainerBag加入到缓存
    
    修改标识：Senparc - 20160812
    修改描述：v0.2.1  解决Container无法注册的问题

    修改标识：Senparc - 20170205
    修改描述：v0.2.0 重构分布式锁

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redlock.CSharp;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.Redis;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.MessageQueue;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Helpers;
using StackExchange.Redis;


namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    /// Redis容器缓存策略
    /// </summary>
    public sealed class RedisContainerCacheStrategy : BaseContainerCacheStrategy
    {
        #region IDomainExtensionCacheStrategy 成员
        public override ICacheStrategyDomain CacheStrategyDomain { get { return ContainerCacheStrategyDomain.Instance; } }

        #endregion

        #region 单例

        /// <summary>
        /// Redis 缓存策略
        /// </summary>
        RedisContainerCacheStrategy() /*: base()*/
        {
            //base.ChildNamespace = "WeixinContainer";

            //使用底层缓存策略
            BaseCacheStrategy = () => RedisObjectCacheStrategy.Instance;

            //向底层缓存注册当前缓存策略
            base.RegisterCacheStrategyDomain(this);
        }


        //静态SearchCache
        public static RedisContainerCacheStrategy Instance
        {
            get
            {
                return Nested.instance;//返回Nested类中的静态成员instance
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            //将instance设为一个初始化的BaseCacheStrategy新实例
            internal static readonly RedisContainerCacheStrategy instance = new RedisContainerCacheStrategy();
        }

        #endregion

        static RedisContainerCacheStrategy()
        {
        }

        /// <summary>
        /// Redis 缓存策略析构函数，用于 _client 资源回收
        /// </summary>
        ~RedisContainerCacheStrategy()
        {
        }

        #region 实现 IContainerCacheStrategy 接口

        /// <summary>
        ///  获取所有 Bag 对象
        /// </summary>
        /// <typeparam name="TBag"></typeparam>
        /// <returns></returns>
        public override IDictionary<string, TBag> GetAll<TBag>()
        {
            #region 旧方法（没有使用Hash之前）

            //var itemCacheKey = ContainerHelper.GetItemCacheKey(typeof(TBag), "*");   
            ////var keyPattern = string.Format("*{0}", itemCacheKey);
            //var keyPattern = GetFinalKey(itemCacheKey);

            //var keys = GetServer().Keys(pattern: keyPattern);
            //var dic = new Dictionary<string, TBag>();
            //foreach (var redisKey in keys)
            //{
            //    try
            //    {
            //        var bag = Get(redisKey, true);
            //        dic[redisKey] = (TBag)bag;
            //    }
            //    catch (Exception)
            //    {

            //    }

            //}

            #endregion

            var baseCacheStrategy = BaseCacheStrategy();
            var key = ContainerHelper.GetItemCacheKey(typeof(TBag), "");
            key = key.Substring(0, key.Length - 1);//去掉:号
            key = baseCacheStrategy.GetFinalKey(key);//获取带SenparcWeixin:DefaultCache:前缀的Key（[DefaultCache]可配置）
            var list = (baseCacheStrategy as RedisObjectCacheStrategy).GetAllByPrefix<TBag>(key);

            //var list = (baseCacheStrategy as RedisObjectCacheStrategy).GetAll(key);
            var dic = new Dictionary<string, TBag>();

            foreach (var item in list)
            {
                var fullKey = key + ":" + item.Key;//最完整的finalKey（可用于LocalCache），还原完整Key，格式：[命名空间]:[Key]
                //dic[fullKey] = StackExchangeRedisExtensions.Deserialize<TBag>(hashEntry.Value);
                dic[fullKey] = item;
            }

            return dic;
        }


        #region 异步方法

        /// <summary>
        ///  【异步方法】获取所有 Bag 对象
        /// </summary>
        /// <typeparam name="TBag"></typeparam>
        /// <returns></returns>
        public override async Task<IDictionary<string, TBag>> GetAllAsync<TBag>()
        {
            var baseCacheStrategy = BaseCacheStrategy();
            var key = ContainerHelper.GetItemCacheKey(typeof(TBag), "");
            key = key.Substring(0, key.Length - 1);//去掉:号
            key = baseCacheStrategy.GetFinalKey(key);//获取带SenparcWeixin:DefaultCache:前缀的Key（[DefaultCache]可配置）
            var list =  await (baseCacheStrategy as RedisObjectCacheStrategy).GetAllByPrefixAsync<TBag>(key).ConfigureAwait(false);

            //var list = (baseCacheStrategy as RedisObjectCacheStrategy).GetAll(key);
            var dic = new Dictionary<string, TBag>();

            foreach (var item in list)
            {
                var fullKey = key + ":" + item.Key;//最完整的finalKey（可用于LocalCache），还原完整Key，格式：[命名空间]:[Key]
                //dic[fullKey] = StackExchangeRedisExtensions.Deserialize<TBag>(hashEntry.Value);
                dic[fullKey] = item;
            }

            return dic;
        }
      
        #endregion

        #endregion

    }
}
