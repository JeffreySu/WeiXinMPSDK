/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：WorkMessageHandler.cs
    文件功能描述：企业号请求的集中处理方法
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
    
    修改标识：Senparc - 20150507
    修改描述：添加 事件 异步任务完成事件推送
 
    修改标识：Senparc - 20160802
    修改描述：将其AgentID类型改为int?

    修改标识：heavenwing - 20160909
    修改描述：完善第三方回调事件处理

    修改标识：pekrr1e - 20180503
    修改描述：v1.4.1 增加“接收通讯录变更事件”

    修改标识：Senparc - 20180909
    修改描述：v3.1.2 RequestMessageInfo_Contact_Sync 改名为 RequestMessageInfo_Change_Contact；
                     枚举 ThirdPartyInfo.CONTACT_SYNC 改名为 ThirdPartyInfo.CHANGE_CONTACT；
                     OnThirdPartyEvent_Contact_Sync 改名为 OnThirdPartyEvent_Change_Contact()

    修改标识：pekrr1e - 20180503
    修改描述：v3.1.16 优化 MessageHandler 构造函数，提供 PostModel 默认值

    修改标识：Senparc - 20181117
    修改描述：v3.2.0 Execute() 重写方法名称改为 BuildResponseMessage()
    
    修改标识：Senparc - 20181226
    修改描述：v3.3.2 修改 DateTime 为 DateTimeOffset

    修改标识：Senparc - 20190917
    修改描述：v3.6.0 支持新版本 MessageHandler 和 WeixinContext，支持使用分布式缓存储存上下文消息

    修改标识：OrchesAdam - 2019119
    修改描述：v3.7.104.2 添加“上报企业客户变更事件”

    修改标识：OrchesAdam - 20200430
    修改描述：添加“外部联系人编辑企业客户”消息推送

    修改标识：OrchesAdam - 20200430
    修改描述：添加“客户群变更事件”（OnEvent_ChangeExternalChatRequest）

    修改标识：gokeiyou - 20201013
    修改描述：v3.7.604 添加外部联系人管理 > 客户管理相关接口

    修改标识：Billzjh - 20201210
    修改描述：v3.8.101 添加 OnThirdPartyEvent_REGISTER_CORP() 事件

    修改标识：WangDrama - 20210630
    修改描述：v3.9.600 添加 RequestMessageEvent_Change_External_Chat_Base 事件中 ChangeType 的判断
    
    修改标识：Senparc - 20210324
    修改描述：v3.14.6 添加：审批申请状态变化回调通知
    
    修改标识：ccccccmd - 20220227
    修改描述：v3.14.10 添加异步方法

    修改标识：Senparc - 20230914
    修改描述：v3.16.4 企业微信三方代开发处理事件: 修复 Async 方法循环调用的 Bug

    修改标识：XiaoPoTian - 20231119
    修改描述：v3.18.1 添加 RequestMessageEvent_Change_External_Tag_Base 事件中 ChangeType 的判断
    
    修改标识：IcedMango - 20240229
    修改描述：添加: 企业微信会话存档-产生会话回调事件

    修改标识：LofyLiu - 20240315
    修改描述：添加: 模板卡片点击回调事件
----------------------------------------------------------------*/

using System;
using System.IO;
using System.Xml.Linq;
using Senparc.NeuChar.Context;
using Senparc.Weixin.Exceptions;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Helpers;
using Senparc.Weixin.Work.Tencent;
using Senparc.NeuChar;
using Senparc.NeuChar.ApiHandlers;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.Entities.Request.Event;

namespace Senparc.Weixin.Work.MessageHandlers
{
    public interface IWorkMessageHandler : IMessageHandler<IWorkRequestMessageBase, IWorkResponseMessageBase>
    {
        /// <summary>
        /// 原始加密信息
        /// </summary>
        EncryptPostData EncryptPostData { get; set; }
        new IWorkRequestMessageBase RequestMessage { get; set; }
        new IWorkResponseMessageBase ResponseMessage { get; set; }
    }

