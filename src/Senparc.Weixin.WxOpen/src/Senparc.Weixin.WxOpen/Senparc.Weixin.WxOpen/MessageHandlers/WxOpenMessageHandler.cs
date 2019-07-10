#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：WxOpenMessageHandler.cs
    文件功能描述：小程序MessageHandler
    
    
    创建标识：Senparc - 20170103

    修改标识：Senparc - 20181030
    修改描述：v3.1.16 优化 MessageHandler 构造函数，提供 PostModel 默认值
    
    修改标识：Senparc - 20181117
    修改描述：v3.2.0 Execute() 重写方法名称改为 BuildResponseMessage()

----------------------------------------------------------------*/

using System;
using System.IO;
using System.Xml.Linq;
using Senparc.NeuChar.Context;
using Senparc.Weixin.Exceptions;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.WxOpen.Entities;
using Senparc.Weixin.WxOpen.Entities.Request;
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.ApiHandlers;
using Senparc.Weixin.WxOpen.AdvancedAPIs;
using Senparc.CO2NET.Trace;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.Tencent;
using Senparc.NeuChar.Helpers;
//using IRequestMessageBase = Senparc.Weixin.WxOpen.Entities.IRequestMessageBase;

namespace Senparc.Weixin.WxOpen.MessageHandlers
{
    /// <summary>
    /// 小程序MessageHandler
    /// </summary>
    /// <typeparam name="TC">上下文MessageContext类型</typeparam>
    public abstract partial class WxOpenMessageHandler<TC> : MessageHandler<TC, IRequestMessageBase, IResponseMessageBase>
        where TC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        /// <summary>
        /// 上下文（仅限于当前MessageHandler基类内）
        /// </summary>
        public static GlobalMessageContext<TC, IRequestMessageBase, IResponseMessageBase> GlobalWeixinContext = new GlobalMessageContext<TC, IRequestMessageBase, IResponseMessageBase>();
        //TODO:这里如果用一个MP自定义的WeixinContext，继承WeixinContext<TC, IRequestMessageBase, IResponseMessageBase>，在下面的WeixinContext中将无法转换成基类要求的类型

        /// <summary>
        /// 全局消息上下文
        /// </summary>
        public override GlobalMessageContext<TC, IRequestMessageBase, IResponseMessageBase> GlobalMessageContext
        {
            get
            {
                return GlobalWeixinContext;
            }
        }

        /// <summary>
        /// 原始的加密请求（如果不加密则为null）
        /// </summary>
        public XDocument EcryptRequestDocument { get; set; }


        /// <summary>
        /// 请求实体
        /// </summary>
        public new IRequestMessageBase RequestMessage
        {
            get
            {
                return base.RequestMessage as IRequestMessageBase;
            }
            set
            {
                base.RequestMessage = value;
            }
        }

        /// <summary>
        /// 响应实体
        /// 正常情况下只有当执行Execute()方法后才可能有值。
        /// 也可以结合Cancel，提前给ResponseMessage赋值。
        /// </summary>
        public new IResponseMessageBase ResponseMessage
        {
            get
            {
                return base.ResponseMessage as IResponseMessageBase;
            }
            set
            {
                base.ResponseMessage = value;
            }
        }


        public override XDocument ResponseDocument
        {
            get
            {
                return ResponseMessage != null ? EntityHelper.ConvertEntityToXml(ResponseMessage as ResponseMessageBase) : null;
            }
        }

