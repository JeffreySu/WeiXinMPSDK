/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc

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
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache.Memcached
{
    public class MemcachedContainerStrategy : MemcachedObjectCacheStrategy, IContainerCacheStrategy
    {
        #region 单例

        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        MemcachedContainerStrategy()
        {
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
            internal static readonly MemcachedContainerStrategy instance = new MemcachedContainerStrategy();
        }

        #endregion


        #region IContainerCacheStrategy 成员

        public void InsertToCache(string key, IBaseContainerBag value)//TODO:添加Timeout参数
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return;
            }

            base.InsertToCache(key, value);
#if DEBUG
            var cacheKey = GetFinalKey(key);
            value = _cache.Get(cacheKey) as IBaseContainerBag;
#endif
        }

        public void RemoveFromCache(string key, bool isFullKey = false)
        {
            base.RemoveFromCache(key, isFullKey);
        }

        public IBaseContainerBag Get(string key, bool isFullKey = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            var cacheKey = GetFinalKey(key, isFullKey);
            return _cache.Get<IBaseContainerBag>(cacheKey);
        }

        public IDictionary<string, TBag> GetAll<TBag>() where TBag : IBaseContainerBag
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, IBaseContainerBag> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool CheckExisted(string key, bool isFullKey = false)
        {
            return base.CheckExisted(key, isFullKey);
        }

        public long GetCount()
        {
            throw new NotImplementedException();//TODO:需要定义二级缓存键，从池中获取
        }

        public void Update(string key, IBaseContainerBag value, bool isFullKey = false)
        {
            base.Update(key, value, isFullKey);
        }

        public void UpdateContainerBag(string key, IBaseContainerBag containerBag, bool isFullKey = false)
        {
            var cacheKey = GetFinalKey(key, isFullKey);
            object value;
            if (_cache.TryGet(cacheKey, out value))
            {
                Update(cacheKey, containerBag, true);
            }
        }
        
        #endregion

    }
}
