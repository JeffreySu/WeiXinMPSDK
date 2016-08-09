using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Cache
{
    public class CacheLockWrapper : IDisposable
    {
        private string _resourceName;
        private IContainerCacheStragegy _containerCacheStragegy;
        public bool LockSuccessful { get; set; }

        public CacheLockWrapper(IContainerCacheStragegy containerCacheStragegy, string resourceName, string key)
        {
            _containerCacheStragegy = containerCacheStragegy;
            _resourceName = resourceName + key;/*加上Key可以针对某个AppId加锁*/
            LockSuccessful = _containerCacheStragegy.Lock(_resourceName);
        }

        public void Dispose()
        {
            _containerCacheStragegy.UnLock(_resourceName);
        }
    }
}
