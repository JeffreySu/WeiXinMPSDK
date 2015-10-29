using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// 本地容器缓存策略
    /// </summary>
    /// <typeparam name="TContainerBag">容器内</typeparam>
    public class LocalContainerCacheStrategy<TContainerType, TContainerBag> : IBaseCacheStrategy<Type, Dictionary<string, TContainerBag>> where TContainerBag : IBaseContainerBag
    {
        #region 数据源

        /// <summary>
        /// 所有数据集合的列表
        /// </summary>
        private static readonly Dictionary<object, object> _cache = new Dictionary<object, object>();

        //TODO:2选1
        private static readonly Dictionary<Type, Dictionary<string, TContainerBag>> _collectionList = new Dictionary<Type, Dictionary<string, TContainerBag>>();

        #endregion

        #region 单例

        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        LocalContainerCacheStrategy()
        {
        }

        //静态LocalCacheStrategy
        public static IBaseCacheStrategy<Type, Dictionary<string, TContainerBag>> Instance
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
            internal static readonly LocalContainerCacheStrategy<TContainerBag> instance = new LocalContainerCacheStrategy<TContainerBag>();
        }

        #endregion

        #region ILocalCacheStrategy 成员


        public string CacheSetKey { get; set; }
        public void InsertToCache(string key, TContainerBag containerBag)
        {
            if (key == null || containerBag == null)
            {
                return;
            }

            _collectionList[key] = containerBag;
        }

        public void RemoveFromCache(string key)
        {
            _collectionList.Remove(key);
        }

        public TContainerBag Get(string key)
        {
            if (_collectionList.ContainsKey(key))
            {
                return _collectionList[key] as TContainerBag;
            }
            return null;
        }

        public IList<TContainerBag> GetAll()
        {
            var list = _collectionList.Values.Select(z => z as TContainerBag).ToList();
            return list;
        }

        public bool CheckExisted(string key)
        {
            return _collectionList.ContainsKey(key);
        }

        public long GetCount()
        {
            return _collectionList.Count;
        }

        #endregion
    }
}
