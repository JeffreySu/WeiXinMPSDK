/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：WorkMessageHandler.Async.cs
    文件功能描述：企业号请求的集中处理方法


    创建标识：Senparc - 20191004

    修改标识：ccccccmd - 20220227
    修改描述：v3.14.10 添加异步方法

    修改标识：Senparc - 20230914
    修改描述：v3.16.4 企业微信三方代开发处理事件: 修复 Async 方法循环调用的 Bug

    修改标识：IcedMango - 20240229
    修改描述：添加: 企业微信会话存档-产生会话回调事件

    修改标识：LofyLiu - 20240315
    修改描述：添加: 模板卡片点击回调事件

    修改标识: IcedMango - 20241114
    修改描述: 添加: 通用模板卡片右上角菜单事件推送; 修复不正确的通用模板卡片事件推送类型

    修改标识：Senparc - 20251203
    修改描述：添加: 微信客服消息与事件回调通知（KF_MSG_OR_EVENT）异步事件处理方法

    修改标识：Senparc - 20260523
    修改描述：补充更新日志，完善文件头修改记录

    修改标识：Senparc - 20260718
    修改描述：v3.32.0 移除异步兼容回调中的 Task.Run 线程池排队

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

    修改标识：Senparc - 20260725
    修改描述：v3.32.1 增加应用邮箱新邮件变更异步回调处理入口

----------------------------------------------------------------*/

using Senparc.NeuChar.Context;
using Senparc.Weixin.Exceptions;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.Work.Entities;
using Senparc.NeuChar;
using System.Threading.Tasks;
using System.Threading;
using Senparc.Weixin.Work.Entities.Request.Event;
using System;

