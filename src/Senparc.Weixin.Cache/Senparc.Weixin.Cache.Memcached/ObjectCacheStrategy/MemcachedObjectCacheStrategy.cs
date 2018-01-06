/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：MemcachedObjectCacheStrategy.cs
    文件功能描述：本地锁


    创建标识：Senparc - 20161025

    修改标识：Senparc - 20170205
    修改描述：v0.2.0 重构分布式锁

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace Senparc.Weixin.Cache.Memcached
{
    public class MemcachedObjectCacheStrategy : BaseCacheStrategy, IObjectCacheStrategy
    {
        internal MemcachedClient _cache;
        private MemcachedClientConfiguration _config;
        private static Dictionary<string, int> _serverlist;// = SiteConfig.MemcachedAddresss; TODO:全局注册配置

        /// <summary>
        /// 注册列表
        /// </summary>
        /// <param name="serverlist">Key：服务器地址（通常为IP），Value：端口</param>
        public static void RegisterServerList(Dictionary<string, int> serverlist)
        {
            _serverlist = serverlist;
        }

        #region 单例

        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        public MemcachedObjectCacheStrategy()
        {
            _config = GetMemcachedClientConfiguration();

#if NET45 || NET461
            _cache = new MemcachedClient(_config);
#else
            _cache = new MemcachedClient(null, _config);
#endif
        }

        //静态LocalCacheStrategy
        public static IObjectCacheStrategy Instance
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
            //将instance设为一个初始化的LocalCacheStrategy新实例
            internal static readonly MemcachedObjectCacheStrategy instance = new MemcachedObjectCacheStrategy();
        }

        #endregion

        #region 配置

        private static MemcachedClientConfiguration GetMemcachedClientConfiguration()
        {
            //每次都要新建

#if NET45 || NET461
            var config = new MemcachedClientConfiguration();
#else
            var config = new MemcachedClientConfiguration(null, null);
#endif

            foreach (var server in _serverlist)
            {
                config.Servers.Add(new IPEndPoint(IPAddress.Parse(server.Key), server.Value));
            }
            config.Protocol = MemcachedProtocol.Binary;

            return config;
        }

        static MemcachedObjectCacheStrategy()
        {
            // //初始化memcache服务器池
            //SockIOPool pool = SockIOPool.GetInstance();
            ////设置Memcache池连接点服务器端。
            //pool.SetServers(serverlist);
            ////其他参数根据需要进行配置

            //pool.InitConnections = 3;
            //pool.MinConnections = 3;
            //pool.MaxConnections = 5;

            //pool.SocketConnectTimeout = 1000;
            //pool.SocketTimeout = 3000;

            //pool.MaintenanceSleep = 30;
            //pool.Failover = true;

            //pool.Nagle = false;
            //pool.Initialize();

            //cache = new MemcachedClient();
            //cache.EnableCompression = false;
            try
            {
                //config.Authentication.Type = typeof(PlainTextAuthenticator);
                //config.Authentication.Parameters["userName"] = "username";
                //config.Authentication.Parameters["password"] = "password";
                //config.Authentication.Parameters["zone"] = "zone";//domain?   ——Jeffrey 2015.10.20
                DateTime dt1 = DateTime.Now;
                var config = GetMemcachedClientConfiguration();
                //var cache = new MemcachedClient(config);'


#if NET45 || NET461
                var cache = new MemcachedClient(config);
#else
                var cache = new MemcachedClient(null, config);
#endif

                var testKey = Guid.NewGuid().ToString();
                var testValue = Guid.NewGuid().ToString();
                cache.Store(StoreMode.Set, testKey, testValue);
                var storeValue = cache.Get(testKey);
                if (storeValue as string != testValue)
                {
                    throw new Exception("MemcachedStrategy失效，没有计入缓存！");
                }
                cache.Remove(testKey);
                DateTime dt2 = DateTime.Now;

                WeixinTrace.Log(string.Format("MemcachedStrategy正常启用，启动及测试耗时：{0}ms", (dt2 - dt1).TotalMilliseconds));
            }
            catch (Exception ex)
            {
                //TODO:记录是同日志
                WeixinTrace.Log(string.Format("MemcachedStrategy静态构造函数异常：{0}", ex.Message));
            }
        }

        #endregion


        #region IContainerCacheStrategy 成员

        public IContainerCacheStrategy ContainerCacheStrategy
        {
            get { return MemcachedContainerStrategy.Instance; }
        }

        public void InsertToCache(string key, object value)//TODO:添加Timeout参数
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return;
            }

            var cacheKey = GetFinalKey(key);

            //TODO：加了绝对过期时间就会立即失效（再次获取后为null），memcache低版本的bug
            _cache.Store(StoreMode.Set, cacheKey, value, DateTime.Now.AddDays(1));
        }

        public void RemoveFromCache(string key, bool isFullKey = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }
            var cacheKey = GetFinalKey(key, isFullKey);
            _cache.Remove(cacheKey);
        }

        public object Get(string key, bool isFullKey = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            var cacheKey = GetFinalKey(key, isFullKey);
            return _cache.Get<object>(cacheKey);
        }

        public IDictionary<string, object> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool CheckExisted(string key, bool isFullKey = false)
        {
            var cacheKey = GetFinalKey(key, isFullKey);
            object value;
            if (_cache.TryGet(cacheKey, out value))
            {
                return true;
            }
            return false;
        }

        public long GetCount()
        {
            throw new NotImplementedException();//TODO:需要定义二级缓存键，从池中获取
        }

        public void Update(string key, object value, bool isFullKey = false)
        {
            var cacheKey = GetFinalKey(key, isFullKey);
            _cache.Store(StoreMode.Set, cacheKey, value, DateTime.Now.AddDays(1));
        }

        #endregion


        public override ICacheLock BeginCacheLock(string resourceName, string key, int retryCount = 0, TimeSpan retryDelay = new TimeSpan())
        {
            return new MemcachedCacheLock(this, resourceName, key, retryCount, retryDelay);
        }
    }
}
