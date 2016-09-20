/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：RedisContainerCacheStrategy.cs
    文件功能描述：Redis 容器缓存策略。


    创建标识：Senparc - 20160308

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Senparc.Weixin.Containers;
using Senparc.Weixin.MessageQueue;
using StackExchange.Redis;


namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    /// Redis容器缓存策略
    /// </summary>
    public sealed class RedisContainerCacheStrategy : IContainerCacheStragegy
    {
        private ConnectionMultiplexer _client;
        private IDatabase _cache;

        #region 单例

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
        public RedisContainerCacheStrategy()
        {
            _client = RedisManager.Manager;
            _cache = _client.GetDatabase();
        }

        /// <summary>
        /// Redis 缓存策略析构函数，用于 _client 资源回收
        /// </summary>
        ~RedisContainerCacheStrategy()
        {
            _client.Dispose();//释放
        }

        private string GetFinalKey(string key)
        {
            //return String.Format("{0}:{1}", CacheSetKey, key);
            return String.Format("{0}{1}", CacheSetKey, key);//这里 CacheSetKey 暂时留空，保持key原貌
        }

        /// <summary>
        /// 获取 Server 对象
        /// </summary>
        /// <returns></returns>
        private IServer GetServer()
        {
            //https://github.com/StackExchange/StackExchange.Redis/blob/master/Docs/KeysScan.md
            var server = _client.GetServer(_client.GetEndPoints()[0]);
            return server;
        }


        #region 实现 IContainerCacheStragegy 接口

        public string CacheSetKey { get; set; }

        public bool CheckExisted(string key)
        {
            var cacheKey = GetFinalKey(key);
            return _cache.KeyExists(cacheKey);
        }

        public IContainerItemCollection Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            if (!CheckExisted(key))
            {
                return null;
                //InsertToCache(key, new ContainerItemCollection());
            }

            var cacheKey = GetFinalKey(key);

            var value = _cache.StringGet(cacheKey);
            return StackExchangeRedisExtensions.Deserialize<IContainerItemCollection>(value);
        }

        public IDictionary<string, IContainerItemCollection> GetAll()
        {
            var keys = GetServer().Keys();
            var dic = new Dictionary<string, IContainerItemCollection>();
            foreach (var redisKey in keys)
            {
                dic[redisKey] = Get(redisKey);
            }
            return dic;
        }

        public long GetCount()
        {
            var count = GetServer().Keys().Count();
            return count;
        }

        public void InsertToCache(string key, IContainerItemCollection value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return;
            }

            var cacheKey = GetFinalKey(key);

            //if (value is IDictionary)
            //{
            //    //Dictionary类型
            //}

            _cache.StringSet(cacheKey, value.Serialize());

#if DEBUG
            var value1 = _cache.StringGet(cacheKey);//正常情况下可以得到 //_cache.GetValue(cacheKey);
#endif
        }

        public void RemoveFromCache(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            SenparcMessageQueue.OperateQueue();//延迟缓存立即生效
            _cache.KeyDelete(key);//删除键
        }

        public void Update(string key, IContainerItemCollection value)
        {
            var cacheKey = GetFinalKey(key);
            _cache.StringSet(cacheKey, value.Serialize());
        }

        public void UpdateContainerBag(string key, IBaseContainerBag containerBag)
        {
            if (this.CheckExisted(key))
            {
                var containerItemCollection = Get(key);
                containerItemCollection[containerBag.Key] = containerBag;

                Update(key, containerItemCollection);
            }
        }

        #endregion
    }
}
