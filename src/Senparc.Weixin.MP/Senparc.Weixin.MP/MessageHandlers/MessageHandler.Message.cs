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
    
    文件名：MessageHandler.Message.cs
    文件功能描述：微信请求的集中处理方法：Message相关
    
    
    创建标识：Senparc - 20150924
  
    修改标识：Senparc - 20180829
    修改描述：v15.4.0 支持NeuChar，添加 OnNeuCharRequest() 方法

----------------------------------------------------------------*/

using System;
using Senparc.Weixin.Exceptions;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;
using Senparc.CO2NET.Helpers;
using Senparc.NeuChar;
using Senparc.CO2NET.Trace;
using System.IO;
using Senparc.CO2NET.Utilities;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.Helpers;

namespace Senparc.Weixin.MP.MessageHandlers
{
    public abstract partial class MessageHandler<TC>
    {
        #region 默认方法及未知类型方法


        /// <summary>
        /// 默认返回消息（当任何OnXX消息没有被重写，都将自动返回此默认消息）
        /// </summary>
        public abstract IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage);
        //{
        //    例如可以这样实现：
        //    var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
        //    responseMessage.Content = "您发送的消息类型暂未被识别。";
        //    return responseMessage;
        //}

        /// <summary>
        /// 未知类型消息触发的事件，默认将抛出异常，建议进行重写
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public virtual IResponseMessageBase OnUnknownTypeRequest(RequestMessageUnknownType requestMessage)
        {
            var msgType = MsgTypeHelper.GetRequestMsgTypeString(requestMessage.RequestDocument);
            throw new UnknownRequestMsgTypeException("MsgType：{0} 在RequestMessageFactory中没有对应的处理程序！".FormatWith(msgType), new ArgumentOutOfRangeException());//为了能够对类型变动最大程度容错（如微信目前还可以对公众账号suscribe等未知类型，但API没有开放），建议在使用的时候catch这个异常
        }

        #endregion

        #region 接收消息方法


        /// <summary>
        /// 预处理文字或事件类型请求。
        /// 这个请求是一个比较特殊的请求，通常用于统一处理来自文字或菜单按钮的同一个执行逻辑，
        /// 会在执行OnTextRequest或OnEventRequest之前触发，具有以下一些特征：
        /// 1、如果返回null，则继续执行OnTextRequest或OnEventRequest
        /// 2、如果返回不为null，则终止执行OnTextRequest或OnEventRequest，返回最终ResponseMessage
        /// 3、如果是事件，则会将RequestMessageEvent自动转为RequestMessageText类型，其中RequestMessageText.Content就是RequestMessageEvent.EventKey
        /// </summary>
        public virtual IResponseMessageBase OnTextOrEventRequest(RequestMessageText requestMessage)
        {
            return null;
        }

        /// <summary>
        /// 文字类型请求
        /// </summary>
        public virtual IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

         /// <summary>
        /// 位置类型请求
        /// </summary>
        public virtual IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 图片类型请求
        /// </summary>
        public virtual IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 语音类型请求
        /// </summary>
        public virtual IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 视频类型请求
        /// </summary>
        public virtual IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 链接消息类型请求
        /// </summary>
        public virtual IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        /// <summary>
        /// 小视频类型请求
        /// </summary>
        public virtual IResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 文件请求
        /// </summary>
        public virtual IResponseMessageBase OnFileRequest(RequestMessageFile requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        #endregion

    }
}
