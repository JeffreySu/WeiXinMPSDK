using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.MessageQueue
{
	public class LSWMessageQueueItem
	{
		public string Key { get; set; }
		public Action Action { get; set; }
		public DateTime AddTime { get; set; }
		public string Description { get; set; }

		public LSWMessageQueueItem(string key, Action action, string description = null)
		{
			Key = key;
			Action = action;
			Description = description;
			AddTime = DateTime.Now;
		}
	}
}
