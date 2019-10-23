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
    
    文件名：Extensions.cs
    文件功能描述：将RequestMessageEventBase转换成RequestMessageText类型，其中Content = requestMessage.EventKey
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 实体扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 将RequestMessageEventBase转换成RequestMessageText类型，其中Content = requestMessage.EventKey
        /// </summary>
        /// <param name="requestMessageEvent"></param>
        /// <returns></returns>
        public static RequestMessageText ConvertToRequestMessageText(this IRequestMessageEventBase requestMessageEvent)
        {
            var requestMessage = requestMessageEvent;
            var requestMessageText = new RequestMessageText()
            {
                FromUserName = requestMessage.FromUserName,
                ToUserName = requestMessage.ToUserName,
                CreateTime = requestMessage.CreateTime,
                MsgId = requestMessage.MsgId
            };

            //判断是否具有EventKey属性
            if (requestMessageEvent is IRequestMessageEventKey)
            {
                requestMessageText.Content = (requestMessageEvent as IRequestMessageEventKey).EventKey;
            }
            else
            {
                requestMessageText.Content = "";
            }

            return requestMessageText;
        }
    }
}
