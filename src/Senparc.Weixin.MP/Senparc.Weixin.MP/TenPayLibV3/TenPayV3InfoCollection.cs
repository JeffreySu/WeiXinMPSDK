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

    文件名：TenPayV3InfoCollection.cs
    文件功能描述：微信支付V3信息集合，Key为商户号（MchId）


    创建标识：Senparc - 20150211

    修改标识：Senparc - 20150303
    修改描述：整理接口

    TODO：升级为Container
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.MP.TenPayLibV3
{
    /// <summary>
    /// 微信支付信息集合，Key为商户号（MchId）
    /// </summary>
    public class TenPayV3InfoCollection : Dictionary<string, TenPayV3Info>
    {
        /// <summary>
        /// 微信支付信息集合，Key为商户号（MchId）
        /// </summary>
        public static TenPayV3InfoCollection Data = new TenPayV3InfoCollection();

        /// <summary>
        /// 注册TenPayV3Info信息
        /// </summary>
        /// <param name="tenPayV3Info"></param>
        public static void Register(TenPayV3Info tenPayV3Info)
        {
            Data[tenPayV3Info.MchId] = tenPayV3Info;
        }

        public new TenPayV3Info this[string key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    throw new WeixinException(string.Format("TenPayV3InfoCollection尚未注册Mch：{0}", key));
                }
                else
                {
                    return base[key];
                }
            }
            set
            {
                base[key] = value;
            }
        }

        /// <summary>
        /// TenPayV3InfoCollection 构造函数
        /// </summary>
        public TenPayV3InfoCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {

        }
    }
}
