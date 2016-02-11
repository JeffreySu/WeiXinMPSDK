/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SenparcMessageQueueThreadUtility.cs
    文件功能描述：SenparcMessageQueue消息列队线程处理
    
    
    创建标识：Senparc - 20160210
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Senparc.Weixin.MessageQueue;

namespace Senparc.Weixin.Threads
{
    /// <summary>
    /// SenparcMessageQueue线程自动处理
    /// </summary>
    public class SenparcMessageQueueThreadUtility
    {
        private readonly int _sleepMilliSeconds;

        public SenparcMessageQueueThreadUtility(int sleepMilliSeconds = 2000)
        {
            _sleepMilliSeconds = sleepMilliSeconds;
        }

        /// <summary>
        /// 启动线程轮询
        /// </summary>
        public void Run()
        {
            do
            {
                var mq = new SenparcMessageQueue();
                var key = mq.GetCurrentKey();//获取最新的Key
                while (!string.IsNullOrEmpty(key))
                {
                    var mqItem = mq.GetItem(key);//获取任务项
                    mqItem.Action();//执行
                    mq.Remove(key);//清除
                    key = mq.GetCurrentKey();//获取最新的Key
                }
                Thread.Sleep(_sleepMilliSeconds);
            } while (true);
        }
    }
}
