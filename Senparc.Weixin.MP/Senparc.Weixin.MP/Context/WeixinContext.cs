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
    /// </summary>
    public class WeixinContext<TM> where TM : IMessageContext, new()
    {
        protected Dictionary<string, TM> MessageCollection = new Dictionary<string, TM>(StringComparer.OrdinalIgnoreCase);
        /// <summary>
        /// 每一个MessageContext过期时间
        /// </summary>
        public int ExpireMinutes = 90;

        public WeixinContext()
        {
        }

        /// <summary>
        /// 获取MessageContext，如果不存在，返回null
        /// </summary>
        /// <param name="userName">用户名（OpenId）</param>
        /// <returns></returns>
        private IMessageContext GetMessageContext(string userName)
        {
            //全局只有在这里用到MessageCollection.ContainsKey
            //充分分离MessageCollection内部操作，
            //为以后变化或扩展MessageCollection留余地
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
        private IMessageContext GetMessageContext(string userName, bool createIfNotExists)
        {
            var weixinContext = GetMessageContext(userName);

            if (GetMessageContext(userName) == null)
            {
                if (createIfNotExists)
                {
                    MessageCollection.Add(userName, new TM());
                }
                else
                {
                    return null;
                }
            }
            return MessageCollection[userName];
        }

        /// <summary>
        /// 获取MessageContext，如果不存在，使用requestMessage信息初始化一个，并返回原始实例
        /// </summary>
        /// <returns></returns>
        public IMessageContext GetMessageContext(IRequestMessageBase requestMessage)
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
        public IMessageContext GetMessageContext(IResponseMessageBase responseMessage)
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
                var messageContext = GetMessageContext(requestMessage.FromUserName, true);
                messageContext.RequestMessages.Add(requestMessage);
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
