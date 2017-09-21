﻿#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc
    
    文件名：MessageHandler.Event.cs
    文件功能描述：微信请求的集中处理方法：Event相关
    
    
    创建标识：Senparc - 20150924
    
    修改标识：Senparc - 20170114
    修改描述：v14.3.119 OnEvent_ShakearoundUserShake接口默认返回ResponseMessageNoResponse信息
    
----------------------------------------------------------------*/

using Senparc.Weixin.Exceptions;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.MessageHandlers
{
    public abstract partial class MessageHandler<TC>
    {
        /// <summary>
        /// Event事件类型请求
        /// </summary>
        public virtual IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {
            var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
            IResponseMessageBase responseMessage = null;
            switch (strongRequestMessage.Event)
            {
                case Event.ENTER:
                    responseMessage = OnEvent_EnterRequest(RequestMessage as RequestMessageEvent_Enter);
                    break;
                case Event.LOCATION://自动发送的用户当前位置
                    responseMessage = OnEvent_LocationRequest(RequestMessage as RequestMessageEvent_Location);
                    break;
                case Event.subscribe://订阅
                    responseMessage = OnEvent_SubscribeRequest(RequestMessage as RequestMessageEvent_Subscribe);
                    break;
                case Event.unsubscribe://退订
                    responseMessage = OnEvent_UnsubscribeRequest(RequestMessage as RequestMessageEvent_Unsubscribe);
                    break;
                case Event.CLICK://菜单点击
                    responseMessage = OnEvent_ClickRequest(RequestMessage as RequestMessageEvent_Click);
                    break;
                case Event.scan://二维码
                    responseMessage = OnEvent_ScanRequest(RequestMessage as RequestMessageEvent_Scan);
                    break;
                case Event.VIEW://URL跳转（view视图）
                    responseMessage = OnEvent_ViewRequest(RequestMessage as RequestMessageEvent_View);
                    break;
                case Event.MASSSENDJOBFINISH://群发消息成功
                    responseMessage = OnEvent_MassSendJobFinishRequest(RequestMessage as RequestMessageEvent_MassSendJobFinish);
                    break;
                case Event.TEMPLATESENDJOBFINISH://推送模板消息成功
                    responseMessage = OnEvent_TemplateSendJobFinishRequest(RequestMessage as RequestMessageEvent_TemplateSendJobFinish);
                    break;
                case Event.pic_photo_or_album://弹出拍照或者相册发图
                    responseMessage = OnEvent_PicPhotoOrAlbumRequest(RequestMessage as RequestMessageEvent_Pic_Photo_Or_Album);
                    break;
                case Event.scancode_push://扫码推事件
                    responseMessage = OnEvent_ScancodePushRequest(RequestMessage as RequestMessageEvent_Scancode_Push);
                    break;
                case Event.scancode_waitmsg://扫码推事件且弹出“消息接收中”提示框
                    responseMessage = OnEvent_ScancodeWaitmsgRequest(RequestMessage as RequestMessageEvent_Scancode_Waitmsg);
                    break;
                case Event.location_select://弹出地理位置选择器
                    responseMessage = OnEvent_LocationSelectRequest(RequestMessage as RequestMessageEvent_Location_Select);
                    break;
                case Event.pic_weixin://弹出微信相册发图器
                    responseMessage = OnEvent_PicWeixinRequest(RequestMessage as RequestMessageEvent_Pic_Weixin);
                    break;
                case Event.pic_sysphoto://弹出系统拍照发图
                    responseMessage = OnEvent_PicSysphotoRequest(RequestMessage as RequestMessageEvent_Pic_Sysphoto);
                    break;
                case Event.card_pass_check://卡券通过审核
                    responseMessage = OnEvent_Card_Pass_CheckRequest(RequestMessage as RequestMessageEvent_Card_Pass_Check);
                    break;
                case Event.card_not_pass_check://卡券未通过审核
                    responseMessage = OnEvent_Card_Not_Pass_CheckRequest(RequestMessage as RequestMessageEvent_Card_Not_Pass_Check);
                    break;
                case Event.user_get_card://领取卡券
                    responseMessage = OnEvent_User_Get_CardRequest(RequestMessage as RequestMessageEvent_User_Get_Card);
                    break;
                case Event.user_del_card://删除卡券
                    responseMessage = OnEvent_User_Del_CardRequest(RequestMessage as RequestMessageEvent_User_Del_Card);
                    break;
                case Event.kf_create_session://多客服接入会话
                    responseMessage = OnEvent_Kf_Create_SessionRequest(RequestMessage as RequestMessageEvent_Kf_Create_Session);
                    break;
                case Event.kf_close_session://多客服关闭会话
                    responseMessage = OnEvent_Kf_Close_SessionRequest(RequestMessage as RequestMessageEvent_Kf_Close_Session);
                    break;
                case Event.kf_switch_session://多客服转接会话
                    responseMessage = OnEvent_Kf_Switch_SessionRequest(RequestMessage as RequestMessageEvent_Kf_Switch_Session);
                    break;
                case Event.poi_check_notify://审核结果事件推送
                    responseMessage = OnEvent_Poi_Check_NotifyRequest(RequestMessage as RequestMessageEvent_Poi_Check_Notify);
                    break;
                case Event.WifiConnected://Wi-Fi连网成功
                    responseMessage = OnEvent_WifiConnected(RequestMessage as RequestMessageEvent_WifiConnected);
                    break;
                case Event.user_consume_card://卡券核销
                    responseMessage = OnEvent_User_Consume_Card(RequestMessage as RequestMessageEvent_User_Consume_Card);
                    break;
                case Event.user_enter_session_from_card://从卡券进入公众号会话
                    responseMessage = OnEvent_User_Enter_Session_From_Card(RequestMessage as RequestMessageEvent_User_Enter_Session_From_Card);
                    break;
                case Event.user_view_card://进入会员卡
                    responseMessage = OnEvent_User_View_Card(RequestMessage as RequestMessageEvent_User_View_Card);
                    break;
                case Event.merchant_order://微小店订单付款通知
                    responseMessage = OnEvent_Merchant_Order(RequestMessage as RequestMessageEvent_Merchant_Order);
                    break;
                case Event.submit_membercard_user_info://接收会员信息事件通知
                    responseMessage = OnEvent_Submit_Membercard_User_Info(RequestMessage as RequestMessageEvent_Submit_Membercard_User_Info);
                    break;
                case Event.ShakearoundUserShake://摇一摇事件通知
                    responseMessage = OnEvent_ShakearoundUserShake(RequestMessage as RequestMessageEvent_ShakearoundUserShake);
                    break;
                case Event.user_gifting_card://卡券转赠事件推送
                    responseMessage = OnEvent_User_Gifting_Card(RequestMessage as RequestMessageEvent_User_Gifting_Card);
                    break;
                case Event.user_pay_from_pay_cell://微信买单完成
                    responseMessage = OnEvent_User_Pay_From_Pay_Cell(RequestMessage as RequestMessageEvent_User_Pay_From_Pay_Cell);
                    break;
                case Event.update_member_card://会员卡内容更新事件：会员卡积分余额发生变动时
                    responseMessage = OnEvent_Update_Member_Card(RequestMessage as RequestMessageEvent_Update_Member_Card);
                    break;
                case Event.card_sku_remind://卡券库存报警事件：当某个card_id的初始库存数大于200且当前库存小于等于100时
                    responseMessage = OnEvent_Card_Sku_Remind(RequestMessage as RequestMessageEvent_Card_Sku_Remind);
                    break;
                case Event.card_pay_order://券点流水详情事件：当商户朋友的券券点发生变动时
                    responseMessage = OnEvent_Card_Pay_Order(RequestMessage as RequestMessageEvent_Card_Pay_Order);
                    break;

                #region 微信认证事件推送

                case Event.qualification_verify_success://资质认证成功（此时立即获得接口权限）
                    responseMessage = OnEvent_QualificationVerifySuccess(RequestMessage as RequestMessageEvent_QualificationVerifySuccess);
                    break;
                case Event.qualification_verify_fail://资质认证失败
                    responseMessage = OnEvent_QualificationVerifyFail(RequestMessage as RequestMessageEvent_QualificationVerifyFail);
                    break;
                case Event.naming_verify_success://名称认证成功（即命名成功）
                    responseMessage = OnEvent_NamingVerifySuccess(RequestMessage as RequestMessageEvent_NamingVerifySuccess);
                    break;
                case Event.naming_verify_fail://名称认证失败（这时虽然客户端不打勾，但仍有接口权限）
                    responseMessage = OnEvent_NamingVerifyFail(RequestMessage as RequestMessageEvent_NamingVerifyFail);
                    break;
                case Event.annual_renew://年审通知
                    responseMessage = OnEvent_AnnualRenew(RequestMessage as RequestMessageEvent_AnnualRenew);
                    break;
                case Event.verify_expired://认证过期失效通知
                    responseMessage = OnEvent_VerifyExpired(RequestMessage as RequestMessageEvent_VerifyExpired);
                    break;
                #endregion

                #region 小程序审核事件推送

                case Event.weapp_audit_success://
                    responseMessage = OnEvent_WeAppAuditSuccess(RequestMessage as RequestMessageEvent_WeAppAuditSuccess);
                    break;
                case Event.weapp_audit_fail://
                    responseMessage = OnEvent_WeAppAuditFail(RequestMessage as RequestMessageEvent_WeAppAuditFail);
                    break;
                #endregion
                default:
                    throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
            }
            return responseMessage;
        }

        #region Event下属分类，接收事件方法

        /// <summary>
        /// Event事件类型请求之ENTER
        /// </summary>
        public virtual IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之LOCATION
        /// </summary>
        public virtual IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之subscribe
        /// </summary>
        public virtual IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之unsubscribe
        /// </summary>
        public virtual IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之CLICK
        /// </summary>
        public virtual IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之scan
        /// </summary>
        public virtual IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
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
        /// 事件推送群发结果
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_MassSendJobFinishRequest(RequestMessageEvent_MassSendJobFinish requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 发送模板消息返回结果
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_TemplateSendJobFinishRequest(RequestMessageEvent_TemplateSendJobFinish requestMessage)
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
        /// 卡券通过审核
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_Card_Pass_CheckRequest(RequestMessageEvent_Card_Pass_Check requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 卡券未通过审核
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_Card_Not_Pass_CheckRequest(RequestMessageEvent_Card_Not_Pass_Check requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 领取卡券
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_User_Get_CardRequest(RequestMessageEvent_User_Get_Card requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 删除卡券
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_User_Del_CardRequest(RequestMessageEvent_User_Del_Card requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 多客服接入会话
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_Kf_Create_SessionRequest(RequestMessageEvent_Kf_Create_Session requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 多客服关闭会话
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_Kf_Close_SessionRequest(RequestMessageEvent_Kf_Close_Session requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 多客服转接会话
        /// </summary>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_Kf_Switch_SessionRequest(RequestMessageEvent_Kf_Switch_Session requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之审核结果事件推送
        /// </summary>
        public virtual IResponseMessageBase OnEvent_Poi_Check_NotifyRequest(RequestMessageEvent_Poi_Check_Notify requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之Wi-Fi连网成功
        /// </summary>
        public virtual IResponseMessageBase OnEvent_WifiConnected(RequestMessageEvent_WifiConnected requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之卡券核销
        /// </summary>
        public virtual IResponseMessageBase OnEvent_User_Consume_Card(RequestMessageEvent_User_Consume_Card requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之从卡券进入公众号会话
        /// </summary>
        public virtual IResponseMessageBase OnEvent_User_Enter_Session_From_Card(RequestMessageEvent_User_Enter_Session_From_Card requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之进入会员卡
        /// </summary>
        public virtual IResponseMessageBase OnEvent_User_View_Card(RequestMessageEvent_User_View_Card requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之微小店订单付款通知
        /// </summary>
        public virtual IResponseMessageBase OnEvent_Merchant_Order(RequestMessageEvent_Merchant_Order requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之接收会员信息事件通知
        /// </summary>
        public virtual IResponseMessageBase OnEvent_Submit_Membercard_User_Info(RequestMessageEvent_Submit_Membercard_User_Info requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// Event事件类型请求之摇一摇事件通知
        /// </summary>
        public virtual IResponseMessageBase OnEvent_ShakearoundUserShake(RequestMessageEvent_ShakearoundUserShake requestMessage)
        {
            return requestMessage.CreateResponseMessage<ResponseMessageNoResponse>();
            //return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 卡券转赠事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_User_Gifting_Card(RequestMessageEvent_User_Gifting_Card requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 微信买单完成
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_User_Pay_From_Pay_Cell(RequestMessageEvent_User_Pay_From_Pay_Cell requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 会员卡内容更新事件：会员卡积分余额发生变动时
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_Update_Member_Card(RequestMessageEvent_Update_Member_Card requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 卡券库存报警事件：当某个card_id的初始库存数大于200且当前库存小于等于100时
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_Card_Sku_Remind(RequestMessageEvent_Card_Sku_Remind requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 券点流水详情事件：当商户朋友的券券点发生变动时
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_Card_Pay_Order(RequestMessageEvent_Card_Pay_Order requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        #region 微信认证事件推送

        /// <summary>
        /// 资质认证成功（此时立即获得接口权限）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_QualificationVerifySuccess(RequestMessageEvent_QualificationVerifySuccess requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 资质认证失败
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_QualificationVerifyFail(RequestMessageEvent_QualificationVerifyFail requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 名称认证成功（即命名成功）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_NamingVerifySuccess(RequestMessageEvent_NamingVerifySuccess requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 名称认证失败（这时虽然客户端不打勾，但仍有接口权限）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_NamingVerifyFail(RequestMessageEvent_NamingVerifyFail requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 年审通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_AnnualRenew(RequestMessageEvent_AnnualRenew requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 认证过期失效通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_VerifyExpired(RequestMessageEvent_VerifyExpired requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        #endregion

        #region 小程序审核事件推送

        /// <summary>
        /// 小程序审核失败通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_WeAppAuditFail(RequestMessageEvent_WeAppAuditFail requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 小程序审核成功通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_WeAppAuditSuccess(RequestMessageEvent_WeAppAuditSuccess requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        #endregion

        #endregion
    }
}
