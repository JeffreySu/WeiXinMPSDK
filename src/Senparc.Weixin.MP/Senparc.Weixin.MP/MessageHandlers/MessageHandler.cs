#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2024 Senparc
    
    文件名：MessageHandler.cs
    文件功能描述：微信请求的集中处理方法
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150327
    修改描述：添加接收小视频消息方法

    修改标识：Senparc - 20151205
    修改描述：v13.4.5 提供OmitRepeatedMessageFunc方法增强消息去重灵活性
  
    修改标识：Senparc - 20160722
    修改描述：记录上下文，此处修改

    修改标识：Senparc - 20180122
    修改描述：OnExecuting() 和 OnExecuted() 方法改为 override

    修改标识：Senparc - 20180318
    修改描述：v14.10.7 MessageHandler消息去重增加对“领取事件推送”的特殊判断 - https://github.com/JeffreySu/WeiXinMPSDK/issues/1106

    修改标识：Senparc - 20181030
    修改描述：v16.4.10 优化 MessageHandler 构造函数，提供 PostModel 默认值

    修改标识：Senparc - 20181117
    修改描述：v16.5.0 Execute() 重写方法名称改为 BuildResponseMessage()
    
    修改标识：Senparc - 20190917
    修改描述：v16.8.0 支持新版本 MessageHandler 和 WeixinContext，支持使用分布式缓存储存上下文消息

----------------------------------------------------------------*/

