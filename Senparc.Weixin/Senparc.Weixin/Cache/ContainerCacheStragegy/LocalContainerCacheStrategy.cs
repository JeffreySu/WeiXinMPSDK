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
    public class LocalContainerCacheStrategy<Type, TContainerBag> : IBaseCacheStrategy<Type, Dictionary<string, TContainerBag>>
        where TContainerBag : IBaseContainerBag
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
            internal static readonly LocalContainerCacheStrategy<Type, TContainerBag> instance = new LocalContainerCacheStrategy<Type, TContainerBag>();
        }

        #endregion

        #region ILocalCacheStrategy 成员


        public string CacheSetKey { get; set; }

   
        public void InsertToCache(Type key, Dictionary<string, TContainerBag> value)
        {
            if (key == null || value == null)
            {
                return;
            }

            _collectionList[key] = value;
        }

        public void RemoveFromCache(Type key)
        {
            _collectionList.Remove(key);
        }

        public Dictionary<string, TContainerBag> Get(Type key)
        {
            if (_collectionList.ContainsKey(key))
            {
                return _collectionList[key];
            }
            return null;
        }

        public IList<Dictionary<string, TContainerBag>> GetAll()
        {
            var list = _collectionList.Values.ToList();
            return list;
        }

        public bool CheckExisted(Type key)
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
