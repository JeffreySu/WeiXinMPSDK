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

    文件名：SenparcMessageQueueThreadUtility.cs
    文件功能描述：SenparcMessageQueue消息队列线程处理


    创建标识：Senparc - 20160210

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Senparc.Weixin.MessageQueue;

namespace Senparc.Weixin.Threads
{
    /// <summary>
    /// SenparcMessageQueue线程自动处理
    /// </summary>
    public class SenparcMessageQueueThreadUtility
    {
        private readonly int _sleepMilliSeconds;


        public SenparcMessageQueueThreadUtility(int sleepMilliSeconds = 1000)
        {
            _sleepMilliSeconds = sleepMilliSeconds;
        }

        /// <summary>
        /// 析构函数，将未处理的队列处理掉
        /// </summary>
        ~SenparcMessageQueueThreadUtility()
        {
            try
            {
                var mq = new SenparcMessageQueue();

#if NET35 || NET40 || NET45
                System.Diagnostics.Trace.WriteLine(string.Format("SenparcMessageQueueThreadUtility执行析构函数"));
                System.Diagnostics.Trace.WriteLine(string.Format("当前队列数量：{0}", mq.GetCount()));
#endif

                SenparcMessageQueue.OperateQueue();//处理队列
            }
            catch (Exception ex)
            {
                //此处可以添加日志
#if NET35 || NET40 || NET45

                System.Diagnostics.Trace.WriteLine(string.Format("SenparcMessageQueueThreadUtility执行析构函数错误：{0}", ex.Message));
#endif
            }
        }

        /// <summary>
        /// 启动线程轮询
        /// </summary>
        public void Run()
        {
            do
            {
                SenparcMessageQueue.OperateQueue();
                Thread.Sleep(_sleepMilliSeconds);
            } while (true);
        }
    }
}
