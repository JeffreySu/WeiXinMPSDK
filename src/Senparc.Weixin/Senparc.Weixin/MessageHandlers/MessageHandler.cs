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
    
    文件名：MessageHandler.cs
    文件功能描述：微信请求的集中处理方法
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口

    修改标识：Senparc - 20160909
    修改描述：v4.7.8 修正在ResponseMessage都null的情况下，
              没有对_textResponseMessage做判断就直接返回空字符串的问题

    修改标识：Senparc - 20170409
    修改描述：v4.11.8 （MessageHandler V3.2）修复 TextResponseMessage 不输出加密信息的问题

    修改标识：Senparc - 20170409
    修改描述：v4.12.4  MessageHandler基类默认开启消息去重

----------------------------------------------------------------*/


/*
 * V3.2
 * V4.0 添加异步方法
 */

using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Senparc.CO2NET.Utilities;
using Senparc.Weixin.Context;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.MessageHandlers
{
    /// <summary>
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于Senparc.Weixin的基础功能，只为简化代码而设。
    /// </summary>
    public abstract partial class MessageHandler<TC, TRequest, TResponse> : IMessageHandler<TRequest, TResponse>
        where TC : class, IMessageContext<TRequest, TResponse>, new()
        where TRequest : IRequestMessageBase
        where TResponse : IResponseMessageBase
    {
        ///// <summary>
        ///// 上下文
        ///// </summary>
        //public static WeixinContext<TC> GlobalWeixinContext = new WeixinContext<TC>();

        /// <summary>
        /// 全局消息上下文
        /// </summary>
        public abstract WeixinContext<TC, TRequest, TResponse> WeixinContext { get; }

        /// <summary>
        /// 当前用户消息上下文
        /// </summary>
        public virtual TC CurrentMessageContext
        {
            get { return WeixinContext.GetMessageContext(RequestMessage); }
        }

        /// <summary>
        /// 发送者用户名（OpenId）
        /// </summary>
        public string WeixinOpenId
        {
            get
            {
                if (RequestMessage != null)
                {
                    return RequestMessage.FromUserName;
                }
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("UserName属性从v0.6起已过期，建议使用WeixinOpenId")]
        public string UserName
        {
            get
            {
                return WeixinOpenId;
            }
        }

        /// <summary>
        /// 取消执行Execute()方法。一般在OnExecuting()中用于临时阻止执行Execute()。
        /// 默认为False。
        /// 如果在执行OnExecuting()执行前设为True，则所有OnExecuting()、Execute()、OnExecuted()代码都不会被执行。
        /// 如果在执行OnExecuting()执行过程中设为True，则后续Execute()及OnExecuted()代码不会被执行。
        /// 建议在设为True的时候，给ResponseMessage赋值，以返回友好信息。
        /// </summary>
        public bool CancelExcute { get; set; }

        /// <summary>
        /// 在构造函数中转换得到原始XML数据
        /// </summary>
        public XDocument RequestDocument { get; set; }

        /// <summary>
        /// 根据ResponseMessageBase获得转换后的ResponseDocument
        /// 注意：这里每次请求都会根据当前的ResponseMessageBase生成一次，如需重用此数据，建议使用缓存或局部变量
        /// </summary>
        public abstract XDocument ResponseDocument { get; }

        /// <summary>
        /// 最后返回的ResponseDocument。
        /// 如果是Senparc.Weixin.QY，则应当和ResponseDocument一致；如果是Senparc.Weixin.QY，则应当在ResponseDocument基础上进行加密
        /// </summary>
        public abstract XDocument FinalResponseDocument { get; }

        //protected Stream InputStream { get; set; }
        /// <summary>
        /// 请求实体
        /// </summary>
        public virtual TRequest RequestMessage { get; set; }
        /// <summary>
        /// 响应实体
        /// 正常情况下只有当执行Execute()方法后才可能有值。
        /// 也可以结合Cancel，提前给ResponseMessage赋值。
        /// </summary>
        public virtual TResponse ResponseMessage { get; set; }

        /// <summary>
        /// 是否使用了MessageAgent代理
        /// </summary>
        public bool UsedMessageAgent { get; set; }

        /// <summary>
        /// 忽略重复发送的同一条消息（通常因为微信服务器没有收到及时的响应）
        /// </summary>
        public bool OmitRepeatedMessage { get; set; }

        /// <summary>
        /// 消息是否已经被去重
        /// </summary>
        public bool MessageIsRepeated { get; set; }

        private string _textResponseMessage = null;

        /// <summary>
        /// 文字类型返回消息
        /// </summary>
        public string TextResponseMessage
        {
            get
            {
                if (ResponseMessage != null && ResponseMessage is SuccessResponseMessageBase)
                {
                    _textResponseMessage = (ResponseMessage as SuccessResponseMessageBase).ReturnText;//返回"success"
                }

                if (_textResponseMessage == null //原先为 _textResponseMessage != null     ——Jeffrey Su 2017.06.01
                    && (ResponseMessage == null || ResponseMessage is IResponseMessageNoResponse))
                {
                    return "";//返回空消息
                }

                if (_textResponseMessage == null)
                {
                    return /*ResponseDocument == null ? null : */
                            FinalResponseDocument != null
                            ? FinalResponseDocument.ToString()
                            : "";
                    //ResponseDocument.ToString();
                }
                else
                {
                    return _textResponseMessage;
                }
            }
            set
            {
                _textResponseMessage = value;
            }
        }

        /// <summary>
        /// 构造函数公用的初始化方法
        /// </summary>
        /// <param name="postDataDocument"></param>
        /// <param name="maxRecordCount"></param>
        /// <param name="postData"></param>
        public void CommonInitialize(XDocument postDataDocument, int maxRecordCount, object postData)
        {
            OmitRepeatedMessage = true;//默认开启去重
            WeixinContext.MaxRecordCount = maxRecordCount;
            RequestDocument = Init(postDataDocument, postData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="maxRecordCount"></param>
        /// <param name="postData">需要传入到Init的参数</param>
        public MessageHandler(Stream inputStream, int maxRecordCount = 0, object postData = null)
        {
            var postDataDocument = XmlUtility.Convert(inputStream);

            CommonInitialize(postDataDocument, maxRecordCount, postData);
        }

        /// <summary>
        /// 使用postDataDocument的构造函数
        /// </summary>
        /// <param name="postDataDocument"></param>
        /// <param name="maxRecordCount"></param>
        /// <param name="postData">需要传入到Init的参数</param>
        public MessageHandler(XDocument postDataDocument, int maxRecordCount = 0, object postData = null)
        {
            CommonInitialize(postDataDocument, maxRecordCount, postData);
        }

        /// <summary>
        /// <para>使用requestMessageBase的构造函数</para>
        /// <para>次构造函数提供给具体的类库进行测试使用，例如Senparc.Weixin.Work</para>
        /// </summary>
        /// <param name="requestMessageBase"></param>
        /// <param name="maxRecordCount"></param>
        /// <param name="postData">需要传入到Init的参数</param>
        public MessageHandler(RequestMessageBase requestMessageBase, int maxRecordCount = 0, object postData = null)
        {
            ////将requestMessageBase生成XML格式。
            //var xmlStr = XmlUtility.XmlUtility.Serializer(requestMessageBase);
            //var postDataDocument = XDocument.Parse(xmlStr);

            //CommonInitialize(postDataDocument, maxRecordCount, postData);

            //此方法不执行任何方法，提供给具体的类库进行测试使用，例如Senparc.Weixin.Work
        }


        /// <summary>
        /// 初始化，获取RequestDocument。
        /// Init中需要对上下文添加当前消息（如果使用上下文）
        /// </summary>
        /// <param name="requestDocument"></param>
        public abstract XDocument Init(XDocument requestDocument, object postData = null);

        //public abstract TR CreateResponseMessage<TR>() where TR : ResponseMessageBase;


        public virtual void OnExecuting()
        {
        }

        /// <summary>
        /// 执行微信请求
        /// </summary>
        public abstract void Execute();

        public virtual void OnExecuted()
        {
        }


        ///// <summary>
        ///// 默认返回消息（当任何OnXX消息没有被重写，都将自动返回此默认消息）
        ///// </summary>
        //public abstract IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage);
    }
}
