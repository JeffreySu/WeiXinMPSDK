﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：WeixinContainer.cs
    文件功能描述：微信容器（如Ticket、AccessToken）


    创建标识：Senparc - 20151003

    修改标识：Senparc - 20160321
    修改描述：v4.5.18 完善 ItemCollection 中项目删除的方法

    修改标识：Senparc - 20160808
    修改描述：v4.7.0 删除 ItemCollection 属性，直接使用ContainerBag加入到缓存

    修改标识：Senparc - 20160813
    修改描述：v4.7.5 添加TryReRegister()方法，处理分布式缓存重启（丢失）的情况
    
    修改标识：Senparc - 20170204
    修改描述：v4.10.3 添加RemoveFromCache方法

    修改标识：Senparc - 20180606
    修改描述：缓存工厂重命名为 ContainerCacheStrategyFactory

    修改标识：Senparc - 20180614
    修改描述：CO2NET v0.1.0 ContainerBag 取消属性变动通知机制，使用手动更新缓存

    修改标识：Senparc - 20180707
    修改描述：Update() 方法中记录缓存时间 bag.CacheTime = DateTime.Now;

    修改标识：Senparc - 20180917
    修改描述：BaseContainer.GetFirstOrDefaultAppId() 方法添加 PlatformType 属性

    修改标识：Senparc - 20170522
    修改描述：v6.3.2 修改 DateTime 为 DateTimeOffset

    修改标识：Senparc - 20190418
    修改描述：v17.0.0 支持异步 Container

