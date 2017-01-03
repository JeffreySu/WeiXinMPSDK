/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
    
    文件名：WxOpenMessageHandler.cs
    文件功能描述：小程序MessageHandler
    
    
    创建标识：Senparc - 20170103
    
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <param name="inputStream"></param>
        /// <param name="postModel"></param>
        /// <param name="maxRecordCount"></param>
        /// <param name="developerInfo"></param>
        public WxOpenMessageHandler(Stream inputStream, PostModel postModel = null, int maxRecordCount = 0, DeveloperInfo developerInfo = null)
            : base(inputStream, postModel, maxRecordCount, developerInfo)
        {
        }

        /// <summary>
        /// 小程序MessageHandler构造函数
        /// </summary>
        /// <param name="requestDocument"></param>
        /// <param name="postModel"></param>
        /// <param name="maxRecordCount"></param>
        /// <param name="developerInfo"></param>
        public WxOpenMessageHandler(XDocument requestDocument, PostModel postModel = null, int maxRecordCount = 0, DeveloperInfo developerInfo = null)
            : base(requestDocument, postModel, maxRecordCount, developerInfo)
        {
        }

        /// <summary>
        /// 小程序MessageHandler构造函数
        /// </summary>
        /// <param name="requestMessageBase"></param>
        /// <param name="postModel"></param>
        /// <param name="maxRecordCount"></param>
        /// <param name="developerInfo"></param>
        public WxOpenMessageHandler(RequestMessageBase requestMessageBase, PostModel postModel = null, int maxRecordCount = 0, DeveloperInfo developerInfo = null) :
            base(requestMessageBase, postModel, maxRecordCount, developerInfo)
        {
        }
    }
}
