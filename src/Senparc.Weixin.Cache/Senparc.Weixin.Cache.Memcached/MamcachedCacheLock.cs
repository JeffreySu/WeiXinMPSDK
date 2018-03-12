/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：RedisCacheLock.cs
    文件功能描述：本地锁


    创建标识：Senparc - 20160810

    修改标识：Senparc - 20170205
    修改描述：v0.2.0 重构分布式锁

    修改标识：spadark - 20170419
    修改描述：v0.3.0 Memcached同步锁改为使用StoreMode.Add方法

----------------------------------------------------------------*/


using System;
using System.Threading;
using Enyim.Caching.Memcached;

namespace Senparc.Weixin.Cache.Memcached
{
    public class MemcachedCacheLock : BaseCacheLock
    {
        private MemcachedObjectCacheStrategy _mamcachedStrategy;
        public MemcachedCacheLock(MemcachedObjectCacheStrategy strategy, string resourceName, string key, int retryCount, TimeSpan retryDelay)
            : base(strategy, resourceName, key, retryCount, retryDelay)
        {
            _mamcachedStrategy = strategy;
            LockNow();//立即等待并抢夺锁
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
                    if (_mamcachedStrategy._cache.Store(StoreMode.Add, key, new object(), new TimeSpan(0, 0, 10)))
                    {
                        return true;//取得锁 
                    }
                    else
                    {
                        return false;//已被别人锁住，没有取得锁
                    }

                    //if (_mamcachedStrategy._cache.Get(key) != null)
                    //{
                    //    return false;//已被别人锁住，没有取得锁
                    //}
                    //else
                    //{
                    //    _mamcachedStrategy._cache.Store(StoreMode.set, key, new object(), new TimeSpan(0, 0, 10));//创建锁
                    //    return true;//取得锁
                    //}
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
