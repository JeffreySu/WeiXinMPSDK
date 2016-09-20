/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：QyMessageHandler.cs
    文件功能描述：企业号请求的集中处理方法
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
    
    修改标识：Senparc - 20150507
    修改描述：添加 事件 异步任务完成事件推送
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Xml.Linq;
using Senparc.Weixin.Context;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MessageHandlers;
using Senparc.Weixin.QY.Entities;
using Senparc.Weixin.QY.Helpers;
using Tencent;

namespace Senparc.Weixin.QY.MessageHandlers
{
    public interface IQyMessageHandler : IMessageHandler<IRequestMessageBase, IResponseMessageBase>
    {
        /// <summary>
        /// 原始加密信息
        /// </summary>
        EncryptPostData EncryptPostData { get; set; }
        new IRequestMessageBase RequestMessage { get; set; }
        new IResponseMessageBase ResponseMessage { get; set; }
    }

    public abstract class QyMessageHandler<TC> : MessageHandler<TC, IRequestMessageBase, IResponseMessageBase>, IQyMessageHandler
        where TC : class ,IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        /// <summary>
        /// 上下文（仅限于当前MessageHandler基类内）
        /// </summary>
        public static WeixinContext<TC, IRequestMessageBase, IResponseMessageBase> GlobalWeixinContext = new WeixinContext<TC, IRequestMessageBase, IResponseMessageBase>();

        /// <summary>
        /// 全局消息上下文
        /// </summary>
        public override WeixinContext<TC, IRequestMessageBase, IResponseMessageBase> WeixinContext
        {
            get { return GlobalWeixinContext; }
        }

        /// <summary>
        /// 根据ResponseMessageBase获得转换后的ResponseDocument
        /// 注意：这里每次请求都会根据当前的ResponseMessageBase生成一次，如需重用此数据，建议使用缓存或局部变量
        /// </summary>
        public override XDocument ResponseDocument
        {
            get
            {
                if (ResponseMessage == null)
                {
                    return null;
                }
                return EntityHelper.ConvertEntityToXml(ResponseMessage as ResponseMessageBase);
            }
        }

