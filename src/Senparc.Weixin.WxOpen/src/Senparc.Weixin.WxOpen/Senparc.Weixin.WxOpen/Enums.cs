/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
  
    文件名：Enums.cs
    文件功能描述：枚举类型
    
    
    创建标识：Senparc - 20170106

----------------------------------------------------------------*/

using System.ComponentModel;

namespace Senparc.Weixin.WxOpen
{
    /// <summary>
    /// 接收消息类型
    /// </summary>
    public enum RequestMsgType
    {
        Text, //文本
        Image, //图片
        Event, //事件推送
    }

    /// <summary>
    /// 当RequestMsgType类型为Event时，Event属性的类型
    /// </summary>
    public enum Event
    {
        /// <summary>
        /// 进入会话事件
        /// </summary>
        user_enter_tempsession
    }

    /// <summary>
    /// 发送消息类型
    /// </summary>
    public enum ResponseMsgType
    {
        [Description("文本")]
        Text = 0,
        [Description("图片")]
        Image = 3,

        //以下为延伸类型，微信官方并未提供具体的回复类型
        [Description("无回复")]
        NoResponse = 110,
        [Description("success")]
        SuccessResponse = 200
    }
}
