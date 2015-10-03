/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：ComponentTicketContainer.cs
    文件功能描述：ComponentTicket容器，用于储存和快速查询各个第三方APP的ComponentTicket
                 （注意：是开发者在开放平台申请的“第三方公众平台”，不是用户授权的公众号）
    
    
    创建标识：Senparc - 20151003
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Containers;

namespace Senparc.Weixin.Open.CommonAPIs
{
    /// <summary>
    /// ComponentTicketBag
    /// </summary>
    public class ComponentTicketBag : BaseContainerBag
    {
        /// <summary>
        /// AppId
        /// </summary>
        public string ComponentAppId { get; set; }
        /// <summary>
        /// ComponentVerifyTicket
        /// </summary>
        public string ComponentVerifyTicket { get; set; }
        ///// <summary>
        ///// 只针对这个AppId的锁
        ///// </summary>
        //public object Lock = new object();
    }

    /// <summary>
    /// ComponentTicket容器
    /// </summary>
    public class ComponentTicketContainer : BaseContainer<ComponentTicketBag>
    {
        /// <summary>
        /// 获取ComponentVerifyTicket
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <returns>如果不存在，则返回null</returns>
        public static string TryGetComponentVerifyTicket(string componentAppId)
        {
            return TryGetItem(componentAppId, bag => bag.ComponentVerifyTicket);
        }

        /// <summary>
        /// 更新ComponentVerifyTicket信息
        /// </summary>
        /// <param name="componentAppId"></param>
        /// <param name="componentVerifyTicket"></param>
        public static void Update(string componentAppId, string componentVerifyTicket)
        {
            Update(componentAppId, bag =>
            {
                bag.ComponentVerifyTicket = componentVerifyTicket;
            });
        }
    }
}