        /// <summary>
        /// 最后返回的ResponseDocument。
        /// 这里是Senparc.Weixin.QY，应当在ResponseDocument基础上进行加密（每次获取重新加密，所以结果会不同）
        /// </summary>
        public override XDocument FinalResponseDocument
        {
            get
            {
                if (ResponseDocument == null)
                {
                    return null;
                }

                var timeStamp = DateTime.Now.Ticks.ToString();
                var nonce = DateTime.Now.Ticks.ToString();

                WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, _postModel.CorpId);
                string finalResponseXml = null;
                msgCrype.EncryptMsg(ResponseDocument.ToString(), timeStamp, nonce, ref finalResponseXml);//TODO:这里官方的方法已经把EncryptResponseMessage对应的XML输出出来了

                return XDocument.Parse(finalResponseXml);
            }
        }

        /// <summary>
        /// 应用ID
        /// </summary>
        public int AgentId
        {
            get
            {
                return EncryptPostData != null ? EncryptPostData.AgentID : -1;
            }
        }

        /// <summary>
        /// 原始加密信息
        /// </summary>
        public EncryptPostData EncryptPostData { get; set; }

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

      

        private PostModel _postModel;

        public QyMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, maxRecordCount, postModel)
        {
        }

        public QyMessageHandler(XDocument requestDocument, PostModel postModel, int maxRecordCount = 0)
            : base(requestDocument, maxRecordCount, postModel)
        {
        }


        public override XDocument Init(XDocument postDataDocument, object postData)
        {
            _postModel = postData as PostModel;

            var postDataStr = postDataDocument.ToString();
            EncryptPostData = RequestMessageFactory.GetEncryptPostData(postDataStr);

            //2、解密：获得明文字符串
            WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, _postModel.CorpId);
            string msgXml = null;
            var result = msgCrype.DecryptMsg(_postModel.Msg_Signature, _postModel.Timestamp, _postModel.Nonce, postDataStr, ref msgXml);
            /* msgXml
<xml><ToUserName><![CDATA[wx7618c0a6d9358622]]></ToUserName>
<FromUserName><![CDATA[001]]></FromUserName>
<CreateTime>1412585107</CreateTime>
<MsgType><![CDATA[text]]></MsgType>
<Content><![CDATA[你好]]></Content>
<MsgId>4299263624800632834</MsgId>
<AgentID>2</AgentID>
</xml>
             */

            //判断result类型
            if (result != 0)
            {
                //验证没有通过，取消执行
                CancelExcute = true;
                return null;
            }

            var requestDocument = XDocument.Parse(msgXml);
            RequestMessage = RequestMessageFactory.GetRequestEntity(requestDocument);

            //记录上下文
            if (WeixinContextGlobal.UseWeixinContext)
            {
                WeixinContext.InsertMessage(RequestMessage);
            }

            return requestDocument;
        }

        /// <summary>
        /// 根据当前的RequestMessage创建指定类型的ResponseMessage
        /// </summary>
        /// <typeparam name="TR">基于ResponseMessageBase的响应消息类型</typeparam>
        /// <returns></returns>
        public TR CreateResponseMessage<TR>() where TR : ResponseMessageBase
        {
            if (RequestMessage == null)
            {
                return null;
            }

            return RequestMessage.CreateResponseMessage<TR>();
        }


        public override void Execute()
        {
            if (CancelExcute)
            {
                return;
            }

            OnExecuting();

            if (CancelExcute)
            {
                return;
            }

            try
            {
                if (RequestMessage == null)
                {
                    return;
                }

                switch (RequestMessage.MsgType)
                {
                    case RequestMsgType.DEFAULT://第三方回调
                        {
                            if (RequestMessage is IThirdPartyInfoBase)
                            {
                                var thirdPartyInfo = RequestMessage as IThirdPartyInfoBase;
                                switch (thirdPartyInfo.InfoType)
                                {
                                    case ThirdPartyInfo.SUITE_TICKET:
                                        break;
                                    case ThirdPartyInfo.CHANGE_AUTH:
                                        break;
                                    case ThirdPartyInfo.CANCEL_AUTH:
                                        break;
                                    default:
                                        throw new UnknownRequestMsgTypeException("未知的InfoType请求类型", null);
                                }
                                TextResponseMessage = "success";//设置文字类型返回
                            }
                            else
                            {
                                throw new WeixinException("没有找到合适的消息类型。");
                            }
                        }
                        break;
                    //以下是普通信息
                    case RequestMsgType.Text:
                        {
                            var requestMessage = RequestMessage as RequestMessageText;
                            ResponseMessage = OnTextOrEventRequest(requestMessage) ?? OnTextRequest(requestMessage);
                        }
                        break;
                    case RequestMsgType.Location:
                        ResponseMessage = OnLocationRequest(RequestMessage as RequestMessageLocation);
                        break;
                    case RequestMsgType.Image:
                        ResponseMessage = OnImageRequest(RequestMessage as RequestMessageImage);
                        break;
                    case RequestMsgType.Voice:
                        ResponseMessage = OnVoiceRequest(RequestMessage as RequestMessageVoice);
                        break;
                    case RequestMsgType.Video:
                        ResponseMessage = OnVideoRequest(RequestMessage as RequestMessageVideo);
                        break;
                    case RequestMsgType.ShortVideo:
                        ResponseMessage = OnShortVideoRequest(RequestMessage as RequestMessageShortVideo);
                        break;
                    case RequestMsgType.Event:
                        {
                            var requestMessageText = (RequestMessage as IRequestMessageEventBase).ConvertToRequestMessageText();
                            ResponseMessage = OnTextOrEventRequest(requestMessageText) ?? OnEventRequest(RequestMessage as IRequestMessageEventBase);
                        }
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException("未知的MsgType请求类型", null);
                }

                //记录上下文
                if (WeixinContextGlobal.UseWeixinContext && ResponseMessage != null)
                {
                    WeixinContext.InsertMessage(ResponseMessage);
                }
            }
            catch (Exception ex)
            {
                throw new MessageHandlerException("MessageHandler中Execute()过程发生错误：" + ex.Message, ex);
            }
            finally
            {
                OnExecuted();
            }
        }

        public virtual void OnExecuting()
        {
            //消息去重
            if (OmitRepeatedMessage && CurrentMessageContext.RequestMessages.Count > 1)
            {
                var lastMessage = CurrentMessageContext.RequestMessages[CurrentMessageContext.RequestMessages.Count - 2];
                if ((lastMessage.MsgId != 0 && lastMessage.MsgId == RequestMessage.MsgId)//使用MsgId去重
                    ||
                    ((lastMessage.CreateTime == RequestMessage.CreateTime) && lastMessage.MsgType != RequestMessage.MsgType)//使用CreateTime去重（OpenId对象已经是同一个）
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

        /// <summary>
        /// 默认返回消息（当任何OnXX消息没有被重写，都将自动返回此默认消息）
        /// </summary>
        public abstract IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage);

        #region 接收消息方法

        /// <summary>
        /// 预处理文字或事件类型请求。
        /// 这个请求是一个比较特殊的请求，通常用于统一处理来自文字或菜单按钮的同一个执行逻辑，
        /// 会在执行OnTextRequest或OnEventRequest之前触发，具有以下一些特征：
        /// 1、如果返回null，则继续执行OnTextRequest或OnEventRequest
        /// 2、如果返回不为null，则终止执行OnTextRequest或OnEventRequest，返回最终ResponseMessage
        /// 3、如果是事件，则会将RequestMessageEvent自动转为RequestMessageText类型，其中RequestMessageText.Content就是RequestMessageEvent.EventKey
        /// </summary>
        public virtual IResponseMessageBase OnTextOrEventRequest(RequestMessageText requestMessage)
        {
            return null;
        }

        /// <summary>
        /// 文字类型请求
        /// </summary>
        public virtual IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 位置类型请求
        /// </summary>
        public virtual IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 图片类型请求
        /// </summary>
        public virtual IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 语音类型请求
        /// </summary>
        public virtual IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 视频类型请求
        /// </summary>
        public virtual IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 小视频类型请求
        /// </summary>
        public virtual IResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// Event事件类型请求
        /// </summary>
        public virtual IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {
            var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
            IResponseMessageBase responseMessage = null;
            switch (strongRequestMessage.Event)
            {
                case Event.CLICK://菜单点击
                    responseMessage = OnEvent_ClickRequest(RequestMessage as RequestMessageEvent_Click);
                    break;
                case Event.VIEW://URL跳转（view视图）
                    responseMessage = OnEvent_ViewRequest(RequestMessage as RequestMessageEvent_View);
                    break;
                case Event.PIC_PHOTO_OR_ALBUM://弹出拍照或者相册发图
                    responseMessage = OnEvent_PicPhotoOrAlbumRequest(RequestMessage as RequestMessageEvent_Pic_Photo_Or_Album);
                    break;
                case Event.SCANCODE_PUSH://扫码推事件
                    responseMessage = OnEvent_ScancodePushRequest(RequestMessage as RequestMessageEvent_Scancode_Push);
                    break;
                case Event.SCANCODE_WAITMSG://扫码推事件且弹出“消息接收中”提示框
                    responseMessage = OnEvent_ScancodeWaitmsgRequest(RequestMessage as RequestMessageEvent_Scancode_Waitmsg);
                    break;
                case Event.LOCATION_SELECT://弹出地理位置选择器
                    responseMessage = OnEvent_LocationSelectRequest(RequestMessage as RequestMessageEvent_Location_Select);
                    break;
                case Event.PIC_WEIXIN://弹出微信相册发图器
                    responseMessage = OnEvent_PicWeixinRequest(RequestMessage as RequestMessageEvent_Pic_Weixin);
                    break;
                case Event.PIC_SYSPHOTO://弹出系统拍照发图
                    responseMessage = OnEvent_PicSysphotoRequest(RequestMessage as RequestMessageEvent_Pic_Sysphoto);
                    break;
                case Event.subscribe://订阅
                    responseMessage = OnEvent_SubscribeRequest(RequestMessage as RequestMessageEvent_Subscribe);
                    break;
                case Event.unsubscribe://取消订阅
                    responseMessage = OnEvent_UnSubscribeRequest(RequestMessage as RequestMessageEvent_UnSubscribe);
                    break;
                case Event.LOCATION://上报地理位置事件
                    responseMessage = OnEvent_LocationRequest(RequestMessage as RequestMessageEvent_Location);
                    break;
                case Event.ENTER_AGENT://用户进入应用的事件推送(enter_agent)
                    responseMessage = OnEvent_EnterAgentRequest(RequestMessage as RequestMessageEvent_Enter_Agent);
                    break;
                case Event.BATCH_JOB_RESULT://异步任务完成事件推送(batch_job_result)
                    responseMessage = OnEvent_BatchJobResultRequest(RequestMessage as RequestMessageEvent_Batch_Job_Result);
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
            }
            return responseMessage;
        }

        #region Event 下属分类


        /// <summary>
        /// Event事件类型请求之CLICK
        /// </summary>
        public virtual IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 事件之URL跳转视图（View）
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_ViewRequest(RequestMessageEvent_View requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_PicPhotoOrAlbumRequest(RequestMessageEvent_Pic_Photo_Or_Album requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 扫码推事件
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_ScancodePushRequest(RequestMessageEvent_Scancode_Push requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_ScancodeWaitmsgRequest(RequestMessageEvent_Scancode_Waitmsg requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_LocationSelectRequest(RequestMessageEvent_Location_Select requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_PicWeixinRequest(RequestMessageEvent_Pic_Weixin requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_PicSysphotoRequest(RequestMessageEvent_Pic_Sysphoto requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_UnSubscribeRequest(RequestMessageEvent_UnSubscribe requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 用户进入应用的事件推送(enter_agent)
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_EnterAgentRequest(RequestMessageEvent_Enter_Agent requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 异步任务完成事件推送(batch_job_result)
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_BatchJobResultRequest(RequestMessageEvent_Batch_Job_Result requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        #endregion


        #endregion

    }
}
