/*----------------------------------------------------------------
    Copyright (C) 2025 Senparc
    
    文件名：WorkBotMessageHandler.cs
    文件功能描述：企业号智能机器人请求的集中处理方法
    
    
    创建标识：Wang Qian - 20250825  
----------------------------------------------------------------*/

using System;
using System.IO;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Helpers;
using Senparc.Weixin.Work.Tencent;
using Senparc.NeuChar;
using Senparc.NeuChar.ApiHandlers;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.Entities.Request.Event;
using System.Xml.Linq;
using Senparc.NeuChar.Entities;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar.Exceptions;

namespace Senparc.Weixin.Work.MessageHandlers
{
    public interface IWorkBotMessageHandler : IMessageHandler<IWorkRequestMessageBase, IWorkResponseMessageBase>
    {
        /// <summary>
        /// 原始加密信息
        /// </summary>
        BotEncryptPostData EncryptPostData { get; set; }
        new IWorkRequestMessageBase RequestMessage { get; set; }
        new IWorkResponseMessageBase ResponseMessage { get; set; }

    }

    public abstract partial class WorkBotMessageHandler<TMC>
        : MessageHandler<TMC, IWorkRequestMessageBase, IWorkResponseMessageBase>, IWorkBotMessageHandler
        where TMC : class, IMessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>, new()
    {
        private PostModel _postModel;

        public BotEncryptPostData EncryptPostData { get; set; }

        /// <summary>

        /// 请求实体
        /// </summary>
        public new IWorkRequestMessageBase RequestMessage
        {
            get
            {
                return base.RequestMessage as IWorkRequestMessageBase;
            }
            set
            {
                base.RequestMessage = value;
            }
        }

        /// <summary>
        /// 响应实体
        /// </summary>
        public new IWorkResponseMessageBase ResponseMessage
        {
            get
            {
                return base.ResponseMessage as IWorkResponseMessageBase;
            }
            set
            {
                base.ResponseMessage = value;
            }
        }

        /// <summary>
        /// 从ResponseMessage转换而来的响应消息JSON字符串（未加密）
        /// </summary>
        public override string ResponseJsonStr
        { 
            get
        {
            if (ResponseMessage == null)
            {
                return null;
            }
            return BotEntityHelper.GetResponseMsgString(ResponseMessage as WorkResponseMessageBase);
        } 
        }

        /// <summary>
        /// 最终响应消息JSON字符串（已加密）
        /// </summary>
        public override string FinalResponseJsonStr
        { 
            get
        {
            if (ResponseJsonStr == null)
            {
                return null;
            }
            var timeStamp = SystemTime.Now.Ticks.ToString();
                var nonce = Guid.NewGuid().ToString("N");

                //加解密库要求传 receiveid 参数，企业自建智能机器人的使用场景里，receiveid直接传空字符串即可
                WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, "");
                string finalResponseJson = null;
                msgCrype.EncryptJsonMsg(ResponseJsonStr, timeStamp, nonce, ref finalResponseJson);
                return finalResponseJson;
        } 
        }
        /// <summary>
        /// 在Bot场景下，将此属性设置为null，这样才能记录日志
        /// </summary>
        public override XDocument FinalResponseDocument
        {
            get
            {
                return null;
            }
        }

        public override XDocument ResponseDocument
        {
            get
            {
                return null;
            }
        }




        protected WorkBotMessageHandler(Stream inputStream, IEncryptPostModel postModel, int maxRecordCount = 0, bool onlyAllowEncryptMessage = false, IServiceProvider serviceProvider = null, bool useJson = true) : base(inputStream, postModel, maxRecordCount, onlyAllowEncryptMessage, serviceProvider, useJson)
        {
        }

        protected WorkBotMessageHandler(string postDataJson, IEncryptPostModel postModel, int maxRecordCount = 0, bool onlyAllowEncryptMessage = false, IServiceProvider serviceProvider = null) : base(postDataJson, postModel, maxRecordCount, onlyAllowEncryptMessage, serviceProvider)
        {
        }

        public override string Init(string postDataJson, IEncryptPostModel postModel)
        {
            _postModel = postModel as PostModel ?? new PostModel();

            //解密，获得明文字符串
            string msgJson = null;

            //加解密库要求传 receiveid 参数，企业自建智能机器人的使用场景里，receiveid直接传空字符串即可
            WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, "");
            var result = msgCrype.DecryptJsonMsg(_postModel.Msg_Signature, _postModel.Timestamp, _postModel.Nonce, postDataJson, ref msgJson);

            if (result != 0)
            {
                throw new MessageHandlerException($"解密失败，错误码：{result}");
            }
            RequestMessage = BotEntityHelper.GetRequestEntity(msgJson);
            RequestJsonStr = msgJson;
            return msgJson;
        }

        /// <summary>
        /// 根据当前的RequestMessage创建指定类型的ResponseMessage
        /// </summary>
        /// <typeparam name="TR">基于ResponseMessageBase的响应消息类型</typeparam>
        /// <returns></returns>
        public TR CreateResponseMessage<TR>() where TR : WorkResponseMessageBase
        {
            if (RequestMessage == null)
            {
                return null;
            }

            return RequestMessage.CreateResponseMessage<TR>();
        }

        public override MessageEntityEnlightener MessageEntityEnlightener { get { return WorkMessageEntityEnlightener.Instance; } }

        public override ApiEnlightener ApiEnlightener { get { return WorkApiEnlightener.Instance; } }



        [Obsolete("请使用异步方法 OnExecutingAsync()", true)]
        public virtual void OnExecuting()
        {
            throw new MessageHandlerException("请使用异步方法 OnExecutingAsync()");
        }

        [Obsolete("请使用异步方法 OnExecutedAsync()", true)]
        public virtual void OnExecuted()
        {
            throw new MessageHandlerException("请使用异步方法 OnExecutedAsync()");
        }

        /// <summary>
        /// 默认返回消息（当任何OnXX消息没有被重写，都将自动返回此默认消息）
        /// </summary>
        public abstract IWorkResponseMessageBase DefaultResponseMessage(IWorkRequestMessageBase requestMessage);

        #region 接收消息方法
        /// <summary>
        /// 文字类型请求
        /// </summary>
        public virtual IWorkResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        #endregion
    }
}