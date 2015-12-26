using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MessageQueue
{
    /// <summary>
    /// SenparcMessageQueueItem
    /// </summary>
    public class SenparcMessageQueueItem
    {
        public string Key { get; set; }
        public Action Action { get; set; }
        public DateTime AddTime { get; set; }

        public SenparcMessageQueueItem(string key, Action action)
        {
            Key = key;
            Action = action;
            AddTime = DateTime.Now;
        }
    }
}
