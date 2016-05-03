using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;
using StackExchange.Redis;
using StackExchange.Redis.KeyspaceIsolation;


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

        public RedisContainerCacheStrategy()
        {
            _client = RedisManager.Manager;
            _cache = _client.GetDatabase();
        }

        ~RedisContainerCacheStrategy()
        {
            _client.Dispose();//释放
            //_client.Dispose();//释放
            //GC.SuppressFinalize(_client);
        }

        private string GetFinalKey(string key)
        {
            //return String.Format("{0}:{1}", CacheSetKey, key);
            return String.Format("{0}{1}", CacheSetKey, key);
        }

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
            return _cache.KeyExists(key);
        }

        public IContainerItemCollection Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            var cacheKey = GetFinalKey(key);
            return _cache.HashGetAll(cacheKey).ConvertFromRedis<IContainerItemCollection>();
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

            _cache.HashSet(cacheKey, value.ToHashEntries());
            //_cache.SetEntry(cacheKey, obj);

#if DEBUG
            var value1 = _cache.HashGetAll(cacheKey);//正常情况下可以得到 //_cache.GetValue(cacheKey);
#endif
        }

        public void RemoveFromCache(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            var cacheKey = GetFinalKey(key);
            _cache.HashDelete(cacheKey, RedisValue.Null);
        }

        public void Update(string key, IContainerItemCollection value)
        {
            _cache.HashSet(key, value.ToHashEntries());
        }

        public void UpdateContainerBag(string key, IBaseContainerBag containerBag)
        {
            if (this.CheckExisted(key))
            {
                var containerItemCollection = Get(key);
                containerItemCollection[containerBag.Key] = containerBag;

                _cache.HashSet(key, containerItemCollection.ToHashEntries());
            }
        }

        #endregion
    }
}
