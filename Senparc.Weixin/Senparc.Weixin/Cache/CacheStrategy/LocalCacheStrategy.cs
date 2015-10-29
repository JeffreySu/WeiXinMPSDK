using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Senparc.Weixin.Cache
{
    public class LocalCacheStrategy<T> : IBaseCacheStrategy<T> where T : class
    {
        #region 数据源

        /// <summary>
        /// 所有数据集合的列表
        /// </summary>
        private static readonly Dictionary<object, object> _collectionList = new Dictionary<object, object>();

        #endregion

        #region 单例

        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        LocalCacheStrategy()
        {
        }

        //静态LocalCacheStrategy
        public static IBaseCacheStrategy<T> Instance
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
            internal static readonly LocalCacheStrategy<T> instance = new LocalCacheStrategy<T>();
        }

        #endregion

        #region ILocalCacheStrategy 成员


        public string CacheSetKey { get; set; }
        public void InsertToCache(string key, T value)
        {
            if (string.IsNullOrEmpty(key) || value == null)
            {
                return;
            }

            _collectionList[key] = value;
        }

        public void RemoveFromCache(string key)
        {
            _collectionList.Remove(key);
        }

        public T Get(string key)
        {
            if (_collectionList.ContainsKey(key))
            {
                return _collectionList[key] as T;
            }
            return null;
        }

        public IList<T> GetAll()
        {
            var list = _collectionList.Values.Select(z => z as T).ToList();
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
