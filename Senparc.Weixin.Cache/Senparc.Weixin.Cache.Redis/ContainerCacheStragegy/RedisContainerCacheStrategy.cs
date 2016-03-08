using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    /// Redis容器缓存策略
    /// </summary>
    public sealed class RedisContainerCacheStrategy : IContainerCacheStragegy
    {
        private IRedisClient _client;
        private IRedisTypedClient<IContainerItemCollection> _cache;

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
            DateTime dt1 = DateTime.Now;
            //var config = RedisConfigInfo.GetConfig();
            var cache = RedisManager.GetClient();

            var testKey = Guid.NewGuid().ToString();
            var testValue = Guid.NewGuid().ToString();
            cache.Set(testKey, testValue);
            var storeValue = cache.Get<string>(testKey);
            if (storeValue as string != testValue)
            {
                throw new Exception("RedisStrategy失效，没有计入缓存！");
            }
            cache.Remove(testKey);
        }

        public RedisContainerCacheStrategy()
        {
            //_config = RedisConfigInfo.GetConfig();
            _client = RedisManager.GetClient();
            _cache = _client.As<IContainerItemCollection>();
        }

        ~RedisContainerCacheStrategy()
        {
            _client.Dispose();//释放
            //GC.SuppressFinalize(_client);
        }

        private string GetFinalKey(string key)
        {
            return String.Format("{0}:{1}",CacheSetKey, key);
        }

        /// <summary>
        /// 获取hash
        /// </summary>
        /// <returns></returns>
        private IRedisHash<string, IContainerItemCollection> GetHash()
        {
            return _cache.GetHash<string>(CacheSetKey);
        }

        #region 实现 IContainerCacheStragegy 接口

        public string CacheSetKey
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool CheckExisted(string key)
        {
            throw new NotImplementedException();
        }

        public IContainerItemCollection Get(string key)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, IContainerItemCollection> GetAll()
        {
            throw new NotImplementedException();
        }

        public long GetCount()
        {
            throw new NotImplementedException();
        }

        public void InsertToCache(string key, IContainerItemCollection value)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromCache(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(string key, IContainerItemCollection value)
        {
            throw new NotImplementedException();
        }

        public void UpdateContainerBag(string key, IBaseContainerBag containerBag)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
