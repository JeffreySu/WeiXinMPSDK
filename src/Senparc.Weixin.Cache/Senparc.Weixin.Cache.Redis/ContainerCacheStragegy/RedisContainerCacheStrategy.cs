/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：RedisContainerCacheStrategy.cs
    文件功能描述：Redis 容器缓存策略。


    创建标识：Senparc - 20160308

    修改标识：Senparc - 20160808
    修改描述：v0.2.0 删除 ItemCollection 属性，直接使用ContainerBag加入到缓存

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Redlock.CSharp;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MessageQueue;
using StackExchange.Redis;


namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    /// Redis容器缓存策略
    /// </summary>
    public sealed class RedisContainerCacheStrategy : IContainerCacheStragegy
    {
        internal ConnectionMultiplexer _client;
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


        public string GetFinalKey(string key, bool isFullKey = false)
        {
            return isFullKey ? key : String.Format("{0}:{1}", "SenparcWeixinContainer", key);
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

        //public string CacheSetKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="isFullKey">是否已经是完整的Key</param>
        /// <returns></returns>
        public bool CheckExisted(string key, bool isFullKey = false)
        {
            var cacheKey = isFullKey ? key : GetFinalKey(key);
            return _cache.KeyExists(cacheKey);
        }

        public IBaseContainerBag Get(string key, bool isFullKey = false)
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

            var cacheKey = GetFinalKey(key, isFullKey);

            var value = _cache.StringGet(cacheKey);
            return StackExchangeRedisExtensions.Deserialize<IBaseContainerBag>(value);
        }

        public IDictionary<string, TBag> GetAll<TBag>() where TBag : IBaseContainerBag
        {
            var itemCacheKey = ContainerHelper.GetItemCacheKey(typeof(TBag),"*");

            //var keyPattern = string.Format("*{0}", itemCacheKey);
            var keyPattern = GetFinalKey(itemCacheKey);

            var keys = GetServer().Keys(pattern: keyPattern);
            var dic = new Dictionary<string, TBag>();
            foreach (var redisKey in keys)
            {
                try
                {
                    var bag = Get(redisKey, true);
                    dic[redisKey] = (TBag)bag;
                }
                catch (Exception)
                {
                    
                }
               
            }
            return dic;
        }

        public IDictionary<string, IBaseContainerBag> GetAll()
        {
            //尽量不要用此方法！
            return GetAll<IBaseContainerBag>();
        }


        public long GetCount()
        {
            var count = GetServer().Keys().Count();
            return count;
        }

        public void InsertToCache(string key, IBaseContainerBag value)
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

            var cacheKey = GetFinalKey(key);


            SenparcMessageQueue.OperateQueue();//延迟缓存立即生效
            _cache.KeyDelete(cacheKey);//删除键
        }

        public void Update(string key, IBaseContainerBag value)
        {
            var cacheKey = GetFinalKey(key);
            //value.Key = cacheKey;//储存最终的键
            _cache.StringSet(cacheKey, value.Serialize());
        }

        public void UpdateContainerBag(string key, IBaseContainerBag containerBag)
        {
            if (this.CheckExisted(key))
            {
                Update(key, containerBag);
            }
        }

        #endregion

        public ICacheLock BeginCacheLock(string resourceName, string key, int retryCount = 0, TimeSpan retryDelay = new TimeSpan())
        {
            return new RedisCacheLock(this, resourceName, key, retryCount, retryDelay).LockNow();
        }

    }
}
