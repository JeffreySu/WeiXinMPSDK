using Senparc.Weixin.MessageQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.Threads
{
	public class LSWMessageQueueThreadUtility
	{
		private readonly int _sleepMilliSeconds;

		public LSWMessageQueueThreadUtility(int sleepMilliSeconds = 2000)
		{
			_sleepMilliSeconds = sleepMilliSeconds;
		}

		/// <summary>
		/// 析构函数，将未处理的列队处理掉
		/// </summary>
		~LSWMessageQueueThreadUtility()
		{
			try
			{
				var mq = new SenparcMessageQueue();
#if dnx451
				System.Diagnostics.Trace.WriteLine(string.Format("LSWMessageQueueThreadUtility执行析构函数"));
				System.Diagnostics.Trace.WriteLine(string.Format("当前列队数量：{0}", mq.GetCount()));
#endif
				SenparcMessageQueue.OperateQueue();//处理列队
			}
			catch (Exception ex)
			{
#if dnx451
				//此处可以添加日志
				System.Diagnostics.Trace.WriteLine(string.Format("LSWMessageQueueThreadUtility执行析构函数错误：{0}", ex.Message));
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