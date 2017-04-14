﻿/*----------------------------------------------------------------
    Copyright (C) 2017 Senparc
  
    文件名：FixWeixinBugWeixinResult.cs
    文件功能描述：修复微信换行等问题
    
    
    创建标识：Senparc - 20160801
    
    修改标识：Senparc - 20170413
    修改描述：修改 .net core mvc 的ExecuteResult(ActionContext context)方法
    
    修改标识：Senparc - 20150507
    修改描述：添加 事件 异步任务完成事件推送
----------------------------------------------------------------*/



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MessageHandlers;
using Microsoft.AspNetCore.Mvc;

namespace Senparc.Weixin.MP.MvcExtension
{
    /// <summary>
    /// 修复微信换行 bug
    /// </summary>
    public class FixWeixinBugWeixinResult : ContentResult
    {
        //private string _content;
        protected IMessageHandlerDocument _messageHandlerDocument;

		/// <summary>
		/// 这个类型只用于特殊阶段：目前IOS版本微信有换行的bug，\r\n会识别为2行
		/// </summary>
		public FixWeixinBugWeixinResult(IMessageHandlerDocument messageHandlerDocument)
		{
			_messageHandlerDocument = messageHandlerDocument;
		}

		public FixWeixinBugWeixinResult(string content)
		{
			//_content = content;
			base.Content = content;
		}


        new public string Content
        {
            get
            {
                if (base.Content != null)
                {
                    return base.Content;
                }

                if (_messageHandlerDocument != null)
                {
                    //var textResponseMessag = _messageHandlerDocument.TextResponseMessage;
                    if (_messageHandlerDocument.TextResponseMessage != null)
                    {
                        return _messageHandlerDocument.TextResponseMessage.Replace("\r\n", "\n");
                    }

					//if (_messageHandlerDocument.TextResponseMessage.Equals(String.Empty))
					//{
					//    //无需响应，开发者返回了ResponseNoResponse
					//    return null;
					//}

                    //if (_messageHandlerDocument.ResponseDocument != null)
                    //{
                    //    //返回XML响应信息
                    //    return _messageHandlerDocument.TextResponseMessage.Replace("\r\n", "\n");
                    //}
                    //else
                    //{
                    //    //返回XML响应信息或用户指定的文本内容
                    //    return _messageHandlerDocument.TextResponseMessage;
                    //}
                }
                return null;
            }
            set { base.Content = value; }
        }

        //public override void ExecuteResult(ActionContext context)
        //{
        //    base.ExecuteResult(context);
        //}

        public override void ExecuteResult(ActionContext context)
        {
            var content = this.Content;

            if (content == null)
            {
                //使用IMessageHandler输出
                if (_messageHandlerDocument == null)
                {
                    throw new Senparc.Weixin.Exceptions.WeixinException("执行WeixinResult时提供的MessageHandler不能为Null！", null);
                }
                var finalResponseDocument = _messageHandlerDocument.FinalResponseDocument;


                if (finalResponseDocument == null)
                {
                    //throw new Senparc.Weixin.MP.WeixinException("FinalResponseDocument不能为Null！", null);
                }
                else
                {
                    content = finalResponseDocument.ToString();
                }
            }

            context.HttpContext.Response.ContentType = "text/xml";
            content = (content ?? "").Replace("\r\n", "\n");

            var bytes = Encoding.UTF8.GetBytes(content);
            context.HttpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
        }
    }
}
