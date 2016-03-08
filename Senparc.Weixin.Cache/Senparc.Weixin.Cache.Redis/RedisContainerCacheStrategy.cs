using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache.Redis
{
    /// <summary>
    /// Redis容器缓存策略
    /// </summary>
    public sealed class RedisContainerCacheStrategy : IContainerCacheStragegy
    {
        #region 实现 IContainerCacheStragegy 接口

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

        public IContainerItemCollection Get(string key)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, IContainerItemCollection> GetAll()
        {
            throw new NotImplementedException();
        }

        public long GetCount()
        {
            throw new NotImplementedException();
        }

        public void InsertToCache(string key, IContainerItemCollection value)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromCache(string key)
        {
            throw new NotImplementedException();
        }

        public void Update(string key, IContainerItemCollection value)
        {
            throw new NotImplementedException();
        }

        public void UpdateContainerBag(string key, IBaseContainerBag containerBag)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
