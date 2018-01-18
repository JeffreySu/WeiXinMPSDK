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

    文件名：ThreadUtility.cs
    文件功能描述：线程工具类


    创建标识：Senparc - 20151226

----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Senparc.Weixin.Threads
{
    /// <summary>
    /// 线程处理类
    /// </summary>
    public static class ThreadUtility
    {
        /// <summary>
        /// 异步线程容器
        /// </summary>
        public static Dictionary<string, Thread> AsynThreadCollection = new Dictionary<string, Thread>();//后台运行线程

        /// <summary>
        /// 注册线程
        /// </summary>
        public static void Register()
        {
            if (AsynThreadCollection.Count==0)
            {
                {
                    SenparcMessageQueueThreadUtility senparcMessageQueue = new SenparcMessageQueueThreadUtility();
                    Thread senparcMessageQueueThread = new Thread(senparcMessageQueue.Run) { Name = "SenparcMessageQueue" };
                    AsynThreadCollection.Add(senparcMessageQueueThread.Name, senparcMessageQueueThread);
                }

                AsynThreadCollection.Values.ToList().ForEach(z =>
                {
                    z.IsBackground = true;
                    z.Start();
                });//全部运行

            }
        }
    }
}
