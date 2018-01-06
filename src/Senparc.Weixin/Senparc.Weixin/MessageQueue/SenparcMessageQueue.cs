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

    文件名：SenparcMessageQueue.cs
    文件功能描述：SenparcMessageQueue消息队列


    创建标识：Senparc - 20151226

    修改标识：Senparc - 20160210
    修改描述：v4.5.10 取消MessageQueueList，使用MessageQueueDictionary.Keys记录标示
              （使用MessageQueueDictionary.Keys会可能会使储存项目的无序执行）
----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.Linq;

namespace Senparc.Weixin.MessageQueue
{
    /// <summary>
    /// 消息队列
    /// </summary>
    public class SenparcMessageQueue
    {
        /// <summary>
        /// 队列数据集合
        /// </summary>
        private static Dictionary<string, SenparcMessageQueueItem> MessageQueueDictionary = new Dictionary<string, SenparcMessageQueueItem>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 同步执行锁
        /// </summary>
        private static object MessageQueueSyncLock = new object();
        /// <summary>
        /// 立即同步所有缓存执行锁（给OperateQueue()使用）
        /// </summary>
        private static object FlushCacheLock = new object();

        /// <summary>
        /// 生成Key
        /// </summary>
        /// <param name="name">队列应用名称，如“ContainerBag”</param>
        /// <param name="senderType">操作对象类型</param>
        /// <param name="identityKey">对象唯一标识Key</param>
        /// <param name="actionName">操作名称，如“UpdateContainerBag”</param>
        /// <returns></returns>
        public static string GenerateKey(string name, Type senderType, string identityKey, string actionName)
        {
            var key = string.Format("Name@{0}||Type@{1}||Key@{2}||ActionName@{3}",
                name, senderType, identityKey, actionName);
            return key;
        }

        /// <summary>
        /// 操作队列
        /// </summary>
        public static void OperateQueue()
        {
            lock (FlushCacheLock)
            {
                var mq = new SenparcMessageQueue();
                var key = mq.GetCurrentKey(); //获取最新的Key
                while (!string.IsNullOrEmpty(key))
                {
                    var mqItem = mq.GetItem(key); //获取任务项
                    mqItem.Action(); //执行
                    mq.Remove(key); //清除
                    key = mq.GetCurrentKey(); //获取最新的Key
                }
            }
        }

        /// <summary>
        /// 获取当前等待执行的Key
        /// </summary>
        /// <returns></returns>
        public string GetCurrentKey()
        {
            lock (MessageQueueSyncLock)
            {
                return MessageQueueDictionary.Keys.FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取SenparcMessageQueueItem
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SenparcMessageQueueItem GetItem(string key)
        {
            lock (MessageQueueSyncLock)
            {
                if (MessageQueueDictionary.ContainsKey(key))
                {
                    return MessageQueueDictionary[key];
                }
                return null;
            }
        }

        /// <summary>
        /// 添加队列成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="action"></param>
        public SenparcMessageQueueItem Add(string key, Action action)
        {
            lock (MessageQueueSyncLock)
            {
                //if (!MessageQueueDictionary.ContainsKey(key))
                //{
                //    MessageQueueList.Add(key);
                //}
                //else
                //{
                //    MessageQueueList.Remove(key);
                //    MessageQueueList.Add(key);//移动到末尾
                //}

                var mqItem = new SenparcMessageQueueItem(key, action);
                MessageQueueDictionary[key] = mqItem;
                return mqItem;
            }
        }

        /// <summary>
        /// 移除队列成员
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            lock (MessageQueueSyncLock)
            {
                if (MessageQueueDictionary.ContainsKey(key))
                {
                    MessageQueueDictionary.Remove(key);
                    //MessageQueueList.Remove(key);
                }
            }
        }

        /// <summary>
        /// 获得当前队列数量
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            lock (MessageQueueSyncLock)
            {
                return MessageQueueDictionary.Count;
            }
        }

    }
}
