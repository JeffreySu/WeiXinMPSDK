/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：WxOpenMessageHandler.cs
    文件功能描述：小程序MessageHandler
    
    
    创建标识：Senparc - 20170103
    
----------------------------------------------------------------*/

using System.IO;
using System.Xml.Linq;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.AppStore;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;

namespace Senparc.Weixin.WxOpen.MessageHandlers
{
    /// <summary>
    /// 小程序MessageHandler
    /// </summary>
    /// <typeparam name="TC"></typeparam>
    public abstract class WxOpenMessageHandler<TC> : MessageHandler<TC> where TC : class, IMessageContext<IRequestMessageBase, IResponseMessageBase>, new()
    {
        /// <summary>
        /// 小程序MessageHandler构造函数
        /// </summary>
        /// <param name="inputStream">XML流（后期会支持JSON）</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">上下文最多保留消息（0为保存所有）</param>
        /// <param name="developerInfo">开发者信息（非必填）</param>
        public WxOpenMessageHandler(Stream inputStream, PostModel postModel = null, int maxRecordCount = 0, DeveloperInfo developerInfo = null)
            : base(inputStream, postModel, maxRecordCount, developerInfo)
        {
        }

        /// <summary>
        /// 小程序MessageHandler构造函数
        /// </summary>
        /// <param name="requestDocument">XML格式的请求</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">上下文最多保留消息（0为保存所有）</param>
        /// <param name="developerInfo">开发者信息（非必填）</param>
        public WxOpenMessageHandler(XDocument requestDocument, PostModel postModel = null, int maxRecordCount = 0, DeveloperInfo developerInfo = null)
            : base(requestDocument, postModel, maxRecordCount, developerInfo)
        {
        }

        /// <summary>
        /// 小程序MessageHandler构造函数
        /// </summary>
        /// <param name="requestMessageBase">RequestMessageBase</param>
        /// <param name="postModel">PostModel</param>
        /// <param name="maxRecordCount">上下文最多保留消息（0为保存所有）</param>
        /// <param name="developerInfo">开发者信息（非必填）</param>
        public WxOpenMessageHandler(RequestMessageBase requestMessageBase, PostModel postModel = null, int maxRecordCount = 0, DeveloperInfo developerInfo = null) :
            base(requestMessageBase, postModel, maxRecordCount, developerInfo)
        {
        }
    }
}
