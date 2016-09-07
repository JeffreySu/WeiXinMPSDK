/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：WeixinContext.cs
    文件功能描述：微信消息上下文（全局）
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

/*
 * V2.0
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Context
{
    public static class WeixinContextGlobal
    {
        public static object Lock = new object();

        /// <summary>
        /// 是否开启上下文记录
        /// </summary>
        public static bool UseWeixinContext = true;

    }

    #region 废除接口
    //public interface IWeixinContext<TM, TRequest, TResponse>
    //    where TM : class, IMessageContext<TRequest, TResponse>, new()
    //    where TRequest : IRequestMessageBase
    //    where TResponse : IResponseMessageBase
    //{
    //    /// <summary>
    //    /// 所有MessageContext集合，不要直接操作此对象
    //    /// </summary>
    //    Dictionary<string, TM> MessageCollection { get; set; }
    //    /// <summary>
    //    /// MessageContext列队（LastActiveTime升序排列）,不要直接操作此对象
    //    /// </summary>
    //    MessageQueue<TM, TRequest, TResponse> MessageQueue { get; set; }

    //    /// <summary>
    //    /// 每一个MessageContext过期时间
    //    /// </summary>
    //    Double ExpireMinutes { get; set; }

    //    /// <summary>
    //    /// 最大储存上下文数量（分别针对请求和响应信息）
    //    /// </summary>
    //    int MaxRecordCount { get; set; }

    //    TM GetMessageContext(TRequest requestMessage);
    //    TM GetMessageContext(TResponse responseMessage);
    //}
    #endregion

    /// <summary>
    /// 微信消息上下文（全局）
    /// 默认过期时间：90分钟
    /// </summary>
    public class WeixinContext<TM, TRequest, TResponse> /*: IWeixinContext<TM, TRequest, TResponse>*/
        where TM : class, IMessageContext<TRequest, TResponse>, new() //TODO:TRequest, TResponse直接写明基类类型
        where TRequest : IRequestMessageBase
        where TResponse : IResponseMessageBase
    {
        private int _maxRecordCount;

        /// <summary>
        /// 所有MessageContext集合，不要直接操作此对象
        /// </summary>
        public Dictionary<string, TM> MessageCollection { get; set; }
        /// <summary>
        /// MessageContext列队（LastActiveTime升序排列）,不要直接操作此对象
        /// </summary>
        public MessageQueue<TM, TRequest, TResponse> MessageQueue { get; set; }

        /// <summary>
        /// 每一个MessageContext过期时间
        /// </summary>
        public Double ExpireMinutes { get; set; }

        /// <summary>
        /// 最大储存上下文数量（分别针对请求和响应信息）
        /// </summary>
        public int MaxRecordCount { get; set; }


        public WeixinContext()
        {
            Restore();
        }

        /// <summary>
        /// 重置所有上下文参数，所有记录将被清空
        /// </summary>
        public void Restore()
        {
            MessageCollection = new Dictionary<string, TM>(StringComparer.OrdinalIgnoreCase);
            MessageQueue = new MessageQueue<TM, TRequest, TResponse>();
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
                //确定对话过期时间
                var expireMinutes = firstMessageContext.ExpireMinutes.HasValue
                    ? firstMessageContext.ExpireMinutes.Value //列队自定义事件
                    : this.ExpireMinutes;//全局统一默认时间
                if (timeSpan.TotalMinutes >= expireMinutes)
                {
                    MessageQueue.RemoveAt(0);//从队列中移除过期对象
                    MessageCollection.Remove(firstMessageContext.UserName);//从集合中删除过期对象

                    //添加事件回调
                    firstMessageContext.OnRemoved();//TODO:此处异步处理，或用户在自己操作的时候异步处理需要耗费时间比较长的操作。
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
        /// <param name="createIfNotExists">True：如果用户不存在，则创建一个实例，并返回这个最新的实例
        /// False：用户储存在，则返回null</param>
        /// <returns></returns>
        private TM GetMessageContext(string userName, bool createIfNotExists)
        {
            var messageContext = GetMessageContext(userName);

            if (messageContext == null)
            {
                if (createIfNotExists)
                {
                    //全局只在这一个地方使用MessageCollection[Key]写入
                    MessageCollection[userName] = new TM()
                    {
                        UserName = userName,
                        MaxRecordCount = MaxRecordCount
                    };

                    messageContext = GetMessageContext(userName);
                    //插入列队
                    MessageQueue.Add(messageContext); //最新的排到末尾
                }
                else
                {
                    return null;
                }
            }
            return messageContext;
        }

        /// <summary>
        /// 获取MessageContext，如果不存在，使用requestMessage信息初始化一个，并返回原始实例
        /// </summary>
        /// <returns></returns>
        public TM GetMessageContext(TRequest requestMessage)
        {
            lock (WeixinContextGlobal.Lock)
            {
                return GetMessageContext(requestMessage.FromUserName, true);
            }
        }

        /// <summary>
        /// 获取MessageContext，如果不存在，使用requestMessage信息初始化一个，并返回原始实例
        /// </summary>
        /// <returns></returns>
        public TM GetMessageContext(TResponse responseMessage)
        {
            lock (WeixinContextGlobal.Lock)
            {
                return GetMessageContext(responseMessage.ToUserName, true);
            }
        }

        /// <summary>
        /// 记录请求信息
        /// </summary>
        /// <param name="requestMessage">请求信息</param>
        public void InsertMessage(TRequest requestMessage)
        {
            lock (WeixinContextGlobal.Lock)
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
        public void InsertMessage(TResponse responseMessage)
        {
            lock (WeixinContextGlobal.Lock)
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
        public TRequest GetLastRequestMessage(string userName)
        {
            lock (WeixinContextGlobal.Lock)
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
        public TResponse GetLastResponseMessage(string userName)
        {
            lock (WeixinContextGlobal.Lock)
            {
                var messageContext = GetMessageContext(userName, true);
                return messageContext.ResponseMessages.LastOrDefault();
            }
        }
    }
}
