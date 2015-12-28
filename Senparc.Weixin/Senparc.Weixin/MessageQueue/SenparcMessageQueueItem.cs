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
        public string Description { get; set; }

        public SenparcMessageQueueItem(string key, Action action, string description = null)
        {
            Key = key;
            Action = action;
            Description = description;
            AddTime = DateTime.Now;
        }
    }
}
