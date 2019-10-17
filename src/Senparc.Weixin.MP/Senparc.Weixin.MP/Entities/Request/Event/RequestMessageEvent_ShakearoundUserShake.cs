#region Apache License Version 2.0
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
    
    文件名：RequestMessageEvent_ShakearoundUserShake.cs
    文件功能描述：事件之弹出微信相册发图器(ShakearoundUserShake)
    
    
    创建标识：Senparc - 20150914
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 事件之摇一摇事件通知(ShakearoundUserShake)
    /// </summary>
    public class RequestMessageEvent_ShakearoundUserShake : RequestMessageEventBase, IRequestMessageEventBase
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public override Event Event
        {
            get { return Event.ShakearoundUserShake; }
        }

        /// <summary>
        /// 最近的IBeacon信息
        /// </summary>
        public ChosenBeacon ChosenBeacon { get; set; }

        /// <summary>
        /// 附近的IBeacon信息
        /// </summary>
        public List<AroundBeacon> AroundBeacons { get; set; }
    }
}
