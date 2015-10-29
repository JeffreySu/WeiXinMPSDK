using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// 公共缓存策略接口
    /// </summary>
    public interface IBaseCacheStrategy<T> where T : class
    {
        /// <summary>
        /// 整个Cache集合的Key
        /// </summary>
        string CacheSetKey { get; set; }
        
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        void InsertToCache(string key, T value);
      
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="timeout">单位：分</param>
        void InsertToCache(string key, T value, int timeout);
      
        /// <summary>
        /// 添加指定ID的对象，使用依赖项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="timeout">单位：分</param>
        /// <param name="dependencies">依赖项</param>
        void InsertToCache(string key, T value, int timeout, CacheDependency dependencies);

        /// <summary>
        /// 移除指定缓存键的对象
        /// </summary>
        /// <param name="key">缓存键</param>
        void RemoveFromCache(string key);

        /// <summary>
        /// 返回指定缓存键的对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        T Get(string key);

        /// <summary>
        /// 获取所有细信息
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        /// <summary>
        /// 检查是否存在Key及对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool CheckExisted(string key);

        long GetCount();
    }
}
