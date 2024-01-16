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
    
    文件名：DefaultMpMessageContext.cs
    文件功能描述：公众号上下文消息的默认实现
    
    
    创建标识：Senparc - 20190916

    修改标识：Senparc - 20200731
    修改描述：v16.10.502.2 添加微信电子发票 2.3 接收授权完成事件的处理

    
----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities;
using System.Xml.Linq;

namespace Senparc.Weixin.MP.MessageContexts
{
    /// <summary>
    /// 公众号上下文消息的默认实现
    /// </summary>
    public class DefaultMpMessageContext : MessageContext<IRequestMessageBase, IResponseMessageBase>, IMessageContext<IRequestMessageBase, IResponseMessageBase>
    {
        /// <summary>
        /// 获取请求消息和实体之间的映射结果
        /// </summary>
        /// <param name="requestMsgType"></param>
        /// <returns></returns>
        public override IRequestMessageBase GetRequestEntityMappingResult(RequestMsgType requestMsgType, XDocument doc = null)
        {
            RequestMessageBase requestMessage;
            switch (requestMsgType)
            {
                case RequestMsgType.Text:
                    requestMessage = new RequestMessageText();
                    break;
                case RequestMsgType.Location:
                    requestMessage = new RequestMessageLocation();
                    break;
                case RequestMsgType.Image:
                    requestMessage = new RequestMessageImage();
                    break;
                case RequestMsgType.Voice:
                    requestMessage = new RequestMessageVoice();
                    break;
                case RequestMsgType.Video:
                    requestMessage = new RequestMessageVideo();
                    break;
                case RequestMsgType.Link:
                    requestMessage = new RequestMessageLink();
                    break;
                case RequestMsgType.ShortVideo:
                    requestMessage = new RequestMessageShortVideo();
                    break;
                case RequestMsgType.File:
                    requestMessage = new RequestMessageFile();
                    break;
                case RequestMsgType.NeuChar:
                    requestMessage = new RequestMessageNeuChar();
                    break;
                case RequestMsgType.Event:
                    //判断Event类型
                    switch (doc.Root.Element("Event").Value.ToUpper())
                    {
                        case "ENTER"://进入会话
                            requestMessage = new RequestMessageEvent_Enter();
                            break;
                        case "LOCATION"://地理位置
                            requestMessage = new RequestMessageEvent_Location();
                            break;
                        case "SUBSCRIBE"://订阅（关注）
                            requestMessage = new RequestMessageEvent_Subscribe();
                            break;
                        case "UNSUBSCRIBE"://取消订阅（关注）
                            requestMessage = new RequestMessageEvent_Unsubscribe();
                            break;
                        case "CLICK"://菜单点击
                            requestMessage = new RequestMessageEvent_Click();
                            break;
                        case "SCAN"://二维码扫描
                            requestMessage = new RequestMessageEvent_Scan();
                            break;
                        case "VIEW"://URL跳转
                            requestMessage = new RequestMessageEvent_View();
                            break;
                        case "MASSSENDJOBFINISH":
                            requestMessage = new RequestMessageEvent_MassSendJobFinish();
                            break;
                        case "PUBLISHJOBFINISH":
                            requestMessage = new RequestMessageEvent_PublishJobFinish();
                            break;
                        case "TEMPLATESENDJOBFINISH"://模板信息
                            requestMessage = new RequestMessageEvent_TemplateSendJobFinish();
                            break;
                        case "SCANCODE_PUSH"://扫码推事件(scancode_push)
                            requestMessage = new RequestMessageEvent_Scancode_Push();
                            break;
                        case "SCANCODE_WAITMSG"://扫码推事件且弹出“消息接收中”提示框(scancode_waitmsg)
                            requestMessage = new RequestMessageEvent_Scancode_Waitmsg();
                            break;
                        case "PIC_SYSPHOTO"://弹出系统拍照发图(pic_sysphoto)
                            requestMessage = new RequestMessageEvent_Pic_Sysphoto();
                            break;
                        case "PIC_PHOTO_OR_ALBUM"://弹出拍照或者相册发图（pic_photo_or_album）
                            requestMessage = new RequestMessageEvent_Pic_Photo_Or_Album();
                            break;
                        case "PIC_WEIXIN"://弹出微信相册发图器(pic_weixin)
                            requestMessage = new RequestMessageEvent_Pic_Weixin();
                            break;
                        case "LOCATION_SELECT"://弹出地理位置选择器（location_select）
                            requestMessage = new RequestMessageEvent_Location_Select();
                            break;
                        case "CARD_PASS_CHECK"://卡券通过审核
                            requestMessage = new RequestMessageEvent_Card_Pass_Check();
                            break;
                        case "CARD_NOT_PASS_CHECK"://卡券未通过审核
                            requestMessage = new RequestMessageEvent_Card_Not_Pass_Check();
                            break;
                        case "USER_GET_CARD"://领取卡券
                            requestMessage = new RequestMessageEvent_User_Get_Card();
                            break;
                        case "USER_DEL_CARD"://删除卡券
                            requestMessage = new RequestMessageEvent_User_Del_Card();
                            break;
                        case "KF_CREATE_SESSION"://多客服接入会话
                            requestMessage = new RequestMessageEvent_Kf_Create_Session();
                            break;
                        case "KF_CLOSE_SESSION"://多客服关闭会话
                            requestMessage = new RequestMessageEvent_Kf_Close_Session();
                            break;
                        case "KF_SWITCH_SESSION"://多客服转接会话
                            requestMessage = new RequestMessageEvent_Kf_Switch_Session();
                            break;
                        case "POI_CHECK_NOTIFY"://审核结果事件推送
                            requestMessage = new RequestMessageEvent_Poi_Check_Notify();
                            break;
                        case "WIFICONNECTED"://Wi-Fi连网成功事件
                            requestMessage = new RequestMessageEvent_WifiConnected();
                            break;
                        case "USER_CONSUME_CARD"://卡券核销
                            requestMessage = new RequestMessageEvent_User_Consume_Card();
                            break;
                        case "USER_ENTER_SESSION_FROM_CARD"://从卡券进入公众号会话
                            requestMessage = new RequestMessageEvent_User_Enter_Session_From_Card();
                            break;
                        case "USER_VIEW_CARD"://进入会员卡
                            requestMessage = new RequestMessageEvent_User_View_Card();
                            break;
                        case "MERCHANT_ORDER"://微小店订单付款通知
                            requestMessage = new RequestMessageEvent_Merchant_Order();
                            break;
                        case "SUBMIT_MEMBERCARD_USER_INFO"://接收会员信息事件通知
                            requestMessage = new RequestMessageEvent_Submit_Membercard_User_Info();
                            break;
                        case "SHAKEAROUNDUSERSHAKE"://摇一摇事件通知
                            requestMessage = new RequestMessageEvent_ShakearoundUserShake();
                            break;
                        case "USER_GIFTING_CARD"://卡券转赠事件推送
                            requestMessage = new RequestMessageEvent_User_Gifting_Card();
                            break;
                        case "USER_PAY_FROM_PAY_CELL":// 微信买单完成
                            requestMessage = new RequestMessageEvent_User_Pay_From_Pay_Cell();
                            break;
                        case "UPDATE_MEMBER_CARD":// 会员卡内容更新事件：会员卡积分余额发生变动时
                            requestMessage = new RequestMessageEvent_Update_Member_Card();
                            break;

                        case "CARD_SKU_REMIND"://卡券库存报警事件：当某个card_id的初始库存数大于200且当前库存小于等于100时
                            requestMessage = new RequestMessageEvent_Card_Sku_Remind();
                            break;
                        case "CARD_PAY_ORDER"://券点流水详情事件：当商户朋友的券券点发生变动时
                            requestMessage = new RequestMessageEvent_Card_Pay_Order();
                            break;
                        case "APPLY_MERCHANT_AUDIT_INFO"://创建门店小程序审核事件
                            requestMessage = new RequestMessageEvent_ApplyMerchantAuditInfo();
                            break;
                        case "CREATE_MAP_POI_AUDIT_INFO"://从腾讯地图中创建门店审核事件
                            requestMessage = new RequestMessageEvent_CreateMapPoiAuditInfo();
                            break;
                        case "ADD_STORE_AUDIT_INFO"://门店小程序中创建门店审核事件
                            requestMessage = new RequestMessageEvent_AddStoreAuditInfo();
                            break;
                        case "MODIFY_STORE_AUDIT_INFO"://修改门店图片审核事件
                            requestMessage = new RequestMessageEvent_ModifyStoreAuditInfo();
                            break;

                        case "VIEW_MINIPROGRAM"://微信点击菜单跳转小程序的事件推送的事件
                            requestMessage = new RequestMessageEvent_View_Miniprogram();
                            break;

                        #region 卡券回调
                        case "GIFTCARD_PAY_DONE"://券点流水详情事件：当商户朋友的券券点发生变动时
                            requestMessage = new RequestMessageEvent_GiftCard_Pay_Done();
                            break;
                        case "GIFTCARD_SEND_TO_FRIEND"://券点流水详情事件：当商户朋友的券券点发生变动时
                            requestMessage = new RequestMessageEvent_GiftCard_Send_To_Friend();
                            break;
                        case "GIFTCARD_USER_ACCEPT"://券点流水详情事件：当商户朋友的券券点发生变动时
                            requestMessage = new RequestMessageEvent_GiftCard_User_Accept();
                            break;
                        #endregion

                        #region 微信电子发票
                        case "USER_AUTHORIZE_INVOICE":
                            requestMessage = new RequestMessageEvent_User_Authorize_Invoice();
                            break;
                        #endregion

                        #region 微信认证事件推送
                        case "QUALIFICATION_VERIFY_SUCCESS"://资质认证成功（此时立即获得接口权限）
                            requestMessage = new RequestMessageEvent_QualificationVerifySuccess();
                            break;
                        case "QUALIFICATION_VERIFY_FAIL"://资质认证失败
                            requestMessage = new RequestMessageEvent_QualificationVerifyFail();
                            break;
                        case "NAMING_VERIFY_SUCCESS"://名称认证成功（即命名成功）
                            requestMessage = new RequestMessageEvent_NamingVerifySuccess();
                            break;
                        case "NAMING_VERIFY_FAIL"://名称认证失败（这时虽然客户端不打勾，但仍有接口权限）
                            requestMessage = new RequestMessageEvent_NamingVerifyFail();
                            break;
                        case "ANNUAL_RENEW"://年审通知
                            requestMessage = new RequestMessageEvent_AnnualRenew();
                            break;
                        case "VERIFY_EXPIRED"://认证过期失效通知
                            requestMessage = new RequestMessageEvent_VerifyExpired();
                            break;

                        #endregion

                        #region 小程序审核事件推送
                        //该事件已移动到Senparc.Weixin.WxOpen
                        //case "WEAPP_AUDIT_SUCCESS": //小程序审核成功
                        //    requestMessage = new RequestMessageEvent_WeAppAuditSuccess();
                        //    break;
                        //case "WEAPP_AUDIT_FAIL": //小程序审核失败
                        //    requestMessage = new RequestMessageEvent_WeAppAuditFail();
                        //    break;
                        #endregion

                        #region 订阅通知事件
                        case "SUBSCRIBE_MSG_POPUP_EVENT":
                            requestMessage = new RequestMessageEvent_Subscribe_Msg_Popup();
                            break;
                        case "SUBSCRIBE_MSG_CHANGE_EVENT":
                            requestMessage = new RequestMessageEvent_Subscribe_Msg_Change();
                            break;
                        case "SUBSCRIBE_MSG_SENT_EVENT":
                            requestMessage = new RequestMessageEvent_Subscribe_Msg_Sent();
                            break;
                        #endregion
                        
                        default://其他意外类型（也可以选择抛出异常）
                            requestMessage = new RequestMessageEventBase();
                            break;
                    }
                    break;
                default:
                    {

                        requestMessage = new RequestMessageUnknownType()
                        {
                            RequestDocument = doc
                        };
                        ;
                        #region v14.8.3 之前的方案，直接在这里抛出异常

                        /*
                        throw new UnknownRequestMsgTypeException(string.Format("MsgType：{0} 在RequestMessageFactory中没有对应的处理程序！", msgType), new ArgumentOutOfRangeException());//为了能够对类型变动最大程度容错（如微信目前还可以对公众账号suscribe等未知类型，但API没有开放），建议在使用的时候catch这个异常
                        */

                        #endregion

                        break;
                    }
            }
            return requestMessage;
        }

