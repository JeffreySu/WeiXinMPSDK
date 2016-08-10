using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Enyim.Caching.Memcached;

namespace Senparc.Weixin.Cache.Memcached
{
    public class MemcachedCacheLock : BaseCacheLock
    {
        private MemcachedContainerStrategy _mamcachedStrategy;
        public MemcachedCacheLock(MemcachedContainerStrategy stragegy, string resourceName, string key, int retryCount, TimeSpan retryDelay)
            : base(stragegy, resourceName, key, retryCount, retryDelay)
        {
            _mamcachedStrategy = stragegy;
        }

        private static Random _rnd = new Random();

        private string GetLockKey(string resourceName)
        {
            return string.Format("{0}:{1}", "Lock", resourceName);
        }

        private bool RetryLock(string resourceName, int retryCount, TimeSpan retryDelay, Func<bool> action)
        {
            int currentRetry = 0;
            int maxRetryDelay = (int)retryDelay.TotalMilliseconds;
            while (currentRetry++ < retryCount)
            {
                if (action())
                {
                    return true;//取得锁
                }
                Thread.Sleep(_rnd.Next(maxRetryDelay));
            }
            return false;
        }

        public override bool Lock(string resourceName)
        {
            return Lock(resourceName, 9999, new TimeSpan(0, 0, 0, 0, 20));
        }

        public override bool Lock(string resourceName, int retryCount, TimeSpan retryDelay)
        {
            var key = _mamcachedStrategy.GetFinalKey(resourceName);
            var successfull = RetryLock(key, retryCount /*暂时不限制*/, retryDelay, () =>
            {
                try
                {
                    if (_mamcachedStrategy._cache.Get(key) != null)
                    {
                        return false;//已被别人锁住，没有取得锁
                    }
                    else
                    {
                        _mamcachedStrategy._cache.Store(StoreMode.Set, key, new object(), new TimeSpan(0, 0, 10));//创建锁
                        return true;//取得锁
                    }
                }
                catch (Exception ex)
                {
                    WeixinTrace.Log("Memcached同步锁发生异常：" + ex.Message);
                    return false;
                }
            }
              );
            return successfull;
        }

        public override void UnLock(string resourceName)
        {
            var key = _mamcachedStrategy.GetFinalKey(resourceName);
            _mamcachedStrategy._cache.Remove(key);
        }
    }
}
