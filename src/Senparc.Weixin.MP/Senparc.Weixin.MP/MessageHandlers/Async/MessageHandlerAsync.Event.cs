#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：MessageHandlerAsync.Event.cs
    文件功能描述：微信请求【异步方法】的集中处理方法：Event相关
    
    
    创建标识：Senparc - 20180122
    
----------------------------------------------------------------*/

#if !NET35 && !NET40
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.NeuChar.MessageHandlers;
using System.Threading.Tasks;
using System;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.MP.MessageHandlers
{
    public abstract partial class MessageHandler<TC>
    {
        /// <summary>
        /// 【异步方法】Event事件类型请求
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEventRequestAsync(IRequestMessageEventBase requestMessage)
        {
            var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
            IResponseMessageBase responseMessage = null;
            var weixinAppId = this._postModel == null ? "" : this._postModel.AppId;

            switch (strongRequestMessage.Event)
            {
                case Event.ENTER:
                    responseMessage = await OnEvent_EnterRequestAsync(RequestMessage as RequestMessageEvent_Enter);
                    break;
                case Event.LOCATION://自动发送的用户当前位置
                    responseMessage = await OnEvent_LocationRequestAsync(RequestMessage as RequestMessageEvent_Location);
                    break;
                case Event.subscribe://订阅
                    responseMessage = await OnEvent_SubscribeRequestAsync(RequestMessage as RequestMessageEvent_Subscribe);
                    break;
                case Event.unsubscribe://退订
                    responseMessage = await OnEvent_UnsubscribeRequestAsync(RequestMessage as RequestMessageEvent_Unsubscribe);
                    break;
                case Event.CLICK://菜单点击
                    responseMessage = await CurrentMessageHandlerNode.ExecuteAsync(RequestMessage, this, weixinAppId) ??
                                        await OnEvent_ClickRequestAsync(RequestMessage as RequestMessageEvent_Click);
                    break;
                case Event.scan://二维码
                    responseMessage = await OnEvent_ScanRequestAsync(RequestMessage as RequestMessageEvent_Scan);
                    break;
                case Event.VIEW://URL跳转（view视图）
                    responseMessage = await OnEvent_ViewRequestAsync(RequestMessage as RequestMessageEvent_View);
                    break;
                case Event.MASSSENDJOBFINISH://群发消息成功
                    responseMessage = await OnEvent_MassSendJobFinishRequestAsync(RequestMessage as RequestMessageEvent_MassSendJobFinish);
                    break;
                case Event.TEMPLATESENDJOBFINISH://推送模板消息成功
                    responseMessage = await OnEvent_TemplateSendJobFinishRequestAsync(RequestMessage as RequestMessageEvent_TemplateSendJobFinish);
                    break;
                case Event.pic_photo_or_album://弹出拍照或者相册发图
                    responseMessage = await OnEvent_PicPhotoOrAlbumRequestAsync(RequestMessage as RequestMessageEvent_Pic_Photo_Or_Album);
                    break;
                case Event.scancode_push://扫码推事件
                    responseMessage = await OnEvent_ScancodePushRequestAsync(RequestMessage as RequestMessageEvent_Scancode_Push);
                    break;
                case Event.scancode_waitmsg://扫码推事件且弹出“消息接收中”提示框
                    responseMessage = await OnEvent_ScancodeWaitmsgRequestAsync(RequestMessage as RequestMessageEvent_Scancode_Waitmsg);
                    break;
                case Event.location_select://弹出地理位置选择器
                    responseMessage = await OnEvent_LocationSelectRequestAsync(RequestMessage as RequestMessageEvent_Location_Select);
                    break;
                case Event.pic_weixin://弹出微信相册发图器
                    responseMessage = await OnEvent_PicWeixinRequestAsync(RequestMessage as RequestMessageEvent_Pic_Weixin);
                    break;
                case Event.pic_sysphoto://弹出系统拍照发图
                    responseMessage = await OnEvent_PicSysphotoRequestAsync(RequestMessage as RequestMessageEvent_Pic_Sysphoto);
                    break;
                case Event.card_pass_check://卡券通过审核
                    responseMessage = await OnEvent_Card_Pass_CheckRequestAsync(RequestMessage as RequestMessageEvent_Card_Pass_Check);
                    break;
                case Event.card_not_pass_check://卡券未通过审核
                    responseMessage = await OnEvent_Card_Not_Pass_CheckRequestAsync(RequestMessage as RequestMessageEvent_Card_Not_Pass_Check);
                    break;
                case Event.user_get_card://领取卡券
                    responseMessage = await OnEvent_User_Get_CardRequestAsync(RequestMessage as RequestMessageEvent_User_Get_Card);
                    break;
                case Event.user_del_card://删除卡券
                    responseMessage = await OnEvent_User_Del_CardRequestAsync(RequestMessage as RequestMessageEvent_User_Del_Card);
                    break;
                case Event.kf_create_session://多客服接入会话
                    responseMessage = await OnEvent_Kf_Create_SessionRequestAsync(RequestMessage as RequestMessageEvent_Kf_Create_Session);
                    break;
                case Event.kf_close_session://多客服关闭会话
                    responseMessage = await OnEvent_Kf_Close_SessionRequestAsync(RequestMessage as RequestMessageEvent_Kf_Close_Session);
                    break;
                case Event.kf_switch_session://多客服转接会话
                    responseMessage = await OnEvent_Kf_Switch_SessionRequestAsync(RequestMessage as RequestMessageEvent_Kf_Switch_Session);
                    break;
                case Event.poi_check_notify://审核结果事件推送
                    responseMessage = await OnEvent_Poi_Check_NotifyRequestAsync(RequestMessage as RequestMessageEvent_Poi_Check_Notify);
                    break;
                case Event.WifiConnected://Wi-Fi连网成功
                    responseMessage = await OnEvent_WifiConnectedRequestAsync(RequestMessage as RequestMessageEvent_WifiConnected);
                    break;
                case Event.user_consume_card://卡券核销
                    responseMessage = await OnEvent_User_Consume_CardRequestAsync(RequestMessage as RequestMessageEvent_User_Consume_Card);
                    break;
                case Event.user_enter_session_from_card://从卡券进入公众号会话
                    responseMessage = await OnEvent_User_Enter_Session_From_CardRequestAsync(RequestMessage as RequestMessageEvent_User_Enter_Session_From_Card);
                    break;
                case Event.user_view_card://进入会员卡
                    responseMessage = await OnEvent_User_View_CardRequestAsync(RequestMessage as RequestMessageEvent_User_View_Card);
                    break;
                case Event.merchant_order://微小店订单付款通知
                    responseMessage = await OnEvent_Merchant_OrderRequestAsync(RequestMessage as RequestMessageEvent_Merchant_Order);
                    break;
                case Event.submit_membercard_user_info://接收会员信息事件通知
                    responseMessage = await OnEvent_Submit_Membercard_User_InfoRequestAsync(RequestMessage as RequestMessageEvent_Submit_Membercard_User_Info);
                    break;
                case Event.ShakearoundUserShake://摇一摇事件通知
                    responseMessage = await OnEvent_ShakearoundUserShakeRequestAsync(RequestMessage as RequestMessageEvent_ShakearoundUserShake);
                    break;
                case Event.user_gifting_card://卡券转赠事件推送
                    responseMessage = await OnEvent_User_Gifting_CardRequestAsync(RequestMessage as RequestMessageEvent_User_Gifting_Card);
                    break;
                case Event.user_pay_from_pay_cell://微信买单完成
                    responseMessage = await OnEvent_User_Pay_From_Pay_CellRequestAsync(RequestMessage as RequestMessageEvent_User_Pay_From_Pay_Cell);
                    break;
                case Event.update_member_card://会员卡内容更新事件：会员卡积分余额发生变动时
                    responseMessage = await OnEvent_Update_Member_CardRequestAsync(RequestMessage as RequestMessageEvent_Update_Member_Card);
                    break;
                case Event.card_sku_remind://卡券库存报警事件：当某个card_id的初始库存数大于200且当前库存小于等于100时
                    responseMessage = await OnEvent_Card_Sku_RemindRequestAsync(RequestMessage as RequestMessageEvent_Card_Sku_Remind);
                    break;
                case Event.card_pay_order://券点流水详情事件：当商户朋友的券券点发生变动时
                    responseMessage = await OnEvent_Card_Pay_OrderRequestAsync(RequestMessage as RequestMessageEvent_Card_Pay_Order);
                    break;

#region 微信认证事件推送

                case Event.qualification_verify_success://资质认证成功（此时立即获得接口权限）
                    responseMessage = await OnEvent_QualificationVerifySuccessRequestAsync(RequestMessage as RequestMessageEvent_QualificationVerifySuccess);
                    break;
                case Event.qualification_verify_fail://资质认证失败
                    responseMessage = await OnEvent_QualificationVerifyFailRequestAsync(RequestMessage as RequestMessageEvent_QualificationVerifyFail);
                    break;
                case Event.naming_verify_success://名称认证成功（即命名成功）
                    responseMessage = await OnEvent_NamingVerifySuccessRequestAsync(RequestMessage as RequestMessageEvent_NamingVerifySuccess);
                    break;
                case Event.naming_verify_fail://名称认证失败（这时虽然客户端不打勾，但仍有接口权限）
                    responseMessage = await OnEvent_NamingVerifyFailRequestAsync(RequestMessage as RequestMessageEvent_NamingVerifyFail);
                    break;
                case Event.annual_renew://年审通知
                    responseMessage = await OnEvent_AnnualRenewRequestAsync(RequestMessage as RequestMessageEvent_AnnualRenew);
                    break;
                case Event.verify_expired://认证过期失效通知
                    responseMessage = await OnEvent_VerifyExpiredRequestAsync(RequestMessage as RequestMessageEvent_VerifyExpired);
                    break;
#endregion

#region 小程序审核事件推送

                case Event.weapp_audit_success://
                    responseMessage = await OnEvent_WeAppAuditSuccessRequestAsync(RequestMessage as RequestMessageEvent_WeAppAuditSuccess);
                    break;
                case Event.weapp_audit_fail://
                    responseMessage = await OnEvent_WeAppAuditFailRequestAsync(RequestMessage as RequestMessageEvent_WeAppAuditFail);
                    break;
#endregion

#region 卡券回调

                case Event.giftcard_pay_done:
                    responseMessage = await OnEvent_GiftCard_Pay_DoneRequestAsync(RequestMessage as RequestMessageEvent_GiftCard_Pay_Done);
                    break;

                case Event.giftcard_send_to_friend:
                    responseMessage = await OnEvent_GiftCard_Send_To_FriendRequestAsync(RequestMessage as RequestMessageEvent_GiftCard_Send_To_Friend);
                    break;

                case Event.giftcard_user_accept:
                    responseMessage = await OnEvent_GiftCard_User_AcceptRequestAsync(RequestMessage as RequestMessageEvent_GiftCard_User_Accept);
                    break;

#endregion


                default:
                    throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
            }
            return responseMessage;
        }

#region Event下属分类，接收事件方法

        /// <summary>
        /// 【异步方法】Event事件类型请求之ENTER
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_EnterRequestAsync(RequestMessageEvent_Enter requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_EnterRequest(requestMessage));
        }


        /// <summary>
        /// 【异步方法】Event事件类型请求之LOCATION
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_LocationRequestAsync(RequestMessageEvent_Location requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_LocationRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之subscribe
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_SubscribeRequestAsync(RequestMessageEvent_Subscribe requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_SubscribeRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之unsubscribe
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_UnsubscribeRequestAsync(RequestMessageEvent_Unsubscribe requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_UnsubscribeRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之CLICK
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_ClickRequestAsync(RequestMessageEvent_Click requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_ClickRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之scan
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_ScanRequestAsync(RequestMessageEvent_Scan requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_ScanRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】事件之URL跳转视图（View）
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_ViewRequestAsync(RequestMessageEvent_View requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_ViewRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】事件推送群发结果
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_MassSendJobFinishRequestAsync(RequestMessageEvent_MassSendJobFinish requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_MassSendJobFinishRequest(requestMessage));
        }


        /// <summary>
        /// 【异步方法】发送模板消息返回结果
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_TemplateSendJobFinishRequestAsync(RequestMessageEvent_TemplateSendJobFinish requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_TemplateSendJobFinishRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】弹出拍照或者相册发图
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_PicPhotoOrAlbumRequestAsync(RequestMessageEvent_Pic_Photo_Or_Album requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_PicPhotoOrAlbumRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】扫码推事件
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_ScancodePushRequestAsync(RequestMessageEvent_Scancode_Push requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_ScancodePushRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】扫码推事件且弹出“消息接收中”提示框
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_ScancodeWaitmsgRequestAsync(RequestMessageEvent_Scancode_Waitmsg requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_ScancodeWaitmsgRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】弹出地理位置选择器
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_LocationSelectRequestAsync(RequestMessageEvent_Location_Select requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_LocationSelectRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】弹出微信相册发图器
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_PicWeixinRequestAsync(RequestMessageEvent_Pic_Weixin requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_PicWeixinRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】弹出系统拍照发图
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_PicSysphotoRequestAsync(RequestMessageEvent_Pic_Sysphoto requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_PicSysphotoRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】卡券通过审核
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_Card_Pass_CheckRequestAsync(RequestMessageEvent_Card_Pass_Check requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_Card_Pass_CheckRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】券未通过审核
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_Card_Not_Pass_CheckRequestAsync(RequestMessageEvent_Card_Not_Pass_Check requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_Card_Not_Pass_CheckRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】领取卡券
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_User_Get_CardRequestAsync(RequestMessageEvent_User_Get_Card requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_User_Get_CardRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】删除卡券
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_User_Del_CardRequestAsync(RequestMessageEvent_User_Del_Card requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_User_Del_CardRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】多客服接入会话
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_Kf_Create_SessionRequestAsync(RequestMessageEvent_Kf_Create_Session requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_Kf_Create_SessionRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】多客服关闭会话
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_Kf_Close_SessionRequestAsync(RequestMessageEvent_Kf_Close_Session requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_Kf_Close_SessionRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】多客服转接会话
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_Kf_Switch_SessionRequestAsync(RequestMessageEvent_Kf_Switch_Session requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_Kf_Switch_SessionRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之审核结果事件推送
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_Poi_Check_NotifyRequestAsync(RequestMessageEvent_Poi_Check_Notify requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_Poi_Check_NotifyRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之Wi-Fi连网成功
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_WifiConnectedRequestAsync(RequestMessageEvent_WifiConnected requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_WifiConnectedRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之卡券核销
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_User_Consume_CardRequestAsync(RequestMessageEvent_User_Consume_Card requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_User_Consume_CardRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之从卡券进入公众号会话
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_User_Enter_Session_From_CardRequestAsync(RequestMessageEvent_User_Enter_Session_From_Card requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_User_Enter_Session_From_CardRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之进入会员卡
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_User_View_CardRequestAsync(RequestMessageEvent_User_View_Card requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_User_View_CardRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之微小店订单付款通知
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_Merchant_OrderRequestAsync(RequestMessageEvent_Merchant_Order requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_Merchant_OrderRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之接收会员信息事件通知
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_Submit_Membercard_User_InfoRequestAsync(RequestMessageEvent_Submit_Membercard_User_Info requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_Submit_Membercard_User_InfoRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】Event事件类型请求之摇一摇事件通知
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_ShakearoundUserShakeRequestAsync(RequestMessageEvent_ShakearoundUserShake requestMessage)
        {
            return OnEvent_ShakearoundUserShakeRequest(requestMessage);
            //return requestMessage.CreateResponseMessage<ResponseMessageNoResponse>();
            //return await DefaultAsyncMethod(requestMessage, () => OnEvent_XX(requestMessage));
        }

        /// <summary>
        /// 【异步方法】卡券转赠事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_User_Gifting_CardRequestAsync(RequestMessageEvent_User_Gifting_Card requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_User_Gifting_CardRequest(requestMessage));
        }
        /// <summary>
        /// 【异步方法】微信买单完成
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_User_Pay_From_Pay_CellRequestAsync(RequestMessageEvent_User_Pay_From_Pay_Cell requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_User_Pay_From_Pay_CellRequest(requestMessage));
        }
        /// <summary>
        /// 【异步方法】会员卡内容更新事件：会员卡积分余额发生变动时
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_Update_Member_CardRequestAsync(RequestMessageEvent_Update_Member_Card requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_Update_Member_CardRequest(requestMessage));
        }
        /// <summary>
        /// 【异步方法】卡券库存报警事件：当某个card_id的初始库存数大于200且当前库存小于等于100时
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_Card_Sku_RemindRequestAsync(RequestMessageEvent_Card_Sku_Remind requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_Card_Sku_RemindRequest(requestMessage));
        }
        /// <summary>
        /// 【异步方法】券点流水详情事件：当商户朋友的券券点发生变动时
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_Card_Pay_OrderRequestAsync(RequestMessageEvent_Card_Pay_Order requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_Card_Pay_OrderRequest(requestMessage));
        }

#region 微信认证事件推送

        /// <summary>
        /// 【异步方法】资质认证成功（此时立即获得接口权限）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_QualificationVerifySuccessRequestAsync(RequestMessageEvent_QualificationVerifySuccess requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_QualificationVerifySuccessRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】资质认证失败
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_QualificationVerifyFailRequestAsync(RequestMessageEvent_QualificationVerifyFail requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_QualificationVerifyFailRequest(requestMessage));
        }


        /// <summary>
        /// 【异步方法】名称认证成功（即命名成功）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_NamingVerifySuccessRequestAsync(RequestMessageEvent_NamingVerifySuccess requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_NamingVerifySuccessRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】名称认证失败（这时虽然客户端不打勾，但仍有接口权限）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_NamingVerifyFailRequestAsync(RequestMessageEvent_NamingVerifyFail requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_NamingVerifyFailRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】年审通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_AnnualRenewRequestAsync(RequestMessageEvent_AnnualRenew requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_AnnualRenewRequest(requestMessage));
        }

        /// <summary>
        /// 【异步方法】认证过期失效通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_VerifyExpiredRequestAsync(RequestMessageEvent_VerifyExpired requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_VerifyExpiredRequest(requestMessage));
        }

#endregion

#region 小程序审核事件推送

        /// <summary>
        /// 【异步方法】小程序审核失败通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_WeAppAuditFailRequestAsync(RequestMessageEvent_WeAppAuditFail requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_WeAppAuditFailRequest(requestMessage));
        }
        /// <summary>
        /// 【异步方法】小程序审核成功通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_WeAppAuditSuccessRequestAsync(RequestMessageEvent_WeAppAuditSuccess requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_WeAppAuditSuccessRequest(requestMessage));
        }

#endregion

#region 卡券回调

        /// <summary>
        /// 用户购买礼品卡付款成功
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_GiftCard_Pay_DoneRequestAsync(RequestMessageEvent_GiftCard_Pay_Done requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_GiftCard_Pay_DoneRequest(requestMessage));
        }

        /// <summary>
        /// 用户购买后赠送
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_GiftCard_Send_To_FriendRequestAsync(RequestMessageEvent_GiftCard_Send_To_Friend requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_GiftCard_Send_To_FriendRequest(requestMessage));
        }

        /// <summary>
        /// 用户领取礼品卡成功
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_GiftCard_User_AcceptRequestAsync(RequestMessageEvent_GiftCard_User_Accept requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_GiftCard_User_AcceptRequest(requestMessage));
        }


#endregion

#endregion
    }
}
#endif