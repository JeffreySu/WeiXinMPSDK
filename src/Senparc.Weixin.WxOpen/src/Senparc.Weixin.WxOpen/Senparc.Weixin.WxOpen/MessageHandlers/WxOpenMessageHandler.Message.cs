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
    
    文件名：MessageHandler.Event.cs
    文件功能描述：微信请求的集中处理方法：Message相关
    
    
    创建标识：Senparc - 20170106
  
    修改标识：Senparc - 20200909
    修改描述：v3.8.511 小程序 WxOpenMessageHandler 增加 OnImageRequestAsync 和 OnTextRequestAsync

----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using Senparc.NeuChar.Entities;
using Senparc.Weixin.WxOpen.Entities;

namespace Senparc.Weixin.WxOpen.MessageHandlers
{
    public abstract partial class WxOpenMessageHandler<TMC>
    {
        #region 接收消息方法
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

        #region 同步方法

        /// <summary>
        /// 文字类型请求
        /// </summary>
        [Obsolete("请使用异步方法 OnTextRequestAsync()")]
        public virtual IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }


        /// <summary>
        /// 图片类型请求
        /// </summary>
        [Obsolete("请使用异步方法 OnImageRequestAsync()")]
        public virtual IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        #endregion

        #region 异步方法


        public async virtual Task<IResponseMessageBase> OnImageRequestAsync(RequestMessageImage requestMessage)
        {

            return await DefaultAsyncMethod(requestMessage, () => OnImageRequest(requestMessage)).ConfigureAwait(false);

        }

        public async virtual Task<IResponseMessageBase> OnTextRequestAsync(RequestMessageText requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnTextRequest(requestMessage)).ConfigureAwait(false);
        }

        public async virtual Task<IResponseMessageBase> OnMiniProgramPageRequestAsync(RequestMessageMiniProgramPage requestMessage)
        {
            return await DefaultResponseMessageAsync(requestMessage).ConfigureAwait(false);
        }



        #endregion
        #endregion
    }
}