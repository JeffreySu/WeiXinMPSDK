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
    
    文件名：BaseContainerBag.cs
    文件功能描述：微信容器接口中的封装Value（如Ticket、AccessToken等数据集合）
    
    
    创建标识：Senparc - 20151003

    修改标识：Senparc - 20160717
    修改描述：v4.5.21 添加BaseContainerBag.Name属性

    修改标识：Senparc - 20180606
    修改描述：缓存工厂重命名为 ContainerCacheStrategyFactory

    修改标识：Senparc - 20180614
    修改描述：CO2NET v0.1.0 ContainerBag 取消属性变动通知机制，使用手动更新缓存

----------------------------------------------------------------*/




using System;
using System.Runtime.CompilerServices;
using Senparc.CO2NET.MessageQueue;
using Senparc.Weixin.Annotations;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Helpers;

namespace Senparc.Weixin.Containers
{
    /// <summary>
    /// IBaseContainerBag，BaseContainer容器中的Value类型
    /// </summary>
    public interface IBaseContainerBag
    {
        /// <summary>
        /// 用于标记，方便后台管理
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 缓存键，形如：wx669ef95216eef885，最底层的Key，不考虑命名空间等
        /// </summary>
        string Key { get; set; }
        /// <summary>
        /// 当前对象被缓存的时间
        /// </summary>
        DateTime CacheTime { get; set; }
    }

    /// <summary>
    /// 提供给具有 AppId 的 IBaseContainerBag 使用的接口
    /// </summary>
    public interface IBaseContainerBag_AppId
    {
        /// <summary>
        /// AppId
        /// </summary>
        string AppId { get; set; }
    }

    /// <summary>
    /// BaseContainer容器中的Value类型
    /// </summary>
    [Serializable]
    public class BaseContainerBag : /*BindableBase, */IBaseContainerBag
    {
        //private string _key;
        //private string _name;

        /// <summary>
        /// 用于标记，方便后台管理
        /// </summary>
        public string Name { get; set; }
//        {
//            get { return _name; }
//#if NET35 || NET40
//            set { this.SetContainerProperty(ref _name, value, "Name"); }
//#else
//            set { this.SetContainerProperty(ref _name, value); }
//#endif
        //}

        /// <summary>
        /// 通常为AppId
        /// </summary>
        public string Key { get; set; }
//        {
//            get { return _key; }
//#if NET35 || NET40
//            set { this.SetContainerProperty(ref _key, value, "Key"); }
//#else
//            set { this.SetContainerProperty(ref _key, value); }
//#endif
        //}

        /// <summary>
        /// 缓存时间，不使用属性变化监听
        /// </summary>
        public DateTime CacheTime { get; set; }


//        private void BaseContainerBag_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
//        {
//            var containerBag = (IBaseContainerBag)sender;
//            var mqKey = SenparcMessageQueue.GenerateKey("ContainerBag", sender.GetType(), containerBag.Key, "UpdateContainerBag");

//            //获取对应Container的缓存相关

//            //加入消息队列，每过一段时间进行自动更新，防止属性连续被编辑，短时间内反复更新缓存。
//            SenparcMessageQueue mq = new SenparcMessageQueue();
//            mq.Add(mqKey, () =>
//            {
//                //var containerCacheStrategy = CacheStrategyFactory.GetContainerCacheStrategyInstance();
//                var containerCacheStrategy = ContainerCacheStrategyFactory.GetContainerCacheStrategyInstance()/*.ContainerCacheStrategy*/;
//                var itemCacheKey = ContainerHelper.GetItemCacheKey(containerBag);
//                containerBag.CacheTime = DateTime.Now;//记录缓存时间

//                //cacheKey形如:Container:Senparc.Weixin.MP.Containers.AccessTokenBag:wx669ef95216eef885
//                containerCacheStrategy.UpdateContainerBag(itemCacheKey, containerBag);
//            });
//        }


//        /// <summary>
//        /// 设置Container属性
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="storage"></param>
//        /// <param name="value"></param>
//        /// <param name="propertyName"></param>
//        /// <returns></returns>
//#if NET35 || NET40
//        protected bool SetContainerProperty<T>(ref T storage, T value, String propertyName)
//#else
//        protected bool SetContainerProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
//#endif
//        {
//            var result = base.SetProperty(ref storage, value, propertyName);
//            return result;
//        }

        public BaseContainerBag()
        {
            //base.PropertyChanged += BaseContainerBag_PropertyChanged;
        }
    }
}
