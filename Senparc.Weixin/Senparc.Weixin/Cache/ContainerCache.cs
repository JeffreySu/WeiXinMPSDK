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
    /// 缓存类
    /// </summary>
    public class ContainerCache<T> : IBaseCacheStrategy<IBaseContainer<T>> where T : IBaseContainerBag, new()
    {
        public string CacheSetKey
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool CheckExisted(string key)
        {
            throw new NotImplementedException();
        }

        public IBaseContainer<T> Get(string key)
        {
            throw new NotImplementedException();
        }

        public IList<IBaseContainer<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public long GetCount()
        {
            throw new NotImplementedException();
        }

        public void InsertToCache(string key, IBaseContainer<T> value)
        {
            throw new NotImplementedException();
        }

        public void InsertToCache(string key, IBaseContainer<T> value, int timeout)
        {
            throw new NotImplementedException();
        }

        public void InsertToCache(string key, IBaseContainer<T> value, int timeout, CacheDependency dependencies)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromCache(string key)
        {
            throw new NotImplementedException();
        }
    }
}
