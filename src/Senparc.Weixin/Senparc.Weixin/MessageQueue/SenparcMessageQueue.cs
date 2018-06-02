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

    文件名：SenparcMessageQueue.cs
    文件功能描述：SenparcMessageQueue消息队列


    创建标识：Senparc - 20151226

    修改标识：Senparc - 20160210
    修改描述：v4.5.10 取消MessageQueueList，使用MessageQueueDictionary.Keys记录标示
              （使用MessageQueueDictionary.Keys会可能会使储存项目的无序执行）


    ----  CO2NET   ----

    修改标识：Senparc - 20180601
    修改描述：v5.0.0 引入 Senparc.CO2NET

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;

namespace Senparc.Weixin.MessageQueue
{
    /// <summary>
    /// 消息队列
    /// </summary>
    [Obsolete("请使用 Senparc.CO2NET.MessageQueue.SenparcMessageQueue 类")]
    public class SenparcMessageQueue : CO2NET.MessageQueue.SenparcMessageQueue
    {
      
    }
}
