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
        internal static Func<IContainerCacheStrategy> ContainerCacheStrateFunc;

        internal static Func<IObjectCacheStrategy> ObjectCacheStrateFunc;
        //internal static IBaseCacheStrategy<TKey, TValue> GetContainerCacheStrategy<TKey, TValue>()
        //    where TKey : class
        //    where TValue : class
        //{
        //    return;
        //}

        public static void RegisterObjectCacheStrategy(Func<IObjectCacheStrategy> func)
        {
            ObjectCacheStrateFunc = func;
        }


        public static IObjectCacheStrategy GetObjectCacheStrategyInstance()
        {
            if (ObjectCacheStrateFunc == null)
            {
                //默认状态
                return LocalObjectCacheStrategy.Instance;
            }
            else
            {
                //自定义类型
                var instance = ObjectCacheStrateFunc();
                return instance;
            }
        }


        //public static void RegisterContainerCacheStrategy(Func<IContainerCacheStrategy> func)
        //{
        //    ContainerCacheStrateFunc = func;
        //}

        //public static IContainerCacheStrategy GetContainerCacheStrategyInstance()
        //{
        //    if (ContainerCacheStrateFunc == null)
        //    {
        //        //默认状态
        //        return LocalContainerCacheStrategy.Instance;
        //    }
        //    else
        //    {
        //        //自定义类型
        //        var instance = ContainerCacheStrateFunc();
        //        return instance;
        //    }
        //}
    }
}
