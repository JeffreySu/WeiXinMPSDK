using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Context
{
    public static class WeixinContextLock
    {
        public static object Lock = new object();
    }

    /// <summary>
    /// 微信消息上下文（全局）
    /// 默认过期时间：90分钟
    /// </summary>
    public class WeixinContext<TM> where TM : class, IMessageContext, new()
    {
        /// <summary>
        /// 所有MessageContext集合
        /// </summary>
        protected Dictionary<string, TM> MessageCollection { get; set; }
        /// <summary>
        /// MessageContext列队（LastActiveTime升序排列）
        /// </summary>
        protected List<TM> MessageQueue { get; set; }

        /// <summary>
        /// 每一个MessageContext过期时间
        /// </summary>
        public Double ExpireMinutes { get; set; }

        public WeixinContext()
        {
            MessageCollection = new Dictionary<string, TM>(StringComparer.OrdinalIgnoreCase);
            MessageQueue = new List<TM>();
            ExpireMinutes = 90;
        }

        /// <summary>
        /// 获取MessageContext，如果不存在，返回null
        /// 这个方法的更重要意义在于操作TM队列，及时移除过期信息，并将最新活动的对象移到尾部
        /// </summary>
        /// <param name="userName">用户名（OpenId）</param>
        /// <returns></returns>
        private TM GetMessageContext(string userName)
        {
            //检查并移除过期记录，为了尽量节约资源，这里暂不使用独立线程轮询
            while (MessageQueue.Count > 0)
            {
                var firstMessageContext = MessageQueue[0];
                var timeSpan = DateTime.Now - firstMessageContext.LastActiveTime;
                if (timeSpan.TotalMinutes >= ExpireMinutes)
                {
                    MessageQueue.RemoveAt(0);//从队列中移除过期对象
                    MessageCollection.Remove(firstMessageContext.UserName);//从集合中删除过期对象
                }
                else
                {
                    break;
                }
            }

            /* 
             * 全局只有在这里用到MessageCollection.ContainsKey
             * 充分分离MessageCollection内部操作，
             * 为以后变化或扩展MessageCollection留余地
             */
            if (!MessageCollection.ContainsKey(userName))
            {
                return null;
            }

            return MessageCollection[userName];
        }

        /// <summary>
        /// 获取MessageContext
        /// </summary>
        /// <param name="userName">用户名（OpenId）</param>
        /// <param name="createIfNotExists">True：如果用户不存在，则创建一个实例</param>
        /// <returns></returns>
        private TM GetMessageContext(string userName, bool createIfNotExists)
        {
            var weixinContext = GetMessageContext(userName);

            if (weixinContext == null)
            {
                if (createIfNotExists)
                {
                    //全局只在这一个地方使用MessageCollection.Add
                    MessageCollection[userName] = new TM() { UserName = userName };
                    weixinContext = GetMessageContext(userName);
                    //插入列队
                    MessageQueue.Add(weixinContext); //最新的排到末尾
                }
                else
                {
                    return null;
                }
            }
            return weixinContext;
        }

        /// <summary>
        /// 获取MessageContext，如果不存在，使用requestMessage信息初始化一个，并返回原始实例
        /// </summary>
        /// <returns></returns>
        public TM GetMessageContext(IRequestMessageBase requestMessage)
        {
            lock (WeixinContextLock.Lock)
            {
                return GetMessageContext(requestMessage.FromUserName, true);
            }
        }

        /// <summary>
        /// 获取MessageContext，如果不存在，使用requestMessage信息初始化一个，并返回原始实例
        /// </summary>
        /// <returns></returns>
        public TM GetMessageContext(IResponseMessageBase responseMessage)
        {
            lock (WeixinContextLock.Lock)
            {
                return GetMessageContext(responseMessage.ToUserName, true);
            }
        }

        /// <summary>
        /// 记录请求信息
        /// </summary>
        /// <param name="requestMessage">请求信息</param>
        public void InsertMessage(IRequestMessageBase requestMessage)
        {
            lock (WeixinContextLock.Lock)
            {
                var userName = requestMessage.FromUserName;
                var messageContext = GetMessageContext(userName, true);
                if (messageContext.RequestMessages.Count > 0)
                {
                    //如果不是新建的对象，把当前对象移到队列尾部（新对象已经在底部）
                    var messageContextInQueue =
                        MessageQueue.FindIndex(z => z.UserName == userName);

                    if (messageContextInQueue >= 0)
                    {
                        MessageQueue.RemoveAt(messageContextInQueue); //移除当前对象
                        MessageQueue.Add(messageContext); //插入到末尾
                    }
                }

                messageContext.LastActiveTime = DateTime.Now;//记录请求时间
                messageContext.RequestMessages.Add(requestMessage);//录入消息
            }
        }

        /// <summary>
        /// 记录响应信息
        /// </summary>
        /// <param name="responseMessage">响应信息</param>
        public void InsertMessage(IResponseMessageBase responseMessage)
        {
            lock (WeixinContextLock.Lock)
            {
                var messageContext = GetMessageContext(responseMessage.ToUserName, true);
                messageContext.ResponseMessages.Add(responseMessage);
            }
        }

        /// <summary>
        /// 获取最新一条请求数据，如果不存在，则返回Null
        /// </summary>
        /// <param name="userName">用户名（OpenId）</param>
        /// <returns></returns>
        public IRequestMessageBase GetLastRequestMessage(string userName)
        {
            lock (WeixinContextLock.Lock)
            {
                var messageContext = GetMessageContext(userName, true);
                return messageContext.RequestMessages.LastOrDefault();
            }
        }

        /// <summary>
        /// 获取最新一条响应数据，如果不存在，则返回Null
        /// </summary>
        /// <param name="userName">用户名（OpenId）</param>
        /// <returns></returns>
        public IResponseMessageBase GetLastResponseMessage(string userName)
        {
            lock (WeixinContextLock.Lock)
            {
                var messageContext = GetMessageContext(userName, true);
                return messageContext.ResponseMessages.LastOrDefault();
            }
        }
    }
}
