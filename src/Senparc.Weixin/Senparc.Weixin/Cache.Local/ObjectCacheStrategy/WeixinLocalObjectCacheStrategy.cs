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

    文件名：LocalContainerCacheStrategy.cs
    文件功能描述：本地容器缓存。


    创建标识：Senparc - 20160308

    修改标识：Senparc - 20160812
    修改描述：v4.7.4  解决Container无法注册的问题

    修改标识：Senparc - 20170205
    修改描述：v0.2.0 重构分布式锁

    --CO2NET---

    修改标识：Senparc - 20180606
    修改描述：设置ChildNamespace


 ----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Cache;
using Senparc.CO2NET.Cache;

namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// 本地容器缓存策略
    /// </summary>
    public class WeixinLocalObjectCacheStrategy : LocalObjectCacheStrategy, IWeixinObjectCacheStrategy
    //where TContainerBag : class, IBaseContainerBag, new()
    {

        #region 单例

        /// <summary>
        /// LocalCacheStrategy的构造函数
        /// </summary>
        //LocalObjectCacheStrategy()
        //{
        //}

        //静态LocalCacheStrategy
        public new static  WeixinLocalObjectCacheStrategy Instance
        {
            get
            {
                return Nested.instance;//返回Nested类中的静态成员instance
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            //将instance设为一个初始化的LocalCacheStrategy新实例
            internal static readonly WeixinLocalObjectCacheStrategy instance = new WeixinLocalObjectCacheStrategy();
        }


        #endregion

        #region IWeixinObjectCacheStrategy 成员

        public IContainerCacheStrategy ContainerCacheStrategy
        {
            get { return LocalContainerCacheStrategy.Instance; }
        }

        #endregion


        public WeixinLocalObjectCacheStrategy()
        {
            base.ChildNamespace = "Weixin";//设置下级命名空间
        }
    }
}
