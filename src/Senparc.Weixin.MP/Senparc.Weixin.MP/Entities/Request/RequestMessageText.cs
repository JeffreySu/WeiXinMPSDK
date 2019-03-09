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
    
    文件名：RequestMessageText.cs
    文件功能描述：接收普通文本消息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20170422
    修改描述：添加IRequestMessageText接口
    
    修改标识：Senparc - 20180901
    修改描述：支持 NeuChar

    修改标识：Senparc - 20190307
    修改描述：v16.6.13 添加 SendMenu 相关接口，并打通消息回复响应，添加 bizmsgmenuid 属性

----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 文本类型消息
    /// </summary>
    public class RequestMessageText : RequestMessageBase, IRequestMessageText, IRequestMessageSelectMenu
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.Text; }
        }

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 点击的菜单ID
        /// <para>收到XML推送之后，开发者可以根据提取出来的bizmsgmenuid和Content识别出微信用户点击的是哪个菜单。</para>
        /// </summary>
        public string bizmsgmenuid { get; set; }
    }
}
