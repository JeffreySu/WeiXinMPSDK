#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc

    文件名：IContainerCacheStrategy.cs
    文件功能描述：容器缓存策略基类。


    创建标识：Senparc - 20160308

    修改标识：Senparc - 20160812
    修改描述：v4.7.4  解决Container无法注册的问题

 ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.CO2NET.Cache;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// 容器缓存策略接口（属于扩展领域缓存）
    /// </summary>
    public interface IContainerCacheStrategy : IDomainExtensionCacheStrategy /*: IBaseCacheStrategy<string, IBaseContainerBag>*/
    {
        /// <summary>
        /// 获取所有ContainerBag
        /// </summary>
        /// <typeparam name="TBag"></typeparam>
        /// <returns></returns>
        IDictionary<string, TBag> GetAll<TBag>() where TBag : IBaseContainerBag;

        /// <summary>
        /// 获取单个ContainerBag
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="isFullKey">是否已经是完整的Key，如果不是，则会调用一次GetFinalKey()方法</param>
        /// <returns></returns>
        TBag GetContainerBag<TBag>(string key, bool isFullKey = false) where TBag : IBaseContainerBag;

        /// <summary>
        /// 更新ContainerBag
        /// </summary>
        /// <param name="key"></param>
        /// <param name="containerBag"></param>
        /// <param name="expiry">超时时间</param>
        /// <param name="isFullKey">是否已经是完整的Key，如果不是，则会调用一次GetFinalKey()方法</param>
        void UpdateContainerBag(string key, IBaseContainerBag containerBag, TimeSpan? expiry = null, bool isFullKey = false);
    }
}
