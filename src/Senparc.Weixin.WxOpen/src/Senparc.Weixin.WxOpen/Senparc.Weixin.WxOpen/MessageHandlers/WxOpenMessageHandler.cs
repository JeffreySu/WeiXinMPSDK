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
    
    修改标识：Senparc - 20190917
    修改描述：v3.6.0 支持新版本 MessageHandler 和 WeixinContext，支持使用分布式缓存储存上下文消息

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
using System.Threading.Tasks;
using System.Threading;
//using IRequestMessageBase = Senparc.Weixin.WxOpen.Entities.IRequestMessageBase;

namespace Senparc.Weixin.WxOpen.MessageHandlers
{
    /// <summary>
    /// 小程序MessageHandler
    /// </summary>
    /// <typeparam name="TMC">上下文MessageContext类型</typeparam>
    public abstract partial class WxOpenMessageHandler<TMC> : MessageHandler<TMC, IRequestMessageBase, IResponseMessageBase>
        where TMC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        #region 属性设置

        /// <summary>
        /// 原始的加密请求（如果不加密则为null）
        /// </summary>
        public XDocument EcryptRequestDocument { get; set; }

        /// <summary>
        /// 请求实体
        /// </summary>
        public new IRequestMessageBase RequestMessage { get => base.RequestMessage as IRequestMessageBase; set => base.RequestMessage = value; }

        /// <summary>
        /// 响应实体
        /// 正常情况下只有当执行Execute()方法后才可能有值。
        /// 也可以结合Cancel，提前给ResponseMessage赋值。
        /// </summary>
        public new IResponseMessageBase ResponseMessage { get => base.ResponseMessage as IResponseMessageBase; set => base.ResponseMessage = value; }


        public override XDocument ResponseDocument => ResponseMessage != null ? EntityHelper.ConvertEntityToXml(ResponseMessage as ResponseMessageBase) : null;


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
                msgCrype.EncryptResponseMsg(ResponseDocument.ToString().Replace("\r\n", "\n")/* 替换\r\n是为了处理iphone设备上换行bug */, timeStamp, nonce, ref finalResponseXml);//TODO:这里官方的方法已经把EncryptResponseMessage对应的XML输出出来了

                return XDocument.Parse(finalResponseXml);
            }
        }

        private PostModel _postModel;

        /// <summary>
        /// 请求和响应消息定义
        /// </summary>
        public override MessageEntityEnlightener MessageEntityEnlightener => WxOpenMessageEntityEnlightener.Instance;
        /// <summary>
        /// Api 接口定义
        /// </summary>
        public override ApiEnlightener ApiEnlightener => WxOpenApiEnlightener.Instance;


        #endregion

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
        /// <param name="onlyAllowEcryptMessage">当平台同时兼容明文消息和加密消息时，只允许处理加密消息（不允许处理明文消息），默认为 False</param>
        ///// <param name="developerInfo">开发者信息（非必填）</param>
        public WxOpenMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0, bool onlyAllowEcryptMessage = false)
            : base(inputStream, postModel, maxRecordCount, onlyAllowEcryptMessage)
        {
        }

        /// <summary>
        /// 小程序MessageHandler构造函数
        /// </summary>
        /// <param name="requestDocument">XML格式的请求</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">上下文最多保留消息（0为保存所有）</param>
        /// <param name="onlyAllowEcryptMessage">当平台同时兼容明文消息和加密消息时，只允许处理加密消息（不允许处理明文消息），默认为 False</param>
        public WxOpenMessageHandler(XDocument requestDocument, PostModel postModel, int maxRecordCount = 0, bool onlyAllowEcryptMessage = false)
            : base(requestDocument, postModel, maxRecordCount, onlyAllowEcryptMessage)
        {
        }

        /// <summary>
        /// 小程序MessageHandler构造函数
        /// </summary>
        /// <param name="requestMessageBase">RequestMessageBase</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">上下文最多保留消息（0为保存所有）</param>
        /// <param name="onlyAllowEcryptMessage">当平台同时兼容明文消息和加密消息时，只允许处理加密消息（不允许处理明文消息），默认为 False</param>
        public WxOpenMessageHandler(RequestMessageBase requestMessageBase, PostModel postModel, int maxRecordCount = 0, bool onlyAllowEcryptMessage = false)
            : base(requestMessageBase, postModel, maxRecordCount, onlyAllowEcryptMessage)
        {
        }

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

            //检查是否限定只能用加密模式
            if (OnlyAllowEcryptMessage && !UsingEcryptMessage)
            {
                CancelExcute = true;
                TextResponseMessage = "当前 MessageHandler 开启了 OnlyAllowEcryptMessage 设置，只允许处理加密消息，以提高安全性！";
                return null;
            }

            RequestMessage = RequestMessageFactory.GetRequestEntity(new TMC(), decryptDoc);
            if (UsingEcryptMessage)
            {
                RequestMessage.Encrypt = postDataDocument.Root.Element("Encrypt").Value;
            }

            return decryptDoc;

            //消息上下文记录将在 base.CommonInitialize() 中根据去重等条件判断后进行添加
        }

        #endregion

        #region 消息处理




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

        #endregion

        #region 异步方法


        /// <summary>
        /// 执行微信请求
        /// </summary>
        public override async Task BuildResponseMessageAsync(CancellationToken cancellationToken)
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
                        ResponseMessage = await CurrentMessageHandlerNode.ExecuteAsync(RequestMessage, this, weixinAppId) ??
                                OnTextRequest(RequestMessage as RequestMessageText);
                        //SenparcTrace.SendCustomLog("wxTest-response", ResponseMessage.ToJson());
                        //SenparcTrace.SendCustomLog("WxOpen RequestMsgType", ResponseMessage.ToJson());

                        SenparcTrace.SendCustomLog("WXOPEN-TEXT ResponseMessage:", ResponseMessage.ToJson());
                    }
                    break;
                case RequestMsgType.Image:
                    {
                        ResponseMessage = await CurrentMessageHandlerNode.ExecuteAsync(RequestMessage, this, weixinAppId) ??
                                OnImageRequest(RequestMessage as RequestMessageImage);
                    }
                    break;
                case RequestMsgType.NeuChar:
                    ResponseMessage = await OnNeuCharRequestAsync(RequestMessage as RequestMessageNeuChar);
                    break;
                case RequestMsgType.Event:
                    {
                        await OnEventRequestAsync(RequestMessage as IRequestMessageEventBase);
                    }
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的MsgType请求类型", null);
            }

            #endregion
        }

        #endregion
    }
}
