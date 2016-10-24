using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Redlock.CSharp;

namespace Senparc.Weixin.Cache.Redis
{
    public class RedisCacheLock : BaseCacheLock
    {
        private Redlock.CSharp.Redlock _dlm;
        private Lock _lockObject;

        private RedisContainerCacheStrategy _redisStrategy;

        public RedisCacheLock(RedisContainerCacheStrategy strategy, string resourceName, string key, int retryCount, TimeSpan retryDelay)
            :base(strategy,resourceName,key,retryCount,retryDelay)
        {
            _redisStrategy = strategy;
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

            var successfull = _dlm.Lock(resourceName, new TimeSpan(0, 0, 10), out _lockObject);
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
