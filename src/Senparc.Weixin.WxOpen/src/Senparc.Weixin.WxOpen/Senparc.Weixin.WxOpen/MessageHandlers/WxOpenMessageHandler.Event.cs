#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2023 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2023 Senparc
    
    文件名：MessageHandler.Event.cs
    文件功能描述：微信请求的集中处理方法：Event相关
    
    
    创建标识：Senparc - 20150924
  
    修改标识：Senparc - 20191004
    修改描述：添加异步方法

    修改标识：Senparc - 20200909
    修改描述：v3.8.511 调整 MessageHandler 异步方法执行代码

    修改标识：mc7246 - 20211209
    修改描述：v4.13.2 添加 OnEvent_AppealRecordRequest()、OnEvent_IllegalRecordRequest() 方法

    修改标识：mc7246 - 20220504
    修改描述：v3.15.2 添加小程序隐私权限审核结果推送

    修改标识：Senparc - 20220806
    修改描述：v3.15.7 添加 OnEvent_MediaCheckRequest() 方法 
               - 内容安全回调：wxa_media_check 推送结果内容安全回调：wxa_media_check 推送结果

    修改标识：mc7246 - 20230119
    修改描述：v3.15.12 添加小程序类目审核结果事件推送，增加 OnEvent_WxaCategoryAuditRequestAsync() 方法

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
                case Event.nearby_category_audit_info:
                    responseMessage = OnEvent_NearbyCategoryAuditInfoRequest(RequestMessage as RequestMessageEvent_NearbyCategoryAuditInfo);
                    break;
                case Event.add_nearby_poi_audit_info:
                    responseMessage = OnEvent_AddNearbyPoiAuditInfoRequest(RequestMessage as RequestMessageEvent_AddNearbyPoiAuditInfo);
                    break;
                case Event.create_map_poi_audit_info:
                    responseMessage = OnEvent_CreateMapPoiAuditInfoRequest(RequestMessage as RequestMessageEvent_CreateMapPoiAuditInfo);
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
                case Event.wxa_illegal_record:
                    responseMessage = OnEvent_IllegalRecordRequest(RequestMessage as RequestMessageEvent_IllegalRecord);
                    break;
                case Event.wxa_appeal_record:
                    responseMessage = OnEvent_AppealRecordRequest(RequestMessage as RequestMessageEvent_AppealRecord);
                    break;
                case Event.wxa_privacy_apply:
                    responseMessage = OnEvent_PrivacyApplyRequest(RequestMessage as RequestMessageEvent_PrivacyApply);
                    break;
                case Event.wxa_media_check:
                    responseMessage = OnEvent_MediaCheckRequest(RequestMessage as RequestMessageEvent_MediaCheck);
                    break;
                case Event.wxa_category_audit:
                    responseMessage = OnEvent_WxaCategoryAuditRequest(RequestMessage as RequestMessageEvent_WxaCategoryAudit);
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
            }
            return responseMessage;
        }

        #region Event 下属分类
        /// <summary>
        /// 小程序类目审核结果事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_WxaCategoryAuditRequest(RequestMessageEvent_WxaCategoryAudit requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 在腾讯地图中创建门店的审核结果
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_CreateMapPoiAuditInfoRequest(RequestMessageEvent_CreateMapPoiAuditInfo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 门店小程序类目审核事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_NearbyCategoryAuditInfoRequest(RequestMessageEvent_NearbyCategoryAuditInfo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

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

        /// <summary>
        /// 违规记录事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_IllegalRecordRequest(RequestMessageEvent_IllegalRecord requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 小程序申诉记录推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_AppealRecordRequest(RequestMessageEvent_AppealRecord requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 隐私权限申请结果推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_PrivacyApplyRequest(RequestMessageEvent_PrivacyApply requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 内容安全回调：wxa_media_check 推送结果
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnEvent_MediaCheckRequest(RequestMessageEvent_MediaCheck requestMessage)
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
                case Event.nearby_category_audit_info:
                    responseMessage = await OnEvent_NearbyCategoryAuditInfoRequestAsync(RequestMessage as RequestMessageEvent_NearbyCategoryAuditInfo);
                    break;
                case Event.create_map_poi_audit_info:
                    responseMessage = await OnEvent_CreateMapPoiAuditInfoRequestAsync(RequestMessage as RequestMessageEvent_CreateMapPoiAuditInfo);
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
                case Event.wxa_illegal_record:
                    responseMessage = await OnEvent_IllegalRecordRequestAsync(RequestMessage as RequestMessageEvent_IllegalRecord);
                    break;
                case Event.wxa_appeal_record:
                    responseMessage = await OnEvent_AppealRecordRequestAsync(RequestMessage as RequestMessageEvent_AppealRecord);
                    break;
                case Event.wxa_privacy_apply:
                    responseMessage = await OnEvent_PrivacyApplyRequestAsync(RequestMessage as RequestMessageEvent_PrivacyApply);
                    break;
                case Event.wxa_media_check:
                    responseMessage = await OnEvent_MediaCheckRequestAsync(RequestMessage as RequestMessageEvent_MediaCheck);
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
            }
            return responseMessage;
        }

        #region Event 下属分类

        /// <summary>
        /// 【异步方法】小程序类目审核结果事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_WxaCategoryAuditRequestAsync(RequestMessageEvent_WxaCategoryAudit requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_WxaCategoryAuditRequest(requestMessage)).ConfigureAwait(false);
        }
        /// <summary>
        /// 在腾讯地图中创建门店的审核结果
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_CreateMapPoiAuditInfoRequestAsync(RequestMessageEvent_CreateMapPoiAuditInfo requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_CreateMapPoiAuditInfoRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】门店小程序类目审核事件
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_NearbyCategoryAuditInfoRequestAsync(RequestMessageEvent_NearbyCategoryAuditInfo requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_NearbyCategoryAuditInfoRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】进入客服会话事件
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnEvent_UserEnterTempSessionRequestAsync(RequestMessageEvent_UserEnterTempSession requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_UserEnterTempSessionRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】地点审核事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_AddNearbyPoiAuditInfoRequestAsync(RequestMessageEvent_AddNearbyPoiAuditInfo requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_AddNearbyPoiAuditInfoRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】小程序审核延后通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_WeAppAuditDelayRequestAsync(RequestMessageEvent_WeAppAuditDelay requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_WeAppAuditDelayRequest(requestMessage)).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】小程序审核失败通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_WeAppAuditFailRequestAsync(RequestMessageEvent_WeAppAuditFail requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_WeAppAuditFailRequest(requestMessage)).ConfigureAwait(false);
        }
        /// <summary>
        /// 【异步方法】小程序审核成功通知
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_WeAppAuditSuccessRequestAsync(RequestMessageEvent_WeAppAuditSuccess requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_WeAppAuditSuccessRequest(requestMessage)).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】名称审核结果事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_NicknameAuditRequestAsync(RequestMessageEvent_NicknameAudit requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_NicknameAuditRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】违规记录事件推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_IllegalRecordRequestAsync(RequestMessageEvent_IllegalRecord requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_IllegalRecordRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】小程序申诉记录推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_AppealRecordRequestAsync(RequestMessageEvent_AppealRecord requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_AppealRecordRequest(requestMessage)).ConfigureAwait(false);
        }

        /// <summary>
        /// 【异步方法】隐私权限申请结果推送
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_PrivacyApplyRequestAsync(RequestMessageEvent_PrivacyApply requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_PrivacyApplyRequest(requestMessage)).ConfigureAwait(false);
        }


        /// <summary>
        /// 【异步方法】内容安全回调：wxa_media_check 推送结果
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual async Task<IResponseMessageBase> OnEvent_MediaCheckRequestAsync(RequestMessageEvent_MediaCheck requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnEvent_MediaCheckRequest(requestMessage)).ConfigureAwait(false);
        }

        #endregion

        #endregion

    }
}
