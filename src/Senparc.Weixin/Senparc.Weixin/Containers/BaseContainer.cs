/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

    文件名：WeixinContainer.cs
    文件功能描述：微信容器（如Ticket、AccessToken）


    创建标识：Senparc - 20151003

    修改标识：Senparc - 20160321
    修改描述：v4.5.18 完善 ItemCollection 中项目删除的方法

    修改标识：Senparc - 20160808
    修改描述：v4.7.0 删除 ItemCollection 属性，直接使用ContainerBag加入到缓存

    修改标识：Senparc - 20160813
    修改描述：v4.7.5 添加TryReRegister()方法，处理分布式缓存重启（丢失）的情况

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Helpers;
using Senparc.Weixin.MessageQueue;

namespace Senparc.Weixin.Containers
{
    /// <summary>
    /// IBaseContainer
    /// </summary>
    public interface IBaseContainer
    {
    }

    /// <summary>
    /// 带IBaseContainerBag泛型的IBaseContainer
    /// </summary>
    /// <typeparam name="TBag"></typeparam>
    public interface IBaseContainer<TBag> : IBaseContainer where TBag : IBaseContainerBag, new()
    {
    }

    /// <summary>
    /// 微信容器接口（如Ticket、AccessToken）
    /// </summary>
    /// <typeparam name="TBag"></typeparam>
    [Serializable]
    public abstract class BaseContainer<TBag> : IBaseContainer<TBag> where TBag : class, IBaseContainerBag, new()
    {
        /// <summary>
        /// 获取符合当前缓存策略配置的缓存的操作对象实例
        /// </summary>
        protected static IContainerCacheStrategy /*IBaseCacheStrategy<string,Dictionary<string, TBag>>*/ Cache
        {
            get
            {
                //使用工厂模式或者配置进行动态加载
                //return CacheStrategyFactory.GetContainerCacheStrategyInstance();
                return CacheStrategyFactory.GetObjectCacheStrategyInstance().ContainerCacheStrategy;
            }
        }

        //2016.8.8注释掉
        /// <summary>
        /// 获取当前容器的数据项集合
        /// </summary>
        /// <returns></returns>
        //protected static IContainerItemCollection ItemCollection
        //{
        //    get
        //    {
        //        var cacheKey = GetContainerCacheKey();
        //        IContainerItemCollection itemCollection;
        //        if (!Cache.CheckExisted(cacheKey))
        //        {
        //            itemCollection = new ContainerItemCollection();
        //            //CollectionList[cacheKey] = newItemCollection;

        //            //直接执行
        //            //{
        //            //}
        //            //var containerCacheStrategy = CacheStrategyFactory.GetContainerCacheStrategyInstance();
        //            //containerCacheStrategy.InsertToCache(cacheKey, itemCollection);//插入到缓存

        //            //保存到缓存列队，等待执行
        //            SenparcMessageQueue mq = new SenparcMessageQueue();
        //            var mqKey = SenparcMessageQueue.GenerateKey("ContainerItemCollection", typeof(BaseContainer<TBag>), cacheKey, "InsertItemCollection");
        //            mq.Add(mqKey, () =>
        //            {
        //                var containerCacheStrategy = CacheStrategyFactory.GetContainerCacheStrategyInstance();
        //                containerCacheStrategy.InsertToCache(cacheKey, itemCollection);//插入到缓存
        //            });
        //        }
        //        else
        //        {
        //            itemCollection = Cache.Get(cacheKey);
        //        }

        //        return itemCollection;
        //    }
        //}



        ///// <summary>
        ///// 获取Container缓存Key
        ///// </summary>
        ///// <returns></returns>
        //public static string GetContainerCacheKey()
        //{
        //    return ContainerHelper.GetCacheKey(typeof(TBag));
        //}



        /// <summary>
        /// 进行注册过程的委托
        /// </summary>
        protected static Func<TBag> RegisterFunc { get; set; }

        /// <summary>
        /// 如果注册不成功，测尝试重新注册（前提是已经进行过注册），这种情况适用于分布式缓存被清空（重启）的情况。
        /// </summary>
        private static TBag TryReRegister()
        {
            return RegisterFunc();
            //TODO:如果需要校验ContainerBag的正确性，可以从返回值进行判断
        }