using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Extensions;
using Senparc.NeuChar;
using Senparc.NeuChar.ApiHandlers;
using Senparc.NeuChar.App.AppStore;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Helpers;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.Tencent;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.MessageHandlers
{
    /// <summary>
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于Senparc.Weixin.MP的基础功能，只为简化代码而设。
    /// </summary>
    public abstract partial class MessageHandler<TMC> :
        MessageHandler<TMC, IRequestMessageBase, IResponseMessageBase>, IMessageHandler
        where TMC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        #region 属性设置

        ///// <summary>
        ///// 原始的加密请求（如果不加密则为null）
        ///// </summary>
        //public XDocument EcryptRequestDocument { get; set; }

        /// <summary>
        /// 根据ResponseMessageBase获得转换后的ResponseDocument
        /// 注意：这里每次请求都会根据当前的ResponseMessageBase生成一次，如需重用此数据，建议使用缓存或局部变量
        /// </summary>
        public override XDocument ResponseDocument => ResponseMessage != null ? EntityHelper.ConvertEntityToXml(ResponseMessage as ResponseMessageBase) : null;

        /// <summary>
        /// 最后返回的ResponseDocument。
        /// 这里是Senparc.Weixin.MP，根据请求消息半段在ResponseDocument基础上是否需要再次进行加密（每次获取重新加密，所以结果会不同）
        /// </summary>
        public override XDocument FinalResponseDocument
        {
            get
            {
                if (ResponseDocument == null)
                {
                    return null;
                }

                if (!UsingEncryptMessage)
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

        private PostModel _postModel { get => base.PostModel as PostModel; set => base.PostModel = value; }

        /// <summary>
        /// 微微嗨开发者信息
        /// </summary>
        public DeveloperInfo DeveloperInfo { get; set; }

        /// <summary>
        /// 请求和响应消息定义
        /// </summary>
        public override MessageEntityEnlightener MessageEntityEnlightener => MpMessageEntityEnlightener.Instance;
        /// <summary>
        /// Api 接口定义
        /// </summary>
        public override ApiEnlightener ApiEnlightener => MpApiEnlightener.Instance;

        #endregion

        #region 构造函数 / 初始化相关

        /// <summary>
        /// 构造MessageHandler
        /// </summary>
        /// <param name="inputStream">请求消息流</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">单个用户上下文消息列表储存的最大长度</param>
        /// <param name="onlyAllowEncryptMessage">当平台同时兼容明文消息和加密消息时，只允许处理加密消息（不允许处理明文消息），默认为 False</param>
        /// <param name="developerInfo">微微嗨开发者信息，如果不为空，则优先请求云端应用商店的资源</param>
        /// <param name="serviceProvider">ServiceProvider，.NET Framework 可留空（null）</param>
        public MessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0, bool onlyAllowEncryptMessage = false, DeveloperInfo developerInfo = null, IServiceProvider serviceProvider = null)
            : base(inputStream, postModel, maxRecordCount, onlyAllowEncryptMessage, serviceProvider)
        {
            DeveloperInfo = developerInfo;
            _postModel = postModel ?? new PostModel();
        }

        /// <summary>
        /// 构造MessageHandler
        /// </summary>
        /// <param name="requestDocument">请求消息的XML</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">单个用户上下文消息列表储存的最大长度</param>
        /// <param name="onlyAllowEncryptMessage">当平台同时兼容明文消息和加密消息时，只允许处理加密消息（不允许处理明文消息），默认为 False</param>
        /// <param name="developerInfo">微微嗨开发者信息，如果不为空，则优先请求云端应用商店的资源</param>
        /// <param name="serviceProvider">ServiceProvider，.NET Framework 可留空（null）</param>
        public MessageHandler(XDocument requestDocument, PostModel postModel, int maxRecordCount = 0, bool onlyAllowEncryptMessage = false, DeveloperInfo developerInfo = null, IServiceProvider serviceProvider = null)
            : base(requestDocument, postModel, maxRecordCount, onlyAllowEncryptMessage, serviceProvider)
        {
            DeveloperInfo = developerInfo;
            _postModel = postModel ?? new PostModel();
            //GlobalMessageContext.MaxRecordCount = maxRecordCount;
            //Init(requestDocument);
        }

        /// <summary>
        /// 直接传入 IRequestMessageBase，仅供单元测试使用！
        /// </summary>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">单个用户上下文消息列表储存的最大长度</param>
        /// <param name="developerInfo">微微嗨开发者信息，如果不为空，则优先请求云端应用商店的资源</param>
        /// <param name="onlyAllowEncryptMessage">当平台同时兼容明文消息和加密消息时，只允许处理加密消息（不允许处理明文消息），默认为 False</param>
        /// <param name="requestMessageBase"></param>
        /// <param name="serviceProvider">ServiceProvider，.NET Framework 可留空（null）</param>
        public MessageHandler(RequestMessageBase requestMessageBase, PostModel postModel, int maxRecordCount = 0, bool onlyAllowEncryptMessage = false, DeveloperInfo developerInfo = null, IServiceProvider serviceProvider = null)
            : base(requestMessageBase, postModel, maxRecordCount, onlyAllowEncryptMessage, serviceProvider)
        {
            DeveloperInfo = developerInfo;
            postModel = postModel ?? new PostModel();

            var postDataDocument = requestMessageBase.ConvertEntityToXml();
            base.CommonInitialize(postDataDocument, maxRecordCount, postModel, onlyAllowEncryptMessage);
        }

        /// <summary>
        /// 初始化，获取RequestDocument。（必须要完成 RequestMessage 数据赋值）。
        /// Init中需要对上下文添加当前消息（如果使用上下文）；以及判断消息的加密情况，对解密信息进行解密
        /// </summary>
        /// <param name="postDataDocument"></param>
        /// <param name="postModel"></param>
        /// <returns></returns>
        public override XDocument Init(XDocument postDataDocument, IEncryptPostModel postModel)
        {
            //进行加密判断并处理
            _postModel = postModel as PostModel;
            var postDataStr = postDataDocument.ToString();

            XDocument decryptDoc = postDataDocument;

            if (_postModel != null && !_postModel.Token.IsNullOrWhiteSpace()
                && postDataDocument.Root.Element("Encrypt") != null && !string.IsNullOrEmpty(postDataDocument.Root.Element("Encrypt").Value))
            {
                //使用了加密
                UsingEncryptMessage = true;
                EcryptRequestDocument = postDataDocument;

                WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, _postModel.AppId);
                string msgXml = null;
                var result = msgCrype.DecryptMsg(_postModel.Msg_Signature, _postModel.Timestamp, _postModel.Nonce, postDataStr, ref msgXml);

                //判断result类型
                if (result != 0)
                {
                    //验证没有通过，取消执行
                    CancelExecute = true;
                    TextResponseMessage = "当前 MessageHandler 开启了 OnlyAllowEncryptMessage 设置，只允许处理加密消息，以提高安全性！";
                    return null;
                }

                if (postDataDocument.Root.Element("FromUserName") != null && !string.IsNullOrEmpty(postDataDocument.Root.Element("FromUserName").Value))
                {
                    //TODO：使用了兼容模式，进行验证即可
                    UsingCompatibilityModelEncryptMessage = true;
                }

                decryptDoc = XDocument.Parse(msgXml);//完成解密
            }

            //检查是否限定只能用加密模式
            if (OnlyAllowEncryptMessage && !UsingEncryptMessage)
            {
                CancelExecute = true;
                TextResponseMessage = "当前 MessageHandler 只允许处理加密消息";
                return null;
            }

            RequestMessage = RequestMessageFactory.GetRequestEntity(new TMC(), decryptDoc);
            if (UsingEncryptMessage)
            {
                RequestMessage.Encrypt = postDataDocument.Root.Element("Encrypt").Value;
            }

            //消息去重的基本方法已经在基类 CommonInitialize() 中实现，此处定义特殊规则
            base.SpecialDeduplicationAction = (requestMessage, messageHandler) =>
            {
                var currentMessageContext = messageHandler.GetCurrentMessageContext().ConfigureAwait(false).GetAwaiter().GetResult();
                var lastMessage = currentMessageContext.RequestMessages[currentMessageContext.RequestMessages.Count - 1];

                if (!MessageIsRepeated &&
                            lastMessage is RequestMessageEventBase &&
                            RequestMessage is RequestMessageEventBase &&
                            (lastMessage as RequestMessageEventBase).Event == (RequestMessage as RequestMessageEventBase).Event
                            )
                {
                    var lastEventMessage = lastMessage as RequestMessageEventBase;
                    var currentEventMessage = RequestMessage as RequestMessageEventBase;
                    switch (lastEventMessage.Event)
                    {
                        case Event.user_get_card://领取事件推送
                                                 //文档：https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1451025274
                                                 //问题反馈：https://github.com/JeffreySu/WeiXinMPSDK/issues/1106
                            var lastGetUserCardMessage = lastMessage as RequestMessageEvent_User_Get_Card;
                            var currentGetUserCardMessage = RequestMessage as RequestMessageEvent_User_Get_Card;
                            if (lastGetUserCardMessage.UserCardCode == currentGetUserCardMessage.UserCardCode &&
                                lastGetUserCardMessage.CardId == currentGetUserCardMessage.CardId)
                            {
                                return true;
                            }
                            break;
                        default:
                            break;
                    }
                }
                return false;
            };

            return decryptDoc;
            //消息上下文记录将在 base.CommonInitialize() 中根据去重等条件判断后进行添加
        }

        #endregion

        #region 扩展

        ///// <summary>
        ///// 根据当前的RequestMessage创建指定类型的ResponseMessage
        ///// </summary>
        ///// <typeparam name="TR">基于ResponseMessageBase的响应消息类型</typeparam>
        ///// <returns></returns>
        //public ResponseMessageText CreateResponseMessage<TR>(string content) where TR : ResponseMessageText
        //{
        //    if (RequestMessage == null)
        //    {
        //        return null;
        //    }

        //    var responseMessage = RequestMessage.CreateResponseMessage<TR>();
        //    responseMessage.Content = content;
        //    return responseMessage;
        //}

        #endregion

        #region 消息处理

        /// <summary>
        /// OnExecuting
        /// </summary>
        [Obsolete("请使用异步方法 OnExecutingAsync()", true)]
        public override void OnExecuting()
        {
            throw new MessageHandlerException("请使用异步方法 OnExecutingAsync()");
        }

        [Obsolete("请使用异步方法 OnExecutedAsync()", true)]
        public override void OnExecuted()
        {
            throw new MessageHandlerException("请使用异步方法 OnExecutedAsync()");
        }

        #endregion }
    }
}