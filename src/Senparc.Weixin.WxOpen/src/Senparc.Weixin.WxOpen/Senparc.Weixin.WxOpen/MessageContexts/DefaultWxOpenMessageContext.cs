/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：DefaultWxOpenMessageContext.cs
    文件功能描述：小程序上下文消息的默认实现
    
    
    创建标识：Senparc - 20230905

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Exceptions;
using Senparc.Weixin.WxOpen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.WxOpen.MessageContexts
{
    /// <summary>
    /// 小程序上下文消息的默认实现
    /// </summary>
    public class DefaultWxOpenMessageContext : MessageContext<IRequestMessageBase, IResponseMessageBase>
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
                case RequestMsgType.Image:
                    requestMessage = new RequestMessageImage();
                    break;
                case RequestMsgType.MiniProgramPage://小程序页面
                    requestMessage = new RequestMessageMiniProgramPage();
                    break;
                case RequestMsgType.NeuChar:
                    requestMessage = new RequestMessageNeuChar();
                    break;
                case RequestMsgType.Event:
                    //判断Event类型
                    switch (doc.Root.Element("Event").Value.ToUpper())
                    {
                        case "USER_ENTER_TEMPSESSION"://进入客服会话
                            requestMessage = new RequestMessageEvent_UserEnterTempSession();
                            break;
                        case "WEAPP_AUDIT_SUCCESS": //小程序审核成功
                            requestMessage = new RequestMessageEvent_WeAppAuditSuccess();
                            break;
                        case "WEAPP_AUDIT_FAIL": //小程序审核失败
                            requestMessage = new RequestMessageEvent_WeAppAuditFail();
                            break;
                        case "WEAPP_AUDIT_DELAY": //小程序审核延后
                            requestMessage = new RequestMessageEvent_WeAppAuditDelay();
                            break;
                        case "WXA_NICKNAME_AUDIT": //名称审核结果
                            requestMessage = new RequestMessageEvent_NicknameAudit();
                            break;
                        case "WXA_ILLEGAL_RECORD": //小程序违规记录事件
                            requestMessage = new RequestMessageEvent_IllegalRecord();
                            break;
                        case "WXA_APPEAL_RECORD": //小程序申诉记录推送
                            requestMessage = new RequestMessageEvent_AppealRecord();
                            break;
                        case "WXA_PRIVACY_APPLY": //隐私权限审核结果推送
                            requestMessage = new RequestMessageEvent_PrivacyApply();
                            break;
                        case "WXA_MEDIA_CHECK"://内容安全回调：wxa_media_check 推送结果
                            requestMessage = new RequestMessageEvent_MediaCheck();
                            break;
                        case "WXA_CATEGORY_AUDIT": //小程序类目审核事件
                            requestMessage = new RequestMessageEvent_WxaCategoryAudit();
                            break;
                        case "ADD_EXPRESS_PATH": //运单轨迹推送事件
                            requestMessage = new RequestMessageEvent_AddExpressPath();
                            break;
                        case "TRADE_MANAGE_ORDER_SETTLEMENT"://订单将要结算或已经结算
                            requestMessage = new RequestMessageEvent_TradeManageOrderSettlement();
                            break;
                        case "TRADE_MANAGE_REMIND_ACCESS_API": //提醒接入发货信息管理服务API
                            requestMessage = new RequestMessageEvent_TradeManageRemindAccessApi();
                            break;
                        case "TRADE_MANAGE_REMIND_SHIPPING"://提醒需要上传发货信息
                            requestMessage = new RequestMessageEvent_TradeManageRemindShipping();
                            break;
                        case "WX_VERIFY_PAY_SUCC": //微信认证支付成功通知
                            requestMessage = new RequestMessageEvent_WxVerifyPaySucc();
                            break;
                        case "WX_VERIFY_DISPATCH": //微信认证派单事件
                            requestMessage = new RequestMessageEvent_WxVerifyDispatch();
                            break;
                        default://其他意外类型（也可以选择抛出异常）
                            requestMessage = new RequestMessageEventBase();
                            break;
                    }
                    break;
                default:
                    throw new UnknownRequestMsgTypeException(string.Format("MsgType：{0} 在RequestMessageFactory中没有对应的处理程序！", requestMsgType), new ArgumentOutOfRangeException());//为了能够对类型变动最大程度容错（如微信目前还可以对公众账号suscribe等未知类型，但API没有开放），建议在使用的时候catch这个异常
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
            IResponseMessageBase responseMessage;
            switch (responseMsgType)
            {
                case ResponseMsgType.Transfer_Customer_Service:
                    responseMessage = new ResponseMessageTransfer_Customer_Service();
                    break;
                case ResponseMsgType.NoResponse:
                    responseMessage = new ResponseMessageNoResponse();
                    break;
                case ResponseMsgType.SuccessResponse:
                    responseMessage = new SuccessResponseMessage();
                    break;


                #region 不支持
                case ResponseMsgType.Other:
                case ResponseMsgType.Unknown:
                case ResponseMsgType.Text:
                case ResponseMsgType.News:
                case ResponseMsgType.Music:
                case ResponseMsgType.Image:
                case ResponseMsgType.Voice:
                case ResponseMsgType.Video:
                case ResponseMsgType.MpNews:
                case ResponseMsgType.MultipleNews:
                case ResponseMsgType.LocationMessage:
                case ResponseMsgType.UseApi:
                #endregion
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
