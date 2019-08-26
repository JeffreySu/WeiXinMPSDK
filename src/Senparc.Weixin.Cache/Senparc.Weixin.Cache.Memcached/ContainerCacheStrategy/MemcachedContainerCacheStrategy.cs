/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：MemcachedContainerStrategy.cs
    文件功能描述：Memcached 容器缓存策略。


    创建标识：Senparc - 20160308

    修改标识：Senparc - 20160808
    修改描述：v0.0.2 删除 ItemCollection 属性，直接使用ContainerBag加入到缓存

    修改标识：Senparc - 20160812
    修改描述：v0.0.3  解决Container无法注册的问题

    修改标识：Senparc - 20160812
    修改描述：v0.0.5  添加ServerList配制方法

    修改标识：Senparc - 20170205
    修改描述：v0.2.0 重构分布式锁

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.Memcached;
using Senparc.Weixin.Containers;
#if NET45 || NET461

#else
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
#endif

namespace Senparc.Weixin.Cache.Memcached
{
    public class MemcachedContainerCacheStrategy : BaseContainerCacheStrategy
    {
        #region IDomainExtensionCacheStrategy 成员
        public override ICacheStrategyDomain CacheStrategyDomain { get { return ContainerCacheStrategyDomain.Instance; } }

        #endregion


        #region 单例
        /// <summary>
        /// MemcachedContainerCacheStrategy 的构造函数
        /// </summary>
        MemcachedContainerCacheStrategy()
        {
            //使用底层缓存策略
            BaseCacheStrategy = () => MemcachedObjectCacheStrategy.Instance;

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
            internal static readonly MemcachedContainerCacheStrategy instance = new MemcachedContainerCacheStrategy();
        }
        #endregion


        #region IContainerCacheStrategy 成员

        public override IDictionary<string, TBag> GetAll<TBag>()
        {
            throw new NotImplementedException();
        }

        public override void UpdateContainerBag(string key, IBaseContainerBag containerBag, TimeSpan? expiry = null, bool isFullKey = false)
        {
            var baseCacheStrategy = BaseCacheStrategy();
            object value;
            if ((baseCacheStrategy as MemcachedObjectCacheStrategy).TryGet(key, out value))
            {
                baseCacheStrategy.Update(key, containerBag, expiry, isFullKey);
            }
        }

        #region 异步方法

        /// <summary>
        ///  【异步方法】获取所有 Bag 对象
        /// </summary>
        /// <typeparam name="TBag"></typeparam>
        /// <returns></returns>
        public override async Task<IDictionary<string, TBag>> GetAllAsync<TBag>()
        {
            throw new NotImplementedException();
        }

        public override async Task UpdateContainerBagAsync(string key, IBaseContainerBag bag, TimeSpan? expiry = null, bool isFullKey = false)
        {
            var baseCacheStrategy = BaseCacheStrategy();
            object value;
            if ((baseCacheStrategy as MemcachedObjectCacheStrategy).TryGet(key, out value))
            {
               await baseCacheStrategy.UpdateAsync(key, bag, expiry, isFullKey);
            }

            //Memcached 组件没有提供对应 TryGet() 的异步方法，所以也可以考虑使用 Task.Factory 完成异步
            //await Task.Factory.StartNew(() => UpdateContainerBag(key, bag, expiry, isFullKey)).ConfigureAwait(false);
        }

        #endregion

        #endregion

    }
}
