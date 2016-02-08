/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：SenparcMessageQueueItem.cs
    文件功能描述：SenparcMessageQueue消息列队项
    
    
    创建标识：Senparc - 20151226
    
----------------------------------------------------------------*/

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
