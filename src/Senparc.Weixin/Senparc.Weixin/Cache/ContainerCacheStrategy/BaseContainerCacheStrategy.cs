#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc

    文件名：BaseContainerCacheStrategy.cs
    文件功能描述：容器策略（领域缓存）基类。


    创建标识：Senparc - 20180614

    修改标识：Senparc - 20200301
    修改描述：v6.7.303 BaseContainerCacheStrategy.UpdateContainerBag() 方法自动更新 CacheTime 值

 ----------------------------------------------------------------*/


using Senparc.CO2NET.Cache;
using Senparc.Weixin.Containers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// 容器策略（领域缓存）基类
    /// </summary>
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
            bag.CacheTime = SystemTime.Now;//更新缓存时间（上一级 Container 通常已经有设置，这里是为了小粒度确保一下）
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
            bag.CacheTime = SystemTime.Now;//更新缓存时间（上一级 Container 通常已经有设置，这里是为了小粒度确保一下）
            await baseCacheStrategy.UpdateAsync(key, bag, expiry, isFullKey).ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
