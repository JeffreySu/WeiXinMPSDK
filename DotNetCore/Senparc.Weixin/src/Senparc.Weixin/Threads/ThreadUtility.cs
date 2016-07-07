using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
			if (AsynThreadCollection.Count == 0)
			{
				{
					LSWMessageQueueThreadUtility LSWMessageQueue = new LSWMessageQueueThreadUtility();
					Thread LSWMessageQueueThread = new Thread(LSWMessageQueue.Run) { Name = "LSWMessageQueue" };
					AsynThreadCollection.Add(LSWMessageQueueThread.Name, LSWMessageQueueThread);
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
