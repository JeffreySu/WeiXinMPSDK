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

    文件名：RedisHashSetContainerCacheStrategy.cs
    文件功能描述：Redis HashSet 格式容器缓存策略。


    创建标识：Senparc - 20190404


    修改标识：Senparc - 20190418
    修改描述：v2.5.5 提供  GetAllAsync() 异步方法


----------------------------------------------------------------*/

using System.Collections.Generic;
using System.Threading.Tasks;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.Redis;
using Senparc.Weixin.Helpers;


namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    /// Redis容器缓存策略
    /// </summary>
    public sealed class RedisHashSetContainerCacheStrategy : BaseContainerCacheStrategy
    {
        #region IDomainExtensionCacheStrategy 成员
        public override ICacheStrategyDomain CacheStrategyDomain { get { return ContainerCacheStrategyDomain.Instance; } }

        #endregion

        #region 单例

        /// <summary>
        /// Redis 缓存策略
        /// </summary>
        RedisHashSetContainerCacheStrategy() /*: base()*/
        {
            //base.ChildNamespace = "WeixinContainer";

            //使用底层缓存策略
            BaseCacheStrategy = () => RedisHashSetObjectCacheStrategy.Instance;

            //向底层缓存注册当前缓存策略
            base.RegisterCacheStrategyDomain(this);
        }


        //静态SearchCache
        public static RedisHashSetContainerCacheStrategy Instance
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
            internal static readonly RedisHashSetContainerCacheStrategy instance = new RedisHashSetContainerCacheStrategy();
        }

        #endregion

        static RedisHashSetContainerCacheStrategy()
        {
        }

        /// <summary>
        /// Redis 缓存策略析构函数，用于 _client 资源回收
        /// </summary>
        ~RedisHashSetContainerCacheStrategy()
        {
        }

        #region 实现 IContainerCacheStrategy 接口


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

            var baseCacheStrategy = BaseCacheStrategy() as RedisHashSetObjectCacheStrategy;
            var key = ContainerHelper.GetItemCacheKey(typeof(TBag), "");
            key = key.Substring(0, key.Length - 1);//去掉:号
            key = baseCacheStrategy.GetFinalKey(key);//获取带 SenparcWeixin:DefaultCache: 前缀的Key（[DefaultCache]可配置）

            var allHashEntry = baseCacheStrategy.HashGetAll(key);
            //var list = (baseCacheStrategy as RedisObjectCacheStrategy).GetAll(key);
            var dic = new Dictionary<string, TBag>();

            foreach (var item in allHashEntry)
            {
                var fullKey = key + ":" + item.Name;//最完整的finalKey（可用于LocalCache），还原完整Key，格式：[命名空间]:[Key]
                //dic[fullKey] = StackExchangeRedisExtensions.Deserialize<TBag>(hashEntry.Value);
                dic[fullKey] = item.Value.ToString().DeserializeFromCache<TBag>();
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
            var baseCacheStrategy = BaseCacheStrategy() as RedisHashSetObjectCacheStrategy;
            var key = ContainerHelper.GetItemCacheKey(typeof(TBag), "");
            key = key.Substring(0, key.Length - 1);//去掉:号
            key = baseCacheStrategy.GetFinalKey(key);//获取带 SenparcWeixin:DefaultCache: 前缀的Key（[DefaultCache]可配置）

            var allHashEntry = await baseCacheStrategy.HashGetAllAsync(key).ConfigureAwait(false);
            //var list = (baseCacheStrategy as RedisObjectCacheStrategy).GetAll(key);
            var dic = new Dictionary<string, TBag>();

            foreach (var item in allHashEntry)
            {
                var fullKey = key + ":" + item.Name;//最完整的finalKey（可用于LocalCache），还原完整Key，格式：[命名空间]:[Key]
                //dic[fullKey] = StackExchangeRedisExtensions.Deserialize<TBag>(hashEntry.Value);
                dic[fullKey] = item.Value.ToString().DeserializeFromCache<TBag>();
            }

            return dic;
        }

        #endregion

        #endregion

    }
}
