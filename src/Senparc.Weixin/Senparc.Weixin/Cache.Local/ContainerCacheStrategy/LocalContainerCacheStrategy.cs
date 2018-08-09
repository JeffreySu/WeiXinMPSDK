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

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

    文件名：LocalContainerCacheStrategy.cs
    文件功能描述：本地容器缓存。


    创建标识：Senparc - 20160308

    修改标识：Senparc - 20160812
    修改描述：v4.7.4  解决Container无法注册的问题

    修改标识：Senparc - 20161024
    修改描述：v4.8.3  废除LocalCacheHelper类，将LocalObjectCacheStrategy做为基类

 ----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Cache;
using Senparc.CO2NET.Cache;
using System;

namespace Senparc.Weixin.Cache
{
    ///// <summary>
    ///// 全局静态数据源帮助类
    ///// </summary>
    //public static class LocalContainerCacheHelper
    //{
    //    /// <summary>
    //    /// 所有数据集合的列表
    //    /// </summary>
    //    internal static IDictionary<string, IBaseContainerBag> LocalContainerCache { get; set; }

    //    static LocalContainerCacheHelper()
    //    {
    //        LocalContainerBaseCacheStrategy() = new Dictionary<string, IBaseContainerBag>(StringComparer.OrdinalIgnoreCase);
    //    }
    //}

    /// <summary>
    /// 本地容器缓存策略
    /// </summary>
    public sealed class LocalContainerCacheStrategy : BaseContainerCacheStrategy
    {
        #region IDomainExtensionCacheStrategy 成员
        public override ICacheStrategyDomain CacheStrategyDomain { get { return ContainerCacheStrategyDomain.Instance; } }

        #endregion

        #region 单例

        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        private LocalContainerCacheStrategy() /*: base()*/
        {
            //使用底层缓存策略
            BaseCacheStrategy = () => LocalObjectCacheStrategy.Instance;

            //向底层缓存注册当前缓存策略
            base.RegisterCacheStrategyDomain(this);
        }

        //静态LocalCacheStrategy
        public static IContainerCacheStrategy Instance
        {
            get
            {
                return Nested.instance;//返回Nested类中的静态成员instance
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            //将instance设为一个初始化的LocalCacheStrategy新实例
            internal static readonly LocalContainerCacheStrategy instance = new LocalContainerCacheStrategy();
        }

        #endregion

        #region BaseContainerCacheStrategy 成员


        public override IDictionary<string, TBag> GetAll<TBag>()
        {
            var dic = new Dictionary<string, TBag>();
            var baseCacheStrategy = BaseCacheStrategy();
            var cacheList = baseCacheStrategy.GetAll();
            foreach (var baseContainerBag in cacheList)
            {
                if (baseContainerBag.Value is TBag)
                {
                    dic[baseContainerBag.Key] = (TBag)baseContainerBag.Value;
                }
            }
            return dic;
        }


        #endregion


        //public IDictionary<string, IBaseContainerBag> GetAll()
        //{
        //    var dic = new Dictionary<string, IBaseContainerBag>();
        //    foreach (var item in BaseCacheStrategy().GetAll())
        //    {
        //        if (item.Value is IBaseContainerBag)
        //        {
        //            dic[item.Key] = (IBaseContainerBag)item.Value;
        //        }
        //    }
        //    return dic;
        //}


        //public ICacheLock BeginCacheLock(string resourceName, string key, int retryCount = 0, TimeSpan retryDelay = default(TimeSpan))
        //{
        //    return new LocalCacheLock(BaseCacheStrategy() as LocalObjectCacheStrategy, resourceName, key, retryCount, retryDelay);
        //    //return BaseCacheStrategy().BeginCacheLock(resourceName, key, retryCount, retryDelay);
        //}
    }
}