        /// <summary>
        /// 返回已经注册的第一个AppId
        /// </summary>
        /// <returns></returns>
        public static string GetFirstOrDefaultAppId()
        {
            var firstBag = GetAllItems().FirstOrDefault() as IBaseContainerBag_AppId;
            return firstBag == null ? null : firstBag.AppId;
        }

        /// <summary>
        /// 获取ItemCollection缓存Key
        /// </summary>
        /// <param name="shortKey">最简短的Key，比如AppId，不需要考虑容器前缀</param>
        /// <returns></returns>
        public static string GetBagCacheKey(string shortKey)
        {
            return ContainerHelper.GetItemCacheKey(typeof(TBag), shortKey);
        }


        ///// <summary>
        ///// 获取完整的数据集合的列表，包括所有的Container数据在内（建议不要进行任何修改操作）
        ///// </summary>
        ///// <returns></returns>
        //public static IDictionary<string, IContainerItemCollection> GetCollectionList()
        //{
        //    return CollectionList;
        //}

        /// <summary>
        /// 获取所有容器内已经注册的项目
        /// （此方法将会遍历Dictionary，当数据项很多的时候效率会明显降低）
        /// </summary>
        /// <returns></returns>
        public static List<TBag> GetAllItems()
        {
            return Cache.GetAll<TBag>().Values.Select(z => z).ToList();
        }

        /// <summary>
        /// 尝试获取某一项Bag
        /// </summary>
        /// <param name="shortKey"></param>
        /// <returns></returns>
        public static TBag TryGetItem(string shortKey)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (Cache.CheckExisted(cacheKey))
            {
                return (TBag)Cache.Get(cacheKey);
            }

            return default(TBag);
        }

        /// <summary>
        /// 尝试获取某一项Bag中的具体某个属性
        /// </summary>
        /// <param name="shortKey"></param>
        /// <param name="property">具体某个属性</param>
        /// <returns></returns>
        public static TK TryGetItem<TK>(string shortKey, Func<TBag, TK> property)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (Cache.CheckExisted(cacheKey))
            {
                var item = Cache.Get(cacheKey) as TBag;
                return property(item);
            }
            return default(TK);
        }

        /// <summary>
        /// 更新数据项
        /// </summary>
        /// <param name="shortKey"></param>
        /// <param name="bag">为null时删除该项</param>
        public static void Update(string shortKey, TBag bag)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (bag == null)
            {
                Cache.RemoveFromCache(cacheKey);
            }
            else
            {
                if (string.IsNullOrEmpty(bag.Key))
                {
                    bag.Key = shortKey;//确保Key有值，形如：wx669ef95216eef885，最底层的Key
                }
                //else
                //{
                //    cacheKey = bag.Key;//统一key
                //}

                //if (string.IsNullOrEmpty(cacheKey))
                //{
                //    throw new WeixinException("key和value,Key不可以同时为null或空字符串！");
                //}

                //var c1 = ItemCollection.GetCount();
                //ItemCollection[key] = bag;
                //var c2 = ItemCollection.GetCount();
            }
            //var containerCacheKey = GetContainerCacheKey();
            Cache.Update(cacheKey, bag);//更新到缓存，TODO：有的缓存框架可一直更新Hash中的某个键值对
        }

        /// <summary>
        /// 更新数据项
        /// </summary>
        /// <param name="shortKey"></param>
        /// <param name="partialUpdate">为null时删除该项</param>
        public static void Update(string shortKey, Action<TBag> partialUpdate)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (partialUpdate == null)
            {
                Cache.RemoveFromCache(cacheKey);//移除对象
            }
            else
            {
                if (!Cache.CheckExisted(cacheKey))
                {
                    var newBag = new TBag()
                    {
                        Key = cacheKey//确保这一项Key已经被记录
                    };

                    Cache.InsertToCache(cacheKey, newBag);
                }
                partialUpdate(TryGetItem(shortKey));//更新对象
            }
        }

        /// <summary>
        /// 检查Key是否已经注册
        /// </summary>
        /// <param name="shortKey"></param>
        /// <returns></returns>
        public static bool CheckRegistered(string shortKey)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            var registered = Cache.CheckExisted(cacheKey);
            if (!registered && RegisterFunc != null)
            {
                //如果注册不成功，测尝试重新注册（前提是已经进行过注册），这种情况适用于分布式缓存被清空（重启）的情况。
                TryReRegister();
            }

            return Cache.CheckExisted(cacheKey);
        }

    }
}