namespace Senparc.Weixin.Work.MessageHandlers
{
    public abstract partial class WorkMessageHandler<TMC>
        : MessageHandler<TMC, IWorkRequestMessageBase, IWorkResponseMessageBase>, IWorkMessageHandler
        where TMC : class, IMessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>, new()
    {
        public override async Task BuildResponseMessageAsync(CancellationToken cancellationToken)
        {
            switch (RequestMessage.MsgType)
            {
                case RequestMsgType.Unknown: //第三方回调
                {
                    if (RequestMessage is IThirdPartyInfoBase)
                    {
                        var thirdPartyInfo = RequestMessage as IThirdPartyInfoBase;
                        TextResponseMessage = await OnThirdPartyEventAsync(thirdPartyInfo);
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

                    ResponseMessage = await OnTextOrEventRequestAsync(requestMessage) ??
                                      await OnTextRequestAsync(requestMessage);
                }
                    break;
                case RequestMsgType.Location:
                    ResponseMessage = await OnLocationRequestAsync(RequestMessage as RequestMessageLocation);
                    break;
                case RequestMsgType.Image:
                    ResponseMessage = await OnImageRequestAsync(RequestMessage as RequestMessageImage);
                    break;
                case RequestMsgType.Voice:
                    ResponseMessage = await OnVoiceRequestAsync(RequestMessage as RequestMessageVoice);
                    break;
                case RequestMsgType.Video:
                    ResponseMessage = await OnVideoRequestAsync(RequestMessage as RequestMessageVideo);
                    break;
                case RequestMsgType.ShortVideo:
                    ResponseMessage = await OnShortVideoRequestAsync(RequestMessage as RequestMessageShortVideo);
                    break;
                case RequestMsgType.File:
                    ResponseMessage = await OnFileRequestAsync(RequestMessage as RequestMessageFile);
                    break;
                case RequestMsgType.Event:
                {
                    var requestMessageText = (RequestMessage as IRequestMessageEventBase).ConvertToRequestMessageText();

                    ResponseMessage = await OnTextOrEventRequestAsync(requestMessageText) ??
                                      await OnEventRequestAsync(RequestMessage as IRequestMessageEventBase);
                }
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的MsgType请求类型", null);
            }
        }


        #region 接收消息方法

        public virtual async Task<IWorkResponseMessageBase> DefaultResponseMessageAsync(
            IWorkRequestMessageBase requestMessage)
        {
            return await Task.FromResult(DefaultResponseMessage(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 预处理文字或事件类型请求。
        /// 这个请求是一个比较特殊的请求，通常用于统一处理来自文字或菜单按钮的同一个执行逻辑，
        /// 会在执行OnTextRequest或OnEventRequest之前触发，具有以下一些特征：
        /// 1、如果返回null，则继续执行OnTextRequest或OnEventRequest
        /// 2、如果返回不为null，则终止执行OnTextRequest或OnEventRequest，返回最终ResponseMessage
        /// 3、如果是事件，则会将RequestMessageEvent自动转为RequestMessageText类型，其中RequestMessageText.Content就是RequestMessageEvent.EventKey
        /// </summary>
        public virtual async Task<IWorkResponseMessageBase> OnTextOrEventRequestAsync(RequestMessageText requestMessage)
        {
            return await Task.FromResult(OnTextOrEventRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 文字类型请求
        /// </summary>
        public virtual async Task<IWorkResponseMessageBase> OnTextRequestAsync(RequestMessageText requestMessage)
        {
            return await Task.FromResult(OnTextRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 位置类型请求
        /// </summary>
        public virtual async Task<IWorkResponseMessageBase> OnLocationRequestAsync(
            RequestMessageLocation requestMessage)
        {
            return await Task.FromResult(OnLocationRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 图片类型请求
        /// </summary>
        public virtual async Task<IWorkResponseMessageBase> OnImageRequestAsync(RequestMessageImage requestMessage)
        {
            return await Task.FromResult(OnImageRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 语音类型请求
        /// </summary>
        public virtual async Task<IWorkResponseMessageBase> OnVoiceRequestAsync(RequestMessageVoice requestMessage)
        {
            return await Task.FromResult(OnVoiceRequest(requestMessage)).ConfigureAwait(false);
        }


        /// <summary>
        /// 视频类型请求
        /// </summary>
        public virtual async Task<IWorkResponseMessageBase> OnVideoRequestAsync(RequestMessageVideo requestMessage)
        {
            return await Task.FromResult(OnVideoRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 小视频类型请求
        /// </summary>
        public virtual async Task<IWorkResponseMessageBase> OnShortVideoRequestAsync(
            RequestMessageShortVideo requestMessage)
        {
            return await Task.FromResult(OnShortVideoRequest(requestMessage)).ConfigureAwait(false);
        }


        /// <summary>
        /// 文件类型请求
        /// </summary>
        public virtual async Task<IWorkResponseMessageBase> OnFileRequestAsync(RequestMessageFile requestMessage)
        {
            return await Task.FromResult(OnFileRequest(requestMessage)).ConfigureAwait(false);
        }


        /// <summary>
        /// Event事件类型请求
        /// </summary>
        public virtual async Task<IWorkResponseMessageBase> OnEventRequestAsync(IRequestMessageEventBase requestMessage)
        {
            var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
            IWorkResponseMessageBase responseMessage = null;

            switch (strongRequestMessage.Event)
            {
                case Event.CLICK: //菜单点击
                    responseMessage = await OnEvent_ClickRequestAsync(RequestMessage as RequestMessageEvent_Click);
                    break;
                case Event.VIEW: //URL跳转（view视图）
                    responseMessage = await OnEvent_ViewRequestAsync(RequestMessage as RequestMessageEvent_View);
                    break;
                case Event.PIC_PHOTO_OR_ALBUM: //弹出拍照或者相册发图
                    responseMessage = await OnEvent_PicPhotoOrAlbumRequestAsync(
                        RequestMessage as RequestMessageEvent_Pic_Photo_Or_Album);
                    break;
                case Event.SCANCODE_PUSH: //扫码推事件
                    responseMessage =
                        await OnEvent_ScancodePushRequestAsync(RequestMessage as RequestMessageEvent_Scancode_Push);
                    break;
                case Event.SCANCODE_WAITMSG: //扫码推事件且弹出“消息接收中”提示框
                    responseMessage = await
                        OnEvent_ScancodeWaitmsgRequestAsync(RequestMessage as RequestMessageEvent_Scancode_Waitmsg);
                    break;
                case Event.LOCATION_SELECT: //弹出地理位置选择器
                    responseMessage = await
                        OnEvent_LocationSelectRequestAsync(RequestMessage as RequestMessageEvent_Location_Select);
                    break;
                case Event.PIC_WEIXIN: //弹出微信相册发图器
                    responseMessage =
                        await OnEvent_PicWeixinRequestAsync(RequestMessage as RequestMessageEvent_Pic_Weixin);
                    break;
                case Event.PIC_SYSPHOTO: //弹出系统拍照发图
                    responseMessage =
                        await OnEvent_PicSysphotoRequestAsync(RequestMessage as RequestMessageEvent_Pic_Sysphoto);
                    break;
                case Event.subscribe: //订阅
                    responseMessage =
                        await OnEvent_SubscribeRequestAsync(RequestMessage as RequestMessageEvent_Subscribe);
                    break;
                case Event.unsubscribe: //取消订阅
                    responseMessage =
                        await OnEvent_UnSubscribeRequestAsync(RequestMessage as RequestMessageEvent_UnSubscribe);
                    break;
                case Event.LOCATION: //上报地理位置事件
                    responseMessage =
                        await OnEvent_LocationRequestAsync(RequestMessage as RequestMessageEvent_Location);
                    break;
                case Event.ENTER_AGENT: //用户进入应用的事件推送(enter_agent)
                    responseMessage =
                        await OnEvent_EnterAgentRequestAsync(RequestMessage as RequestMessageEvent_Enter_Agent);
                    break;
                case Event.BATCH_JOB_RESULT: //异步任务完成事件推送(batch_job_result)
                    responseMessage = await
                        OnEvent_BatchJobResultRequestAsync(RequestMessage as RequestMessageEvent_Batch_Job_Result);
                    break;
                case Event.change_chain: //上下游空间、分组或企业变更事件(change_chain)
                    responseMessage = await
                        OnEvent_ChangeChainRequestAsync(RequestMessage as RequestMessageEvent_Change_Chain);
                    break;
                case Event.security: //安全管理事件
                    responseMessage = await
                        OnEvent_SecurityRequestAsync(RequestMessage as RequestMessageEvent_Security);
                    break;
                case Event.change_contact:
                    var ccRequestMessage = RequestMessage as IRequestMessageEvent_Change_Contact_Base;

                    switch (ccRequestMessage.ChangeType)
                    {
                        case ContactChangeType.create_user:
                            responseMessage = await
                                OnEvent_ChangeContactCreateUserRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_Contact_User_Create);
                            break;
                        case ContactChangeType.update_user:
                            responseMessage = await
                                OnEvent_ChangeContactUpdateUserRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_Contact_User_Update);
                            break;
                        case ContactChangeType.delete_user:
                            responseMessage = await
                                OnEvent_ChangeContactDeleteUserRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_Contact_User_Base);
                            break;
                        case ContactChangeType.create_party:
                            responseMessage = await
                                OnEvent_ChangeContactCreatePartyRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_Contact_Party_Create);
                            break;
                        case ContactChangeType.update_party:
                            responseMessage = await
                                OnEvent_ChangeContactUpdatePartyRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_Contact_Party_Update);
                            break;
                        case ContactChangeType.delete_party:
                            responseMessage = await
                                OnEvent_ChangeContactDeletePartyRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_Contact_Party_Base);
                            break;
                        case ContactChangeType.update_tag:
                            responseMessage = await
                                OnEvent_ChangeContactUpdateTagRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_Contact_Tag_Update);
                            break;
                        default:
                            throw new UnknownRequestMsgTypeException("未知的Event.change_contact下属请求信息", null);
                    }

                    break;
                //外部联系人事件相关
                case Event.CHANGE_EXTERNAL_CONTACT:
                    var cecRequestMessage = RequestMessage as IRequestMessageEvent_Change_ExternalContact_Base;

                    switch (cecRequestMessage.ChangeType)
                    {
                        case ExternalContactChangeType.add_external_contact:
                            responseMessage = await
                                OnEvent_ChangeExternalContactAddRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_ExternalContact_Add);
                            break;
                        case ExternalContactChangeType.edit_external_contact:
                            responseMessage = await OnEvent_ChangeExternalContactUpdateRequestAsync(
                                requestMessage as RequestMessageEvent_Change_ExternalContact_Modified);
                            break;
                        case ExternalContactChangeType.add_half_external_contact:
                            responseMessage = await
                                OnEvent_ChangeExternalContactAddHalfRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_ExternalContact_Add_Half);
                            break;
                        case ExternalContactChangeType.del_external_contact:
                            responseMessage = await
                                OnEvent_ChangeExternalContactDelRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_ExternalContact_Del);
                            break;
                        case ExternalContactChangeType.del_follow_user:
                            responseMessage = await OnEvent_ChangeExternalContactDelFollowUserRequestAsync(
                                RequestMessage as RequestMessageEvent_Change_ExternalContact_Del_FollowUser);
                            break;
                        case ExternalContactChangeType.transfer_fail:
                            responseMessage = await OnEvent_ChangeExternalContactTransferFailRequestAsync(
                                RequestMessage as RequestMessageEvent_Change_ExternalContact_Transfer_Fail);
                            break;
                        case ExternalContactChangeType.msg_audit_approved:
                            responseMessage = await
                                OnEvent_ChangeExternalContactMsgAuditAsync(
                                    RequestMessage as RequestMessageEvent_Change_ExternalContact_MsgAudit);
                            break;
                        default:
                            throw new UnknownRequestMsgTypeException("未知的外部联系人事件Event.CHANGE_EXTERNAL_CONTACT下属请求信息",
                                null);
                    }

                    break;
                case Event.CHANGE_EXTERNAL_CHAT: //客户群变更事件
                    var cechat = RequestMessage as RequestMessageEvent_Change_External_Chat_Base;

                    switch (cechat.ChangeType)
                    {
                        case ExternalChatChangeType.create:
                            responseMessage = await
                                OnEvent_ChangeExternalChatCreateRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_External_Chat_Create);
                            break;
                        case ExternalChatChangeType.update:
                            responseMessage = await
                                OnEvent_ChangeExternalChatUpdateRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_External_Chat_Update);
                            break;
                        case ExternalChatChangeType.dismiss:
                            responseMessage = await
                                OnEvent_ChangeExternalChatDismissRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_External_Chat_Dismiss);
                            break;
                        default:
                            throw new UnknownRequestMsgTypeException("未知的企业客户标签变更事件Event.CHANGE_EXTERNAL_Tag下属请求信息",
                                null);
                    }

                    break;
                case Event.CHANGE_EXTERNAL_TAG: //企业客户标签变更事件
                    var tag = RequestMessage as RequestMessageEvent_Change_External_Tag_Base;

                    switch (tag.ChangeType)
                    {
                        case ExternalTagChangeType.create:
                            responseMessage = await
                                OnEvent_ChangeExternalTagCreateRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_External_Tag_Create);
                            break;
                        case ExternalTagChangeType.update:
                            responseMessage = await
                                OnEvent_ChangeExternalTagUpdateRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_External_Tag_Update);
                            break;
                        case ExternalTagChangeType.delete:
                            responseMessage = await
                                OnEvent_ChangeExternalTagDeleteRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_External_Tag_Delete);
                            break;
                        case ExternalTagChangeType.shuffle:
                            responseMessage = await
                                OnEvent_ChangeExternalTagShuffleRequestAsync(
                                    RequestMessage as RequestMessageEvent_Change_External_Tag_Shuffle);
                            break;
                        default:
                            throw new UnknownRequestMsgTypeException("未知的客户群变更事件Event.CHANGE_EXTERNAL_CHAT下属请求信息",
                                null);
                    }

                    break;
                case Event.LIVING_STATUS_CHANGE: //直播事件回调
                    responseMessage = await
                        OnEvent_Living_Status_ChangeRequestAsync(
                            RequestMessage as RequestMessageEvent_Living_Status_Change_Base);
                    break;
                case Event.SYS_APPROVAL_CHANGE: //系统应用审批状态变化通知回调
                    responseMessage = await
                        OnEvent_Sys_Approval_Change_Status_ChangeRequestAsync(
                            RequestMessage as RequestMessageEvent_SysApprovalChange);
                    break;
                case Event.OPEN_APPROVAL_CHANGE: //自建应用审批状态变化通知回调
                    responseMessage = await
                        OnEvent_Open_Approval_Change_Status_ChangeRequestAsync(
                            RequestMessage as RequestMessageEvent_OpenApprovalChange);
                    break;

                case Event.MSGAUDIT_NOTIFY: //企业微信会话存档-产生会话回调事件
                    responseMessage = await
                        OnEvent_MsgAuditNotifyRequestAsync(
                            RequestMessage as RequestMessageEvent_MsgAuditNotify);
                    break;
                case Event.TEMPLATE_CARD_EVENT: // 企业微信-模板卡片事件推送
                    responseMessage = await
                        OnEvent_TemplateCardEventRequestAsync(
                            RequestMessage as RequestMessageEvent_TemplateCardEvent);
                    break;
                case Event.TEMPLATE_CARD_MENU_EVENT: // 通用模板卡片右上角菜单事件推送
                    responseMessage = await
                        OnEvent_TemplateCardMenuEventRequestAsync(
                            RequestMessage as RequestMessageEvent_TemplateCardMenuEvent);
                    break;
                case Event.KF_MSG_OR_EVENT: // 微信客服消息与事件回调通知
                    responseMessage = await
                        OnEvent_KfMsgOrEventRequestAsync(
                            RequestMessage as RequestMessageEvent_Kf_Msg_Or_Event);
                    break;
                case Event.KF_ACCOUNT_AUTH_CHANGE: // 微信客服账号授权变更通知
                    responseMessage = await
                        OnEvent_KfAccountAuthChangeRequestAsync(
                            RequestMessage as RequestMessageEvent_Kf_Account_Auth_Change);
                    break;
                case Event.CUSTOMER_ACQUISITION: // 获客助手事件通知
                    responseMessage = await
                        OnEvent_CustomerAcquisitionRequestAsync(
                            RequestMessage as RequestMessageEvent_Customer_Acquisition);
                    break;
                case Event.app_email_change: // 应用邮箱新邮件变更事件
                    responseMessage = await
                        OnEvent_AppEmailChangeRequestAsync(
                            RequestMessage as RequestMessageEvent_App_Email_Change);
                    break;
                case Event.book_meeting_room: // 会议室预定事件
                    responseMessage = await
                        OnEvent_BookMeetingRoomRequestAsync(
                            RequestMessage as RequestMessageEvent_Book_Meeting_Room);
                    break;
                case Event.cancel_meeting_room: // 会议室取消预定事件
                    responseMessage = await
                        OnEvent_CancelMeetingRoomRequestAsync(
                            RequestMessage as RequestMessageEvent_Cancel_Meeting_Room);
                    break;
                case Event.pay_transaction: // 小程序对外收款支付成功通知
                    responseMessage = await
                        OnEvent_MiniProgramPayTransactionRequestAsync(
                            RequestMessage as RequestMessageEvent_MiniProgramPay_Transaction);
                    break;
                case Event.pay_refund: // 小程序对外收款退款通知
                    responseMessage = await
                        OnEvent_MiniProgramPayRefundRequestAsync(
                            RequestMessage as RequestMessageEvent_MiniProgramPay_Refund);
                    break;
                case Event.delete_calendar: // 删除日历事件
                    responseMessage = await
                        OnEvent_DeleteCalendarRequestAsync(
                            RequestMessage as RequestMessageEvent_Delete_Calendar);
                    break;
                case Event.modify_calendar: // 修改日历事件
                    responseMessage = await
                        OnEvent_ModifyCalendarRequestAsync(
                            RequestMessage as RequestMessageEvent_Modify_Calendar);
                    break;
                case Event.modify_schedule: // 修改日程事件
                    responseMessage = await
                        OnEvent_ModifyScheduleRequestAsync(
                            RequestMessage as RequestMessageEvent_Modify_Schedule);
                    break;
                case Event.delete_schedule: // 删除日程事件
                    responseMessage = await
                        OnEvent_DeleteScheduleRequestAsync(
                            RequestMessage as RequestMessageEvent_Delete_Schedule);
                    break;
                case Event.respond_schedule: // 日程回执事件
                    responseMessage = await
                        OnEvent_RespondScheduleRequestAsync(
                            RequestMessage as RequestMessageEvent_Respond_Schedule);
                    break;
                case Event.submit_vip_account_approval: // 成员提交高级功能账号申请
                    responseMessage = await
                        OnEvent_SubmitVipAccountApprovalRequestAsync(
                            RequestMessage as RequestMessageEvent_Submit_Vip_Account_Approval);
                    break;
                case Event.finish_vip_account_approval: // 成员高级功能账号申请终止
                    responseMessage = await
                        OnEvent_FinishVipAccountApprovalRequestAsync(
                            RequestMessage as RequestMessageEvent_Finish_Vip_Account_Approval);
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
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ClickRequestAsync(
            RequestMessageEvent_Click requestMessage)
        {
            return await Task.FromResult(OnEvent_ClickRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 事件之URL跳转视图（View）
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ViewRequestAsync(
            RequestMessageEvent_View requestMessage)
        {
            return await Task.FromResult(OnEvent_ViewRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 弹出拍照或者相册发图
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_PicPhotoOrAlbumRequestAsync(
            RequestMessageEvent_Pic_Photo_Or_Album requestMessage)
        {
            return await Task.FromResult(OnEvent_PicPhotoOrAlbumRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 扫码推事件
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ScancodePushRequestAsync(
            RequestMessageEvent_Scancode_Push requestMessage)
        {
            return await Task.FromResult(OnEvent_ScancodePushRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ScancodeWaitmsgRequestAsync(
            RequestMessageEvent_Scancode_Waitmsg requestMessage)
        {
            return await Task.FromResult(OnEvent_ScancodeWaitmsgRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 弹出地理位置选择器
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_LocationSelectRequestAsync(
            RequestMessageEvent_Location_Select requestMessage)
        {
            return await Task.FromResult(OnEvent_LocationSelectRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 弹出微信相册发图器
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_PicWeixinRequestAsync(
            RequestMessageEvent_Pic_Weixin requestMessage)
        {
            return await Task.FromResult(OnEvent_PicWeixinRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 弹出系统拍照发图
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_PicSysphotoRequestAsync(
            RequestMessageEvent_Pic_Sysphoto requestMessage)
        {
            return await Task.FromResult(OnEvent_PicSysphotoRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_SubscribeRequestAsync(
            RequestMessageEvent_Subscribe requestMessage)
        {
            return await Task.FromResult(OnEvent_SubscribeRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_UnSubscribeRequestAsync(
            RequestMessageEvent_UnSubscribe requestMessage)
        {
            return await Task.FromResult(OnEvent_UnSubscribeRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_LocationRequestAsync(
            RequestMessageEvent_Location requestMessage)
        {
            return await Task.FromResult(OnEvent_LocationRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 用户进入应用的事件推送(enter_agent)
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_EnterAgentRequestAsync(
            RequestMessageEvent_Enter_Agent requestMessage)
        {
            return await Task.FromResult(OnEvent_EnterAgentRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 异步任务完成事件推送(batch_job_result)
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_BatchJobResultRequestAsync(
            RequestMessageEvent_Batch_Job_Result requestMessage)
        {
            return await Task.FromResult(OnEvent_BatchJobResultRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 上下游空间、分组或企业变更事件(change_chain)
        /// </summary>
        /// <param name="requestMessage">企业微信上下游变更事件请求消息。</param>
        /// <returns>微信接口返回结果。</returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeChainRequestAsync(
            RequestMessageEvent_Change_Chain requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeChainRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>安全管理事件。</summary>
        /// <param name="requestMessage">企业微信事件请求消息。</param>
        /// <returns>微信接口返回结果。</returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_SecurityRequestAsync(
            RequestMessageEvent_Security requestMessage)
        {
            return await Task.FromResult(OnEvent_SecurityRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 新增成员事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeContactCreateUserRequestAsync(
            RequestMessageEvent_Change_Contact_User_Create requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeContactCreateUserRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 更新成员事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeContactUpdateUserRequestAsync(
            RequestMessageEvent_Change_Contact_User_Update requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeContactUpdateUserRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除成员事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeContactDeleteUserRequestAsync(
            RequestMessageEvent_Change_Contact_User_Base requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeContactDeleteUserRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 新增部门事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeContactCreatePartyRequestAsync(
            RequestMessageEvent_Change_Contact_Party_Create requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeContactCreatePartyRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 更新部门事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeContactUpdatePartyRequestAsync(
            RequestMessageEvent_Change_Contact_Party_Update requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeContactUpdatePartyRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除部门事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeContactDeletePartyRequestAsync(
            RequestMessageEvent_Change_Contact_Party_Base requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeContactDeletePartyRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 标签成员变更事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeContactUpdateTagRequestAsync(
            RequestMessageEvent_Change_Contact_Tag_Update requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeContactUpdateTagRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 添加企业客户事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalContactAddRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Add requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalContactAddRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 外部联系人编辑企业客户
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalContactUpdateRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Modified requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalContactUpdateRequest(requestMessage))
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 外部联系人免验证添加成员事件 推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalContactAddHalfRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Add_Half requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalContactAddHalfRequest(requestMessage))
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 删除企业客户事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalContactDelRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Del requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalContactDelRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除跟进成员事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalContactDelFollowUserRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Del_FollowUser requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalContactDelFollowUserRequest(requestMessage))
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 客户接替失败事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalContactTransferFailRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Transfer_Fail requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalContactTransferFailRequest(requestMessage))
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 用户同意消息存档事件（灰度）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalContactMsgAuditAsync(
            RequestMessageEvent_Change_ExternalContact_MsgAudit requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalContactMsgAudit(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户群创建事件 推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalChatCreateRequestAsync(
            RequestMessageEvent_Change_External_Chat_Create requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalChatCreateRequest(requestMessage)).ConfigureAwait(false);
        }


        /// <summary>
        /// 客户群变更事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalChatUpdateRequestAsync(
            RequestMessageEvent_Change_External_Chat_Update requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalChatUpdateRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 客户群解散事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalChatDismissRequestAsync(
            RequestMessageEvent_Change_External_Chat_Dismiss requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalChatDismissRequest(requestMessage)).ConfigureAwait(false);
        }

        #region 企业客户标签事件

        /// <summary>
        /// 企业客户标签-创建
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalTagCreateRequestAsync(
            RequestMessageEvent_Change_External_Tag_Create requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalTagCreateRequest(requestMessage)).ConfigureAwait(false);
        }


        /// <summary>
        /// 企业客户标签-变更
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalTagUpdateRequestAsync(
            RequestMessageEvent_Change_External_Tag_Update requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalTagUpdateRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 企业客户标签-删除
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalTagDeleteRequestAsync(
            RequestMessageEvent_Change_External_Tag_Delete requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalTagDeleteRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 企业客户标签-重排
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ChangeExternalTagShuffleRequestAsync(
            RequestMessageEvent_Change_External_Tag_Shuffle requestMessage)
        {
            return await Task.FromResult(OnEvent_ChangeExternalTagShuffleRequest(requestMessage)).ConfigureAwait(false);
        }

        #endregion

        public virtual async Task<IWorkResponseMessageBase> OnEvent_Living_Status_ChangeRequestAsync(
            RequestMessageEvent_Living_Status_Change_Base requestMessage)
        {
            return await Task.FromResult(OnEvent_Living_Status_ChangeRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 修改设置工作台自定义开关事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_SwitchWorkbenchModel(
            RequestMessageEvent_Switch_WorkBench_Mode requestMessage)
        {
            return await Task.FromResult(OnEvent_SwitchWorkBenchMode(requestMessage)).ConfigureAwait(false);
        }

        #region 审批事件

        /// <summary>
        /// 系统审批申请状态变化回调通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_Sys_Approval_Change_Status_ChangeRequestAsync(
            RequestMessageEvent_SysApprovalChange requestMessage)
        {
            return await Task.FromResult(OnEvent_Sys_Approval_Change_Status_ChangeRequest(requestMessage))
                .ConfigureAwait(false);
        }

        /// <summary>
        /// 自建审批申请状态变化回调通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_Open_Approval_Change_Status_ChangeRequestAsync(
            RequestMessageEvent_OpenApprovalChange requestMessage)
        {
            return await Task.FromResult(OnEvent_Open_Approval_Change_Status_ChangeRequest(requestMessage))
                .ConfigureAwait(false);
        }

        #endregion

        /// <summary>
        /// 企业微信会话存档-产生会话回调事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_MsgAuditNotifyRequestAsync(
            RequestMessageEvent_MsgAuditNotify requestMessage)
        {
            return await Task.FromResult(OnEvent_MsgAuditNotifyRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        ///     企业微信-模板卡片事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_TemplateCardEventRequestAsync(
            RequestMessageEvent_TemplateCardEvent requestMessage)
        {
            return await Task.FromResult(OnEvent_TemplateCardEventRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        ///     通用模板卡片右上角菜单事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_TemplateCardMenuEventRequestAsync(
            RequestMessageEvent_TemplateCardMenuEvent requestMessage)
        {
            return await Task.FromResult(OnEvent_TemplateCardMenuEventRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        ///     微信客服消息与事件回调通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_KfMsgOrEventRequestAsync(
            RequestMessageEvent_Kf_Msg_Or_Event requestMessage)
        {
            return await Task.FromResult(OnEvent_KfMsgOrEventRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>微信客服账号授权变更通知。</summary>
        /// <param name="requestMessage">企业微信事件请求消息。</param>
        /// <returns>微信接口返回结果。</returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_KfAccountAuthChangeRequestAsync(
            RequestMessageEvent_Kf_Account_Auth_Change requestMessage)
        {
            return await Task.FromResult(OnEvent_KfAccountAuthChangeRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>获客助手事件通知。</summary>
        /// <param name="requestMessage">企业微信事件请求消息。</param>
        /// <returns>微信接口返回结果。</returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_CustomerAcquisitionRequestAsync(
            RequestMessageEvent_Customer_Acquisition requestMessage)
        {
            return await Task.FromResult(OnEvent_CustomerAcquisitionRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>应用邮箱新邮件变更事件。</summary>
        /// <param name="requestMessage">包含变更类型和新邮件数量的事件请求消息。</param>
        /// <returns>微信接口返回结果。</returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_AppEmailChangeRequestAsync(
            RequestMessageEvent_App_Email_Change requestMessage)
        {
            return await Task.FromResult(OnEvent_AppEmailChangeRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>会议室预定事件。</summary>
        /// <param name="requestMessage">企业微信会议室预定事件请求消息。</param>
        /// <returns>微信接口返回结果。</returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_BookMeetingRoomRequestAsync(
            RequestMessageEvent_Book_Meeting_Room requestMessage)
        {
            return await Task.FromResult(OnEvent_BookMeetingRoomRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>会议室取消预定事件。</summary>
        /// <param name="requestMessage">企业微信会议室取消预定事件请求消息。</param>
        /// <returns>微信接口返回结果。</returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_CancelMeetingRoomRequestAsync(
            RequestMessageEvent_Cancel_Meeting_Room requestMessage)
        {
            return await Task.FromResult(OnEvent_CancelMeetingRoomRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>小程序对外收款支付成功通知。</summary>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_MiniProgramPayTransactionRequestAsync(
            RequestMessageEvent_MiniProgramPay_Transaction requestMessage)
        {
            return await Task.FromResult(OnEvent_MiniProgramPayTransactionRequest(requestMessage))
                .ConfigureAwait(false);
        }

        /// <summary>小程序对外收款退款通知。</summary>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_MiniProgramPayRefundRequestAsync(
            RequestMessageEvent_MiniProgramPay_Refund requestMessage)
        {
            return await Task.FromResult(OnEvent_MiniProgramPayRefundRequest(requestMessage))
                .ConfigureAwait(false);
        }

        /// <summary>删除日历事件。</summary>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_DeleteCalendarRequestAsync(
            RequestMessageEvent_Delete_Calendar requestMessage)
        {
            return await Task.FromResult(OnEvent_DeleteCalendarRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>修改日历事件。</summary>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ModifyCalendarRequestAsync(
            RequestMessageEvent_Modify_Calendar requestMessage)
        {
            return await Task.FromResult(OnEvent_ModifyCalendarRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>修改日程事件。</summary>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_ModifyScheduleRequestAsync(
            RequestMessageEvent_Modify_Schedule requestMessage)
        {
            return await Task.FromResult(OnEvent_ModifyScheduleRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>删除日程事件。</summary>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_DeleteScheduleRequestAsync(
            RequestMessageEvent_Delete_Schedule requestMessage)
        {
            return await Task.FromResult(OnEvent_DeleteScheduleRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>日程回执事件。</summary>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_RespondScheduleRequestAsync(
            RequestMessageEvent_Respond_Schedule requestMessage)
        {
            return await Task.FromResult(OnEvent_RespondScheduleRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>成员提交高级功能账号申请事件。</summary>
        /// <param name="requestMessage">成员提交申请事件请求消息。</param>
        /// <returns>微信接口返回结果。</returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_SubmitVipAccountApprovalRequestAsync(
            RequestMessageEvent_Submit_Vip_Account_Approval requestMessage)
        {
            return await Task.FromResult(OnEvent_SubmitVipAccountApprovalRequest(requestMessage))
                .ConfigureAwait(false);
        }

        /// <summary>成员高级功能账号申请终止事件。</summary>
        /// <param name="requestMessage">成员申请终止事件请求消息。</param>
        /// <returns>微信接口返回结果。</returns>
        public virtual async Task<IWorkResponseMessageBase> OnEvent_FinishVipAccountApprovalRequestAsync(
            RequestMessageEvent_Finish_Vip_Account_Approval requestMessage)
        {
            return await Task.FromResult(OnEvent_FinishVipAccountApprovalRequest(requestMessage))
                .ConfigureAwait(false);
        }

        #endregion //Event 下属分类

        #endregion

        #region 第三方回调事件

        private Task<string> OnThirdPartyEventAsync(IThirdPartyInfoBase thirdPartyInfo)
        {
            switch (thirdPartyInfo.InfoType)
            {
                case ThirdPartyInfo.SUITE_TICKET:
                    return OnThirdPartyEvent_Suite_TicketAsync((RequestMessageInfo_Suite_Ticket)thirdPartyInfo);
                case ThirdPartyInfo.CHANGE_AUTH:
                    return OnThirdPartyEvent_Change_AuthAsync((RequestMessageInfo_Change_Auth)thirdPartyInfo);
                case ThirdPartyInfo.CANCEL_AUTH:
                    return OnThirdPartyEvent_Cancel_AuthAsync((RequestMessageInfo_Cancel_Auth)thirdPartyInfo);
                case ThirdPartyInfo.CREATE_AUTH:
                    return OnThirdPartyEvent_Create_AuthAsync((RequestMessageInfo_Create_Auth)thirdPartyInfo);
                case ThirdPartyInfo.CHANGE_CONTACT:
                    return OnThirdPartyEvent_Change_ContactAsync((RequestMessageInfo_Change_Contact)thirdPartyInfo);
                case ThirdPartyInfo.REGISTER_CORP:
                    return OnThirdPartyEvent_Register_CorpAsync((RequestMessager_Register_Corp)thirdPartyInfo);
                case ThirdPartyInfo.RESET_PERMANENT_CODE:
                    return OnThirdPartEvent_Reset_Permanent_CodeAsync(
                        (RequestMessageInfo_Reset_Permanent_Code)thirdPartyInfo);
                case ThirdPartyInfo.CHANGE_EXTERNAL_CONTACT:
                {
                    var cecRequestMessage = RequestMessage as IRequestMessageEvent_Change_ExternalContact_Base;

                    switch (cecRequestMessage.ChangeType)
                    {
                        case ExternalContactChangeType.add_external_contact:
                            return OnThirdPartyEvent_ChangeExternalContactAddRequestAsync(
                                RequestMessage as RequestMessageEvent_Change_ExternalContact_Add);
                        case ExternalContactChangeType.edit_external_contact:
                            return OnThirdPartyEvent_ChangeExternalContactUpdateRequestAsync(
                                RequestMessage as RequestMessageEvent_Change_ExternalContact_Modified);
                        case ExternalContactChangeType.add_half_external_contact:
                            return OnThirdPartyEvent_ChangeExternalContactAddHalfRequestAsync(
                                RequestMessage as RequestMessageEvent_Change_ExternalContact_Add_Half);
                        case ExternalContactChangeType.del_external_contact:
                            return OnThirdPartyEvent_ChangeExternalContactDelRequestAsync(
                                RequestMessage as RequestMessageEvent_Change_ExternalContact_Del);
                        case ExternalContactChangeType.del_follow_user:
                            return OnThirdPartyEvent_ChangeExternalContactDelFollowUserRequestAsync(
                                RequestMessage as RequestMessageEvent_Change_ExternalContact_Del_FollowUser);
                        case ExternalContactChangeType.transfer_fail:
                            return OnThirdPartyEvent_ChangeExternalContactTransferFailRequestAsync(
                                RequestMessage as RequestMessageEvent_Change_ExternalContact_Transfer_Fail);
                        case ExternalContactChangeType.msg_audit_approved:
                            return OnThirdPartyEvent_ChangeExternalContactMsgAuditAsync(
                                RequestMessage as RequestMessageEvent_Change_ExternalContact_MsgAudit);
                        default:
                            throw new UnknownRequestMsgTypeException("未知的外部联系人事件Event.CHANGE_EXTERNAL_CONTACT下属请求信息",
                                null);
                    }
                }
                default:
                    throw new UnknownRequestMsgTypeException("未知的InfoType请求类型", null);
            }
        }

        protected virtual async Task<string> OnThirdPartyEvent_Change_ContactAsync(
            RequestMessageInfo_Change_Contact thirdPartyInfo)
        {
            return await Task.FromResult(OnThirdPartyEvent_Change_Contact(thirdPartyInfo)).ConfigureAwait(false);
        }

        protected virtual async Task<string> OnThirdPartyEvent_Register_CorpAsync(
            RequestMessager_Register_Corp thirdPartyInfo)
        {
            return await Task.FromResult(OnThirdPartyEvent_Register_Corp(thirdPartyInfo)).ConfigureAwait(false);
        }

        [Obsolete("请使用修复拼写之后的方法:OnThirdPartyEvent_Register_CorpAsync", true)]
        protected virtual Task<string> OnThirdPartyEvent_REGISTER_CORPAsync(
            RequestMessager_Register_Corp thirdPartyInfo)
        {
            return OnThirdPartyEvent_Register_CorpAsync(thirdPartyInfo);
        }

        protected virtual async Task<string> OnThirdPartyEvent_Create_AuthAsync(
            RequestMessageInfo_Create_Auth thirdPartyInfo)
        {
            return await Task.FromResult(OnThirdPartyEvent_Create_Auth(thirdPartyInfo)).ConfigureAwait(false);
        }

        protected virtual async Task<string> OnThirdPartyEvent_Cancel_AuthAsync(
            RequestMessageInfo_Cancel_Auth thirdPartyInfo)
        {
            return await Task.FromResult(OnThirdPartyEvent_Cancel_Auth(thirdPartyInfo)).ConfigureAwait(false);
        }

        protected virtual async Task<string> OnThirdPartyEvent_Change_AuthAsync(
            RequestMessageInfo_Change_Auth thirdPartyInfo)
        {
            return await Task.FromResult(OnThirdPartyEvent_Change_Auth(thirdPartyInfo)).ConfigureAwait(false);
        }

        protected virtual async Task<string> OnThirdPartyEvent_Suite_TicketAsync(
            RequestMessageInfo_Suite_Ticket thirdPartyInfo)
        {
            return await Task.FromResult(OnThirdPartyEvent_Suite_Ticket(thirdPartyInfo)).ConfigureAwait(false);
        }

        protected virtual async Task<string> OnThirdPartEvent_Reset_Permanent_CodeAsync(
            RequestMessageInfo_Reset_Permanent_Code thirdPartyInfo)
        {
            return await Task.FromResult(OnThirdPartEvent_ResetPermanentCode(thirdPartyInfo)).ConfigureAwait(false);
        }

        #region 外部联系人

        protected virtual async Task<string> OnThirdPartyEvent_ChangeExternalContactAddRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Add requestMessage)
        {
            return await Task.FromResult(OnThirdPartyEvent_ChangeExternalContactAddRequest(requestMessage))
                .ConfigureAwait(false);
        }

        protected virtual async Task<string> OnThirdPartyEvent_ChangeExternalContactUpdateRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Modified requestMessage)
        {
            return await Task.FromResult(OnThirdPartyEvent_ChangeExternalContactUpdateRequest(requestMessage))
                .ConfigureAwait(false);
        }

        protected virtual async Task<string> OnThirdPartyEvent_ChangeExternalContactAddHalfRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Add_Half requestMessage)
        {
            return await Task.FromResult(OnThirdPartyEvent_ChangeExternalContactAddHalfRequest(requestMessage))
                .ConfigureAwait(false);
        }

        protected virtual async Task<string> OnThirdPartyEvent_ChangeExternalContactDelRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Del requestMessage)
        {
            return await Task.FromResult(OnThirdPartyEvent_ChangeExternalContactDelRequest(requestMessage))
                .ConfigureAwait(false);
        }

        protected virtual async Task<string> OnThirdPartyEvent_ChangeExternalContactDelFollowUserRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Del_FollowUser requestMessage)
        {
            return await Task.FromResult(OnThirdPartyEvent_ChangeExternalContactDelFollowUserRequest(requestMessage))
                .ConfigureAwait(false);
        }

        protected virtual async Task<string> OnThirdPartyEvent_ChangeExternalContactTransferFailRequestAsync(
            RequestMessageEvent_Change_ExternalContact_Transfer_Fail requestMessage)
        {
            return await Task.FromResult(OnThirdPartyEvent_ChangeExternalContactTransferFailRequest(requestMessage))
                .ConfigureAwait(false);
        }

        protected virtual async Task<string> OnThirdPartyEvent_ChangeExternalContactMsgAuditAsync(
            RequestMessageEvent_Change_ExternalContact_MsgAudit requestMessage)
        {
            return await Task.FromResult(OnThirdPartyEvent_ChangeExternalContactMsgAudit(requestMessage))
                .ConfigureAwait(false);
        }

        #endregion

        #endregion
    }
}
