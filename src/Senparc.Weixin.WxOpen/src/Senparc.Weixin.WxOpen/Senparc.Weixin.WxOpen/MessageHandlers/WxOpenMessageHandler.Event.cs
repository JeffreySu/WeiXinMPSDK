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
    
    文件名：MessageHandler.Event.cs
    文件功能描述：微信请求的集中处理方法：Event相关
    
    
    创建标识：Senparc - 20150924
  
    修改标识：Senparc - 20191004
    修改描述：添加异步方法

----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.WxOpen.Entities;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.MessageHandlers
{
    public abstract partial class WxOpenMessageHandler<TMC>
    {

        #region 同步方法

        /// <summary>
        /// Event事件类型请求
        /// </summary>
        public virtual IResponseMessageBase OnEventRequest(IRequestMessageEventBase requestMessage)
        {
            var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
            IResponseMessageBase responseMessage = null;
            switch (strongRequestMessage.Event)
            {
                case Event.user_enter_tempsession:
                    responseMessage = OnEvent_UserEnterTempSessionRequest(RequestMessage as RequestMessageEvent_UserEnterTempSession);
                    break;
                case Event.add_nearby_poi_audit_info:
                    responseMessage = OnEvent_AddNearbyPoiAuditInfoRequest(RequestMessage as RequestMessageEvent_AddNearbyPoiAuditInfo);
                    break;
                case Event.weapp_audit_success://
                    responseMessage = OnEvent_WeAppAuditSuccessRequest(RequestMessage as RequestMessageEvent_WeAppAuditSuccess);
                    break;
                case Event.weapp_audit_fail://
                    responseMessage = OnEvent_WeAppAuditFailRequest(RequestMessage as RequestMessageEvent_WeAppAuditFail);
                    break;
                case Event.weapp_audit_delay://
                    responseMessage = OnEvent_WeAppAuditDelayRequest(RequestMessage as RequestMessageEvent_WeAppAuditDelay);
                    break;
                case Event.wxa_nickname_audit:
                    responseMessage = OnEvent_NicknameAuditRequest(RequestMessage as RequestMessageEvent_NicknameAudit);
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
            }
            return responseMessage;
        }

        #region Event 下属分类


        /// <summary>
        /// 进入客服会话事件
        /// </summary>
        public virtual IResponseMessageBase OnEvent_UserEnterTempSessionRequest(RequestMessageEvent_UserEnterTempSession requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 地点审核事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_AddNearbyPoiAuditInfoRequest(RequestMessageEvent_AddNearbyPoiAuditInfo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 小程序审核延后通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_WeAppAuditDelayRequest(RequestMessageEvent_WeAppAuditDelay requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 小程序审核失败通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_WeAppAuditFailRequest(RequestMessageEvent_WeAppAuditFail requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 小程序审核成功通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_WeAppAuditSuccessRequest(RequestMessageEvent_WeAppAuditSuccess requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 名称审核结果事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_NicknameAuditRequest(RequestMessageEvent_NicknameAudit requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }



        #endregion

        #endregion


        #region 异步方法

        /// <summary>
        /// 【异步方法】Event事件类型请求
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEventRequestAsync(IRequestMessageEventBase requestMessage)
        {
            var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
            IResponseMessageBase responseMessage = null;
            switch (strongRequestMessage.Event)
            {
                case Event.user_enter_tempsession:
                    responseMessage = await OnEvent_UserEnterTempSessionRequestAsync(RequestMessage as RequestMessageEvent_UserEnterTempSession);
                    break;
                case Event.add_nearby_poi_audit_info:
                    responseMessage = await OnEvent_AddNearbyPoiAuditInfoRequestAsync(RequestMessage as RequestMessageEvent_AddNearbyPoiAuditInfo);
                    break;
                case Event.weapp_audit_success://
                    responseMessage = await OnEvent_WeAppAuditSuccessRequestAsync(RequestMessage as RequestMessageEvent_WeAppAuditSuccess);
                    break;
                case Event.weapp_audit_fail://
                    responseMessage = await OnEvent_WeAppAuditFailRequestAsync(RequestMessage as RequestMessageEvent_WeAppAuditFail);
                    break;
                case Event.weapp_audit_delay://
                    responseMessage = await OnEvent_WeAppAuditDelayRequestAsync(RequestMessage as RequestMessageEvent_WeAppAuditDelay);
                    break;
                case Event.wxa_nickname_audit://
                    responseMessage = await OnEvent_NicknameAuditRequestAsync(RequestMessage as RequestMessageEvent_NicknameAudit);
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
            }
            return responseMessage;
        }

        #region Event 下属分类


        /// <summary>
        /// 【异步方法】进入客服会话事件
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_UserEnterTempSessionRequestAsync(RequestMessageEvent_UserEnterTempSession requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 【异步方法】地点审核事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_AddNearbyPoiAuditInfoRequestAsync(RequestMessageEvent_AddNearbyPoiAuditInfo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 小程序审核延后通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_WeAppAuditDelayRequestAsync(RequestMessageEvent_WeAppAuditDelay requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 小程序审核失败通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_WeAppAuditFailRequestAsync(RequestMessageEvent_WeAppAuditFail requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 小程序审核成功通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_WeAppAuditSuccessRequestAsync(RequestMessageEvent_WeAppAuditSuccess requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 名称审核结果事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_NicknameAuditRequestAsync(RequestMessageEvent_NicknameAudit requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }



        #endregion

        #endregion

    }
}
