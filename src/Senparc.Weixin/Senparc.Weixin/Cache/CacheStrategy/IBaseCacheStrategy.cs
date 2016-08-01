using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Senparc.Weixin.Cache
{
    public interface IBaseCacheStrategy
    {
        /// <summary>
        /// 整个Cache集合的Key
        /// </summary>
        string CacheSetKey { get; set; }
    }

    /// <summary>
    /// 公共缓存策略接口
    /// </summary>
    public interface IBaseCacheStrategy<TKey, TValue> : IBaseCacheStrategy
        where TValue : class
    {
        /// <summary>
        /// 添加指定ID的对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        void InsertToCache(TKey key, TValue value);

        /// <summary>
        /// 移除指定缓存键的对象
        /// </summary>
        /// <param name="key">缓存键</param>
        void RemoveFromCache(TKey key);

        /// <summary>
        /// 返回指定缓存键的对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        TValue Get(TKey key);

        /// <summary>
        /// 获取所有缓存信息集合
        /// </summary>
        /// <returns></returns>
        IDictionary<TKey, TValue> GetAll();

        /// <summary>
        /// 检查是否存在Key及对象
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        bool CheckExisted(TKey key);

        /// <summary>
        /// 获取缓存集合总数（注意：每个缓存框架的计数对象不一定一致！）
        /// </summary>
        /// <returns></returns>
        long GetCount();

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        void Update(TKey key, TValue value);
    }
}
