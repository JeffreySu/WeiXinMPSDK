/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：LocalContainerCacheStrategy.cs
    文件功能描述：本地容器缓存。


    创建标识：Senparc - 20160308

    修改标识：Senparc - 20160812
    修改描述：v4.7.4  解决Container无法注册的问题

    修改标识：Senparc - 20161024
    修改描述：v4.8.3  废除LocalCacheHelper类，将LocalObjectCacheStrategy做为基类

 ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Cache;

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
    //        LocalContainerCache = new Dictionary<string, IBaseContainerBag>(StringComparer.OrdinalIgnoreCase);
    //    }
    //}

    /// <summary>
    /// 本地容器缓存策略
    /// </summary>
    public sealed class LocalContainerCacheStrategy : LocalObjectCacheStrategy, IContainerCacheStrategy
    //where TContainerBag : class, IBaseContainerBag, new()
    {
        #region 数据源

        private IDictionary<string, object> _cache = LocalObjectCacheHelper.LocalObjectCache;

        #endregion

        #region 单例

        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        LocalContainerCacheStrategy():base()
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
            internal static readonly LocalContainerCacheStrategy instance = new LocalContainerCacheStrategy();
        }

        #endregion

        #region ILocalCacheStrategy 成员

        public void InsertToCache(string key, IBaseContainerBag value)
        {
            base.InsertToCache(key, value);
        }

        public void RemoveFromCache(string key, bool isFullKey = false)
        {
            base.RemoveFromCache(key, isFullKey);
        }

        public IBaseContainerBag Get(string key, bool isFullKey = false)
        {
            return base.Get(key,isFullKey) as IBaseContainerBag;
        }


        public IDictionary<string, TBag> GetAll<TBag>() where TBag : IBaseContainerBag
        {
            var dic = new Dictionary<string, TBag>();
            var cacheList = GetAll();
            foreach (var baseContainerBag in cacheList)
            {
                if (baseContainerBag.Value is TBag)
                {
                    dic[baseContainerBag.Key] = (TBag)baseContainerBag.Value;
                }
            }
            return dic;
        }

        public IDictionary<string, IBaseContainerBag> GetAll()
        {
            var dic = new Dictionary<string, IBaseContainerBag>();
            foreach (var item in _cache)
            {
                if (item.Value is IBaseContainerBag)
                {
                    dic[item.Key] = (IBaseContainerBag)item.Value;
                }
            }
            return dic;
        }

        public bool CheckExisted(string key, bool isFullKey = false)
        {
            var cacheKey = GetFinalKey(key, isFullKey);
            return _cache.ContainsKey(cacheKey);
        }

        public long GetCount()
        {
            return GetAll().Count;
        }

        public void Update(string key, IBaseContainerBag value, bool isFullKey = false)
        {
            base.Update(key, value, isFullKey);
        }

        public void UpdateContainerBag(string key, IBaseContainerBag bag, bool isFullKey = false)
        {
            Update(key, bag, isFullKey);
        }

        #endregion
    }
}
