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
        /// 析构函数，将未处理的列队处理掉
        /// </summary>
        ~SenparcMessageQueueThreadUtility()
        {
            try
            {
                var mq = new SenparcMessageQueue();
                System.Diagnostics.Trace.WriteLine(string.Format("SenparcMessageQueueThreadUtility执行析构函数"));
                System.Diagnostics.Trace.WriteLine(string.Format("当前列队数量：{0}", mq.GetCount()));

                SenparcMessageQueue.OperateQueue();//处理列队
            }
            catch (Exception ex)
            {
                //此处可以添加日志
                System.Diagnostics.Trace.WriteLine(string.Format("SenparcMessageQueueThreadUtility执行析构函数错误：{0}", ex.Message));
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
