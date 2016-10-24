using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Cache
{
    public abstract class BaseCacheLock : ICacheLock
    {
        protected string _resourceName;
        protected IBaseCacheStrategy _cacheStrategy;
        protected int _retryCount;
        protected TimeSpan _retryDelay;
        public bool LockSuccessful { get; set; }

        protected BaseCacheLock(IBaseCacheStrategy strategy, string resourceName, string key, int retryCount, TimeSpan retryDelay)
        {
            _cacheStrategy = strategy;
            _resourceName = resourceName + key;/*加上Key可以针对某个AppId加锁*/
            _retryCount = retryCount;
            _retryDelay = retryDelay;
        }

        public void Dispose()
        {
            UnLock(_resourceName);
        }

        public ICacheLock LockNow()
        {
            if (_retryCount != 0 && _retryDelay.Ticks != 0)
            {
                LockSuccessful = Lock(_resourceName, _retryCount, _retryDelay);
            }
            else
            {
                LockSuccessful = Lock(_resourceName);
            }
            return this;
        }

        public abstract bool Lock(string resourceName);

        public abstract bool Lock(string resourceName, int retryCount, TimeSpan retryDelay);

        public abstract void UnLock(string resourceName);
    }
}
