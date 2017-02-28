/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc

    文件名：RedisCacheLock.cs
    文件功能描述：本地锁


    创建标识：Senparc - 20160810

    修改标识：Senparc - 20170205
    修改描述：v1.2.0 重构分布式锁

----------------------------------------------------------------*/

using System;
using Redlock.CSharp;

namespace Senparc.Weixin.Cache.Redis
{
    public class RedisCacheLock : BaseCacheLock
    {
        private Redlock.CSharp.Redlock _dlm;
        private Lock _lockObject;

        private RedisObjectCacheStrategy _redisStrategy;

        public RedisCacheLock(RedisObjectCacheStrategy strategy, string resourceName, string key, int retryCount, TimeSpan retryDelay)
            : base(strategy, resourceName, key, retryCount, retryDelay)
        {
            _redisStrategy = strategy;
            LockNow();//立即等待并抢夺锁
        }

        public override bool Lock(string resourceName)
        {
            return Lock(resourceName, 0, new TimeSpan());
        }

        public override bool Lock(string resourceName, int retryCount, TimeSpan retryDelay)
        {
            if (retryCount != 0)
            {
                _dlm = new Redlock.CSharp.Redlock(retryCount, retryDelay, _redisStrategy._client);
            }
            else if (_dlm == null)
            {
                _dlm = new Redlock.CSharp.Redlock(_redisStrategy._client);
            }

            var ttl = (retryDelay.TotalMilliseconds > 0 ? retryDelay.TotalMilliseconds : 10)
                       *
                      (retryCount > 0 ? retryCount : 10);


            var successfull = _dlm.Lock(resourceName, TimeSpan.FromMilliseconds(ttl), out _lockObject);
            return successfull;
        }

        public override void UnLock(string resourceName)
        {
            if (_lockObject != null)
            {
                _dlm.Unlock(_lockObject);
            }
        }
    }
}
