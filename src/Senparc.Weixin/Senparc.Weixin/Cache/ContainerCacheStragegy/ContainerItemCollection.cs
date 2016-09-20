using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// IContainerItemCollection，对某个Container下的缓存值ContainerBag进行封装
    /// </summary>
    public interface IContainerItemCollection : IBaseCacheStrategy<string, IBaseContainerBag>
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key">缓存键（通常为AppId，值和IBaseContainerBag.Key相等）</param>
        /// <returns></returns>
        IBaseContainerBag this[string key] { get; set; }
    }

    /// <summary>
    /// 储存某个Container下所有ContainerBag的字典集合
    /// </summary>
    [Serializable]
    public class ContainerItemCollection : IContainerItemCollection
    {
        private Dictionary<string, IBaseContainerBag> _cache;//TODO:可以考虑升级到统一的式缓存策略中

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="key">缓存键（通常为AppId，值和IBaseContainerBag.Key相等）</param>
        /// <returns></returns>
        public IBaseContainerBag this[string key]
        {
            get { return this.Get(key); }
            set { this.Update(key, value); }
        }

        public ContainerItemCollection()
        {
            _cache = new Dictionary<string, IBaseContainerBag>(StringComparer.OrdinalIgnoreCase);
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public string CacheSetKey { get; set; }

        #region 实现IContainerItemCollection : IBaseCacheStrategy<string, IBaseContainerBag>接口

        public void InsertToCache(string key, IBaseContainerBag value)
        {
            _cache[key] = value;
        }

        public void RemoveFromCache(string key)
        {
            _cache.Remove(key);
        }

        public IBaseContainerBag Get(string key)
        {
            if (this.CheckExisted(key))
            {
                return _cache[key];
            }
            return null;
        }

        public IDictionary<string, IBaseContainerBag> GetAll()
        {
            return _cache;
        }

        public bool CheckExisted(string key)
        {
            return _cache.ContainsKey(key);
        }

        public long GetCount()
        {
            return _cache.Count;
        }

        public void Update(string key, IBaseContainerBag value)
        {
            _cache[key] = value;
        }

        #endregion
    }
}
