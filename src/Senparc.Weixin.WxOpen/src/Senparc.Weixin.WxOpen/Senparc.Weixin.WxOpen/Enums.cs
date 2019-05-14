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
  
    文件名：Enums.cs
    文件功能描述：枚举类型
    
    
    创建标识：Senparc - 20170106

----------------------------------------------------------------*/

using System.ComponentModel;

namespace Senparc.Weixin.WxOpen
{
    ///// <summary>
    ///// 接收消息类型
    ///// </summary>
    //public enum RequestMsgType
    //{
    //    Text, //文本
    //    Image, //图片
    //    Event, //事件推送
    //}

    /// <summary>
    /// 当RequestMsgType类型为Event时，Event属性的类型
    /// </summary>
    public enum Event
    {
        /// <summary>
        /// 进入会话事件
        /// </summary>
        user_enter_tempsession,
        add_nearby_poi_audit_info
    }

    ///// <summary>
    ///// 发送消息类型
    ///// </summary>
    //public enum ResponseMsgType
    //{
    //    [Description("文本")]
    //    Text = 0,
    //    [Description("图片")]
    //    Image = 3,

    //    //以下为延伸类型，微信官方并未提供具体的回复类型
    //    [Description("无回复")]
    //    NoResponse = 110,
    //    [Description("success")]
    //    SuccessResponse = 200
    //}
}
