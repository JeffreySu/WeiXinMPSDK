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
    
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;
using Senparc.Weixin.Exceptions;
using Senparc.Weixin.WxOpen;
using Senparc.Weixin.WxOpen.Entities;

namespace Senparc.Weixin.WxOpen.MessageHandlers
{
    public abstract partial class WxOpenMessageHandler<TC>
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
                case Event.user_enter_tempsession:
                    responseMessage = OnEvent_UserEnterTempSessionRequest(RequestMessage as RequestMessageEvent_UserEnterTempSession);
                    break;
                case Event.add_nearby_poi_audit_info:
                    responseMessage = OnEvent_AddNearbyPoiAuditInfoRequest(RequestMessage as RequestMessageEvent_AddNearbyPoiAuditInfo);
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


        #endregion
    }
}