        public override XDocument FinalResponseDocument
        {
            get
            {
                if (ResponseDocument == null)
                {
                    return null;
                }

                if (!UsingEcryptMessage)
                {
                    return ResponseDocument;
                }

                var timeStamp = SystemTime.Now.Ticks.ToString();
                var nonce = SystemTime.Now.Ticks.ToString();

                WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, _postModel.AppId);
                string finalResponseXml = null;
                msgCrype.EncryptMsg(ResponseDocument.ToString().Replace("\r\n", "\n")/* 替换\r\n是为了处理iphone设备上换行bug */, timeStamp, nonce, ref finalResponseXml);//TODO:这里官方的方法已经把EncryptResponseMessage对应的XML输出出来了

                return XDocument.Parse(finalResponseXml);
            }
        }

        private PostModel _postModel;


        /// <summary>
        /// 请求和响应消息定义
        /// </summary>
        public override MessageEntityEnlightener MessageEntityEnlightener { get { return WxOpenMessageEntityEnlightener.Instance; } }
        /// <summary>
        /// Api 接口定义
        /// </summary>
        public override ApiEnlightener ApiEnlightener { get { return WxOpenApiEnlightener.Instance; } }


        #region 构造函数

        /// <summary>
        /// 动态去重判断委托，仅当返回值为false时，不使用消息去重功能
        /// </summary>
        public Func<IRequestMessageBase, bool> OmitRepeatedMessageFunc = null;

        /// <summary>
        /// 小程序MessageHandler构造函数
        /// </summary>
        /// <param name="inputStream">XML流（后期会支持JSON）</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">上下文最多保留消息（0为保存所有）</param>
        ///// <param name="developerInfo">开发者信息（非必填）</param>
        public WxOpenMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
        }

        /// <summary>
        /// 小程序MessageHandler构造函数
        /// </summary>
        /// <param name="requestDocument">XML格式的请求</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">上下文最多保留消息（0为保存所有）</param>
        public WxOpenMessageHandler(XDocument requestDocument, PostModel postModel, int maxRecordCount = 0)
            : base(requestDocument, postModel, maxRecordCount)
        {
        }

        /// <summary>
        /// 小程序MessageHandler构造函数
        /// </summary>
        /// <param name="requestMessageBase">RequestMessageBase</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">上下文最多保留消息（0为保存所有）</param>
        public WxOpenMessageHandler(RequestMessageBase requestMessageBase, PostModel postModel, int maxRecordCount = 0)
            : base(requestMessageBase, postModel, maxRecordCount)
        {
        }

        #endregion

        #region 消息处理

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="postDataDocument"></param>
        /// <param name="postModel"></param>
        /// <returns></returns>
        public override XDocument Init(XDocument postDataDocument, IEncryptPostModel postModel)
        {
            //进行加密判断并处理
            _postModel = postModel as PostModel ?? new PostModel();
            var postDataStr = postDataDocument.ToString();

            XDocument decryptDoc = postDataDocument;

            if (_postModel != null && !_postModel.Token.IsNullOrWhiteSpace()
                && postDataDocument.Root.Element("Encrypt") != null && !string.IsNullOrEmpty(postDataDocument.Root.Element("Encrypt").Value))
            {
                //使用了加密
                UsingEcryptMessage = true;
                EcryptRequestDocument = postDataDocument;

                WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, _postModel.AppId);
                string msgXml = null;
                var result = msgCrype.DecryptMsg(_postModel.Msg_Signature, _postModel.Timestamp, _postModel.Nonce, postDataStr, ref msgXml);

                //判断result类型
                if (result != 0)
                {
                    //验证没有通过，取消执行
                    CancelExcute = true;
                    return null;
                }

                if (postDataDocument.Root.Element("FromUserName") != null && !string.IsNullOrEmpty(postDataDocument.Root.Element("FromUserName").Value))
                {
                    //TODO：使用了兼容模式，进行验证即可
                    UsingCompatibilityModelEcryptMessage = true;
                }

                decryptDoc = XDocument.Parse(msgXml);//完成解密
            }

            RequestMessage = RequestMessageFactory.GetRequestEntity(decryptDoc);
            if (UsingEcryptMessage)
            {
                RequestMessage.Encrypt = postDataDocument.Root.Element("Encrypt").Value;
            }


            //记录上下文
            if (MessageContextGlobalConfig.UseMessageContext)
            {
                GlobalMessageContext.InsertMessage(RequestMessage);
            }

            return decryptDoc;
        }

        /// <summary>
        /// 执行微信请求
        /// </summary>
        public override void BuildResponseMessage()
        {
            #region NeuChar 执行过程

            //var neuralSystem = NeuralSystem.Instance;
            //var messageHandlerNode = neuralSystem.GetNode("MessageHandlerNode") as MessageHandlerNode;

            //messageHandlerNode = messageHandlerNode ?? new MessageHandlerNode();

            var weixinAppId = this._postModel == null ? "" : this._postModel.AppId;

            switch (RequestMessage.MsgType)
            {
                case RequestMsgType.Text:
                    {
                        //SenparcTrace.SendCustomLog("wxTest-request", RequestMessage.ToJson());
                        ResponseMessage = CurrentMessageHandlerNode.Execute(RequestMessage, this, weixinAppId) ??
                                OnTextRequest(RequestMessage as RequestMessageText);
                        //SenparcTrace.SendCustomLog("wxTest-response", ResponseMessage.ToJson());
                        //SenparcTrace.SendCustomLog("WxOpen RequestMsgType", ResponseMessage.ToJson());

                        SenparcTrace.SendCustomLog("WXOPEN-TEXT ResponseMessage:", ResponseMessage.ToJson());
                    }
                    break;
                case RequestMsgType.Image:
                    {
                        ResponseMessage = CurrentMessageHandlerNode.Execute(RequestMessage, this, weixinAppId) ??
                                OnImageRequest(RequestMessage as RequestMessageImage);
                    }
                    break;
                case RequestMsgType.NeuChar:
                    ResponseMessage = OnNeuCharRequestAsync(RequestMessage as RequestMessageNeuChar).GetAwaiter().GetResult();
                    break;
                case RequestMsgType.Event:
                    {
                        OnEventRequest(RequestMessage as IRequestMessageEventBase);
                    }
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的MsgType请求类型", null);
            }


            #endregion
        }

        public virtual void OnExecuting()
        {
            //消息去重
            if ((OmitRepeatedMessageFunc == null || OmitRepeatedMessageFunc(RequestMessage) == true)
                && OmitRepeatedMessage && CurrentMessageContext.RequestMessages.Count > 1
                //&& !(RequestMessage is RequestMessageEvent_Merchant_Order)批量订单的MsgId可能会相同
                )
            {
                var lastMessage = CurrentMessageContext.RequestMessages[CurrentMessageContext.RequestMessages.Count - 2];
                if ((lastMessage.MsgId != 0 && lastMessage.MsgId == RequestMessage.MsgId)//使用MsgId去重
                    ||
                    //使用CreateTime去重（OpenId对象已经是同一个）
                    ((lastMessage.MsgId == RequestMessage.MsgId
                        && lastMessage.CreateTime == RequestMessage.CreateTime
                        && lastMessage.MsgType == RequestMessage.MsgType))
                    )
                {
                    CancelExcute = true;//重复消息，取消执行
                    return;
                }
            }

            base.OnExecuting();
        }

        public virtual void OnExecuted()
        {
            base.OnExecuted();
        }

        #endregion

    }
}
