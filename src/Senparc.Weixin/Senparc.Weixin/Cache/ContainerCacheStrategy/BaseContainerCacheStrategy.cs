using Senparc.CO2NET.Cache;
using Senparc.Weixin.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Cache
{
    public abstract class BaseContainerCacheStrategy : IContainerCacheStrategy
    {
        #region IDomainExtensionCacheStrategy 成员
        public abstract ICacheStrategyDomain CacheStrategyDomain { get; }

        /// <summary>
        /// 数据源缓存策略
        /// </summary>
        public Func<IBaseObjectCacheStrategy> BaseCacheStrategy { get; protected set; }

        #endregion


        #region IContainerCacheStrategy 成员

        /// <summary>
        /// 向底层缓存注册当前缓存策略
        /// </summary>
        /// <param name="extensionCacheStrategy"></param>
        public void RegisterCacheStrategyDomain(IDomainExtensionCacheStrategy extensionCacheStrategy)
        {
            CacheStrategyDomainWarehouse.RegisterCacheStrategyDomain(extensionCacheStrategy);
        }


        #region 同步方法

        /// <summary>
        /// 获取所有 Bag 对象
        /// </summary>
        /// <typeparam name="TBag"></typeparam>
        /// <returns></returns>
        public abstract IDictionary<string, TBag> GetAll<TBag>() where TBag : IBaseContainerBag;


        /// <summary>
        /// 获取单个ContainerBag
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="isFullKey">是否已经是完整的Key，如果不是，则会调用一次GetFinalKey()方法</param>
        /// <returns></returns>
        public virtual TBag GetContainerBag<TBag>(string key, bool isFullKey = false) where TBag : IBaseContainerBag
        {
            var baseCacheStrategy = BaseCacheStrategy();
            return baseCacheStrategy.Get<TBag>(key, isFullKey);
        }


        public virtual void UpdateContainerBag(string key, IBaseContainerBag bag, TimeSpan? expiry = null, bool isFullKey = false)
        {
            var baseCacheStrategy = BaseCacheStrategy();
            baseCacheStrategy.Update(key, bag, expiry, isFullKey);
        }

        #endregion

        #region 异步方法


        /// <summary>
        /// 【异步方法】获取所有 Bag 对象
        /// </summary>
        /// <typeparam name="TBag"></typeparam>
        /// <returns></returns>
        public abstract Task<IDictionary<string, TBag>> GetAllAsync<TBag>() where TBag : IBaseContainerBag;


        /// <summary>
        /// 【异步方法】获取单个ContainerBag
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="isFullKey">是否已经是完整的Key，如果不是，则会调用一次GetFinalKey()方法</param>
        /// <returns></returns>
        public virtual async Task<TBag> GetContainerBagAsync<TBag>(string key, bool isFullKey = false) where TBag : IBaseContainerBag
        {
            var baseCacheStrategy = BaseCacheStrategy();
            return await baseCacheStrategy.GetAsync<TBag>(key, isFullKey).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】更新 ContainerBag
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bag"></param>
        /// <param name="expiry"></param>
        /// <param name="isFullKey"></param>
        public virtual async Task UpdateContainerBagAsync(string key, IBaseContainerBag bag, TimeSpan? expiry = null, bool isFullKey = false)
        {
            var baseCacheStrategy = BaseCacheStrategy();
            await baseCacheStrategy.UpdateAsync(key, bag, expiry, isFullKey).ConfigureAwait(false);
        }


        #endregion

        #endregion
    }
}
