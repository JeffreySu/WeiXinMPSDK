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

    文件名：SessionContainer.cs
    文件功能描述：小程序 Session 容器


    创建标识：Senparc - 20171008

    修改标识：Senparc - 20180614
    修改描述：CO2NET v0.1.0 ContainerBag 取消属性变动通知机制，使用手动更新缓存
  
    修改标识：Senparc - 20180701
    修改描述：V2.0.3 SessionBag 添加 UnionId 属性

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.CO2NET.CacheUtility;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Utilities.WeixinUtility;
using Senparc.Weixin.WxOpen.Helpers;

namespace Senparc.Weixin.WxOpen.Containers
{
    /// <summary>
    /// 第三方APP信息包
    /// </summary>
    [Serializable]
    public class SessionBag : BaseContainerBag
    {
        /// <summary>
        /// Session的Key（3rd_session / sessionId）
        /// </summary>
        new public string Key { get; set; }
        //        {
        //            get { return _key; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _key, value, "Key"); }
        //#else
        //            set { this.SetContainerProperty(ref _key, value); }
        //#endif
        //        }

        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }
        //        {
        //            get { return _openId; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _openId, value, "OpenId"); }
        //#else
        //            set { this.SetContainerProperty(ref _openId, value); }
        //#endif
        //        }

        public string UnionId { get; set; }

        /// <summary>
        /// SessionKey
        /// </summary>
        public string SessionKey { get; set; }
        //        {
        //            get { return _sessionKey; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _sessionKey, value, "SessionKey"); }
        //#else
        //            set { this.SetContainerProperty(ref _sessionKey, value); }
        //#endif
        //        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
        //        {
        //            get { return _expireTime; }
        //#if NET35 || NET40
        //            set { this.SetContainerProperty(ref _expireTime, value, "ExpireTime"); }
        //#else
        //            set { this.SetContainerProperty(ref _expireTime, value); }
        //#endif
        //        }

        //private string _key;
        //private string _openId;
        //private string _sessionKey;
        //private DateTime _expireTime;

        /// <summary>
        /// ComponentBag
        /// </summary>
        public SessionBag()
        {
        }
    }


    /// <summary>
    /// 3rdSession容器
    /// </summary>
    public class SessionContainer : BaseContainer<SessionBag>
    {
        /// <summary>
        /// 获取最新的过期时间
        /// </summary>
        /// <returns></returns>
        private static TimeSpan GetExpireTime()
        {
            return TimeSpan.FromDays(2);//有效期2天
        }

        #region 同步方法

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static SessionBag GetSession(string key)
        {
            var bag = TryGetItem(key);
            if (bag == null)
            {
                return null;
            }

            if (bag.ExpireTime < DateTime.Now)
            {
                //已经过期
                Cache.RemoveFromCache(key);
                return null;
            }

            //using (FlushCache.CreateInstance())
            //{
            bag.ExpireTime = DateTime.Now.Add(GetExpireTime());//滚动过期时间
            Update(key, bag, GetExpireTime());
            //}
            return bag;
        }

        /// <summary>
        /// 更新或插入SessionBag
        /// </summary>
        /// <param name="key">如果留空，则新建一条记录</param>
        /// <param name="openId">OpenId</param>
        /// <param name="sessionKey">SessionKey</param>
        /// <param name="uniondId">UnionId</param>
        /// <returns></returns>
        public static SessionBag UpdateSession(string key, string openId, string sessionKey, string uniondId)
        {
            key = key ?? SessionHelper.GetNewThirdSessionName();

            //using (FlushCache.CreateInstance())
            //{
            var sessionBag = new SessionBag()
            {
                Key = key,
                OpenId = openId,
                UnionId = uniondId,
                SessionKey = sessionKey,
                ExpireTime = DateTime.Now.Add(GetExpireTime())
            };
            Update(key, sessionBag, GetExpireTime());
            return sessionBag;
            //}
        }

        #endregion

    }
}
