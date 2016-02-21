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
    }

    /// <summary>
    /// 储存某个Container下所有ContainerBag的字典集合
    /// </summary>
    public class ContainerItemCollection : Dictionary<string, IBaseContainerBag>, IContainerItemCollection
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public ContainerItemCollection()
        {
            CreateTime = DateTime.Now;
        }

        public string CacheSetKey { get; set; }
        public void InsertToCache(string key, IBaseContainerBag value)
        {
            base[key] = value;
        }

        public void RemoveFromCache(string key)
        {
            base.Remove(key);
        }

        public IBaseContainerBag Get(string key)
        {
            if (this.CheckExisted(key))
            {
                return base[key];
            }
            return null;
        }

        public IDictionary<string, IBaseContainerBag> GetAll()
        {
            return this;
        }

        public bool CheckExisted(string key)
        {
            return base.ContainsKey(key);
        }

        public long GetCount()
        {
            return base.Count;
        }

        public void Update(string key, IBaseContainerBag value)
        {
            base[key] = value;
        }
    }
}
