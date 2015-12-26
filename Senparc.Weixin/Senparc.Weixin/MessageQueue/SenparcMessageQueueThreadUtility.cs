using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.MessageQueue
{
    public class SenparcMessageQueueThreadUtility
    {

        ///// <summary>
        ///// 同步执行锁
        ///// </summary>
        //private object syncLock = new object();//锁

        //private Semaphore _semaphorePool;
        //private int _semaphorePoolPreviousCount;
        //private int semaphorePoolPreviousCount
        //{
        //    get
        //    {
        //        lock (syncLock)
        //        {
        //            return _semaphorePoolPreviousCount;
        //        }
        //    }
        //    set
        //    {
        //        lock (syncLock)
        //        {
        //            _semaphorePoolPreviousCount = value;
        //        }
        //    }
        //}

        private readonly int _sleepMilliSeconds;

        public SenparcMessageQueueThreadUtility(int sleepMilliSeconds = 2000)
        {
            _sleepMilliSeconds = sleepMilliSeconds;
        }

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
