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
    
    文件名：SenparcMessageQueueItem.cs
    文件功能描述：SenparcMessageQueue消息队列项
    
    
    创建标识：Senparc - 20151226
    
    
    ----  CO2NET   ----

    修改标识：Senparc - 20180601
    修改描述：v5.0.0 引入 Senparc.CO2NET

----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MessageQueue
{
    /// <summary>
    /// SenparcMessageQueue消息队列项
    /// </summary>
    public class SenparcMessageQueueItem : CO2NET.MessageQueue.SenparcMessageQueueItem
    {
        /// <summary>
        /// 初始化SenparcMessageQueue消息队列项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="description"></param>
        [Obsolete("请使用 Senparc.CO2NET.MessageQueue.SenparcMessageQueueItem 类")]
        public SenparcMessageQueueItem(string key, Action action, string description = null)
                : base(key, action, description)
        {

        }
    }
}
