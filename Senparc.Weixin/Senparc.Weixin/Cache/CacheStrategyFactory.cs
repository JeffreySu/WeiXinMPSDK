using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache
{
    public class CacheStrategyFactory
    {
        internal static Func<IContainerCacheStragegy> ContainerCacheStrageFunc;

        //internal static IBaseCacheStrategy<TKey, TValue> GetContainerCacheStrategy<TKey, TValue>()
        //    where TKey : class
        //    where TValue : class
        //{
        //    return;
        //}

        public static void RegisterContainerCacheStrategy(Func<IContainerCacheStragegy> func)
        {
            ContainerCacheStrageFunc = func;
        }

        public static IContainerCacheStragegy GetContainerCacheStragegyInstance()
        {
            if (ContainerCacheStrageFunc == null)
            {
                //默认状态
                return LocalContainerCacheStrategy.Instance;
            }
            else
            {
                //自定义类型
                var instance = ContainerCacheStrageFunc();
                return instance;
            }
        }
    }
}
