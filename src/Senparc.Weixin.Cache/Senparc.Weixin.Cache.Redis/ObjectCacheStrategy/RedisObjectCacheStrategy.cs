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

    文件名：RedisObjectCacheStrategy.cs
    文件功能描述：Redis的Object类型容器缓存（Key为String类型）。


    创建标识：Senparc - 20161024

    修改标识：Senparc - 20170205
    修改描述：v0.2.0 重构分布式锁

 ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MessageQueue;
using StackExchange.Redis;

namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    /// Redis的Object类型容器缓存（Key为String类型）
    /// </summary>
    public class RedisObjectCacheStrategy : BaseCacheStrategy, IObjectCacheStrategy
    //where TContainerBag : class, IBaseContainerBag, new()
    {
        /// <summary>
        /// Hash储存的Key和Field集合
        /// </summary>
        protected class HashKeyAndField
        {
            public string Key { get; set; }
            public string Field { get; set; }
        }

        #region 单例

        //静态SearchCache
        public static RedisObjectCacheStrategy Instance
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
            internal static readonly RedisObjectCacheStrategy instance = new RedisObjectCacheStrategy();
        }

        #endregion

        internal ConnectionMultiplexer _client;
        protected IDatabase _cache;

        static RedisObjectCacheStrategy()
        {
            var manager = RedisManager.Manager;
            var cache = manager.GetDatabase();

            var testKey = Guid.NewGuid().ToString();
            var testValue = Guid.NewGuid().ToString();
            cache.StringSet(testKey, testValue);
            var storeValue = cache.StringGet(testKey);
            if (storeValue != testValue)
            {
                throw new Exception("RedisStrategy失效，没有计入缓存！");
            }
            cache.StringSet(testKey, (string)null);
        }

        /// <summary>
        /// Redis 缓存策略
        /// </summary>
        public RedisObjectCacheStrategy()
        {
            _client = RedisManager.Manager;
            _cache = _client.GetDatabase();
        }

        /// <summary>
        /// Redis 缓存策略析构函数，用于 _client 资源回收
        /// </summary>
        ~RedisObjectCacheStrategy()
        {
            _client.Dispose();//释放
        }

        /// <summary>
        /// 获取 Server 对象
        /// </summary>
        /// <returns></returns>
        protected IServer GetServer()
        {
            //https://github.com/StackExchange/StackExchange.Redis/blob/master/Docs/KeysScan.md
            var server = _client.GetServer(_client.GetEndPoints()[0]);
            return server;
        }

        /// <summary>
        /// 获取Hash储存的Key和Field
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isFullKey"></param>
        /// <returns></returns>
        protected HashKeyAndField GetHashKeyAndField(string key, bool isFullKey = false)
        {
            var finalFullKey = base.GetFinalKey(key, isFullKey);
            var index = finalFullKey.LastIndexOf(":");

            if (index == -1)
            {
                index = 0;
            }

            var hashKeyAndField = new HashKeyAndField()
            {
                Key = finalFullKey.Substring(0, index),
                Field = finalFullKey.Substring(index + 1/*排除:号*/, finalFullKey.Length - index - 1)
            };
            return hashKeyAndField;
        }

        #region 实现 IObjectCacheStrategy 接口

        //public string CacheSetKey { get; set; }

        public IContainerCacheStrategy ContainerCacheStrategy
        {
            get { return RedisContainerCacheStrategy.Instance; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isFullKey">是否已经是完整的Key</param>
        /// <returns></returns>
        public bool CheckExisted(string key, bool isFullKey = false)
        {
            //var cacheKey = GetFinalKey(key, isFullKey);
            var hashKeyAndField = this.GetHashKeyAndField(key, isFullKey);

            //return _cache.KeyExists(cacheKey);
            return _cache.HashExists(hashKeyAndField.Key, hashKeyAndField.Field);
        }

        public object Get(string key, bool isFullKey = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            if (!CheckExisted(key, isFullKey))
            {
                return null;
                //InsertToCache(key, new ContainerItemCollection());
            }

            //var cacheKey = GetFinalKey(key, isFullKey);
            var hashKeyAndField = this.GetHashKeyAndField(key, isFullKey);

            //var value = _cache.StringGet(cacheKey);
            var value = _cache.HashGet(hashKeyAndField.Key, hashKeyAndField.Field);
            return value;
        }

        public IDictionary<string, TBag> GetAll<TBag>() where TBag : IBaseContainerBag
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

            var key = ContainerHelper.GetItemCacheKey(typeof(TBag), "");
            key = key.Substring(0, key.Length - 1);//去掉:号
            key = GetFinalKey(key);//获取带SenparcWeixin:DefaultCache:前缀的Key（[DefaultCache]可配置）

            var list = _cache.HashGetAll(key);
            var dic = new Dictionary<string, TBag>();

            foreach (var hashEntry in list)
            {
                var fullKey = key + ":" + hashEntry.Name;//最完整的finalKey（可用于LocalCache），还原完整Key，格式：[命名空间]:[Key]
                dic[fullKey] = StackExchangeRedisExtensions.Deserialize<TBag>(hashEntry.Value);
            }

            return dic;
        }

        /// <summary>
        /// 注意：此方法获取的object为直接储存在缓存中，序列化之后的Value
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> GetAll()
        {
            var keyPrefix = GetFinalKey("");//获取带SenparcWeixin:DefaultCache:前缀的Key（[DefaultCache]可配置）
            var dic = new Dictionary<string, object>();

            var hashKeys = GetServer().Keys(pattern: keyPrefix + "*");
            foreach (var redisKey in hashKeys)
            {
                var list = _cache.HashGetAll(redisKey);

                foreach (var hashEntry in list)
                {
                    var fullKey = redisKey.ToString() + ":" + hashEntry.Name;//最完整的finalKey（可用于LocalCache），还原完整Key，格式：[命名空间]:[Key]
                    dic[fullKey] = hashEntry.Value;
                }
            }
            return dic;
        }


        public long GetCount()
        {
            var keyPattern = GetFinalKey("*");//获取带SenparcWeixin:DefaultCache:前缀的Key（[DefaultCache]         
            var count = GetServer().Keys(pattern: keyPattern).Count();
            return count;
        }

        public void InsertToCache(string key, object value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return;
            }

            //var cacheKey = GetFinalKey(key);
            var hashKeyAndField = this.GetHashKeyAndField(key);


            //if (value is IDictionary)
            //{
            //    //Dictionary类型
            //}

            //_cache.StringSet(cacheKey, value.Serialize());
            //_cache.HashSet(hashKeyAndField.Key, hashKeyAndField.Field, value.Serialize());
            _cache.HashSet(hashKeyAndField.Key, hashKeyAndField.Field, StackExchangeRedisExtensions.Serialize(value));
#if DEBUG
            var value1 = _cache.HashGet(hashKeyAndField.Key, hashKeyAndField.Field);//正常情况下可以得到 //_cache.GetValue(cacheKey);
#endif
        }

        public void RemoveFromCache(string key, bool isFullKey = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            //var cacheKey = GetFinalKey(key, isFullKey);
            var hashKeyAndField = this.GetHashKeyAndField(key);

            SenparcMessageQueue.OperateQueue();//延迟缓存立即生效
            //_cache.KeyDelete(cacheKey);//删除键
            _cache.HashDelete(hashKeyAndField.Key, hashKeyAndField.Field);//删除项
        }

        public void Update(string key, object value, bool isFullKey = false)
        {
            //var cacheKey = GetFinalKey(key, isFullKey);
            var hashKeyAndField = this.GetHashKeyAndField(key);

            //value.Key = cacheKey;//储存最终的键

            //_cache.StringSet(cacheKey, value.Serialize());

            //_cache.HashSet(hashKeyAndField.Key, hashKeyAndField.Field, value.Serialize());
            _cache.HashSet(hashKeyAndField.Key, hashKeyAndField.Field, StackExchangeRedisExtensions.Serialize(value));
        }

        #endregion

        public override ICacheLock BeginCacheLock(string resourceName, string key, int retryCount = 0, TimeSpan retryDelay = new TimeSpan())
        {
            return new RedisCacheLock(this, resourceName, key, retryCount, retryDelay);
        }

    }
}
