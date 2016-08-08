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

        public CacheLockWrapper(IContainerCacheStragegy containerCacheStragegy, string resourceName)
        {
            _containerCacheStragegy = containerCacheStragegy;
            _resourceName = resourceName;
            _containerCacheStragegy.Lock(_resourceName);
        }

        public void Dispose()
        {
            _containerCacheStragegy.UnLock(_resourceName);
        }
    }
}
