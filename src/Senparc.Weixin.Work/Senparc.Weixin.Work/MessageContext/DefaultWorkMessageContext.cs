
using Senparc.NeuChar;
using Senparc.NeuChar.Context;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Exceptions;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Senparc.Weixin.Work.MessageContexts
{
    /// <summary>
    /// 企业号上下文消息的默认实现
    /// </summary>
    public class DefaultWorkMessageContext
        : MessageContext<IWorkRequestMessageBase, IWorkResponseMessageBase>
    {
        /// <summary>
        /// 获取请求消息和实体之间的映射结果
        /// </summary>
        /// <param name="requestMsgType"></param>
        /// <returns></returns>
        public override IWorkRequestMessageBase GetRequestEntityMappingResult(RequestMsgType requestMsgType, XDocument doc = null)
        {
            IWorkRequestMessageBase requestMessage;
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
                case RequestMsgType.ShortVideo:
                    requestMessage = new RequestMessageShortVideo();
                    break;
                case RequestMsgType.File:
                    requestMessage = new RequestMessageFile();
                    break;
                case RequestMsgType.Event:
                    //判断Event类型
                    switch (doc.Root.Element("Event").Value.ToUpper())
                    {
                        case "CLICK"://菜单点击
                            requestMessage = new RequestMessageEvent_Click();
                            break;
                        case "VIEW"://URL跳转
                            requestMessage = new RequestMessageEvent_View();
                            break;
                        case "SUBSCRIBE"://订阅（关注）
                            requestMessage = new RequestMessageEvent_Subscribe();
                            break;
                        case "UNSUBSCRIBE"://取消订阅（关注）
                            requestMessage = new RequestMessageEvent_UnSubscribe();
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
                        case "LOCATION"://上报地理位置事件（location）
                            requestMessage = new RequestMessageEvent_Location();
                            break;
                        case "ENTER_AGENT"://用户进入应用的事件推送（enter_agent）
                            requestMessage = new RequestMessageEvent_Enter_Agent();
                            break;
                        case "BATCH_JOB_RESULT"://异步任务完成事件推送(batch_job_result)
                            requestMessage = new RequestMessageEvent_Batch_Job_Result();
                            break;
                        case "CHANGE_CONTACT"://通讯录更改推送(change_contact)
                            switch (doc.Root.Element("ChangeType").Value.ToUpper())
                            {
                                case "CREATE_USER":
                                    requestMessage = new RequestMessageEvent_Change_Contact_User_Create();
                                    break;
                                case "UPDATE_USER":
                                    requestMessage = new RequestMessageEvent_Change_Contact_User_Update();
                                    break;
                                case "DELETE_USER":
                                    requestMessage = new RequestMessageEvent_Change_Contact_User_Base();
                                    break;
                                case "CREATE_PARTY":
                                    requestMessage = new RequestMessageEvent_Change_Contact_Party_Create();
                                    break;
                                case "UPDATE_PARTY":
                                    requestMessage = new RequestMessageEvent_Change_Contact_Party_Update();
                                    break;
                                case "DELETE_PARTY":
                                    requestMessage = new RequestMessageEvent_Change_Contact_Party_Base();
                                    break;
                                case "UPDATE_TAG":
                                    requestMessage = new RequestMessageEvent_Change_Contact_Tag_Update();
                                    break;
                                default://其他意外类型（也可以选择抛出异常）
                                    requestMessage = new RequestMessageEventBase();
                                    break;
                            }
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
        public override IWorkResponseMessageBase GetResponseEntityMappingResult(ResponseMsgType responseMsgType, XDocument doc = null)
        {
            IWorkResponseMessageBase responseMessage;
            switch (responseMsgType)
            {
                case ResponseMsgType.Text:
                    responseMessage = new ResponseMessageText();
                    break;
                case ResponseMsgType.News:
                    responseMessage = new ResponseMessageNews();
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
                case ResponseMsgType.NoResponse:
                    responseMessage = new WorkResponseMessageNoResponse();
                    break;
                case ResponseMsgType.SuccessResponse:
                    responseMessage = new WorkSuccessResponseMessage();
                    break;

                #region 不支持
                case ResponseMsgType.Transfer_Customer_Service:
                case ResponseMsgType.Music:
                #endregion

                #region 扩展类型
                case ResponseMsgType.MultipleNews:
                case ResponseMsgType.LocationMessage:
                case ResponseMsgType.UseApi:
                #endregion

                case ResponseMsgType.Other:
                case ResponseMsgType.Unknown:
                default:
                    responseMessage = new WorkResponseMessageUnknownType()
                    {
                        ResponseDocument = doc
                    };
                    break;
            }
            return responseMessage;

        }
    }
}
