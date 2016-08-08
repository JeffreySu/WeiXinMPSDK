using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Cache
{
    public static class CacheLockWrapperExtension
    {
        public static CacheLockWrapper InstanceCacheLockWrapper(this IContainerCacheStragegy stragegy, string resourceName, string key)
        {
            return new CacheLockWrapper(stragegy, resourceName,key);
        }
    }
}