    public abstract partial class WorkMessageHandler<TMC>
        : MessageHandler<TMC, IWorkRequestMessageBase, IWorkResponseMessageBase>, IWorkMessageHandler
        where TMC : class, IMessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>, new()
    {
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
                return EntityHelper.ConvertEntityToXml(ResponseMessage as WorkResponseMessageBase);
            }
        }

        /// <summary>
        /// 最后返回的ResponseDocument。
        /// 这里是Senparc.Weixin.Work，应当在ResponseDocument基础上进行加密（每次获取重新加密，所以结果会不同）
        /// </summary>
        public override XDocument FinalResponseDocument
        {
            get
            {
                if (ResponseDocument == null)
                {
                    return null;
                }

                var timeStamp = SystemTime.Now.Ticks.ToString();
                var nonce = SystemTime.Now.Ticks.ToString();

                WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, _postModel.CorpId);
                string finalResponseXml = null;
                msgCrype.EncryptMsg(ResponseDocument.ToString(), timeStamp, nonce, ref finalResponseXml);//TODO:这里官方的方法已经把EncryptResponseMessage对应的XML输出出来了

                return XDocument.Parse(finalResponseXml);
            }
        }

        /// <summary>
        /// 应用ID
        /// </summary>
        public int? AgentId
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
        /// 正常情况下只有当执行Execute()方法后才可能有值。
        /// 也可以结合Cancel，提前给ResponseMessage赋值。
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

        private PostModel _postModel;

        /// <summary>
        /// 请求和响应消息定义
        /// </summary>
        public override MessageEntityEnlightener MessageEntityEnlightener { get { return WorkMessageEntityEnlightener.Instance; } }

        public override ApiEnlightener ApiEnlightener { get { return WorkApiEnlightener.Instance; } }


        public WorkMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0, IServiceProvider serviceProvider = null)
            : base(inputStream, postModel, maxRecordCount, serviceProvider: serviceProvider)
        {
        }

        public WorkMessageHandler(XDocument requestDocument, PostModel postModel, int maxRecordCount = 0, IServiceProvider serviceProvider = null)
            : base(requestDocument, postModel, maxRecordCount, serviceProvider: serviceProvider)
        {
        }


        public override XDocument Init(XDocument postDataDocument, IEncryptPostModel postModel)
        {
            _postModel = postModel as PostModel ?? new PostModel();

            var postDataStr = postDataDocument.ToString();

            //Work中消息默认都是强制加密的，但通知似乎没有加密
            UsingEncryptMessage = postDataDocument.Root.Element("Encrypt") != null;

            EncryptPostData = RequestMessageFactory.GetEncryptPostData(postDataStr);

            XDocument requestDocument;
            //2、解密：获得明文字符串
            if (UsingEncryptMessage)
            {
                string msgXml = null;

                WXBizMsgCrypt msgCrype = new WXBizMsgCrypt(_postModel.Token, _postModel.EncodingAESKey, _postModel.CorpId);
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
                    CancelExecute = true;
                    return null;
                }

                requestDocument = XDocument.Parse(msgXml);
            }
            else
            {
                requestDocument = postDataDocument;//TODO:深拷贝
            }

            RequestMessage = RequestMessageFactory.GetRequestEntity<TMC>(new TMC(), doc: requestDocument);

            return requestDocument;

            //消息上下文记录将在 base.CommonInitialize() 中根据去重等条件判断后进行添加
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
        /// 预处理文字或事件类型请求。
        /// 这个请求是一个比较特殊的请求，通常用于统一处理来自文字或菜单按钮的同一个执行逻辑，
        /// 会在执行OnTextRequest或OnEventRequest之前触发，具有以下一些特征：
        /// 1、如果返回null，则继续执行OnTextRequest或OnEventRequest
        /// 2、如果返回不为null，则终止执行OnTextRequest或OnEventRequest，返回最终ResponseMessage
        /// 3、如果是事件，则会将RequestMessageEvent自动转为RequestMessageText类型，其中RequestMessageText.Content就是RequestMessageEvent.EventKey
        /// </summary>
        public virtual IWorkResponseMessageBase OnTextOrEventRequest(RequestMessageText requestMessage)
        {
            return null;
        }

        /// <summary>
        /// 文字类型请求
        /// </summary>
        public virtual IWorkResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 位置类型请求
        /// </summary>
        public virtual IWorkResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 图片类型请求
        /// </summary>
        public virtual IWorkResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 语音类型请求
        /// </summary>
        public virtual IWorkResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 视频类型请求
        /// </summary>
        public virtual IWorkResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 小视频类型请求
        /// </summary>
        public virtual IWorkResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 文件类型请求
        /// </summary>
        public virtual IWorkResponseMessageBase OnFileRequest(RequestMessageFile requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// Event事件类型请求
        /// </summary>
        // public virtual IWorkResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        // {
        //     var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
        //     IWorkResponseMessageBase responseMessage = null;
        //     switch (strongRequestMessage.Event)
        //     {
        //         case Event.CLICK://菜单点击
        //             responseMessage = OnEvent_ClickRequest(RequestMessage as RequestMessageEvent_Click);
        //             break;
        //         case Event.VIEW://URL跳转（view视图）
        //             responseMessage = OnEvent_ViewRequest(RequestMessage as RequestMessageEvent_View);
        //             break;
        //         case Event.PIC_PHOTO_OR_ALBUM://弹出拍照或者相册发图
        //             responseMessage = OnEvent_PicPhotoOrAlbumRequest(RequestMessage as RequestMessageEvent_Pic_Photo_Or_Album);
        //             break;
        //         case Event.SCANCODE_PUSH://扫码推事件
        //             responseMessage = OnEvent_ScancodePushRequest(RequestMessage as RequestMessageEvent_Scancode_Push);
        //             break;
        //         case Event.SCANCODE_WAITMSG://扫码推事件且弹出“消息接收中”提示框
        //             responseMessage = OnEvent_ScancodeWaitmsgRequest(RequestMessage as RequestMessageEvent_Scancode_Waitmsg);
        //             break;
        //         case Event.LOCATION_SELECT://弹出地理位置选择器
        //             responseMessage = OnEvent_LocationSelectRequest(RequestMessage as RequestMessageEvent_Location_Select);
        //             break;
        //         case Event.PIC_WEIXIN://弹出微信相册发图器
        //             responseMessage = OnEvent_PicWeixinRequest(RequestMessage as RequestMessageEvent_Pic_Weixin);
        //             break;
        //         case Event.PIC_SYSPHOTO://弹出系统拍照发图
        //             responseMessage = OnEvent_PicSysphotoRequest(RequestMessage as RequestMessageEvent_Pic_Sysphoto);
        //             break;
        //         case Event.subscribe://订阅
        //             responseMessage = OnEvent_SubscribeRequest(RequestMessage as RequestMessageEvent_Subscribe);
        //             break;
        //         case Event.unsubscribe://取消订阅
        //             responseMessage = OnEvent_UnSubscribeRequest(RequestMessage as RequestMessageEvent_UnSubscribe);
        //             break;
        //         case Event.LOCATION://上报地理位置事件
        //             responseMessage = OnEvent_LocationRequest(RequestMessage as RequestMessageEvent_Location);
        //             break;
        //         case Event.ENTER_AGENT://用户进入应用的事件推送(enter_agent)
        //             responseMessage = OnEvent_EnterAgentRequest(RequestMessage as RequestMessageEvent_Enter_Agent);
        //             break;
        //         case Event.BATCH_JOB_RESULT://异步任务完成事件推送(batch_job_result)
        //             responseMessage = OnEvent_BatchJobResultRequest(RequestMessage as RequestMessageEvent_Batch_Job_Result);
        //             break;
        //         case Event.change_contact:
        //             var ccRequestMessage = RequestMessage as IRequestMessageEvent_Change_Contact_Base;
        //             switch (ccRequestMessage.ChangeType)
        //             {
        //                 case ContactChangeType.create_user:
        //                     responseMessage = OnEvent_ChangeContactCreateUserRequest(RequestMessage as RequestMessageEvent_Change_Contact_User_Create);
        //                     break;
        //                 case ContactChangeType.update_user:
        //                     responseMessage = OnEvent_ChangeContactUpdateUserRequest(RequestMessage as RequestMessageEvent_Change_Contact_User_Update);
        //                     break;
        //                 case ContactChangeType.delete_user:
        //                     responseMessage = OnEvent_ChangeContactDeleteUserRequest(RequestMessage as RequestMessageEvent_Change_Contact_User_Base);
        //                     break;
        //                 case ContactChangeType.create_party:
        //                     responseMessage = OnEvent_ChangeContactCreatePartyRequest(RequestMessage as RequestMessageEvent_Change_Contact_Party_Create);
        //                     break;
        //                 case ContactChangeType.update_party:
        //                     responseMessage = OnEvent_ChangeContactUpdatePartyRequest(RequestMessage as RequestMessageEvent_Change_Contact_Party_Update);
        //                     break;
        //                 case ContactChangeType.delete_party:
        //                     responseMessage = OnEvent_ChangeContactDeletePartyRequest(RequestMessage as RequestMessageEvent_Change_Contact_Party_Base);
        //                     break;
        //                 case ContactChangeType.update_tag:
        //                     responseMessage = OnEvent_ChangeContactUpdateTagRequest(RequestMessage as RequestMessageEvent_Change_Contact_Tag_Update);
        //                     break;
        //                 default:
        //                     throw new UnknownRequestMsgTypeException("未知的Event.change_contact下属请求信息", null);
        //             }
        //             break;
        //         //外部联系人事件相关
        //         case Event.CHANGE_EXTERNAL_CONTACT:
        //             var cecRequestMessage = RequestMessage as IRequestMessageEvent_Change_ExternalContact_Base;
        //             switch (cecRequestMessage.ChangeType)
        //             {
        //                 case ExternalContactChangeType.add_external_contact:
        //                     responseMessage =
        //                         OnEvent_ChangeExternalContactAddRequest(
        //                             RequestMessage as RequestMessageEvent_Change_ExternalContact_Add);
        //                     break;
        //                 case ExternalContactChangeType.edit_external_contact:
        //                     OnEvent_ChangeExternalContactUpdateRequest(
        //                         requestMessage as RequestMessageEvent_Change_ExternalContact_Modified);
        //                     break;
        //                 case ExternalContactChangeType.add_half_external_contact:
        //                     responseMessage =
        //                         OnEvent_ChangeExternalContactAddHalfRequest(
        //                             RequestMessage as RequestMessageEvent_Change_ExternalContact_Add_Half);
        //                     break;
        //                 case ExternalContactChangeType.del_external_contact:
        //                     responseMessage =
        //                         OnEvent_ChangeExternalContactDelRequest(
        //                             RequestMessage as RequestMessageEvent_Change_ExternalContact_Del);
        //                     break;
        //                 case ExternalContactChangeType.del_follow_user:
        //                     responseMessage = OnEvent_ChangeExternalContactDelFollowUserRequest(
        //                         RequestMessage as RequestMessageEvent_Change_ExternalContact_Del_FollowUser);
        //                     break;
        //                 case ExternalContactChangeType.msg_audit_approved:
        //                     responseMessage =
        //                         OnEvent_ChangeExternalContactMsgAudit(
        //                             RequestMessage as RequestMessageEvent_Change_ExternalContact_MsgAudit);
        //                     break;
        //                 default:
        //                     throw new UnknownRequestMsgTypeException("未知的外部联系人事件Event.CHANGE_EXTERNAL_CONTACT下属请求信息", null);
        //             }
        //             break;
        //         case Event.CHANGE_EXTERNAL_CHAT://客户群变更事件
        //             var cechat = RequestMessage as RequestMessageEvent_Change_External_Chat_Base;
        //             switch (cechat.ChangeType)
        //             {
        //                 case ExternalChatChangeType.create:
        //                     responseMessage = OnEvent_ChangeExternalChatCreateRequest(RequestMessage as RequestMessageEvent_Change_External_Chat_Create);
        //                     break;
        //                 case ExternalChatChangeType.update:
        //                     responseMessage = OnEvent_ChangeExternalChatUpdateRequest(RequestMessage as RequestMessageEvent_Change_External_Chat_Update);
        //                     break;
        //                 case ExternalChatChangeType.dismiss:
        //                     responseMessage = OnEvent_ChangeExternalChatDismissRequest(RequestMessage as RequestMessageEvent_Change_External_Chat_Dismiss);
        //                     break;
        //                 default:
        //                     throw new UnknownRequestMsgTypeException("未知的客户群变更事件Event.CHANGE_EXTERNAL_CHAT下属请求信息", null);
        //             }
        //             break;
        //         case Event.LIVING_STATUS_CHANGE://直播事件回调
        //             responseMessage = OnEvent_Living_Status_ChangeRequest(RequestMessage as RequestMessageEvent_Living_Status_Change_Base);
        //             break;
        //         case Event.SYS_APPROVAL_CHANGE://系统应用审批状态变化通知回调
        //             responseMessage = OnEvent_Sys_Approval_Change_Status_ChangeRequest(RequestMessage as RequestMessageEvent_SysApprovalChange);
        //             break;
        //         case Event.OPEN_APPROVAL_CHANGE://自建应用审批状态变化通知回调
        //             responseMessage = OnEvent_Open_Approval_Change_Status_ChangeRequest(RequestMessage as RequestMessageEvent_OpenApprovalChange);
        //             break;
        //         default:
        //             throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
        //     }
        //     return responseMessage;
        // }

        #region Event 下属分类


        /// <summary>
        /// Event事件类型请求之CLICK
        /// </summary>
        public virtual IWorkResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 事件之URL跳转视图（View）
        /// </summary>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ViewRequest(RequestMessageEvent_View requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_PicPhotoOrAlbumRequest(RequestMessageEvent_Pic_Photo_Or_Album requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 扫码推事件
        /// </summary>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ScancodePushRequest(RequestMessageEvent_Scancode_Push requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ScancodeWaitmsgRequest(RequestMessageEvent_Scancode_Waitmsg requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_LocationSelectRequest(RequestMessageEvent_Location_Select requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_PicWeixinRequest(RequestMessageEvent_Pic_Weixin requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_PicSysphotoRequest(RequestMessageEvent_Pic_Sysphoto requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_UnSubscribeRequest(RequestMessageEvent_UnSubscribe requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 用户进入应用的事件推送(enter_agent)
        /// </summary>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_EnterAgentRequest(RequestMessageEvent_Enter_Agent requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 异步任务完成事件推送(batch_job_result)
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_BatchJobResultRequest(RequestMessageEvent_Batch_Job_Result requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 新增成员事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeContactCreateUserRequest(RequestMessageEvent_Change_Contact_User_Create requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 更新成员事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeContactUpdateUserRequest(RequestMessageEvent_Change_Contact_User_Update requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 删除成员事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeContactDeleteUserRequest(RequestMessageEvent_Change_Contact_User_Base requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 新增部门事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeContactCreatePartyRequest(RequestMessageEvent_Change_Contact_Party_Create requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 更新部门事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeContactUpdatePartyRequest(RequestMessageEvent_Change_Contact_Party_Update requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 删除部门事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeContactDeletePartyRequest(RequestMessageEvent_Change_Contact_Party_Base requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 标签成员变更事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeContactUpdateTagRequest(RequestMessageEvent_Change_Contact_Tag_Update requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 添加企业客户事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalContactAddRequest(
            RequestMessageEvent_Change_ExternalContact_Add requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 外部联系人编辑企业客户
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalContactUpdateRequest(
            RequestMessageEvent_Change_ExternalContact_Modified requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 外部联系人免验证添加成员事件 推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalContactAddHalfRequest(
            RequestMessageEvent_Change_ExternalContact_Add_Half requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 删除企业客户事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalContactDelRequest(
            RequestMessageEvent_Change_ExternalContact_Del requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 删除跟进成员事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalContactDelFollowUserRequest(
            RequestMessageEvent_Change_ExternalContact_Del_FollowUser requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 用户同意消息存档事件（灰度）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalContactMsgAudit(
            RequestMessageEvent_Change_ExternalContact_MsgAudit requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 客户群创建事件 推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalChatCreateRequest(
            RequestMessageEvent_Change_External_Chat_Create requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 客户群变更事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalChatUpdateRequest(
            RequestMessageEvent_Change_External_Chat_Update requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 客户群解散事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalChatDismissRequest(
            RequestMessageEvent_Change_External_Chat_Dismiss requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        #region 企业客户标签事件
        /// <summary>
        /// 企业客户标签事件-创建
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalTagCreateRequest(
            RequestMessageEvent_Change_External_Tag_Create requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 企业客户标签事件-变更
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalTagUpdateRequest(
            RequestMessageEvent_Change_External_Tag_Update requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 企业客户标签事件-删除
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalTagDeleteRequest(
            RequestMessageEvent_Change_External_Tag_Delete requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 企业客户标签事件-重排
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_ChangeExternalTagShuffleRequest(
            RequestMessageEvent_Change_External_Tag_Shuffle requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        #endregion

        public virtual IWorkResponseMessageBase OnEvent_Living_Status_ChangeRequest(
            RequestMessageEvent_Living_Status_Change_Base requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 修改设置工作台自定义开关事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_SwitchWorkBenchMode(RequestMessageEvent_Switch_WorkBench_Mode requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        #region 审批事件
        /// <summary>
        /// 系统审批申请状态变化回调通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_Sys_Approval_Change_Status_ChangeRequest(
          RequestMessageEvent_SysApprovalChange requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 自建审批申请状态变化回调通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_Open_Approval_Change_Status_ChangeRequest(
          RequestMessageEvent_OpenApprovalChange requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        #endregion
        
        /// <summary>
        /// 企业微信会话存档-产生会话回调事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_MsgAuditNotifyRequest(RequestMessageEvent_MsgAuditNotify requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 模板卡片点击回调事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IWorkResponseMessageBase OnEvent_TemplateCardEventClickRequest(RequestMessageEvent_TemplateCardClick requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        #endregion //Event 下属分类
        #endregion

        #region 第三方回调事件
        public const string ThirdPartyEventSuccessResult = "success";
        // private string OnThirdPartyEvent(IThirdPartyInfoBase thirdPartyInfo)
        // {
        //     switch (thirdPartyInfo.InfoType)
        //     {
        //         case ThirdPartyInfo.SUITE_TICKET:
        //             return OnThirdPartyEvent_Suite_Ticket((RequestMessageInfo_Suite_Ticket)thirdPartyInfo);
        //         case ThirdPartyInfo.CHANGE_AUTH:
        //             return OnThirdPartyEvent_Change_Auth((RequestMessageInfo_Change_Auth)thirdPartyInfo);
        //         case ThirdPartyInfo.CANCEL_AUTH:
        //             return OnThirdPartyEvent_Cancel_Auth((RequestMessageInfo_Cancel_Auth)thirdPartyInfo);
        //         case ThirdPartyInfo.CREATE_AUTH:
        //             return OnThirdPartyEvent_Create_Auth((RequestMessageInfo_Create_Auth)thirdPartyInfo);
        //         case ThirdPartyInfo.CHANGE_CONTACT:
        //             return OnThirdPartyEvent_Change_Contact((RequestMessageInfo_Change_Contact)thirdPartyInfo);
        //         case ThirdPartyInfo.REGISTER_CORP:
        //             return OnThirdPartyEvent_REGISTER_CORP((RequestMessager_Register_Corp)thirdPartyInfo);
        //         case ThirdPartyInfo.CHANGE_EXTERNAL_CONTACT:
        //             {
        //                 var cecRequestMessage = RequestMessage as IRequestMessageEvent_Change_ExternalContact_Base;
        //                 switch (cecRequestMessage.ChangeType)
        //                 {
        //                     case ExternalContactChangeType.add_external_contact:
        //                         return OnThirdPartyEvent_ChangeExternalContactAddRequest(
        //                                 RequestMessage as RequestMessageEvent_Change_ExternalContact_Add);
        //                     case ExternalContactChangeType.edit_external_contact:
        //                         return OnThirdPartyEvent_ChangeExternalContactUpdateRequest(
        //                             RequestMessage as RequestMessageEvent_Change_ExternalContact_Modified);
        //                     case ExternalContactChangeType.add_half_external_contact:
        //                         return OnThirdPartyEvent_ChangeExternalContactAddHalfRequest(
        //                                 RequestMessage as RequestMessageEvent_Change_ExternalContact_Add_Half);
        //                     case ExternalContactChangeType.del_external_contact:
        //                         return OnThirdPartyEvent_ChangeExternalContactDelRequest(
        //                                 RequestMessage as RequestMessageEvent_Change_ExternalContact_Del);
        //                     case ExternalContactChangeType.del_follow_user:
        //                         return OnThirdPartyEvent_ChangeExternalContactDelFollowUserRequest(
        //                             RequestMessage as RequestMessageEvent_Change_ExternalContact_Del_FollowUser);
        //                     case ExternalContactChangeType.msg_audit_approved:
        //                         return OnThirdPartyEvent_ChangeExternalContactMsgAudit(
        //                                 RequestMessage as RequestMessageEvent_Change_ExternalContact_MsgAudit);
        //                     default:
        //                         throw new UnknownRequestMsgTypeException("未知的外部联系人事件Event.CHANGE_EXTERNAL_CONTACT下属请求信息", null);
        //                 }
        //             }
        //         default:
        //             throw new UnknownRequestMsgTypeException("未知的InfoType请求类型", null);
        //     }
        // }

        protected virtual string OnThirdPartyEvent_Change_Contact(RequestMessageInfo_Change_Contact thirdPartyInfo)
        {
            return ThirdPartyEventSuccessResult;
        }

        [Obsolete("请使用修复拼写之后的方法:OnThirdPartyEvent_Register_Corp", true)]
        protected virtual string OnThirdPartyEvent_REGISTER_CORP(RequestMessager_Register_Corp thirdPartyInfo)
        {
            return OnThirdPartyEvent_Register_Corp(thirdPartyInfo);
        }

        protected virtual string OnThirdPartyEvent_Register_Corp(RequestMessager_Register_Corp thirdPartyInfo)
        {
            return ThirdPartyEventSuccessResult;
        }

        protected virtual string OnThirdPartyEvent_Create_Auth(RequestMessageInfo_Create_Auth thirdPartyInfo)
        {
            return ThirdPartyEventSuccessResult;
        }

        protected virtual string OnThirdPartyEvent_Cancel_Auth(RequestMessageInfo_Cancel_Auth thirdPartyInfo)
        {
            return ThirdPartyEventSuccessResult;
        }

        protected virtual string OnThirdPartyEvent_Change_Auth(RequestMessageInfo_Change_Auth thirdPartyInfo)
        {
            return ThirdPartyEventSuccessResult;
        }

        protected virtual string OnThirdPartyEvent_Suite_Ticket(RequestMessageInfo_Suite_Ticket thirdPartyInfo)
        {
            return ThirdPartyEventSuccessResult;
        }

        protected virtual string OnThirdPartEvent_ResetPermanentCode(RequestMessageInfo_Reset_Permanent_Code requestMessage)
        {
            return ThirdPartyEventSuccessResult;
        }

        #region 外部联系人

        protected virtual string OnThirdPartyEvent_ChangeExternalContactAddRequest(RequestMessageEvent_Change_ExternalContact_Add requestMessage)
        {
            return ThirdPartyEventSuccessResult;
        }

        protected virtual string OnThirdPartyEvent_ChangeExternalContactUpdateRequest(RequestMessageEvent_Change_ExternalContact_Modified requestMessage)
        {
            return ThirdPartyEventSuccessResult;
        }

        protected virtual string OnThirdPartyEvent_ChangeExternalContactAddHalfRequest(RequestMessageEvent_Change_ExternalContact_Add_Half requestMessage)
        {
            return ThirdPartyEventSuccessResult;
        }

        protected virtual string OnThirdPartyEvent_ChangeExternalContactDelRequest(RequestMessageEvent_Change_ExternalContact_Del requestMessage)
        {
            return ThirdPartyEventSuccessResult;
        }

        protected virtual string OnThirdPartyEvent_ChangeExternalContactDelFollowUserRequest(RequestMessageEvent_Change_ExternalContact_Del_FollowUser requestMessage)
        {
            return ThirdPartyEventSuccessResult;
        }

        protected virtual string OnThirdPartyEvent_ChangeExternalContactMsgAudit(RequestMessageEvent_Change_ExternalContact_MsgAudit requestMessage)
        {
            return ThirdPartyEventSuccessResult;
        }

        #endregion

        #endregion
    }
}
