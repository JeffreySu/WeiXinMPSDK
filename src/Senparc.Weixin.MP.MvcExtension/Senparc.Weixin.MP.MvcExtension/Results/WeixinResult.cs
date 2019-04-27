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
    
    文件名：WeixinResult.cs
    文件功能描述：给MVC使用的返回结果
    
    
    创建标识：Senparc - 20170921
    
    修改标识：Senparc - 20180901
    修改描述：支持 NeuChar

----------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;
using Senparc.NeuChar.MessageHandlers;
#if NET45
using System.Web.Mvc;
using System.Web;
#else
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
#endif

namespace Senparc.Weixin.MP.MvcExtension
{

    //public static class WeixinResultExtension
    //{
    //    public WeixinResult WeixinResult()
    //    { 

    //    }
    //}

    /// <summary>
    /// 返回MessageHandler结果
    /// </summary>
    public class WeixinResult : ContentResult
    {
        //private string _content;
        protected IMessageHandlerDocument _messageHandlerDocument;

        public WeixinResult(string content)
        {
            //_content = content;
            base.Content = content;
        }

        public WeixinResult(IMessageHandlerDocument messageHandlerDocument)
        {
            _messageHandlerDocument = messageHandlerDocument;
        }

        /// <summary>
        /// 获取ContentResult中的Content或IMessageHandler中的ResponseDocument文本结果。
        /// 一般在测试的时候使用。
        /// </summary>
        public new string Content
        {
            get
            {
                if (base.Content != null)
                {
                    return base.Content;
                }
                else if (_messageHandlerDocument != null && _messageHandlerDocument.FinalResponseDocument != null)
                {
                    return _messageHandlerDocument.FinalResponseDocument.ToString();
                }
                else
                {
                    return null;
                }
            }
            set { base.Content = value; }
        }

#if NET45
        public override void ExecuteResult(ControllerContext context)
#else
        public override void ExecuteResult(ActionContext context)
#endif
        {
            if (base.Content == null)
            {
                //使用IMessageHandler输出
                if (_messageHandlerDocument == null)
                {
                    throw new Senparc.Weixin.Exceptions.WeixinException("执行WeixinResult时提供的MessageHandler不能为Null！", null);
                }

                if (_messageHandlerDocument.FinalResponseDocument == null)
                {
                    //throw new Senparc.Weixin.MP.WeixinException("ResponseMessage不能为Null！", null);
                }
                else
                {
#if NET45
                    context.HttpContext.Response.ClearContent();
                    context.HttpContext.Response.ContentType = "text/xml";
                    _messageHandlerDocument.FinalResponseDocument.Save(context.HttpContext.Response.OutputStream);
#else
                    //context.HttpContext.Response.ClearContent();
                    context.HttpContext.Response.ContentType = "text/xml";
                    _messageHandlerDocument.FinalResponseDocument.Save(context.HttpContext.Response.Body);

#endif
                }
            }

            base.ExecuteResult(context);
        }
    }
}
