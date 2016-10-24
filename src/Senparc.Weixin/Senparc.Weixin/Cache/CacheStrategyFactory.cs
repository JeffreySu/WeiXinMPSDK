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
        internal static Func<IContainerCacheStrategy> ContainerCacheStrageFunc;

        internal static Func<IObjectCacheStrategy> ObjectCacheStrageFunc;
        //internal static IBaseCacheStrategy<TKey, TValue> GetContainerCacheStrategy<TKey, TValue>()
        //    where TKey : class
        //    where TValue : class
        //{
        //    return;
        //}

        public static void RegisterObjectCacheStrategy(Func<IObjectCacheStrategy> func)
        {
            ObjectCacheStrageFunc = func;
        }

        public static void RegisterContainerCacheStrategy(Func<IContainerCacheStrategy> func)
        {
            ContainerCacheStrageFunc = func;
        }

        public static IContainerCacheStrategy GetContainerCacheStragegyInstance()
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
