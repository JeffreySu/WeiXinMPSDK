#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc
    
    文件名：MessageReply.cs
    文件功能描述：MessageReply 相关类
    
    
    创建标识：Senparc - 20180901
  
    修改标识：Senparc - 20180902
    修改描述：添加 MessagePair.Responses 属性

----------------------------------------------------------------*/

using Senparc.NeuChar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.NeuChar
{
    /// <summary>
    /// 消息回复设置
    /// </summary>
    public class MessageReply : NeuralNodeConfig
    {
        /// <summary>
        /// 请求-响应 配置列表
        /// </summary>
        public List<MessagePair> MessagePair { get; set; }

        /// <summary>
        /// MessageReply 构造函数
        /// </summary>
        public MessageReply()
        {
            MessagePair = new List<MessagePair>();
        }
    }

    /// <summary>
    /// 请求-响应 配置
    /// </summary>
    public class MessagePair
    {
        /// <summary>
        /// 请求条件设置
        /// </summary>
        public Request Request { get; set; }
        /// <summary>
        /// 优先响应设置（必须）
        /// </summary>
        public Response Response { get; set; }
        /// <summary>
        /// 后续响应设置（可选）
        /// </summary>
        public List<Response> ExtendResponses { get; set; }

        /// <summary>
        /// MessagePair 构造函数
        /// </summary>
        public MessagePair()
        {
            Request = new Request();
            Response = new Response();
            ExtendResponses = new List<Response>();
        }
    }

    /// <summary>
    /// 请求配置
    /// </summary>
    public class Request
    {
        /// <summary>
        /// 说明：目前只支持Text和Image
        /// </summary>
        public RequestMsgType Type { get; set; }
        /// <summary>
        /// 文本、事件的关键字
        /// </summary>
        public List<string> Keywords { get; set; }

        public Request()
        {
            Type = RequestMsgType.Unknown;
            Keywords = new List<string>();
        }
    }

    /// <summary>
    /// 响应配置
    /// </summary>
    public class Response
    {
        /// <summary>
        /// 响应类型
        /// </summary>
        public ResponseMsgType Type { get; set; }
        /// <summary>
        /// 响应内容
        /// </summary>
        public string Content { get; set; }

        public Response()
        {
            Type = ResponseMsgType.Text;
        }
    }
}
