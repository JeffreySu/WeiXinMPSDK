#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.CO2NET.Cache;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache
{
    public class ContainerCacheStrategyFactory
    {
        //internal static Func<IContainerCacheStrategy> ContainerCacheStrateFunc;

        //internal static Func<IContainerCacheStrategy> ObjectCacheStrateFunc = () => LocalContainerCacheStrategy.Instance;//默认为 WeixinLocalObjectCacheStrategy
        //internal static IBaseCacheStrategy<TKey, TValue> GetContainerCacheStrategy<TKey, TValue>()
        //    where TKey : class
        //    where TValue : class
        //{
        //    return;
        //}

        ///// <summary>
        ///// 注册当前全局环境下的缓存策略
        ///// </summary>
        ///// <param name="func">如果为null，将使用默认的本地缓存策略（LocalObjectCacheStrategy.Instance）</param>
        //public static void RegisterContainerCacheStrategy(Func<IContainerCacheStrategy> func)
        //{
        //    ObjectCacheStrateFunc = func;

        //    //TODO:如果不考虑效率，此方法可以使用反射，自动注册所有的扩展缓存
        //}

        /// <summary>
        /// 如果
        /// </summary>
        /// <returns></returns>
        public static IContainerCacheStrategy GetContainerCacheStrategyInstance()
        {
            //从底层进行判断
            var containerCacheStrategy = CacheStrategyFactory.GetExtensionCacheStrategyInstance(ContainerCacheStrategyDomain.Instance)
                                            as IContainerCacheStrategy;
            return containerCacheStrategy;


            //if (ObjectCacheStrateFunc == null)
            //{
            //    //默认状态
            //    return LocalContainerCacheStrategy.Instance /*as IContainerCacheStrategy*/;
            //}
            //else
            //{
            //    //自定义类型
            //    var instance = ObjectCacheStrateFunc();// ?? LocalObjectCacheStrategy.Instance;
            //    return instance;
            //}
        }
    }
}
