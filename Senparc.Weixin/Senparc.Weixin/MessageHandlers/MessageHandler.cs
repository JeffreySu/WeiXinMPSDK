/*
 * V3.1
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Senparc.Weixin.Context;
using Senparc.Weixin.Entities;
using Senparc.Weixin.Exceptions;

namespace Senparc.Weixin.MessageHandlers
{
    /// <summary>
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于Senparc.Weixin的基础功能，只为简化代码而设。
    /// </summary>
    public abstract class MessageHandler<TC, TRest, TResp> : IMessageHandler<TRest, TResp>
        where TC : class, IMessageContext<TRest, TResp>, new()
        where TRest : IRequestMessageBase
        where TResp : IResponseMessageBase
    {
        ///// <summary>
        ///// 上下文
        ///// </summary>
        //public static WeixinContext<TC> GlobalWeixinContext = new WeixinContext<TC>();

        /// <summary>
        /// 全局消息上下文
        /// </summary>
        public abstract WeixinContext<TC, TRest, TResp> WeixinContext { get; }

        /// <summary>
        /// 当前用户消息上下文
        /// </summary>
        public virtual TC CurrentMessageContext
        {
            get { return WeixinContext.GetMessageContext(RequestMessage); }
        }

        /// <summary>
        /// 发送者用户名（OpenId）
        /// </summary>
        public string WeixinOpenId
        {
            get
            {
                if (RequestMessage != null)
                {
                    return RequestMessage.FromUserName;
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("UserName属性从v0.6起已过期，建议使用WeixinOpenId")]
        public string UserName
        {
            get
            {
                return WeixinOpenId;
            }
        }

        /// <summary>
        /// 取消执行Execute()方法。一般在OnExecuting()中用于临时阻止执行Execute()。
        /// 默认为False。
        /// 如果在执行OnExecuting()执行前设为True，则所有OnExecuting()、Execute()、OnExecuted()代码都不会被执行。
        /// 如果在执行OnExecuting()执行过程中设为True，则后续Execute()及OnExecuted()代码不会被执行。
        /// 建议在设为True的时候，给ResponseMessage赋值，以返回友好信息。
        /// </summary>
        public bool CancelExcute { get; set; }

        /// <summary>
        /// 在构造函数中转换得到原始XML数据
        /// </summary>
        public XDocument RequestDocument { get; set; }

        /// <summary>
        /// 根据ResponseMessageBase获得转换后的ResponseDocument
        /// 注意：这里每次请求都会根据当前的ResponseMessageBase生成一次，如需重用此数据，建议使用缓存或局部变量
        /// </summary>
        public abstract XDocument ResponseDocument { get; }

        /// <summary>
        /// 最后返回的ResponseDocument。
        /// 如果是Senparc.Weixin.MP，则应当和ResponseDocument一致；如果是Senparc.Weixin.QY，则应当在ResponseDocument基础上进行加密
        /// </summary>
        public abstract XDocument FinalResponseDocument { get; }

        //protected Stream InputStream { get; set; }
        /// <summary>
        /// 请求实体
        /// </summary>
        public virtual TRest RequestMessage { get; set; }
        /// <summary>
        /// 响应实体
        /// 正常情况下只有当执行Execute()方法后才可能有值。
        /// 也可以结合Cancel，提前给ResponseMessage赋值。
        /// </summary>
        public virtual TResp ResponseMessage { get; set; }

        /// <summary>
        /// 是否使用了MessageAgent代理
        /// </summary>
        public bool UsedMessageAgent { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="maxRecordCount"></param>
        /// <param name="postData">需要传入到Init的参数</param>
        public MessageHandler(Stream inputStream, int maxRecordCount = 0, object postData = null)
        {
            WeixinContext.MaxRecordCount = maxRecordCount;
            inputStream.Seek(0, SeekOrigin.Begin);//强制调整指针位置
            using (XmlReader xr = XmlReader.Create(inputStream))
            {
                var postDataDocument = XDocument.Load(xr);
                RequestDocument = Init(postDataDocument, postData);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postDataDocument"></param>
        /// <param name="maxRecordCount"></param>
        /// <param name="postData">需要传入到Init的参数</param>
        public MessageHandler(XDocument postDataDocument, int maxRecordCount = 0, object postData = null)
        {
            WeixinContext.MaxRecordCount = maxRecordCount;
            RequestDocument = Init(postDataDocument, postData);
        }

        /// <summary>
        /// 初始化，获取RequestDocument
        /// </summary>
        /// <param name="requestDocument"></param>
        public abstract XDocument Init(XDocument requestDocument, object postData = null);

        //public abstract TR CreateResponseMessage<TR>() where TR : ResponseMessageBase;

        /// <summary>
        /// 执行微信请求
        /// </summary>
        public abstract void Execute();

        public virtual void OnExecuting()
        {
        }

        public virtual void OnExecuted()
        {
        }

        ///// <summary>
        ///// 默认返回消息（当任何OnXX消息没有被重写，都将自动返回此默认消息）
        ///// </summary>
        //public abstract IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage);
    }
}