        /// <summary>
        /// 获取响应消息和实体之间的映射结果
        /// </summary>
        /// <param name="responseMsgType"></param>
        /// <returns></returns>
        public override IResponseMessageBase GetResponseEntityMappingResult(ResponseMsgType responseMsgType, XDocument doc = null)
        {
            ResponseMessageBase responseMessage;
            switch (responseMsgType)
            {
                case ResponseMsgType.Text:
                    responseMessage = new ResponseMessageText();
                    break;
                case ResponseMsgType.News:
                    responseMessage = new ResponseMessageNews();
                    break;
                case ResponseMsgType.Music:
                    responseMessage = new ResponseMessageMusic();
                    break;
                case ResponseMsgType.Image:
                    responseMessage = new ResponseMessageImage();
                    break;
                case ResponseMsgType.Voice:
                    responseMessage = new ResponseMessageVoice();
                    break;
                case ResponseMsgType.Video:
                    responseMessage = new ResponseMessageVideo();
                    break;
                case ResponseMsgType.Transfer_Customer_Service:
                    responseMessage = new ResponseMessageTransfer_Customer_Service();
                    break;
                case ResponseMsgType.NoResponse:
                    responseMessage = new ResponseMessageNoResponse();
                    break;
                case ResponseMsgType.SuccessResponse:
                    responseMessage = new SuccessResponseMessage();
                    break;

                #region 扩展类型
                case ResponseMsgType.MultipleNews:
                case ResponseMsgType.LocationMessage:
                case ResponseMsgType.UseApi:
                #endregion

                case ResponseMsgType.MpNews:
                case ResponseMsgType.Unknown:
                case ResponseMsgType.Other:
                default:
                    responseMessage = new ResponseMessageUnknownType()
                    {
                        ResponseDocument = doc
                    };
                    break;
            }
            return responseMessage;
        }
    }
}