----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.CO2NET.Cache;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.Helpers;

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
        private static IBaseObjectCacheStrategy _baseCache = null;
        private static IContainerCacheStrategy _containerCache = null;

        /// <summary>
        /// 获取符合当前缓存策略配置的缓存的操作对象实例
        /// </summary>
        protected static IBaseObjectCacheStrategy /*IBaseCacheStrategy<string,Dictionary<string, TBag>>*/ Cache
        {
            get
            {
                //使用工厂模式或者配置进行动态加载
                //return CacheStrategyFactory.GetContainerCacheStrategyInstance();

                //以下代码可以实现缓存“热切换”，损失的效率有限。如果需要追求极致效率，可以禁用type的判断
                var containerCacheStrategy = ContainerCacheStrategyFactory.GetContainerCacheStrategyInstance()/*.ContainerCacheStrategy*/;
                if (_containerCache == null || _containerCache.GetType() != containerCacheStrategy.GetType())
                {
                    _containerCache = containerCacheStrategy;
                }

                if (_baseCache == null)
                {
                    _baseCache = _baseCache ?? containerCacheStrategy.BaseCacheStrategy();
                }
                return _baseCache;
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

        //            //保存到缓存队列，等待执行
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
        /// 进行注册过程的委托集合
        /// </summary>
        protected static Dictionary<string, Func<Task<TBag>>> RegisterFuncCollection { get; set; } = new Dictionary<string, Func<Task<TBag>>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 如果注册不成功，测尝试重新注册（前提是已经进行过注册），这种情况适用于分布式缓存被清空（重启）的情况。
        /// <param name="shortKey">最简短的Key，比如AppId，不需要考虑容器前缀</param>
        /// </summary>
        private static async Task<TBag> TryReRegister(string shortKey)
        {
            if (shortKey == null)
            {
                throw new ArgumentNullException(nameof(shortKey));
            }

            if (RegisterFuncCollection.TryGetValue(shortKey, out var registerFunc))
            {
                return await registerFunc();
            }

            return null;
            //TODO:如果需要校验ContainerBag的正确性，可以从返回值进行判断
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


        #region 同步方法

        /// <summary>
        /// 返回已经注册的第一个AppId
        /// </summary>
        /// <returns></returns>
        public static string GetFirstOrDefaultAppId(PlatformType platformType)
        {
            string appId = null;
            switch (platformType)
            {
                case PlatformType.MP:
                    appId = Senparc.Weixin.Config.SenparcWeixinSetting.WeixinAppId;
                    break;
                case PlatformType.Open:
                    appId = Senparc.Weixin.Config.SenparcWeixinSetting.WeixinAppId;
                    break;
                case PlatformType.WxOpen:
                    appId = Senparc.Weixin.Config.SenparcWeixinSetting.WxOpenAppId;
                    break;
                case PlatformType.QY:
                    break;
                case PlatformType.Work:
                    break;
                default:
                    break;
            }

            if (appId == null)
            {
                var firstBag = GetAllItems().FirstOrDefault() as IBaseContainerBag_AppId;
                appId = firstBag == null ? null : firstBag.AppId;
            }

            return appId;
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
            //return Cache.GetAll<TBag>().Values
            return _containerCache.GetAll<TBag>().Values
                //如果需要做进一步的筛选，则使用Select或Where，但需要注意效率问题
                //.Select(z => z)
                .ToList();
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
                return Cache.Get<TBag>(cacheKey);
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
                var item = Cache.Get<TBag>(cacheKey);
                return property(item);
            }
            return default(TK);
        }

        /// <summary>
        /// 更新数据项
        /// </summary>
        /// <param name="shortKey">即bag.Key</param>
        /// <param name="bag">为null时删除该项</param>
        /// <param name="expiry"></param>
        public static void Update(string shortKey, TBag bag, TimeSpan? expiry)
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

            bag.CacheTime = SystemTime.Now;

            Cache.Update(cacheKey, bag, expiry);//更新到缓存，TODO：有的缓存框架可一直更新Hash中的某个键值对
        }

        /// <summary>
        /// 更新已经添加过的数据项
        /// </summary>
        /// <param name="bag">为null时删除该项</param>
        /// <param name="expiry"></param>
        public static void Update(TBag bag, TimeSpan? expiry)
        {
            if (string.IsNullOrEmpty(bag.Key))
            {
                throw new WeixinException("ContainerBag 更新时，ey 不能为空！类型：" + bag.GetType());
            }

            Update(bag.Key, bag, expiry);
        }

        /// <summary>
        /// 更新数据项（本地缓存不会改变原有值的 HashCode）
        /// </summary>
        /// <param name="shortKey"></param>
        /// <param name="partialUpdate">为null时删除该项</param>
        /// <param name="expiry"></param>
        public static void Update(string shortKey, Action<TBag> partialUpdate, TimeSpan? expiry)
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

                    Cache.Set(cacheKey, newBag, expiry);
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
            if (!registered && RegisterFuncCollection.ContainsKey(shortKey))
            {
                //如果注册不成功，测尝试重新注册（前提是已经进行过注册），这种情况适用于分布式缓存被清空（重启）的情况。
                RegisterFuncCollection[shortKey]().GetAwaiter().GetResult();//使用同步方法返回
            }

            return Cache.CheckExisted(cacheKey);
        }

        /// <summary>
        /// 从缓存中删除指定项
        /// </summary>
        /// <param name="shortKey"></param>
        public static void RemoveFromCache(string shortKey)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            Cache.RemoveFromCache(cacheKey);
        }

        #endregion

        #region 异步方法


        /// <summary>
        /// 返回已经注册的第一个AppId
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetFirstOrDefaultAppIdAsync(PlatformType platformType)
        {
            string appId = null;
            switch (platformType)
            {
                case PlatformType.MP:
                    appId = Senparc.Weixin.Config.SenparcWeixinSetting.WeixinAppId;
                    break;
                case PlatformType.Open:
                    appId = Senparc.Weixin.Config.SenparcWeixinSetting.WeixinAppId;
                    break;
                case PlatformType.WxOpen:
                    appId = Senparc.Weixin.Config.SenparcWeixinSetting.WxOpenAppId;
                    break;
                case PlatformType.QY:
                    break;
                case PlatformType.Work:
                    break;
                default:
                    break;
            }

            if (appId == null)
            {
                var firstBag = (await GetAllItemsAsync()).FirstOrDefault() as IBaseContainerBag_AppId;
                appId = firstBag == null ? null : firstBag.AppId;
            }

            return appId;
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
        public static async Task<List<TBag>> GetAllItemsAsync()
        {
            //return Cache.GetAll<TBag>().Values
            return (await _containerCache.GetAllAsync<TBag>()).Values
                //如果需要做进一步的筛选，则使用Select或Where，但需要注意效率问题
                //.Select(z => z)
                .ToList();
        }



        /// <summary>
        /// 尝试获取某一项Bag
        /// </summary>
        /// <param name="shortKey"></param>
        /// <returns></returns>
        public static async Task<TBag> TryGetItemAsync(string shortKey)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (await Cache.CheckExistedAsync(cacheKey))
            {
                return await Cache.GetAsync<TBag>(cacheKey);
            }

            return default(TBag);
        }

        /// <summary>
        /// 尝试获取某一项Bag中的具体某个属性
        /// </summary>
        /// <param name="shortKey"></param>
        /// <param name="property">具体某个属性</param>
        /// <returns></returns>
        public static async Task<TK> TryGetItemAsync<TK>(string shortKey, Func<TBag, TK> property)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (await Cache.CheckExistedAsync(cacheKey))
            {
                var item = await Cache.GetAsync<TBag>(cacheKey);
                return property(item);
            }
            return default(TK);
        }

        /// <summary>
        /// 更新数据项
        /// </summary>
        /// <param name="shortKey">即bag.Key</param>
        /// <param name="bag">为null时删除该项</param>
        /// <param name="expiry"></param>
        public static async Task UpdateAsync(string shortKey, TBag bag, TimeSpan? expiry)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (bag == null)
            {
                await Cache.RemoveFromCacheAsync(cacheKey);
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

            bag.CacheTime = SystemTime.Now;

            await Cache.UpdateAsync(cacheKey, bag, expiry);//更新到缓存，TODO：有的缓存框架可一直更新Hash中的某个键值对
        }

        /// <summary>
        /// 更新已经添加过的数据项
        /// </summary>
        /// <param name="bag">为null时删除该项</param>
        /// <param name="expiry"></param>
        public static async Task UpdateAsync(TBag bag, TimeSpan? expiry)
        {
            if (string.IsNullOrEmpty(bag.Key))
            {
                throw new WeixinException("ContainerBag 更新时，ey 不能为空！类型：" + bag.GetType());//TODO：使用异步异常抛出方式
            }

            await UpdateAsync(bag.Key, bag, expiry);
        }

        /// <summary>
        /// 更新数据项（本地缓存不会改变原有值的 HashCode）
        /// </summary>
        /// <param name="shortKey"></param>
        /// <param name="partialUpdate">为null时删除该项</param>
        /// <param name="expiry"></param>
        public static async Task UpdateAsync(string shortKey, Action<TBag> partialUpdate, TimeSpan? expiry)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            if (partialUpdate == null)
            {
                await Cache.RemoveFromCacheAsync(cacheKey);//移除对象
            }
            else
            {
                if (!await Cache.CheckExistedAsync(cacheKey))
                {
                    var newBag = new TBag()
                    {
                        Key = cacheKey//确保这一项Key已经被记录
                    };

                    await Cache.SetAsync(cacheKey, newBag, expiry);
                }
                partialUpdate(await TryGetItemAsync(shortKey));//更新对象
            }
        }

        /// <summary>
        /// 检查Key是否已经注册
        /// </summary>
        /// <param name="shortKey"></param>
        /// <returns></returns>
        public static async Task<bool> CheckRegisteredAsync(string shortKey)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            var registered = await Cache.CheckExistedAsync(cacheKey);

            if (!registered && RegisterFuncCollection.ContainsKey(shortKey))
            {
                //如果注册不成功，测尝试重新注册（前提是已经进行过注册），这种情况适用于分布式缓存被清空（重启）的情况。
                await RegisterFuncCollection[shortKey]();//使用异步方法返回
            }

            return await Cache.CheckExistedAsync(cacheKey);
        }

        /// <summary>
        /// 从缓存中删除指定项
        /// </summary>
        /// <param name="shortKey"></param>
        public static async Task RemoveFromCacheAsync(string shortKey)
        {
            var cacheKey = GetBagCacheKey(shortKey);
            await Cache.RemoveFromCacheAsync(cacheKey);
        }

        #endregion

    }
}
