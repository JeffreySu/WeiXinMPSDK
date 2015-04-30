/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：Enums.cs
    文件功能描述：枚举类型
    
    
    创建标识：Senparc - 20150430
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.Open
{
    /// <summary>
    /// 选项设置信息选项名称
    /// </summary>
    public enum OptionName
    {
        /// <summary>
        /// 地理位置上报选项
        /// 0	无上报
        /// 1	进入会话时上报
        /// 2	每5s上报
        /// </summary>
        location_report,
        /// <summary>
        /// 语音识别开关选项
        /// 0	关闭语音识别
        /// 1	开启语音识别
        /// </summary>
        voice_recognize,
        /// <summary>
        /// 客服开关选项
        /// 0	关闭多客服
        /// 1	开启多客服
        /// </summary>
        customer_service
    }

    /// <summary>
    /// 公众号第三方平台推送消息类型
    /// </summary>
    public enum InfoType
    {
        /// <summary>
        /// 推送component_verify_ticket协议
        /// </summary>
        component_verify_ticket,
        /// <summary>
        /// 推送取消授权通知
        /// </summary>
        unauthorized
    }

    /// <summary>
    /// 应用授权作用域
    /// </summary>
    public enum OAuthScope
    {
        /// <summary>
        /// 不弹出授权页面，直接跳转，只能获取用户openid
        /// </summary>
        snsapi_base,
        /// <summary>
        /// 弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息
        /// </summary>
        snsapi_userinfo
    }
}
